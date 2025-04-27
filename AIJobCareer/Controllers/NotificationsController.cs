using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIJobCareer.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public NotificationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Notifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notification>>> GetNotifications([FromQuery] string status = null, [FromQuery] int limit = 10, [FromQuery] int offset = 0)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var query = _context.Notification
                .Where(n => n.notification_user_id == userId)
                .AsQueryable();

            // Filter by status if provided
            if (!string.IsNullOrEmpty(status) && (status == "read" || status == "unread"))
            {
                query = query.Where(n => n.notification_status == status);
            }

            // Apply pagination
            var totalCount = await query.CountAsync();
            var notifications = await query
                .OrderByDescending(n => n.notification_timestamp)
                .Skip(offset)
                .Take(limit)
                .Include(n => n.company)
                .Select(n =>
                   new NotificationResponseDTO
                   {
                       notification_id = n.notification_id,
                       notification_user_id = n.notification_user_id,
                       notification_company_id = n.notification_company_id,
                       company = new CompanyDto
                       {
                           company_id = n.company.company_id,
                           company_name = n.company.company_name
                       },
                       notification_text = n.notification_text,
                       notification_timestamp = n.notification_timestamp,
                       notification_status = n.notification_status
                })
                .ToListAsync();

            return Ok(new
            {
                notifications,
                pagination = new
                {
                    total = totalCount,
                    limit,
                    offset,
                    hasMore = (offset + limit) < totalCount
                }
            });
        }

        // GET: api/Notifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Notification>> GetNotification(int id)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var notification = await _context.Notification
                .Include(n => n.company)
                .FirstOrDefaultAsync(n => n.notification_id == id && n.notification_user_id == userId);

            if (notification == null)
            {
                return NotFound(new { message = "Notification not found" });
            }

            return notification;
        }

        // GET: api/Notifications/Unread/Count
        [HttpGet("Unread/Count")]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var count = await _context.Notification
                .CountAsync(n => n.notification_user_id == userId && n.notification_status == "unread");

            return Ok(new { unreadCount = count });
        }

        // PUT: api/Notifications/5/MarkAsRead
        [HttpPut("{id}/MarkAsRead")]
        public async Task<IActionResult> MarkNotificationAsRead(int id)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var notification = await _context.Notification
                .FirstOrDefaultAsync(n => n.notification_id == id && n.notification_user_id == userId);

            if (notification == null)
            {
                return NotFound(new { message = "Notification not found" });
            }

            notification.notification_status = "read";

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Notification marked as read", notification });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Failed to update notification status" });
            }
        }

        // PUT: api/Notifications/MarkAllAsRead
        [HttpPut("MarkAllAsRead")]
        public async Task<IActionResult> MarkAllNotificationsAsRead()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var unreadNotifications = await _context.Notification
                .Where(n => n.notification_user_id == userId && n.notification_status == "unread")
                .ToListAsync();

            if (!unreadNotifications.Any())
            {
                return Ok(new { message = "No unread notifications found" });
            }

            foreach (var notification in unreadNotifications)
            {
                notification.notification_status = "read";
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = $"Marked {unreadNotifications.Count} notifications as read" });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, new { message = "Failed to update notification status" });
            }
        }

        // DELETE: api/Notifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
            {
                return Unauthorized(new { message = "Invalid user authentication" });
            }

            var notification = await _context.Notification
                .FirstOrDefaultAsync(n => n.notification_id == id && n.notification_user_id == userId);

            if (notification == null)
            {
                return NotFound(new { message = "Notification not found" });
            }

            _context.Notification.Remove(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Notification deleted successfully" });
        }

        private Guid GetUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid userId))
            {
                return Guid.Empty;
            }
            return userId;
        }

        public class NotificationResponseDTO
        {
            public int notification_id { get; set; }
            public Guid notification_user_id { get; set; }
            public string? notification_company_id { get; set; }
            public CompanyDto company { get; set; }
            public string notification_text { get; set; }
            public DateTime notification_timestamp { get; set; }
            public string notification_status { get; set; }
        }

        public class CompanyDto
        {
            public string company_id { get; set; }
            public string company_name { get; set; }
        }
    }
}

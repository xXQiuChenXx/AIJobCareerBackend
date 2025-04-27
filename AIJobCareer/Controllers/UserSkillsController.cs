using AIJobCareer.Data;
using AIJobCareer.Models;
using AIJobCareer.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AIJobCareer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserSkillsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserSkillsController(ApplicationDBContext context)
        {
            _context = context;
        }
        // Helper method to get current user's ID from token
        private Guid GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out Guid userId))
            {
                throw new UnauthorizedAccessException("Invalid user token");
            }
            return userId;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserSkillDTO>>> GetUserSkills(string userId)
        {
            Guid current_user_id = GetCurrentUserId();

            var userSkills = await _context.User_Skill // Fixed typo here
                .Where(us => us.US_USER_ID.ToString() == userId)
                .Include(us => us.Skill)
                .Select(us => new UserSkillDTO
                {
                    skill_id = us.Skill.skill_id,
                    skill_name = us.Skill.skill_name,
                    skill_level = us.Skill.skill_level,
                })
                .ToListAsync();

            return Ok(userSkills);
        }

        [HttpPost]
        public async Task<ActionResult<UserSkillDTO>> CreateUserSkill(CreateUserSkillDTO createUserSkillDTO)
        {
            Guid current_user_id = GetCurrentUserId();

            var skill = new Skill
            {
                skill_level = createUserSkillDTO.skill_level,
                skill_name = createUserSkillDTO.skill_name,
            };

            _context.Skill.Add(skill);
            await _context.SaveChangesAsync();

            var userSkill = new UserSkill
            {
                US_USER_ID = current_user_id,
                US_SKILL_ID = skill.skill_id,
                User = await _context.User.FindAsync(current_user_id),
            };

            _context.User_Skill.Add(userSkill);
            await _context.SaveChangesAsync();

            var userSkillDTO = new UserSkillDTO
            {
                skill_id = userSkill.US_SKILL_ID,
                skill_name = skill.skill_name,
                skill_level = skill.skill_level
            };

            return CreatedAtAction(nameof(GetUserSkills), new { userId = current_user_id }, userSkillDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserSkill(int id, CreateUserSkillDTO updateUserSkillDTO)
        {
            Guid current_user_id = GetCurrentUserId();
            var userSkill = await _context.User_Skill.Include(us => us.Skill).Where(us => us.US_SKILL_ID == id).FirstOrDefaultAsync();
            if (userSkill == null)
            {
                return NotFound();
            }

            if (userSkill.US_USER_ID != current_user_id)
            {
                return Unauthorized();
            }

            userSkill.Skill.skill_name = updateUserSkillDTO.skill_name;
            userSkill.Skill.skill_level = updateUserSkillDTO.skill_level;

            _context.Entry(userSkill).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSkill(int id)
        {
            Guid current_user_id = GetCurrentUserId();
            var userSkill = await _context.Skill.FindAsync(id);
            if (userSkill == null)
            {
                return NotFound();
            }

            _context.Skill.Remove(userSkill);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

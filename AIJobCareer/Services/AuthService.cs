﻿using AIJobCareer.Controllers;
using AIJobCareer.Data;
using AIJobCareer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace AIJobCareer.Services
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, User? User)> RegisterAsync(RegisterModel input_user);
        Task<(bool Success, string Message, User? User)> RegisterBusinessAsync(BusinessRegistrationModel input_user);
        Task<(bool Success, string Message, User? User)> LoginAsync(string usernameOrEmail, string password);
        Task<User?> ValidateAsync(string? userId, string? username);
    }

    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(ApplicationDBContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<(bool Success, string Message, User? User)> RegisterBusinessAsync(BusinessRegistrationModel model)
        {

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if username or email already exists
                if (await _context.User.AnyAsync(u => u.username == model.Username))
                {
                    return (false, "Username already exists", null);
                }

                if (await _context.User.AnyAsync(u => u.user_email == model.Email))
                {
                    return (false, "Email already exists", null);
                }

                if (await _context.Company.AnyAsync(c => c.company_id == model.CompanyId))
                {
                    return (false, "Company ID already exists", null);
                }

                // Create company first
                var company = new Company
                {
                    company_id = model.CompanyId,
                    company_name = model.CompanyName,
                    company_intro = model.CompanyIntro ?? string.Empty,
                    company_website = model.CompanyWebsite ?? string.Empty,
                    company_industry = model.CompanyIndustry ?? string.Empty,
                    company_area_id = model.CompanyAreaId
                };

                _context.Company.Add(company);
                await _context.SaveChangesAsync();

                // Create user with reference to the new company
                var user = new User
                {
                    username = model.Username,
                    user_first_name = model.FirstName,
                    user_last_name = model.LastName,
                    user_email = model.Email,
                    user_role = "business", // Set role as business
                    user_company_id = company.company_id,
                    user_area_id = model.UserAreaId ?? model.CompanyAreaId, // Use company area if user area not provided
                    user_age = model.Age,
                };

                // Hash the password
                user.user_password = _passwordHasher.HashPassword(user, model.Password);

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return (true, "User registered successfully", user);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Internal server error: {ex.Message}", null);
            }

        }



        public async Task<(bool Success, string Message, User? User)> RegisterAsync(RegisterModel input_user)
        {
            // Check if username already exists
            if (await _context.User.AnyAsync(u => u.username == input_user.username))
            {
                return (false, "Username already taken", null);
            }

            // Check if email already exists
            if (await _context.User.AnyAsync(u => u.user_email == input_user.user_email))
            {
                return (false, "Email already registered", null);
            }

            // Create new user
            var user = new User
            {
                user_id = Guid.NewGuid(),
                username = input_user.username,
                user_first_name = input_user.user_first_name,
                user_last_name = input_user.user_last_name,
                user_email = input_user.user_email,
                user_password = input_user.user_password,
                user_privacy_status = input_user.user_privacy_status,
                user_account_created_time = input_user.user_account_created_time,
                user_role = input_user.user_role
            };

            // Hash the password
            user.user_password = _passwordHasher.HashPassword(user, input_user.user_password);

            // Save to database
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();

            return (true, "Registration successful", user);
        }

        public async Task<(bool Success, string Message, User? User)> LoginAsync(string usernameOrEmail, string password)
        {
            // Find user by username or email
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.username == usernameOrEmail || u.user_email == usernameOrEmail);

            if (user == null)
            {
                return (false, "Invalid username/email or password", null);
            }

            // Verify password
            var result = _passwordHasher.VerifyHashedPassword(user, user.user_password, password);

            if (result == PasswordVerificationResult.Success)
            {
                // Update last login time
                user.last_login_at = DateTime.UtcNow;
                await _context.SaveChangesAsync();

                return (true, "Login successful", user);
            }

            return (false, "Invalid username/email or password", null);
        }
        public async Task<User?> ValidateAsync(string? userId, string? username)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _context.User
                  .FirstOrDefaultAsync(u => u.user_id.ToString() == userId);
            }
            else if (!string.IsNullOrEmpty(username))
            {
                return await _context.User
                    .FirstOrDefaultAsync(u => u.username == username);
            }
            return null;
        }
    }
}
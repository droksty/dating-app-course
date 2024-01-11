﻿using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
	public class AccountController : BaseAPIController
	{
		private readonly DataContext _context;

		public AccountController(DataContext context)
		{
			_context = context;
		}


		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(RegisterDTO registerDTO)
		{
			if (await UserExists(registerDTO.Username)) return BadRequest("Username already exists.");

			using var hmac = new HMACSHA512();
			var user = new User
			{
				Username = registerDTO.Username.ToLower(),
				PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
				PasswordSalt = hmac.Key
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();
			return user;
		}


		private async Task<bool> UserExists(string username)
		{
			return await _context.Users.AnyAsync(user => user.Username == username.ToLower());
		}
	}
}
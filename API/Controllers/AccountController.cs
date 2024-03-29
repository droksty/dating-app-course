﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
	public class AccountController : BaseAPIController
	{
		private readonly DataContext _context;
		private readonly ITokenService _tokenService;


		public AccountController(DataContext context, ITokenService tokenService)
		{
			_context = context;
			_tokenService = tokenService;
		}


		[HttpPost("register")]
		public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
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

			return new UserDTO { Username = user.Username, Token = _tokenService.CreateToken(user) };
		}


		[HttpPost("login")]
		public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
		{
			var user = await _context.Users.SingleOrDefaultAsync(user => user.Username == loginDTO.Username.ToLower());
			if (user == null) return Unauthorized("Invalid username");

			using var hmac = new HMACSHA512(user.PasswordSalt);
			var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDTO.Password));
			for (int i = 0; i < computedHash.Length; i++)
			{
				if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
			}

			return new UserDTO { Username = user.Username, Token = _tokenService.CreateToken(user) };
		}


		// Helpers
		private async Task<bool> UserExists(string username)
		{
			return await _context.Users.AnyAsync(user => user.Username == username.ToLower());
		}
	}
}

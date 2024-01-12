﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Authorize]
	public class UsersController : BaseAPIController
	{
		private readonly DataContext _context;

		public UsersController(DataContext context)
		{
			_context = context;
		}


		// Controllers

		[AllowAnonymous]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			return await _context.Users.ToListAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			return await _context.Users.FindAsync(id);
		}
	}
}

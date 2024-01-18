﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	public class ErrorController : BaseAPIController
	{
		private readonly DataContext _context;

        public ErrorController(DataContext context)
		{
			_context = context;
		}

		[Authorize]
		[HttpGet("auth")]
		public ActionResult<string> GetSecret()
		{
			return "Secret text";
		}

		[HttpGet("not-found")]
		public ActionResult<User> GetNotFound()
		{
			var thing = _context.Users.Find(-1);
			return (thing == null) ? NotFound() : thing;
		}

		[HttpGet("server-error")]
		public ActionResult<string> GetServerError()
		{
			var thing = _context.Users.Find(-1);
			var thingToReturn = thing!.ToString();
			return thingToReturn!;
		}

		[HttpGet("bad-request")]
		public ActionResult<string> GetBadRequest()
		{
			return BadRequest("This was a bad request");
		}
	}
}

﻿using API.DTOs;
using API.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		public UserRepository(DataContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}


		public async Task<MemberDTO?> GetMemberAsync(string username)
		{
			return await _context.Users
				.Where(x => x.Username == username)
				.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
				.SingleOrDefaultAsync();
		}

		public async Task<IEnumerable<MemberDTO>> GetMembersAsync()
		{
			return await _context.Users
				.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider)
				.ToListAsync();
		}

		public async Task<User> GetUserByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<User> GetUserByUsernameAsync(string username)
		{
			return await _context.Users
				.Include(p => p.Photos)
				.SingleOrDefaultAsync(x => x.Username == username);
		}

		public async Task<IEnumerable<User>> GetUsersAsync()
		{
			return await _context.Users
				.Include(p => p.Photos)
				.ToListAsync();
		}

		public async Task<bool> SaveAllAsync(User user)
		{
			return await _context.SaveChangesAsync() > 0;
		}

		public void Update(User user)
		{
			_context.Entry(user).State = EntityState.Modified;
		}
	}
}

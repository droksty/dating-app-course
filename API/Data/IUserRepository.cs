using API.DTOs;
using API.Entities;

namespace API.Data
{
	public interface IUserRepository
	{
		void Update(User user);
		Task<bool> SaveAllAsync(User user);
		Task<IEnumerable<User>> GetUsersAsync();
		Task<User> GetUserByIdAsync(int id);
		Task<User> GetUserByUsernameAsync(string username);
		Task<IEnumerable<MemberDTO>> GetMembersAsync();
		Task<MemberDTO?> GetMemberAsync(string username);
	}
}

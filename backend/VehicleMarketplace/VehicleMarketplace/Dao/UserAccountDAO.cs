using Microsoft.EntityFrameworkCore;
using Posts.Data;
using Posts.Models;

namespace Posts.Dao
{
    public class UserAccountDAO
    {
        private readonly PostsDbContext context;

        public UserAccountDAO(PostsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<UserAccount>> GetUsersAsync()
        {
            return await context.UserAccounts.ToListAsync();
        }

        public async Task<UserAccount> GetUserByIdAsync(int id)
        {
            return await context.UserAccounts.FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<UserAccount> GetUserByEmailPassword(string email, string password)
        {
            return await context.UserAccounts.Where(user => user.Email == email && user.Password == password).FirstOrDefaultAsync();
        }

        public async Task SaveUserAccountAsync(UserAccount userAccount)
        {
            context.UserAccounts.Add(userAccount);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserByIdAsync(UserAccount user)
        {
            context.UserAccounts.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int userId, UserAccount updatedUser)
        {
            UserAccount existingUser = await GetUserByIdAsync(userId);

            existingUser.Firstname = updatedUser.Firstname;
            existingUser.Lastname = updatedUser.Lastname;
            existingUser.Email = updatedUser.Email;
            existingUser.Password = updatedUser.Password;
            existingUser.Role = updatedUser.Role;

            await context.SaveChangesAsync();
        }
    }
}

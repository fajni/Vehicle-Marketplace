using Microsoft.AspNetCore.Authentication.Cookies;
using Posts.Dao;
using Posts.Models;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Posts.Services
{
    public class UserAccountService
    {

        private readonly UserAccountDAO userAccountDAO;

        public UserAccountService(UserAccountDAO userAccountDAO)
        {
            this.userAccountDAO = userAccountDAO;
        }


        public async Task<List<UserAccount>> GetAllUserAccounts()
        {

            try
            {
                List<UserAccount> users = await userAccountDAO.GetUsersAsync();

                if (users.Count == 0)
                {
                    return new List<UserAccount>();
                }

                return users;
            }
            catch (Exception ex)
            {
                return new List<UserAccount>();
            }
        }

        public async Task<UserAccount> GetUserById(int id)
        {

            try
            {
                UserAccount user = await userAccountDAO.GetUserByIdAsync(id);

                if (user == null)
                {
                    throw new Exception($"User {id} not found!");
                }

                return user;
            }
            catch
            {
                throw new Exception($"Could NOT get user {id}");
            }
        }

        public async Task<UserAccount> GetUserByEmailPassword(string email, string password)
        {
            try
            {
                UserAccount user = await userAccountDAO.GetUserByEmailPassword(email, password);

                if(user == null)
                {
                    throw new Exception($"User {email} not found");
                }

                return user;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured! - {ex.Message}", ex);
            }
        }

        public async Task SaveUserAccount(UserAccount userAccount)
        {

            if (userAccount == null)
            {
                throw new ArgumentNullException(nameof(userAccount), "User account must not be null!");
            }

            var users = await GetAllUserAccounts();

            foreach(var user in users)
            {
                if (user.Email.Equals(userAccount.Email))
                {
                    throw new ArgumentNullException(nameof(userAccount), "User account with that email already exist!");
                }
            }

            try
            {
                await userAccountDAO.SaveUserAccountAsync(userAccount);
                Console.Write("User: " + userAccount);
            }
            catch
            {
                throw new Exception($"Could NOT save user {userAccount.Firstname} {userAccount.Lastname}!");
            }

        }

        public async Task DeleteUserAccount(int id)
        {

            UserAccount user = await userAccountDAO.GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception($"User not found!");
            }
            else
            {
                await userAccountDAO.DeleteUserByIdAsync(user);
            }
        }

        public async Task UpdateUserAccount(int userId, UserAccount updatedUser)
        {

            UserAccount user = await GetUserById(userId);

            if(updatedUser == null)
            {
                throw new Exception($"User can't be null!");
            }

            try
            {
                await userAccountDAO.UpdateUserAsync(userId, updatedUser);
            }
            catch (Exception ex) 
            {
                throw new Exception($"Could NOT update the user!");
            }

        }

    }
}

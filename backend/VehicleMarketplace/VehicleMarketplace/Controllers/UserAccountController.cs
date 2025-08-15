using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Posts.Models;
using Posts.Services;
using System.Security.Claims;

namespace Posts.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserAccountController : ControllerBase
    {

        private readonly UserAccountService userAccountService;

        public UserAccountController(UserAccountService userAccountService)
        {
            this.userAccountService = userAccountService;
        }

        // get all users
        [HttpGet, Route("")]
        //[Authorize]
        public async Task<IActionResult> GetAllUserAccounts()
        {
            List<UserAccount> users = await userAccountService.GetAllUserAccounts();

            return Ok(users);
        }

        // get user
        [HttpGet, Route("{userAccountId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetUserAccount([FromRoute] int userAccountId)
        {
            try
            {
                UserAccount user = await userAccountService.GetUserById(userAccountId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"User {userAccountId} not found! " + ex);
            }
        }


        /* REGISTRATION */

        // add new user
        [HttpPost, Route("registration")]
        public async Task<IActionResult> AddUserAccount([FromBody] UserAccount model)
        {
            if (!ModelState.IsValid)
                return BadRequest("User not valid!");

            try
            {
                await userAccountService.SaveUserAccount(model);

                return Ok(new { message = $"{model.Role} - {model.Firstname} {model.Lastname} added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // delete user
        [HttpDelete, Route("delete/{userAccountId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteUserAccount([FromRoute] int userAccountId)
        {
            try
            {
                await userAccountService.DeleteUserAccount(userAccountId);

                return Ok(new { message = $"Successfully deleted user with {userAccountId} id!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        // update user
        [HttpPut, Route("update/{userAccountId}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateUserAccount([FromRoute] int userAccountId, [FromBody] UserAccount updatedUser)
        {

            try
            {
                await userAccountService.UpdateUserAccount(userAccountId, updatedUser);

                return Ok(new { message = $"Successfully updated user with {userAccountId} id!" });
            } 
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        /* LOGIN */

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login([FromQuery]string email, [FromQuery]string password)
        {

            UserAccount? user = null;

            try
            {
                user = await userAccountService.GetUserByEmailPassword(email, password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }

            if (!ModelState.IsValid)
            {
                return StatusCode(500, "Model State not valid!");
            }

            if (user == null)
            {
                return StatusCode(404, "User not found!");
            }

            /* Create Cookie */

            // create claims
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email),
                new Claim("Password", user.Password),
                new Claim("Firstname", user.Firstname),
                new Claim("Lastname", user.Lastname),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //login the user, and create the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            
            //var roles = HttpContext.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            //var role = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
            var role = claimsIdentity.FindFirst(ClaimTypes.Role)?.Value;

            //var cookieValue = HttpContext.Request.Cookies[".AspNetCore.Cookies"];
            var cookieValue = Request.Cookies[".AspNetCore.Cookies"];

            return Ok(new { 
                message = $"Successfully logged in!",
                cookie = HttpContext.Response.Headers["Set-Cookie"],
                cookieValue = cookieValue,
                role = role,
                user=user
            });
        }

        [HttpGet, Route("logout")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return Ok(new { message = $"Successfully logged out!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error! - {ex.Message}");
            }
        }


        [HttpGet, Route("status")]
        //[Authorize]
        public IActionResult Status()
        {
            if(User.Identity.IsAuthenticated)
            {
                return Ok(new { 
                    loggedIn = true,
                    message = "You're logged in!",
                    userEmail = User.Identity.Name, 
                    Firstname = User.FindFirst("Firstname")?.Value,
                    Lastname = User.FindFirst("Lastname")?.Value
                });
            }
            else
            {
                //return StatusCode(StatusCodes.Status401Unauthorized, new { message = "You're not logged in!" });
                //return Unauthorized(new { message = "You're not logged in!" });
                return Ok(new { loggedIn = false, message = "You're NOT logged in!" });
            }
        }
    }
}

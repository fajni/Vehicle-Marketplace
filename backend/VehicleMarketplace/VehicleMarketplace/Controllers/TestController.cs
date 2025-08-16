using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VehicleMarketplace.Controllers
{
    public class TestUser
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public string Email { get; set; }

        public TestUser(int Id, string Name, string Email)
        {
            this.Id = Id;
            this.Name = Name;
            this.Email = Email;
        }
    }

    [ApiController]
    [Route("api")]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet, Route("test"), Route("protected")]
        public List<TestUser> GetTest()
        {
            return new List<TestUser>()
            {
                new TestUser(1, "John", "jogn@yahoo.com"),
                new TestUser(2, "Dia", "dia@gmail.com"),
                new TestUser(3, "Josh", "josh@gmail.com")
            };
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMarketplace.Models;
using VehicleMarketplace.Services;

namespace VehicleMarketplace.Controllers
{
    [ApiController]
    [Route("api/me")]
    public class UserAccountVehicles : ControllerBase
    {

        private readonly CarService carService;

        private readonly MotorcycleService motorcycleService;

        public UserAccountVehicles(CarService carService, MotorcycleService motorcycleService)
        {
            this.carService = carService;
            this.motorcycleService = motorcycleService;
        }


        [HttpGet, Route("vehicles/{userAccountId}")]
        [Authorize]
        public async Task<IActionResult> GetUserAccountVehicles([FromRoute] int userAccountId)
        {
            try
            {
                List<Car> userAccountCars = await this.carService.GetUserAccountCarsByUserAccountId(userAccountId);

                List<Motorcycle> userAccountMotorcycles = await this.motorcycleService.GetUserAccountMotorcyclesByUserAccountId(userAccountId);

                List<VehicleDTO> userAccountVehicles = new List<VehicleDTO>();

                for(int i = 0; i < userAccountCars.Count; i++)
                {
                    VehicleDTO v = new VehicleDTO();

                    v.Vin = userAccountCars[i].Vin;
                    v.Name = userAccountCars[i].Name;
                    v.Description = userAccountCars[i].Description;
                    v.Price = userAccountCars[i].Price;
                    v.Capacity = userAccountCars[i].Capacity;
                    v.Power = userAccountCars[i].Power;

                    v.MakeId = userAccountCars[i].MakeId;
                    v.UserAccountId = userAccountCars[i].UserAccountId;

                    userAccountVehicles.Add(v);
                }

                for (int i = 0; i < userAccountMotorcycles.Count; i++)
                {
                    VehicleDTO v = new VehicleDTO();

                    v.Vin = userAccountMotorcycles[i].Vin;
                    v.Name = userAccountMotorcycles[i].Name;
                    v.Description = userAccountMotorcycles[i].Description;
                    v.Price = userAccountMotorcycles[i].Price;
                    v.Capacity = userAccountMotorcycles[i].Capacity;
                    v.Power = userAccountMotorcycles[i].Power;

                    v.MakeId = userAccountMotorcycles[i].MakeId;
                    v.UserAccountId = userAccountMotorcycles[i].UserAccountId;

                    userAccountVehicles.Add(v);
                }

                return Ok(userAccountVehicles);
            }
            catch (Exception ex)
            {
                return Ok("Error occured - " + ex.Message);
            }
        }

        [HttpGet, Route("cars/{userAccountId}")]
        [Authorize]
        public async Task<IActionResult> GetUserAccountCars([FromRoute] int userAccountId)
        {
            try
            {

                List<Car> userAccountCars = await this.carService.GetUserAccountCarsByUserAccountId(userAccountId);

                return Ok(userAccountCars);
            }
            catch (Exception ex)
            {
                return Ok("Error occured - " + ex.Message);
            }
        }

        [HttpGet, Route("motorcycles/{userAccountId}")]
        [Authorize]
        public async Task<IActionResult> GetUserAccountMotorcycles([FromRoute] int userAccountId)
        {
            try
            {

                List<Motorcycle> userAccountMotorcycles = await this.motorcycleService.GetUserAccountMotorcyclesByUserAccountId(userAccountId);

                return Ok(userAccountMotorcycles);
            }
            catch (Exception ex)
            {
                return Ok("Error occured - " + ex.Message);
            }
        }

    }
}

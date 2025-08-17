using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMarketplace.Models;
using VehicleMarketplace.Services;

namespace VehicleMarketplace.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly CarService carService;

        private readonly MakeService makeService;
        
        private readonly UserAccountService userAccountService;

        public CarController(CarService carService) 
        {
            this.carService = carService;
        }


        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllCars()
        {
            List<Car> cars = await carService.GetAllCars();

            return Ok(cars);
        }

        [HttpGet, Route("{carVin}")]
        public async Task<IActionResult> GetCar([FromRoute] string carVin)
        {
            try
            {
                Car car = await carService.GetCarByVin(carVin);
            
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Car {carVin} not found! " + ex);
            }

        }

        [HttpPost, Route("add")]
        [Authorize]
        public async Task<IActionResult> AddCar([FromBody] VehicleDTO car)
        {
            if (!ModelState.IsValid || car == null)
                return BadRequest($"Car not valid! - {car}");

            try
            {
                await carService.SaveCar(car);

                return Ok(new { message = $"{car.Name} added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete, Route("delete/{carVin}")]
        [Authorize]
        public async Task<IActionResult> DeleteCar([FromRoute] string carVin)
        {
            try
            {
                await carService.DeleteCarByVin(carVin);

                return Ok(new { message = $"Successfully deleted car with {carVin} vin!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut, Route("update/{carVin}")]
        [Authorize]
        public async Task<IActionResult> UpdateCar([FromRoute] string carVin, [FromBody] VehicleDTO updatedCar)
        {
            try
            {
                await carService.UpdateCar(carVin, updatedCar);

                return Ok(new { message = $"Successfully updated car with {carVin} vin!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

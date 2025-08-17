using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleMarketplace.Models;
using VehicleMarketplace.Services;

namespace VehicleMarketplace.Controllers
{
    [ApiController]
    [Route("api/motorcycles"), Route("api/motors")]
    public class MotorcycleController : ControllerBase
    {
        private readonly MotorcycleService motorcycleService;
        
        public MotorcycleController(MotorcycleService motorcycleService)
        {
            this.motorcycleService = motorcycleService;
        }


        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllMotorcycles()
        {
            List<Motorcycle> motorcycles = await motorcycleService.GetAllMotorcycles();

            return Ok(motorcycles);
        }

        [HttpGet, Route("{vin}")]
        public async Task<IActionResult> GetMotorcycle([FromRoute] string vin)
        {
            try
            {
                Motorcycle motorcycle = await motorcycleService.GetMotorcycleByVin(vin);

                return Ok(motorcycle);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Motorcycle {vin} not found! " + ex);
            }
        }

        [HttpPost, Route("add")]
        [Authorize]
        public async Task<IActionResult> AddMotorcycle([FromBody] VehicleDTO motorcycle)
        {
            if (!ModelState.IsValid || motorcycle == null)
            {
                return BadRequest($"Motorcycle not valid! - {motorcycle}");
            }

            try
            {
                await motorcycleService.SaveMotorcycle(motorcycle);

                return Ok(new { message = $"{motorcycle.Name} added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete, Route("delete/{vin}")]
        [Authorize]
        public async Task<IActionResult> DeleteMotorcycle([FromRoute] string vin)
        {
            try
            {
                await motorcycleService.DeleteMotorcycleByVin(vin);

                return Ok(new { message = $"Successfully deleted motorcycle with {vin} vin!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut, Route("update/{vin}")]
        [Authorize]
        public async Task<IActionResult> UpdateMotorcycle([FromRoute] string vin, [FromBody] VehicleDTO updatedMotorcycle)
        {
            try
            {
                await motorcycleService.UpdateMotorcycle(vin, updatedMotorcycle);

                return Ok(new { message = $"Successfully updated motorcycle with {vin} vin!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

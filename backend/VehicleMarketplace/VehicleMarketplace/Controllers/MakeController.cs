using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using VehicleMarketplace.Models;
using VehicleMarketplace.Services;

namespace VehicleMarketplace.Controllers
{
    [ApiController]
    [Route("api/makes")]
    public class MakeController : ControllerBase
    {
        private readonly MakeService makeService;

        public MakeController(MakeService makeService) 
        {
            this.makeService = makeService;
        }


        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllMakes()
        {
            List<Make> makes = await makeService.GetAllMakes();

            return Ok(makes);
        }

        [HttpGet, Route("{id}")]
        public async Task<IActionResult> GetMake([FromRoute] int id)
        {
            try
            {
                Make make = await makeService.GetMakeById(id);

                return Ok(make);
            }
            catch (Exception ex)
            {
                return StatusCode(404, $"Make with {id} id not found! " + ex);
            }
        }

        [HttpPost, Route("add")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddMake([FromBody] Make make)
        {
            if (!ModelState.IsValid || make == null)
            {
                return BadRequest($"Make not valid! - {make}");
            }

            try
            {
                await makeService.SaveMake(make);

                return Ok(new { message = $"{make.MakeName} added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpDelete, Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMake([FromRoute] int id)
        {
            try
            {
                await makeService.DeleteMakeById(id);

                return Ok(new { message = $"Successfully deleted make with {id} id!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut, Route("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateMake([FromRoute] int id, [FromBody] Make updatedMake)
        {
            try
            {
                await makeService.UpdateMake(id, updatedMake);

                return Ok(new { message = $"Successfully updated make with {id} id!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

    }
}

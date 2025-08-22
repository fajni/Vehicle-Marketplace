using Microsoft.AspNetCore.Mvc;
using VehicleMarketplace.Models;
using VehicleMarketplace.Services;

namespace VehicleMarketplace.Controllers
{
    [ApiController]
    [Route("api/images")]
    public class ImageController : ControllerBase
    {
        private readonly ImageService imageService;

        public ImageController(ImageService imageService)
        {
            this.imageService = imageService;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> GetAllImages()
        {
            List<Image> images = await imageService.GetAllImages();

            return Ok(images);
        }

        [HttpGet, Route("cars/{vin}")]
        public async Task<IActionResult> GetAllCarsImages([FromRoute] string vin)
        {
            List<Image> carsImages = await imageService.GetImagesByCarVin(vin);

            return Ok(carsImages);
        }

        [HttpGet, Route("motorcycles/{vin}")]
        public async Task<IActionResult> GetAllMotorcyclesImages([FromRoute] string vin)
        {
            List<Image> motorcycleImages = await imageService.GetImagesByMotorcycleVin(vin);

            return Ok(motorcycleImages);
        }
    }
}

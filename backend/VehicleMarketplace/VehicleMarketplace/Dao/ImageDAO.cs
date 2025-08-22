using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Data;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Dao
{
    public class ImageDAO
    {
        private readonly VehicleMarketplaceDbContext context;

        public ImageDAO(VehicleMarketplaceDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Image>> GetAllImagesAsync()
        {
            return await context.Images.ToListAsync();
        }

        public async Task<List<Image>> GetImagesByCarVinAsync(string carVin)
        {
            List<Image> images = await GetAllImagesAsync();

            List<Image> carImages = new List<Image>();

            foreach (Image image in images) 
            {
                if(image.CarVin == carVin)
                    carImages.Add(image);
            }

            return carImages;
        }

        public async Task<List<Image>> GetImagesByMotorcycleVinAsync(string motorcycleVin)
        {
            List<Image> images = await GetAllImagesAsync();

            List<Image> motorcycleImages = new List<Image>();

            foreach (Image image in images)
            {
                if (image.CarVin == motorcycleVin)
                    motorcycleImages.Add(image);
            }

            return motorcycleImages;
        }

        public async Task<Image> GetImageByIdAsync(int id)
        {
            return await context.Images.FirstOrDefaultAsync(image => image.Id == id);
        }

        public async Task<Image> SaveImageAsync(Image image)
        {
            context.Add(image);
            await context.SaveChangesAsync();

            return image;
        }

        public async Task<Image> DeleteImageAsync(Image image)
        {
            context.Images.Remove(image);
            await context.SaveChangesAsync();

            return image;
        }

        public async Task<Image> UpdateImageAsync(int id, Image updatedImage)
        {
            Image existingImage = await GetImageByIdAsync(id);

            existingImage.Src = updatedImage.Src;

            existingImage.Motorcycle = updatedImage.Motorcycle;
            existingImage.MotorcycleVin = updatedImage.MotorcycleVin;
            existingImage.Car = updatedImage.Car;
            existingImage.CarVin = updatedImage.CarVin;

            await context.SaveChangesAsync();

            return existingImage;
        }
    }
}

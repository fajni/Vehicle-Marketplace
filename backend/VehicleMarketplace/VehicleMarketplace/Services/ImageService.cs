using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class ImageService
    {
        private readonly ImageDAO imageDAO;

        private readonly CarService carService;

        private readonly MotorcycleService motorcycleService;

        public ImageService(ImageDAO imageDAO, CarService carService, MotorcycleService motorcycleService)
        {
            this.imageDAO = imageDAO;
            this.carService = carService;
            this.motorcycleService = motorcycleService;
        }


        public async Task<List<Image>> GetAllImages()
        {
            try
            {
                List<Image> images = await imageDAO.GetAllImagesAsync();

                if (images.Count == 0)
                {
                    return new List<Image>();
                }

                return images;
            }
            catch (Exception ex)
            {
                return new List<Image>();
            }
        }

        public async Task<Image> GetImageById(int id)
        {
            try
            {
                Image image = await imageDAO.GetImageByIdAsync(id);

                if(image == null)
                {
                    throw new Exception($"Image {id} not found!");
                }

                return image;
            }
            catch (Exception ex) 
            {
                throw new Exception($"Could NOT get image {id} - {ex.Message}");
            }
        }

        public async Task<List<Image>> GetImagesByCarVin(string vin)
        {
            List<Image> images = await GetAllImages();
            List<Image> carImages = new List<Image>();

            try
            {

                for(int i = 0; i < images.Count; i++)
                {
                    if (images[i].CarVin == vin)
                        carImages.Add(images[i]);
                }

                return carImages;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured - {ex.Message}");
            }
        }

        public async Task<List<Image>> GetImagesByMotorcycleVin(string vin)
        {
            List<Image> images = await GetAllImages();
            List<Image> motorcycleImages = new List<Image>();

            try
            {

                for (int i = 0; i < images.Count; i++)
                {
                    if (images[i].MotorcycleVin == vin)
                        motorcycleImages.Add(images[i]);
                }

                return motorcycleImages;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured - {ex.Message}");
            }
        }

        public async Task<Image> SaveImage(Image image)
        {
            try
            {
                await imageDAO.SaveImageAsync(image);

                return image;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT save image {image.Id}! - {ex.Message}");
            }
        }

        public async Task<Image> DeleteImageById(int id)
        {
            try
            {
                Image image = await imageDAO.GetImageByIdAsync(id);

                if(image == null)
                {
                    throw new Exception($"Image not found!");
                }

                await imageDAO.DeleteImageAsync(image);

                return image;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete image! - {ex.Message}");
            }
        }

        public async Task<Image> UpdateImage(int id, Image updatedImage)
        {
            try
            {
                await imageDAO.UpdateImageAsync(id, updatedImage);
            
                return await GetImageById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT update the image! - {ex.Message}");
            }
        }
    }
}

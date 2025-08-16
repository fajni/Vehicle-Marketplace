using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class MotorcycleService
    {
        private readonly MotorcycleDAO motorcycleDAO;

        public MotorcycleService(MotorcycleDAO motorcycleDAO)
        {
            this.motorcycleDAO = motorcycleDAO;
        }

        public async Task<List<Motorcycle>> GetAllMotorcycles()
        {
            try
            {
                List<Motorcycle> motorcycles = await motorcycleDAO.GetMotorcyclesAsync();

                if(motorcycles.Count == 0)
                {
                    return new List<Motorcycle>();
                }

                return motorcycles;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT retrive motorcycles! - {ex.Message}");
            }
        }

        public async Task<Motorcycle> GetMotorcycleByVin(string vin)
        {
            try
            {
                Motorcycle motorcycle = await motorcycleDAO.GetMotorcycleByVinAsync(vin);

                if(motorcycle == null)
                {
                    throw new Exception($"Motorcycle {vin} not found!");
                }

                return motorcycle;
            }
            catch (Exception e)
            {
                throw new Exception($"Could NOT retrive motorcycle {vin} - {e.Message}");
            }
        }

        public async Task<Motorcycle> SaveMotorcycle(Motorcycle motorcycle)
        {
            if(motorcycle == null)
            {
                throw new ArgumentNullException(nameof(motorcycle), "Car can NOT be null!");
            }

            try
            {
                await motorcycleDAO.SaveMotorcycleAsync(motorcycle);

                return motorcycle;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT save motorcycle {motorcycle.Vin}! - {ex.Message}");
            }
        }

        public async Task<Motorcycle> DeleteMotorcycleByVin(string vin)
        {
            try
            {
                Motorcycle motorcycle = await motorcycleDAO.GetMotorcycleByVinAsync(vin);

                if(motorcycle == null)
                {
                    throw new Exception($"Motorcycle ${vin} not found!");
                }
                
                await motorcycleDAO.DeleteMotorcycleAsync(motorcycle);

                return motorcycle;
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT delete motorcycle {vin}! - {ex.Message}");
            }
        }

        public async Task<Motorcycle> UpdateMotorcycle(string vin, Motorcycle updatedMotorcycle)
        {
            try
            {
                Motorcycle motorcycle = await GetMotorcycleByVin(vin);

                if(motorcycle == null)
                {
                    throw new Exception($"Motorcycle can't be null!");
                }

                await motorcycleDAO.UpdateMotorcycleAsync(vin, updatedMotorcycle);

                return await GetMotorcycleByVin(vin);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT update the motorcycle! - {ex.Message}");
            }
        }

    }
}

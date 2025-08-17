using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class MotorcycleService
    {
        private readonly MotorcycleDAO motorcycleDAO;

        private readonly MakeService makeService;

        private readonly UserAccountService userAccountService;

        public MotorcycleService(MotorcycleDAO motorcycleDAO, MakeService makeService, UserAccountService userAccountService)
        {
            this.motorcycleDAO = motorcycleDAO;
            this.makeService = makeService;
            this.userAccountService = userAccountService;
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

        public async Task<Motorcycle> SaveMotorcycle(VehicleDTO motorcycleDTO)
        {

            Make make = await makeService.GetMakeById(motorcycleDTO.MakeId);

            UserAccount userAccount = await userAccountService.GetUserById(motorcycleDTO.UserAccountId);

            if (make == null)
            {
                throw new ArgumentNullException(nameof(make), "Make of a car can NOT be found!");
            }

            if (userAccount == null)
            {
                throw new ArgumentNullException(nameof(make), "User account can NOT be found!");
            }

            Motorcycle newMotorcycle = new Motorcycle
            {
                Vin = motorcycleDTO.Vin,
                Name = motorcycleDTO.Name,
                Description = motorcycleDTO.Description,
                Price = motorcycleDTO.Price,
                Capacity = motorcycleDTO.Capacity,
                Power = motorcycleDTO.Power,
                Make = make,
                MakeId = make.Id,
                UserAccount = userAccount,
                UserAccountId = userAccount.Id
            };

            try
            {
                await motorcycleDAO.SaveMotorcycleAsync(newMotorcycle);

                return newMotorcycle;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT save motorcycle {newMotorcycle.Vin}! - {ex.Message}");
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

        public async Task<Motorcycle> UpdateMotorcycle(string vin, VehicleDTO updatedVehicle)
        {
            try
            {
                Motorcycle updatedMotorcycle = await GetMotorcycleByVin(vin);

                Make make = await this.makeService.GetMakeById(updatedVehicle.MakeId);

                UserAccount userAccount = await this.userAccountService.GetUserById(updatedVehicle.UserAccountId);

                if (updatedMotorcycle == null)
                {
                    throw new Exception($"Motorcycle can't be null!");
                }

                if (string.IsNullOrWhiteSpace(updatedVehicle.Name))
                {
                    updatedVehicle.Name = updatedMotorcycle.Name;
                }

                if (string.IsNullOrWhiteSpace(updatedVehicle.Description))
                {
                    updatedVehicle.Description = updatedMotorcycle.Description;
                }

                if (updatedVehicle.Price <= 0)
                {
                    updatedVehicle.Price = updatedMotorcycle.Price;
                }

                if (updatedVehicle.Capacity <= 0)
                {
                    updatedVehicle.Capacity = updatedMotorcycle.Capacity;
                }

                if (updatedVehicle.Power <= 0)
                {
                    updatedVehicle.Power = updatedMotorcycle.Power;
                }

                updatedMotorcycle.Vin = updatedVehicle.Vin;
                updatedMotorcycle.Name = updatedVehicle.Name;
                updatedMotorcycle.Description = updatedVehicle.Description;
                updatedMotorcycle.Price = updatedVehicle.Price;
                updatedMotorcycle.Capacity = updatedVehicle.Capacity;
                updatedMotorcycle.Power = updatedVehicle.Power;

                updatedMotorcycle.MakeId = make.Id;
                updatedMotorcycle.Make = make;
                updatedMotorcycle.UserAccountId = userAccount.Id;
                updatedMotorcycle.UserAccount = userAccount;

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

using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Data;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Dao
{
    public class CarDAO
    {

        private readonly VehicleMarketplaceDbContext context;

        public CarDAO(VehicleMarketplaceDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarByVinAsync(string vin)
        {
            return await context.Cars.FirstOrDefaultAsync(user => user.Vin == vin);
        }

        public async Task<Car> SaveCarAsync(Car car)
        {
            context.Cars.Add(car);
            await context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> DeleteCarAsync(Car car)
        {
            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return car;
        }

        public async Task<Car> UpdateCarAsync(string carVin, Car updatedCar)
        {
            Car existingCar = await GetCarByVinAsync(carVin);

            existingCar.Name = updatedCar.Name;
            existingCar.Description = updatedCar.Description;
            existingCar.Price = updatedCar.Price;
            existingCar.Capacity = updatedCar.Capacity;
            existingCar.Power = updatedCar.Power;

            existingCar.MakeId = updatedCar.MakeId;
            existingCar.Make = updatedCar.Make;
            existingCar.UserAccountId = updatedCar.UserAccountId;
            existingCar.UserAccount = updatedCar.UserAccount;

            await context.SaveChangesAsync();

            return existingCar;
        }

    }
}

using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Data;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Dao
{
    public class MotorcycleDAO
    {
        private readonly VehicleMarketplaceDbContext context;

        public MotorcycleDAO(VehicleMarketplaceDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Motorcycle>> GetMotorcyclesAsync()
        {
            return await context.Motorcyclers.ToListAsync();
        }

        public async Task<Motorcycle> GetMotorcycleByVinAsync(string vin)
        {
            return await context.Motorcyclers.FirstOrDefaultAsync(motorcycle => motorcycle.Vin == vin);
        }

        public async Task<Motorcycle> SaveMotorcycleAsync(Motorcycle motorcycle)
        {
            context.Motorcyclers.Add(motorcycle);

            await context.SaveChangesAsync();

            return motorcycle;
        }

        public async Task<Motorcycle> DeleteMotorcycleAsync(Motorcycle motorcycle)
        {
            context.Motorcyclers.Remove(motorcycle);

            await context.SaveChangesAsync();

            return motorcycle;
        }

        public async Task<Motorcycle> UpdateMotorcycleAsync(string motorcycleVin, Motorcycle updatedMotorcycle)
        {
            Motorcycle existingMotorcycle = await GetMotorcycleByVinAsync(motorcycleVin);

            existingMotorcycle.Name = updatedMotorcycle.Name;
            existingMotorcycle.Description = updatedMotorcycle.Description;
            existingMotorcycle.Price = updatedMotorcycle.Price;
            existingMotorcycle.Capacity = updatedMotorcycle.Capacity;
            existingMotorcycle.Power = updatedMotorcycle.Power;

            existingMotorcycle.MakeId = updatedMotorcycle.MakeId;
            existingMotorcycle.Make = updatedMotorcycle.Make;
            existingMotorcycle.UserAccountId = updatedMotorcycle.UserAccountId;
            existingMotorcycle.UserAccount = updatedMotorcycle.UserAccount;

            await context.SaveChangesAsync();

            return existingMotorcycle;
        }
    }
}

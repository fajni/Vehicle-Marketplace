using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Data;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Dao
{
    public class MakeDAO
    {
        private readonly VehicleMarketplaceDbContext context;

        public MakeDAO(VehicleMarketplaceDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Make>> GetAllMakes()
        {
            return await context.Makes.ToListAsync();
        }

        public async Task<Make> GetMakeById(int id)
        {
            return await context.Makes.FirstOrDefaultAsync(make => make.Id == id);
        }

        public async Task<Make> SaveMakeAsync(Make make)
        {
            context.Makes.Add(make);
            await context.SaveChangesAsync();

            return make;
        }

        public async Task<Make> DeleteMakeAsync(Make make)
        {
            context.Makes.Remove(make);
            await context.SaveChangesAsync();

            return make;
        }

        public async Task<Make> UpdateMake(int id, Make updatedMake)
        {
            Make existingMake = await GetMakeById(id);

            existingMake.MakeName = updatedMake.MakeName;

            if(updatedMake.cars?.Count > 0)
            {
                existingMake.cars = updatedMake.cars;
            }

            if (updatedMake.motorcycles?.Count > 0)
            {
                existingMake.motorcycles = updatedMake.motorcycles;
            }

            await context.SaveChangesAsync();

            return existingMake;
        }
    }
}

using Humanizer;
using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class MakeService
    {
        private readonly MakeDAO makeDAO;

        public MakeService(MakeDAO makeDAO)
        {
            this.makeDAO = makeDAO;
        }

        public async Task<List<Make>> GetAllMakes()
        {
            try
            {
                List<Make> makes = await makeDAO.GetAllMakes();

                if (makes.Count == 0)
                {
                    return new List<Make>();
                }

                return makes;
            }
            catch (Exception ex)
            {
                return new List<Make>();
            }
        }

        public async Task<Make> GetMakeById(int id)
        {
            try
            {
                Make make = await makeDAO.GetMakeById(id);

                if(make == null)
                {
                    throw new Exception($"Make {id} not found!");
                }

                return make;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT get make {id} - {ex.Message}");
            }
        }

        public async Task<Make> SaveMake(Make make)
        {
            if (make == null)
            {
                throw new ArgumentNullException(nameof(make), "Make can NOT be null!");
            }

            try
            {
                await makeDAO.SaveMakeAsync(make);

                return make;
            }
            catch (Exception e)
            {
                throw new Exception($"Could NOT save make {make.Id}! - {e.Message}");
            }

        }

        public async Task<Make> DeleteMakeById(int id)
        {
            try
            {
                Make make = await makeDAO.GetMakeById(id);

                if (make == null)
                {
                    throw new Exception($"Make not found!");
                }
                else
                {
                    await makeDAO.DeleteMakeAsync(make);
                }

                return make;

            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT delete make {id}! - {ex.Message}");
            }
        }

        public async Task<Make> UpdateMake(int id, Make updatedMake)
        {
            try
            {
                Make make = await GetMakeById(id);

                if (make == null)
                {
                    throw new Exception($"Make can't be null!");
                }

                await makeDAO.UpdateMake(id, updatedMake);

                return await GetMakeById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT update the make! - {ex.Message}");
            }
        }
    }
}

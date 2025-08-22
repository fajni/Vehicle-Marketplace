using Humanizer;
using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class CarService
    {
        private readonly CarDAO carDAO;

        private readonly MakeService makeService;

        private readonly UserAccountService userAccountService;

        public CarService(CarDAO carDAO, MakeService makeService, UserAccountService userAccountService) 
        {
            this.carDAO = carDAO;
            this.makeService = makeService;
            this.userAccountService = userAccountService;
        }

        public async Task<List<Car>> GetAllCars()
        {
            try
            {
                List<Car> cars = await carDAO.GetAllCarsAsync();

                if (cars.Count == 0)
                {
                    return new List<Car>();
                }

                return cars;

            }
            catch (Exception ex)
            {
                return new List<Car>();
            }
        }

        public async Task<Car> GetCarByVin(string vin)
        {
            try
            {
                Car car = await carDAO.GetCarByVinAsync(vin);

                if (car == null)
                {
                    throw new Exception($"Car {vin} not found!");
                }
                return car;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could NOT get car {vin} - {ex.Message}");
            }
        }

        public async Task<List<Car>> GetUserAccountCarsByUserAccountId(int userAccountId)
        {

            List<Car> allCars = await GetAllCars();
            List<Car> userAccountCars = new List<Car>();

            try
            {
                for (int i = 0; i < allCars.Count; i++)
                {
                    if (allCars[i].UserAccountId == userAccountId)
                    {
                        userAccountCars.Add(allCars[i]);
                    }
                }

                return userAccountCars;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured - {ex.Message}");
            }
        }

        public async Task<Car> SaveCar(VehicleDTO carDTO)
        {

            Make make = await makeService.GetMakeById(carDTO.MakeId);

            UserAccount userAccount = await userAccountService.GetUserById(carDTO.UserAccountId);

            if (make == null)
            {
                throw new ArgumentNullException(nameof(make), "Make of a car can NOT be found!");
            }

            if (userAccount == null)
            {
                throw new ArgumentNullException(nameof(make), "User account can NOT be found!");
            }

            Car newCar = new Car
            {
                Vin = carDTO.Vin,
                Name = carDTO.Name,
                Description = carDTO.Description,
                Price = carDTO.Price,
                Capacity = carDTO.Capacity,
                Power = carDTO.Power,
                Make = make,
                MakeId = make.Id,
                UserAccount = userAccount,
                UserAccountId = userAccount.Id
            };

            try
            {
                await carDAO.SaveCarAsync(newCar);

                return newCar;
            }
            catch (Exception e)
            {
                throw new Exception($"Could NOT save car {newCar.Vin}! - {e.Message}");
            }

        }

        public async Task<Car> DeleteCarByVin(string vin)
        {
            try
            {
                Car car = await carDAO.GetCarByVinAsync(vin);

                if (car == null)
                {
                    throw new Exception($"Car not found!");
                }
                else
                {
                    await carDAO.DeleteCarAsync(car);
                }

                return car;

            }
            catch (Exception ex) 
            {
                throw new Exception($"Could NOT delete car {vin}! - {ex.Message}");
            }
        }

        public async Task<Car> UpdateCar(string vin, VehicleDTO updatedVehicle)
        {
            try
            {
                Car updatedCar = await GetCarByVin(vin);

                Make make = await this.makeService.GetMakeById(updatedVehicle.MakeId);

                UserAccount userAccount = await this.userAccountService.GetUserById(updatedVehicle.UserAccountId);

                if (updatedCar == null)
                {
                    throw new Exception($"Car can't be null!");
                }

                if(string.IsNullOrWhiteSpace(updatedVehicle.Name))
                {
                    updatedVehicle.Name = updatedCar.Name;
                }

                if (string.IsNullOrWhiteSpace(updatedVehicle.Description))
                {
                    updatedVehicle.Description = updatedCar.Description;
                }

                if (updatedVehicle.Price <= 0)
                {
                    updatedVehicle.Price = updatedCar.Price;
                }

                if (updatedVehicle.Capacity <= 0)
                {
                    updatedVehicle.Capacity = updatedCar.Capacity;
                }

                if (updatedVehicle.Power <= 0)
                {
                    updatedVehicle.Power = updatedCar.Power;
                }

                updatedCar.Vin = updatedVehicle.Vin;
                updatedCar.Name = updatedVehicle.Name;
                updatedCar.Description = updatedVehicle.Description;
                updatedCar.Price = updatedVehicle.Price;
                updatedCar.Capacity = updatedVehicle.Capacity;
                updatedCar.Power = updatedVehicle.Power;

                updatedCar.MakeId = make.Id;
                updatedCar.Make = make;
                updatedCar.UserAccountId = userAccount.Id;
                updatedCar.UserAccount = userAccount;

                await carDAO.UpdateCarAsync(vin, updatedCar);

                return await GetCarByVin(vin);
            }
            catch (Exception ex) 
            {
                throw new Exception($"Could NOT update the car! - {ex.Message}");
            }
        }
    }
}

using VehicleMarketplace.Dao;
using VehicleMarketplace.Models;

namespace VehicleMarketplace.Services
{
    public class CarService
    {
        private readonly CarDAO carDAO;

        public CarService(CarDAO carDAO) 
        {
            this.carDAO = carDAO;
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

        public async Task<Car> SaveCar(Car car)
        {
            if(car == null)
            {
                throw new ArgumentNullException(nameof(car), "Car can NOT be null!");
            }

            try
            {
                await carDAO.SaveCarAsync(car);

                return car;
            }
            catch (Exception e)
            {
                throw new Exception($"Could NOT save car {car.Vin}! - {e.Message}");
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

        public async Task<Car> UpdateCar(string vin, Car updatedCar)
        {
            try
            {
                Car car = await GetCarByVin(vin);

                if(car == null)
                {
                    throw new Exception($"Car can't be null!");
                }

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

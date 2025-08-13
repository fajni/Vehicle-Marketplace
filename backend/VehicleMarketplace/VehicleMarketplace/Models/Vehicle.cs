/* BASE CLASS */

using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMarketplace.Models
{
    [NotMapped]
    public class Vehicle
    {
        public int Vin { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public double Capacity { get; set; }

        public double Power { get; set; }
    }
}

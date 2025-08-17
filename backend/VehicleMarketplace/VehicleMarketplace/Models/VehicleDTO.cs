
/* THIS CLASS IS USED FOR DATA TRANSFER BETWEEN FRONTEND AND BACKEND */

namespace VehicleMarketplace.Models
{
    public class VehicleDTO
    {

        public string Vin { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Capacity { get; set; }
        public double Power { get; set; }
        public int MakeId { get; set; }
        public int UserAccountId { get; set; }
    }
}

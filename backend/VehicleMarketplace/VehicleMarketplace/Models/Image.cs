using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMarketplace.Models
{
    [Table(name: "images")]
    public class Image
    {
        [Key, Column(name: "image_id")]
        public int Id { get; set; }

        [Column(name: "image_path")]
        public string Src { get; set; }


        [Column("image_car_vin")]
        public string? CarVin { get; set; }

        [ForeignKey("CarVin")]
        public Car? Car { get; set; }


        [Column("image_motorcycle_vin")]
        public string? MotorcycleVin { get; set; }

        [ForeignKey("MotorcycleVin")]
        public Motorcycle? Motorcycle { get; set; }
    }
}

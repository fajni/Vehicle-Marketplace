using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMarketplace.Models
{
    [Table(name: "makes")] // marks, model
    [Index(nameof(MakeName), IsUnique = true)] // Unique propertie
    public class Make
    {
        [Key, Column(name: "make_id")]
        public int Id { get; set; }

        [Column(name: "make_name"), MaxLength(30)]
        public string MakeName { get; set; }


        public List<Car>? cars { get; set; }

        public List<Motorcycle>? motorcycles { get; set; }
    }
}

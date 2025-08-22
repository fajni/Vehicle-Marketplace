using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleMarketplace.Models
{
    [Table(name: "cars")]
    public class Car : Vehicle
    {
        // ...

        public List<Image>? Images { get; set; }

        // Foreign Key:

        [Column("make_id")]
        public required int MakeId { get; set; }

        [ForeignKey("MakeId")]
        public required Make Make { get; set; }

        [Column("user_account_id")]
        public required int UserAccountId { get; set; }

        [ForeignKey("UserAccountId")]
        public required UserAccount UserAccount { get; set; }

    }
}

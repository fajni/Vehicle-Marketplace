using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Posts.Models
{
    // Unique properties: 
    [Index(nameof(Email), IsUnique = true)]
    [Table(name: "user_accounts")]
    public class UserAccount
    {
        [Key, Column(name: "user_id")]
        public int Id { get; set; }

        [Column(name: "firstname"), MaxLength(20)]
        public string Firstname { get; set; }

        [Column(name: "lastname"), MaxLength(20)]
        public string Lastname { get; set; }

        [Column(name: "email"), MaxLength(100), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column(name: "password"), MinLength(5), DataType(DataType.Password)]
        public string Password { get; set; }

        [Column(name: "role"), MaxLength(10)]
        public string Role { get; set; }
    }
}

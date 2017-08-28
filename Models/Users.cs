using System.ComponentModel.DataAnnotations;

namespace Travels.Models
{
    public class Users
    {
        public int ID { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

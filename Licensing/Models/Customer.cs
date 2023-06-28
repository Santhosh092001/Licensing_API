using System.ComponentModel.DataAnnotations;

namespace Licensing.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string SPOC { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
    }
}

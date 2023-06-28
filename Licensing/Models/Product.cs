using System.ComponentModel.DataAnnotations;

namespace Licensing.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Version { get; set; }
    }
}

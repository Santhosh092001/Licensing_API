using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licensing.Models
{
    public class ActivationKey
    {
        [Key]
        public string Key { get; set; }
        public int InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public virtual ProductCustomerMap CustomerMap { get; set; }
        public string SerialNumber { get; set; }
        public string MACAddress { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string ActivateKey { get; set; }
        public bool Status { get; set; }

    }
}

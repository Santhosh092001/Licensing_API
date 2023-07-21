using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licensing.Models
{
    public class ActivationKey
    {
        [Key]
        public int Id { get; set; }
        public int InventoryId { get; set; }
        [ForeignKey("InventoryId")]
        public virtual ProductCustomerMap CustomerMap { get; set; } 
        public string MachineId { get; set; }
        public string SerialNumber { get; set; }
        public string MACAddress { get; set; }
        public DateTime KeyGenerationDate { get; set;}
        [DisplayFormat(DataFormatString = "{yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }
        public string ActivateKey { get; set; }  
        public bool Status { get; set; }

    }
}

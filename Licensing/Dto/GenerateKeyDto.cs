using Licensing.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licensing.Dto
{
    public class GenerateKeyDto
    {
        public int InventoryId { get; set; }
        public string MachineId { get; set; }
        public string SerialNumber { get; set; }
        public string MACAddress { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}

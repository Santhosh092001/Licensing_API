namespace Licensing.Dto
{
    public class ProductCustomerDetailsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CustomerName { get; set; }
        public string MACAddress { get; set; }
        public string SerialNumber { get; set; }
        public string MachineId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime KeyGenerationDate { get; set; }
        public bool Status { get; set; }
        public string ActivationKey { get; set; }
    }
}

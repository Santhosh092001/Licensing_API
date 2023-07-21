using Licensing.Dto;

namespace Licensing.IRepositories
{
    public interface IGenerateKeyRepository
    {
        public string generateKey(GenerateKeyDto key);
        public string[] decryptedKey(string encrytedKey, bool UseHashing);

        public List<ProductCustomerDetailsDto> getProductCustomerDetails();

        public bool ChangeStatus();

    }
}

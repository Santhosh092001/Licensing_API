using Licensing.DBContext;
using Licensing.Dto;
using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Utilities.Date;
using System.Security.Cryptography;
using System.Text;

namespace Licensing.Repositories
{
    public class GenerateKeyRepository : IGenerateKeyRepository
    {
        private readonly LDbContext _dbContext;
        public GenerateKeyRepository(LDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public string generateKey(GenerateKeyDto key)
        {
            try
            {
                var inventory = _dbContext.ProductCustomerMaps.FirstOrDefault(x => x.Id == key.InventoryId);
                var product = _dbContext.Products.FirstOrDefault(x => x.Id == inventory.ProductId);
                var customer = _dbContext.Products.FirstOrDefault(x => x.Id == inventory.CustomerId);
                var date = (key.ExpiryDate).ToString();
                var mdate = date.Split(' ');
                date = mdate[0];
                key.ExpiryDate = ModifyDate(date);
                string EncryptionKey = "Exalca";
                byte[] KeyArray;
                byte[] ToEncryptArray = UTF8Encoding.UTF8.GetBytes($"{key.MACAddress}|{key.ExpiryDate}|{product.Name}|{inventory.Id}|{key.SerialNumber}");
                if (key != null)
                {
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    KeyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));
                    hashmd5.Clear();
                }
                else
                {
                    KeyArray = UTF8Encoding.UTF8.GetBytes(EncryptionKey);

                }
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = KeyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tdes.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(ToEncryptArray, 0,
                  ToEncryptArray.Length);
                tdes.Clear();
                var activationKey = new ActivationKey();
                activationKey.ActivateKey = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                activationKey.MachineId = key.MachineId;
                activationKey.MACAddress = key.MACAddress;
                activationKey.SerialNumber = key.SerialNumber;
                activationKey.ExpiryDate = key.ExpiryDate;
                activationKey.Status = true;
                activationKey.InventoryId = key.InventoryId;
                activationKey.CustomerMap = null;
                activationKey.KeyGenerationDate = DateTime.Now;
                _dbContext.ActivationKeys.Add(activationKey);
                _dbContext.SaveChanges();
                return activationKey.ActivateKey;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static DateTime ModifyDate(string modifyDate)
        {
            DateTime date = DateTime.ParseExact(modifyDate, "dd-mm-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            date = date.Date.AddDays(1).AddTicks(-1);
            return date;
        }

        public string[] decryptedKey(string encrytedKey, bool UseHashing)
        {
            encrytedKey = encrytedKey.Replace(' ', '+');
            string EncryptionKey = "Exalca";
            byte[] KeyArray;
            byte[] ToEncryptArray = Convert.FromBase64String(encrytedKey);
            if (UseHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                KeyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));
                hashmd5.Clear();
            }
            else
            {
                KeyArray = UTF8Encoding.UTF8.GetBytes(EncryptionKey);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = KeyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 ToEncryptArray, 0, ToEncryptArray.Length);
            tdes.Clear();

            var key = UTF8Encoding.UTF8.GetString(resultArray);

            string[] decryptedDataarr = key.Split('|');
            /*var date = decryptedDataarr[1].Split(' ');
            decryptedDataarr[1] = date[0];*/
            return decryptedDataarr;
        }

        public List<ProductCustomerDetailsDto> getProductCustomerDetails()
        {
            ChangeStatus();
            var lists = new List<ProductCustomerDetailsDto>();
            var q = (from productCustomer in _dbContext.ProductCustomerMaps
                     join activationKey in _dbContext.ActivationKeys on productCustomer.Id equals activationKey.InventoryId
                     join customers in _dbContext.Customers on productCustomer.CustomerId equals customers.Id
                     join products in _dbContext.Products on productCustomer.ProductId equals products.Id
                     select new
                     {
                         productCustomer.Id,
                         products.Name,
                         CustomerName = customers.Name,
                         activationKey.SerialNumber,
                         activationKey.MACAddress,
                         activationKey.MachineId,
                         activationKey.KeyGenerationDate,
                         activationKey.ExpiryDate,
                         activationKey.Status,
                         activationKey.ActivateKey
                     }).ToList();
            foreach (var qs in q)
            {
                lists.Add(new ProductCustomerDetailsDto()
                {

                    ProductId = qs.Id,
                    ProductName = qs.Name,
                    CustomerName = qs.CustomerName,
                    MACAddress = qs.MACAddress,
                    SerialNumber = qs.SerialNumber,
                    MachineId = qs.MachineId,
                    KeyGenerationDate = qs.KeyGenerationDate,
                    ExpiryDate = qs.ExpiryDate,
                    Status = qs.Status,
                    ActivationKey = qs.ActivateKey
                });
            }
            return lists;
        }


        public bool ChangeStatus()
        {
            var keys = _dbContext.ActivationKeys.ToList();
            foreach(var key in keys)
            {
                if(key.ExpiryDate.AddHours(23).AddMinutes(59).AddSeconds(59) <=  DateTime.UtcNow)
                {
                    key.Status = false;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

    }
}

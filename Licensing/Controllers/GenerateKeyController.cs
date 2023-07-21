using ClosedXML.Excel;
using Irony.Parsing;
using Licensing.Dto;
using Licensing.IRepositories;
using Licensing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NPOI.POIFS.Crypt;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Licensing.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
/*    [Authorize]*/
    public class GenerateKeyController : ControllerBase
    {
        private readonly IGenerateKeyRepository _generateKeyRepository;
        public GenerateKeyController(IGenerateKeyRepository generateKeyRepository)
        {
            _generateKeyRepository = generateKeyRepository;
        }


        [HttpPost]
        public IActionResult EncryptKey(GenerateKeyDto key)
        {
            var encryptKey = _generateKeyRepository.generateKey(key);
            if(encryptKey != null)
            {
                /*var decryptKey = _generateKeyRepository.decryptedKey(encryptKey, true);*/
                return Ok(new { key = encryptKey } );
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public IActionResult DecryptedKey(string encrytedKey, bool UseHashing)
        {

            var decryptKey = _generateKeyRepository.decryptedKey(encrytedKey, UseHashing);
            return Ok(decryptKey);
        }

        [HttpGet]
        public IActionResult GetProductCustomerDetails()
        {
            var productCustomerDetails = _generateKeyRepository.getProductCustomerDetails();
            return Ok(productCustomerDetails);
        }


        [HttpGet]
        public IActionResult GetExcel()
        {
            var productCustomerDetails = _generateKeyRepository.getProductCustomerDetails();
            using(var workBook = new XLWorkbook())
            {
                var workSheet = workBook.Worksheets.Add("CompanyDetails");
                var currentRow = 1;

                workSheet.Cell(currentRow, 1).Value = "Produc Id";
                workSheet.Cell(currentRow, 2).Value = "Product Name";
                workSheet.Cell(currentRow, 3).Value = "Customer Name";
                workSheet.Cell(currentRow, 4).Value = "Serial Number";
                workSheet.Cell(currentRow, 5).Value = "MAC Address";
                workSheet.Cell(currentRow, 6).Value = "Machine Id";
                workSheet.Cell(currentRow, 7).Value = "Status";
                workSheet.Cell(currentRow, 8).Value = "Key Generation Date";
                workSheet.Cell(currentRow, 9).Value = "Expiry Date";
                workSheet.Cell(currentRow, 10).Value = "Activation Key";


                foreach (var item in productCustomerDetails)
                {
                    currentRow++;
                    workSheet.Cell(currentRow, 1).Value = item.ProductId;
                    workSheet.Cell(currentRow, 2).Value = item.ProductName;
                    workSheet.Cell(currentRow, 3).Value = item.CustomerName;
                    workSheet.Cell(currentRow, 4).Value = item.SerialNumber;
                    workSheet.Cell(currentRow, 5).Value = item.MACAddress;
                    workSheet.Cell(currentRow, 6).Value = item.MachineId;
                    workSheet.Cell(currentRow, 7).Value = item.Status;
                    workSheet.Cell(currentRow, 8).Value = item.KeyGenerationDate;
                    workSheet.Cell(currentRow, 9).Value = item.ExpiryDate;
                    workSheet.Cell(currentRow, 10).Value = item.ActivationKey;
                }

                using(var stream = new MemoryStream())
                {
                    workBook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
                        "CompanyDetails.xlsx");
                }

            }


            /*"ProductId"
    "ProductName"
    "CustomerName"
    "MACAddress"
    "SerialNumber"
    "ExpiryDate"
    "KeyGenerationDate"
    "Status"
    "ActivationKey"*/

            return Ok();
        }

    }
}

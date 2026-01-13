using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CargoMISMobileAPI.Repository
{
    public class BarcodeRepository : IBarcodes
    {
        readonly DatabaseContext _dbContext = new();
        public BarcodeRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddBarcode(List<BarcodeModel> barcode)
        {
            try
            {
                if (barcode != null)
                {

                    _dbContext.Barcodes.AddRange(barcode);
                    _dbContext.SaveChanges();
                }
                  
            }
            catch
            {
                throw;
            }
        }
    }
}

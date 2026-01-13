using CargoMISMobileAPI.Models;


namespace CargoMISMobileAPI.Interface
{
    public interface IBarcodes
    {
        public void AddBarcode(List<BarcodeModel> barcode);
	}
}

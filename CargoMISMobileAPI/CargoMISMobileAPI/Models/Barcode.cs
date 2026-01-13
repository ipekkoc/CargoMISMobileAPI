using System.ComponentModel.DataAnnotations;

namespace CargoMISMobileAPI.Models
{
    public class BarcodeModel
    {
        [Key]
        public int Ref { get; set; }
        public string? Modul { get; set; }
        public DateTime Tarih { get; set; }
        public string? Barkod { get; set; }
        public int? Adet { get; set; }
        public int? AdetNo { get; set; }
        public int? TakipNo { get; set; }
        public decimal? En { get; set; }
        public decimal? Boy { get; set; }
        public decimal? Yuk { get; set; }
        public decimal? Desi { get; set; }
        public decimal? Kg { get; set; }
        public string? Sube { get; set; }
        public string? Longitude { get; set; }
        public string? Latitude { get; set; }
        public string? Aciklama { get; set; }
        public int? Statu { get; set; }
        public string? Kullanici { get; set; }
        public string? Plaka { get; set; }
        public string? Imei { get; set; }
        public string? Tahsilat { get; set; }


    }
}

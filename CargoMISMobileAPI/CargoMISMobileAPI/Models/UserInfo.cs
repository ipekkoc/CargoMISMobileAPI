using System.ComponentModel.DataAnnotations;

namespace CargoMISMobileAPI.Models
{
    public class UserInfo
    {
        [Key]
        public int Ref { get; set; }
        public string Kod { get; set; }
        public string Adi { get; set; }
        public string Sifre { get; set; }
        public string LokasyonKodu { get; set; }
        public string Lokasyon { get; set; }
        public string Il { get; set; }
        public string Modul { get; set; }
        public string Gorev { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace CargoMISMobileAPI.Models
{
    public class TerminalModel
    {
        [Key]
        public int Ref { get; set; }
        public string? Model { get; set; }
		public string? SeriNo { get; set; }
		public string? Sube { get; set; }
		public DateTime IslemTarihi { get; set; }
        public string? Kullanici { get; set; }
        
    }
}

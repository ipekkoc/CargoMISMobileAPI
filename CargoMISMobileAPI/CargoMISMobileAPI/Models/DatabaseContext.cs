using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CargoMISMobileAPI.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        public virtual DbSet<BarcodeModel>? Barcodes { get; set; }
        public virtual DbSet<UserInfo>? UserInfos { get; set; }
		//public virtual DbSet<Versiyon> Versions { get; set; }
		public virtual DbSet<TerminalModel>? Terminals{ get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                //entity.HasNoKey();
                entity.ToTable("kullanici");
                entity.Property(e => e.Ref).HasColumnName("ref");
                entity.Property(e => e.Adi).HasColumnName("adi");
                entity.Property(e => e.Gorev).HasColumnName("gorev");
                entity.Property(e => e.Kod).HasColumnName("kod");
                entity.Property(e => e.Lokasyon).HasColumnName("lokasyon");
                entity.Property(e => e.LokasyonKodu).HasColumnName("lokkodu");
                entity.Property(e => e.Modul).HasColumnName("modul");
                entity.Property(e => e.Il).HasColumnName("il");
                entity.Property(e => e.Sifre).HasColumnName("sifre");

            });

            modelBuilder.Entity<BarcodeModel>(entity =>
            {
                entity.ToTable("barkod");
                entity.Property(e => e.Ref).HasColumnName("ref");
                entity.Property(e => e.Aciklama).HasColumnName("aciklama");
                entity.Property(e => e.Adet).HasColumnName("adet");
                entity.Property(e => e.AdetNo).HasColumnName("adetno");
                entity.Property(e => e.Boy).HasColumnName("boy");
                entity.Property(e => e.Desi).HasColumnName("desi");
                entity.Property(e => e.En).HasColumnName("en");
                entity.Property(e => e.Imei).HasColumnName("imei");
                entity.Property(e => e.Kg).HasColumnName("kg");
                entity.Property(e => e.Kullanici).HasColumnName("kullanici");
                entity.Property(e => e.Latitude).HasColumnName("latitude");
                entity.Property(e => e.Longitude).HasColumnName("longitude");
                entity.Property(e => e.Modul).HasColumnName("modul");
                entity.Property(e => e.Plaka).HasColumnName("plaka");
                entity.Property(e => e.Statu).HasColumnName("statu");
                entity.Property(e => e.Sube).HasColumnName("sube");
                entity.Property(e => e.Tahsilat).HasColumnName("tahsilat");
                entity.Property(e => e.TakipNo).HasColumnName("takipno");
                entity.Property(e => e.Tarih).HasColumnName("tarih");
                entity.Property(e => e.Yuk).HasColumnName("yuk");
				entity.Property(e => e.Barkod).HasColumnName("barkod");


			});

			modelBuilder.Entity<TerminalModel>(entity =>
			{
				//entity.HasNoKey();
				entity.ToTable("terminal");
				entity.Property(e => e.Ref).HasColumnName("Ref");
				entity.Property(e => e.Model).HasColumnName("Model");
				entity.Property(e => e.SeriNo).HasColumnName("SeriNo");
				entity.Property(e => e.Sube).HasColumnName("Sube");
				entity.Property(e => e.IslemTarihi).HasColumnName("IslemTarihi");
				entity.Property(e => e.Kullanici).HasColumnName("Kullanici");
				

			});

			//modelBuilder.Entity<Versiyon>(entity =>
			//{
			//    entity.ToTable("versiyon");
			//    entity.Property(e => e.Modul).HasColumnName("modul");
			//    entity.Property(e => e.Version).HasColumnName("versiyon");
			//});

			OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

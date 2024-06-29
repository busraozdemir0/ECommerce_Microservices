using ECommerce.Services.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using System;

namespace ECommerce.Services.ProductAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Kadın Deri Ceket",
                Price = 1250,
                Description = " Suni deri geniş kollu bomber ceket tam kalıplıdır. Ceket omuzdan aşağı 53 cm uzunluğundadır. Ceket deri kumaştır. Ceket esnek değildir. Sıklıkla kullandığınız bedeni tercih edebilirsiniz. Görseldeki model 170 cm, 53 kg'dır. Modelin kendi bedeni S bedendir.",
                ImageUrl = "https://cdn.dsmcdn.com/ty1219/product/media/images/prod/SPM/PIM/20240321/01/9ff9f963-c3a9-320c-a0c1-5f91b675ce22/1_org_zoom.jpg",
                CategoryName = "Giyim"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Apple iPhone 13 256 GB Cep Telefonu",
                Price = 47.499,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://cdn.dsmcdn.com/ty1192/product/media/images/prod/SPM/PIM/20240301/03/53117d3c-898b-333d-a2a4-108ea53e0f63/1_org_zoom.jpg",
                CategoryName = "Elektronik"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Stiletto Ayakkabı",
                Price = 350,
                Description = "Daxtors Nicole Kadın Rahat Şık Topuklu Ayakkabı Stiletto",
                ImageUrl = "https://cdn.dsmcdn.com/ty1207/product/media/images/prod/SPM/PIM/20240313/16/cb83da79-cadd-3d72-a5e3-827edac96d78/1_org_zoom.jpg",
                CategoryName = "Ayakkabı"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "KUMTEL LatteNow Tam Otomatik Espresso Makinesi",
                Price = 13.459,
                Description = "Kumtel LatteNow Tam Otomatik Espresso Makinesi - Kahve Öğütücülü Dokunmatik Renkli Ekranlı Barista kalitesinde kahve keyfini evinize getirin! 1.5 L Su, 750 ml (Ayarlanabilir) Süt Tankı Kapasitesi sayesinde tek seferde 12 fincana kadar (1500 ml) demleyerek, kahve keyfini aileniz ve arkadaşlarınız ile paylaşın. Ayarlanabilir Öğütme Sistemi 1350 Watt Güç sayesinde Her seferinde mükemmel bir fincan kahveyi kolayca demleyin. 220-240V~, 50 Hz Gerili Dokunmatik Renkli Ekran Kontrolü Kahve Çeşitleri: Espresso, Cappuccino, Latte, Americano Otomatik Süt Köpürtme Fonksiyonu Çıkarılabilir 1.5L Su Deposu Kolay Su Doldurma ve Temizleme Çıkarılabilir 750ml Süt Tankı Otomatik Kapanma Fonksiyonu Çıkarılabilir Damlama Tepsisi & Damlama Kutusu Susuz Çalışmama Koruması Otomatik Süt Köpürtme Fonksiyonu: Süt köpürtme işlemini otomatik hale getirerek, cappuccino ve latte yapımını kolaylaştırır. Kolay Temizlenebilir: Çıkarılabilir su ve süt tankları, temizlik ve su doldurma işlemlerini pratik hale getirir. Dokunmatik Kontrol Paneli: Kullanımı kolay ve modern bir arayüz sunar. Ürün Boyutu: 510x238x368 mm 24 Ay Kumtel Garantili.",
                ImageUrl = "https://cdn.dsmcdn.com/ty1362/product/media/images/prod/QC/20240613/16/399ceb1a-78fb-3747-8529-3a00ba41b2a2/1_org_zoom.jpg",
                CategoryName = "Elektronik"
            });
        }
    }
}

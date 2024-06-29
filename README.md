# Mikroservis E-Ticaret Projesi
## Proje hakkında

###
Bu proje; mikroservis mimarisi kullanılarak geliştirilmiş mini bir e-ticaret mantığındaki web sitesidir. Kullanıcıların kayıt olup giriş yapabilme, ana sayfadaki ürünleri veya detayları görüntüleme, 
sepete ürün ekleme, sepetteki ürünlere kupon uygulayabilme, ürün ve kupon için ilgili operasyonları gerçekleştirebilme gibi işlemleri içermektedir.

ASP.NET Core 8.0 Web API ve MVC kullanarak geliştirdiğim mikroservis projemde, tüm CRUD işlemleri için Back-End'de Web API kullanılmış olup Front-End'de ise bu API katmanları consume edilmiştir. 
Dinamik veritabanı işlemleri için ise Entity Framework Code First yaklaşımından yararlanılmıştır.
###

# Kullanılan Teknolojiler
- Asp.Net Core 8.0 MVC
- Asp.Net Core 8.0 Web API
- Mikroservis Mimarisi
- MSSQL Server
- Entity Framework Code First
- Swagger
- Identity
- Html, Css
- Bootstrap
- Automapper

# Front-End
- Asp.Net Core 8.0 MVC
- Html
- Css
- Bootstrap
- Toastr Bildirimleri

# Back-End
- Asp.Net Core 8.0 Web API
- MSSQL Server
- Entity Framework
- Swagger
- Automapper
  
# Projenin Öne Çıkan Özellikleri
- Veritabanı işlemleri için Entity Framework Code First kullanımı
- Identity ile Giriş ve Kayıt Olma işlemleri.
- Rolleme
- Ürün ve kuponlara yönelik ilgili CRUD işlemleri
- Ana sayfada listelenen ürünlerin detayını görüntüleme
- Ürünü sepete ekleme, çıkarma (Ürün sepette varsa ürün adeti 1 arttırılmaktadır.)
- Sepetteki ürünlere kupon uygulama, kaldırma


# Projenin Görselleri

### Ana Sayfa 
![Ana ekran](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/home1.png)

![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/home_detail.png)

### Kayıt Ol Sayfası
![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/registerPage.png)

### Alışveriş Sepeti
![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/shoppingCart2.png)

![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/shoppingCart3_coupon.png)

### Kupon Listesi Sayfası
![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/couponList.png)

### Ürün Listesi Sayfası
![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/productList.png)

### Ürün Güncelleme İşlemi
![Ana sayfa](https://github.com/busraozdemir0/ECommerce_Microservices/blob/master/ProjectScreenShots/productUpdatePage.png)

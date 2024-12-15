## C# Barkodlu Satış Programı

Bu proje, C# programlama dili ve Entity Framework Core kullanılarak geliştirilmiş bir barkodlu satış programıdır. İşletmelerin satış ve stok yönetimi süreçlerini otomatikleştirmek ve verimliliği artırmak için tasarlanmıştır. Proje, kullanıcı dostu bir arayüze, hızlı barkod okuma özelliğine, anlık stok takibine, kapsamlı raporlama seçeneklerine ve esnek özelleştirme imkanlarına sahiptir. Geliştirme sürecinde Udemy'de aldığım bir C# barkodlu satış programı eğitimi bana ilham verdi ve yol gösterdi.

**Özellikler:**

* **Barkod Okuma:** Barkod okuyucu ile entegre çalışarak ürünleri hızlı ve doğru bir şekilde tarar.
* **Stok Takibi:** Ürünlerin stok durumunu anlık olarak takip eder, stok seviyelerini izler ve düşük stok uyarıları verir.
* **Satış İşlemleri:** Satış işlemlerini hızlı ve kolay bir şekilde gerçekleştirir, fatura ve fiş oluşturur.
* **Raporlama:** Detaylı satış raporları, stok raporları ve diğer önemli verileri sağlar.
* **Kullanıcı Yönetimi:** Farklı kullanıcı rolleri ve izinleri tanımlama imkanı sunar.
* **Veritabanı Entegrasyonu:** Verileri güvenli bir şekilde saklamak ve yönetmek için **Entity Framework Core** ile **LocalDB** kullanır.
* **Özelleştirme:** İşletmenin özel ihtiyaçlarına göre özelleştirilebilir.


**Kullanılan Teknolojiler:**

* C#
* .NET Framework / .NET (Belirtmeniz Gerekir)
* Entity Framework Core
* **LocalDB (SQL Server Express LocalDB)**
* [Barkod Okuyucu Kütüphanesi/API (Kullanıyorsanız)]
* [Diğer kütüphaneler (Varsa)]

**Kurulum:**

1. Projeyi klonlayın.
2. Gerekli NuGet paketlerini yükleyin (Entity Framework Core, SQL Server provider, vb.).
3.  `appsettings.json` veya benzeri bir yapılandırma dosyasındaki bağlantı dizesinin LocalDB'ye işaret ettiğinden emin olun. Genellikle varsayılan olarak LocalDB kullanılır, bu yüzden bağlantı dizesi şöyle bir şey olabilir:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=[VeritabanınızınAdı];Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```
4. Migrations'ları uygulayın:  `dotnet ef migrations add InitialCreate` ve `dotnet ef database update` komutlarını Package Manager Console'da veya dotnet CLI ile kullanın.
5. Projeyi derleyin ve çalıştırın.


**Lisans:**

[Lisans Türü (örn., MIT, GPL, Apache)]


**Katkıda Bulunma:**

Katkıda bulunmak isteyen herkese açığız!  Lütfen önerilerinizi ve hata düzeltmelerinizi pull request olarak gönderin.


**İletişim:**

[E-posta Adresiniz]


**Demo:**

[Demo videosu veya ekran görüntüleri varsa link ekleyin.]


**Not:** Bu proje, eğitim amaçlı geliştirilmiştir ve ticari kullanım için uygun olmayabilir. LocalDB, geliştirme ortamı için ideal olan hafif bir veritabanıdır.  Dağıtım için, SQL Server veya başka bir veritabanı kullanmanız önerilir.

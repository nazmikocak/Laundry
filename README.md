# KYK Çamaşırhane Randevu Sistemi

Bu proje, Kredi ve Yurtlar Kurumu'na (KYK) bağlı öğrenci yurtları için geliştirilmiş modern ve kapsamlı bir **Çamaşırhane Randevu Sistemi**'dir. Öğrencilerin, konakladıkları yurttaki çamaşır ve kurutma makineleri için online olarak randevu almalarını, randevularını yönetmelerini ve çamaşırhane yoğunluğunu anlık olarak takip etmelerini sağlar.

**Bartın Üniversitesi, Fen Fakültesi, Bilgisayar Teknolojisi ve Bilişim Sistemleri Bölümü**'nün `BTS304 - Veritabanı Yönetim Sistemleri II` dersi kapsamında, **Dr. Öğr. Üyesi Bayram AKGÜL** danışmanlığında, **Nazmi KOÇAK (23110708002)** tarafından Final Sınavı ödevi olarak hazırlanmıştır.


![localhost_5214_Auth_SignIn](https://github.com/user-attachments/assets/0df53da9-58c3-490c-a72f-bfd135d08b7d)

![localhost_5214_ (3)](https://github.com/user-attachments/assets/c12efcba-b1ad-4c72-8bed-920194e499d7)

![localhost_5214_Appointment_Search](https://github.com/user-attachments/assets/755a0d07-ef3c-42c3-aab3-cdae78e245f0)

![localhost_5214_Appointment_LaundryList_date=06%2F16%2F2025%2000%3A00%3A00 machineType=Washing](https://github.com/user-attachments/assets/9dfdb4f4-e10f-4a28-b10c-4faa63eb889e)

![localhost_5214_Appointment_SelectMachine_laundryId=3 slotStart=2025-06-16T22%3A00%3A00 0000000 machineType=Washing](https://github.com/user-attachments/assets/e7b5ea99-d6f7-437e-a48f-ca3a0c5f3c7e)

![localhost_5214_Appointment_Confirm_machineId=8 slotStart=2025-06-16T22%3A00%3A00 0000000](https://github.com/user-attachments/assets/aeec432e-69e6-4b50-83ae-1434dd4f9412)

---

## 🚀 Projenin Amacı ve Kapsamı

Projenin temel hedefi, yurtlardaki manuel veya verimsiz çamaşırhane randevu süreçlerini dijitalleştirerek aşağıdaki faydaları sağlamaktır:

- **Adil Kullanım:** Her öğrencinin günlük belirli bir yıkama ve kurutma hakkı olmasını sağlayarak makine kullanımını adil bir şekilde dağıtmak.
- **Verimlilik:** Öğrencilerin çamaşırhaneye gitmeden önce makinelerin durumunu (aktif, arızalı, bakımda) ve müsait zaman dilimlerini online olarak görmelerini sağlamak.
- **Planlama Kolaylığı:** Öğrencilerin ders programlarına göre uygun saatler için kolayca randevu oluşturabilmesi, değiştirebilmesi veya iptal edebilmesi.
- **Yönetim ve Takip:** Yurt yöneticilerinin ve çamaşırhane görevlilerinin randevuları merkezi bir sistemden takip edebilmesi, makine ve çamaşırhane durumlarını yönetebilmesi.

---

## 🛠️ Kullanılan Teknolojiler ve Mimari

Bu proje, modern yazılım geliştirme prensipleri ve teknolojileri kullanılarak inşa edilmiştir.

### Arka Plan (Backend)
- **Platform:** .NET 8
- **Mimari:** Katmanlı Mimari (Entities, Repositories, Services, Web)
- **Desenler:** Repository, Unit of Work (Service & Repository Manager), Dependency Injection (DI)
- **Dil:** C#
- **Web Framework:** ASP.NET Core MVC
- **Veri Erişimi:** ADO.NET ve Saklı Yordamlar (Stored Procedures) - **ORM Kullanılmamıştır**
- **Veritabanı:** Microsoft SQL Server (MSSQL)
- **Kimlik Doğrulama:** ASP.NET Core Cookie Authentication

### Ön Yüz (Frontend)
- **HTML5 & CSS3:** Modern ve duyarlı arayüz tasarımı
- **JavaScript:** Dinamik ve etkileşimli kullanıcı deneyimi
- **Kütüphaneler:**
  - **jQuery & jQuery Validate:** Anlık form validasyonu
  - **SweetAlert2:** Estetik ve kullanıcı dostu modal/toast bildirimleri
  - **Font Awesome:** İkon kütüphanesi
- **Tasarım:** Bootstrap 5 (Grid, Layout vb. için temel) ve özel CSS stilleri

---

## 🌟 Temel Özellikler

### Öğrenci Rolü
- Sisteme kişisel bilgileriyle kayıt olma ve giriş yapma.
- Anasayfada aktif ve geçmiş randevuları görüntüleme.
- Kendi yurdundaki aktif çamaşırhaneleri listeleme.
- 4 adımlı kolay bir arayüz ile randevu oluşturma:
  1. **Randevu Ara:** Tarih ve makine tipi seçimi.
  2. **Çamaşırhane Listesi:** Müsait çamaşırhaneleri ve saat dilimlerini görme.
  3. **Randevu Al:** Seçilen saat dilimindeki makinelerin kroki görünümü üzerinden müsait olanı seçme.
  4. **Onay:** Randevu bilgilerini son kez kontrol edip onaylama.
- Aktif randevuları iptal etme.
- Hesap ve parola bilgilerini güncelleme.

### Yurt Yöneticisi & Admin Rolleri
- Sisteme yeni yurt ve çamaşırhane ekleme.
- Çamaşırhanelerin seans süresi, kapasite ve durum (Aktif, Bakımda, Kapalı) bilgilerini yönetme.
- Makinelerin durumunu (Arızalı, Bakımda) güncelleme.
- Tüm randevuları merkezi bir ekrandan görüntüleme ve takip etme.

---

## 🗄️ Veritabanı Yapısı

Sistem, ilişkisel bir veritabanı modeli üzerine kurulmuştur. Temel tablolar şunlardır:

- `Roles`: Kullanıcı rollerini tutar (Admin, Student vb.).
- `Dormitories`: Yurt bilgilerini içerir.
- `Laundries`: Yurtlara bağlı çamaşırhaneleri ve ayarlarını tutar.
- `Users`: Tüm kullanıcıların bilgilerini barındırır.
- `Machines`: Her bir çamaşırhanedeki makineleri ve durumlarını listeler.
- `Appointments`: Öğrencilerin oluşturduğu randevu kayıtlarını tutar.
- `AppointmentStatusLogs`: Randevu durumu değişikliklerinin denetim kaydını tutar.

### Veritabanı Seviyesi İş Kuralları
- **Trigger'lar:**
  - Randevu durumu değiştiğinde otomatik olarak log kaydı oluşturulur.
  - Kapalı veya bakımda olan bir çamaşırhaneye randevu alınması engellenir.
- **Fonksiyonlar & Saklı Yordamlar:**
  - Bir öğrencinin günlük randevu limiti, randevu oluşturma prosedürü içinde bir fonksiyon aracılığıyla kontrol edilir.
  - Tüm veri işlemleri, güvenlik ve performans için Saklı Yordamlar üzerinden gerçekleştirilir.

---

## 🚀 Projeyi Çalıştırma

1.  **Klonlama:** Projeyi bilgisayarınıza klonlayın:
    ```bash
    git clone https://github.com/nazmikocak/LaundrySystem.git
    ```
2.  **Veritabanı Kurulumu:**
    - `CREATE_DATABASE.sql` script'ini kullanarak veritabanını ve tabloları oluşturun.
    - `SEED_DATA.sql` script'ini çalıştırarak test verilerini veritabanına ekleyin.
3.  **Bağlantı Ayarı:**
    - `LaundrySystem.Web` projesi içindeki `appsettings.json` dosyasını açın.
    - `ConnectionStrings` bölümündeki `DefaultConnection` değerini kendi MSSQL sunucu bilgilerinize göre güncelleyin.
4.  **Çalıştırma:**
    - Projeyi Visual Studio üzerinden başlatın veya komut satırından `dotnet run` komutunu kullanın.

---

## 🧑‍💻 Geliştirici

- **Ad Soyad:** Nazmi KOÇAK
- **Öğrenci No:** 23110708002
- **GitHub:** [Nazmi Koçak](https://github.com/nazmikocak)
- **LinkedIn:** [Nazmi Koçak](https://linkedin.com/in/nazmikocak)

Bu proje, akademik bir çalışma olup, sürekli geliştirilmeye açıktır. Katkıda bulunmak isterseniz lütfen bir "issue" açın veya "pull request" gönderin.

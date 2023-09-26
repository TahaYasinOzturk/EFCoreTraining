// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using System.Numerics;

Console.WriteLine("Hello, World!");

#region veriekleme
CodeFirstKitaplikDbContext context = new();
//Book book = new()
//{
//    KitapAdi = "Muhittin",
//    Fiyat = 60
//};
//context.Add(book);
////await context.AddAsync(book); //// olanlar acıktı

//context.SaveChanges();
////await context.SaveChangesAsync(); //aseknron calıstırmayı saglar daha hızlı yapar.
// SaveChanges; insert update delete sorgularını olusturup bir  transaction esliginde vt na gönderip execute eden fonksiyondur.
//eger ki sorgulardan herhangi biri başarısız olursa tüm işlemler geri alınacaktır.
//sorgulardan herhangibiri basarısız olursa tüm islemler geri alanır buna roleback denir. mülekatlarda cıkabilir.
#endregion

#region EfCore verinin eklenecegini nasıl anlıyor?

//Book book2 = new()
//{
//    KitapAdi = "Muhittin2",
//    Fiyat = 80
//};
//Console.WriteLine(context.Entry(book2).State);  // detached
//await context.AddAsync(book2);

//Console.WriteLine(context.Entry(book2).State);  // added
//await context.SaveChangesAsync();

//Console.WriteLine(context.Entry(book2).State);  // unchanged  


#endregion

#region Birden Fazla Veri Eklerken Nelere Dikkat Edeceğiz
////Book book2 = new()
////{
////    KitapAdi = "Muhittin3",
////    Fiyat = 180
////};

////Book book3 = new()
////{
////    KitapAdi = "Ökkeş Dolmuşçu",
////    Fiyat = 50
////};

////Book book4 = new()
////{
////    KitapAdi = "Yunus Gökçe",
////    Fiyat = 70
////};
//1.yöntem
////context.Books.Add(book2);
////context.Books.Add(book3);
////context.Books.Add(book4);



// en son ekle her yere koyma sistemi yoran birşey.
////await context.SaveChangesAsync();
//sql server profiler da baktık rpc completed den baktık
// delete ile tranquate ile arasındaki var nedir. ssms
// delete siler idler 0 den devem eder. 5. satırı silince 6. satırdan devam eder. 
//tranqute de idlerde silinin 1 den baslar . harddelete yapar.

//2.yöntem
////context.Books.AddRange(book2,book3,book4);
////await context.SaveChangesAsync();
// yukardan programı play ile çalıstır.
// db ye ekledei yenilerken sag tık execute de listeyi günceller.
// migrationda update-database yapıyoruz.

//3. yöntem for metodu
//List<Book> Kitaplar = new List<Book>();
//Kitaplar.Add(book2);
//Kitaplar.Add(book3);
//Kitaplar.Add(book4); listeye ekliyorsun.
//for (int i = 0; i < 3; i++)
//{
//    context.Books.Add(book2);
//}

//SaveChanges nedir? Insert, update, delete gibi sorguların olusturulmasını ve bir transaction eşliginde vt ye gönderilip execute edilmesini sağlar.



#endregion

#region  Veri nasıl Güncellenmektedir?
//Book book = context.Books.FirstOrDefault(x => x.Id == 5); // idsi 5 olan veriyi getirr.
//book.KitapAdi = "Ökkeş Balıkçı";
//book.Fiyat = 100;

//context.SaveChanges();


#endregion

#region Change Tracker Nedir? 

//context'ten gelen dataların takibinden sorumlu bir mekanizma. Bu takip mekanizması sayesinde context üzerinden gelen verilerle ilgili işlemlerin sonucunda update veya delete sorgularının oluşacağını anlar. 

////Book book1 = await context.Books.FirstOrDefaultAsync(u => u.Id == 4);
// 4. veriyi tuna tavus yaptık.  contexten buldu cagırdı. 

////Console.WriteLine(context.Entry(book1).State); // buldu degisişiklik yok veritabanında unchanged yazar.

////book1.KitapAdi = "Tuna Tavus";
////Console.WriteLine(context.Entry(book1).State); // modified oldu veritabanında degişiklik oldu diyor.

////await context.SaveChangesAsync();
////Console.WriteLine(context.Entry(book1).State); // sonra birdaha bakıyor degişiklik yok veritabanında unchanged yazar.

#endregion

#region Takip edemedigimiz contextten gelmeyen veriler için
//update gerçekleişir ama takip edilemiyor olur burda baska bisi yapıyoruz.
////Book bookOrnek = new()
////{
////    Id = 2,
////    KitapAdi = "karpuz",
////    Fiyat = 450
////};

////context.Books.Update(bookOrnek);
////context.SaveChanges();
//context.Update();
//update in üstüne gel TEenttiy yerine book.entity . update yazınca görür. burda direk book u update yapıyoz. öncekinde savechanges yapıyorduk.
//ChangeTracker mekanizmasi tarafından takip edilemeyen mnesnelerin güncellenebilmesi açısından Update fonksiyonu kullanılmaktadır. Bu takip edilmeme meselesini şöyle algılayabiliriz. İlk yaptıgımız güncelleme örneginde veritabanımıza context üzerinden bir sorgu atıp istedigimiz Id'ye sahip olan nesneyi programımıza cagırdık. Sonrasında bu gelen ve haliyle takip edilebilen nesne üzerinde degişiklik yapıp savechanges dedik.
//Suanda ise context üzerinden herhangi bir işlem yapmadan ve haliyle takip edemedigimiz bir nesne üzerinden işlem yapmak istedik. bu yüzden update metodunu kullanmak zorunda kaldık.

#endregion

#region  takil edilebilen coklu degişiklik

//verileri context ile cagırdık.
//+= concat görevi yapar.  sonuna Roman kelimesini ekledik hepsine.
////var kitaplar = await context.Books.ToListAsync();
////foreach (var item in kitaplar)
////{
////    item.KitapAdi += " Romanı";
////    await context.SaveChangesAsync();
////}

#endregion

#region Veri Silme
// u yerine istedgini yaz idye göre cektik bunu silcez. ökkeş dolmus silinecek
////Book book1 = context.Books.FirstOrDefault(u => u.Id == 7);
////context.Books.Remove(book1);
////context.SaveChanges();

#endregion

#region Toplu Silme

////List<Book> kitaplar1 = context.Books.ToList();

////foreach (var item in kitaplar1)
////{
////    Console.WriteLine(item.KitapAdi);
////}
////context.Books.RemoveRange(kitaplar1);
////context.SaveChanges();
//tüm veriler silindi.
#endregion

public class CodeFirstKitaplikDbContext : DbContext
{

    public DbSet<Book> Books {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-E30TBPJ;Database=CodeFirstKitaplikDb;Trusted_Connection=True;TrustServerCertificate=Yes");
    }

}
 
public class Book
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public double Fiyat { get; set; }
}


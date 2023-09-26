// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
Console.WriteLine();

//DbContext Tanımlanması
public class DenemeDbContext : DbContext // db.Context ctrl '.' koyunca cikan ekranda 7.0.4 ü kur. prop tab tab  <> jenerik yapılar icine koydukların türden bagımsız olur.  Dbset yapısında türdern bagımsız Urun ekledik.
    // override bosluk on yazinca tab a bas. burda veritabanına baglantı saglayacagız.  alltan package console u secip ac cls temizler.
    // optionsBuilder.UseSqlServer("") tırnak icine veritabanı connection string i yaziyoruz.
    //add-migration CodeFirstInitializeMigration ile migration yazıyoruz console.
{
    public DbSet<Urun> Urunler { get; set; }

    public DbSet<Musteri> Musteriler { get; set; }

    public DbSet<Siparis> Siparisler { get; set; }

    //hazır metot var onun özelligini kullancaz veritabanının baglamak icin. connection stringi miz var oda onconfiguring metodu. 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //// provider
        //// Connectstring
        optionsBuilder.UseSqlServer("Server=DESKTOP-E30TBPJ;Database=501CodeFirstDb;Trusted_Connection=True;TrustServerCertificate=Yes");
    }



}

//Entity lerin Tanımlanması
public class Urun
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    
    
}

public class Musteri

{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }


}

public class Siparis
{
    //entitity lerde id diye prop olması lazım onu otomatik alıp identitiy olarak kullanacak. TAblo adlarıda tekil olacak sekilde class ları olustur.
    public int SiparisId { get; set; }
    public string Name { get; set; }

    public int Quantity { get; set; }
}
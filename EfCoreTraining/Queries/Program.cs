// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

ECommerce501DbContext context = new();

#region 500 veri ekleme
////for (int i = 0; i < 500; i++)
////{
////    Product p = new Product();
////    p.Name = $"Ürün {i + 1}";
////    p.Price = i * 10;
////    context.Products.Add(p);
////}
////context.SaveChanges();
#endregion

#region Method Syntax       En Temel Şekilde Sorgu Kullanılması
//Method Syntax ...dönenlisteye IEnumerable

////var products = context.Products.ToList();

////foreach (var item in products)
////{
////    Console.WriteLine($"Ürün Adı: {item.Name} / Ürün Fiyatı: {item.Price}");
////}


#endregion

#region  Query Syntax
// x üzerinden context producta git arama yap  x callback fonk gibi. select * from  gibi  üstekiyle aynı kodu verir.

////var urunler = await(from x in context.Products select x).ToListAsync();

////foreach (var item in urunler)
////{
////    Console.WriteLine($"Ürün Adı: {item.Name} / Ürün Fiyatı: {item.Price}");
////}

//for eachle sorgumusu execute ettik. foraech gecikmeli bir execute verir yavastır.

#endregion

#region  Gecikmeli Execution  (Deferred Execution)  ==> foreach 

//products üstüne gel IQueryable yazıyor.
////var urunId = 200;
////var products = from x in context.Products where x.Id > urunId && x.Price > 400 select x;

////urunId = 300;
// böyle yapınca normalde 200 den sonra sorgu yaptıgımız icin  200 gelmesi lazım ama foreach den dolayı 300 den sonrası geliyor.


////foreach (var item in products)
////{
////    Console.WriteLine(item.Name + " / " + item.Id + " / " + item.Price);
////}



#endregion

////Çogul veri Getiren Sorgulamalar
#region Where -Method Syntax -Çogul veri Getiren Sorgulamalar

//olusturulan sorguya sart eklemeyi saglar.
//Method Syntax
////var products = context.Products.Where(u=> u.Id >350).ToList();
////Console.WriteLine("***********************");
////var products2 = await context.Products.Where(u=> u.Name.StartsWith("Ü")).ToArrayAsync();
////Console.WriteLine("***********************");
////var products3 = await context.Products.Where(u => u.Name.EndsWith("2")).ToArrayAsync();

////foreach (var item in products2)
////{
////    Console.WriteLine(item.Name);
////}



#region Where -QuerySyntax
////var urunler = from x in context.Products where x.Id > 250 && x.Name.EndsWith("2") select x;
////foreach (var item in urunler)
////{
////    Console.WriteLine(item.Name);

////}
//250den büyük sonu 2 ile bitenler

//Console.WriteLine();
#endregion

//.ToList(); yoksa ıqueryable listelenmis olmayacak  digeri ıenumerable da tolist li coktan veritabaında sorgu yapılmıs oluyor.
#region OrderBy  - Method   Syntax
////var products = context.Products.Where(u => u.Id > 350).OrderBy(x=> x.Price).ToList();

//////OrderByDescending yapınca fiyati yüksek olandan sıralama yapar.
////var products2 = context.Products.Where(u => u.Id > 350).OrderByDescending(x => x.Price).ToList();

////foreach (var product in products)
////{
////    Console.WriteLine($"{product.Name} / {product.Price}");
////}


#endregion

#region OrderBy  - Query  Syntax   Ascending sıralar kücükten büyüge.
////var products = from x in context.Products
////               where x.Id > 350
////               orderby x.Price
////               select x;
////var urunler = products.ToList();
////// products e bak suan ıqueryable.. urunlere bak üzerine gelip ıenumearable  in memoryde olur yani list halinde durur.

////foreach (var product in products)
////{
////    Console.WriteLine($"{product.Name} / {product.Price}");
////}

#endregion

#region  queyable ve numerable arasındaki fark nedir?

//queyable==> Sorguya karşılık gelir. Efcore üzerinde yapılmıs olan sorgunun execute edilmemiş halidir.queryable execute etmek icin foreach kullanmak gerek.

// Sorgunun calıstırılıp execute edilip verilerin in memory e yüklenmiş halini ifade eder.

#endregion

#region ThenBy  - Method Syntax
////var products = context.Products.Where(x => x.Id > 350).OrderBy(u => u.Name).ThenBy(o => o.Price).ToList();
//////böyle yapınca ayni ürün ismine ait 35 ve 3600 tl var bunda yüksekten düsügüe devam eder . sonra ürün ismini ThenBy(o => o.Price). yapınca ascending sıralama yapar kücükten büyüge sıralama yapar 35 önce gelir sonra 3600 geliyor. farkı görebilirsin.
////Console.WriteLine("***********");
//////var products = context.Products.Where(x => x.Id > 350).OrderBy(u => u.Name).ToList();
////foreach (var product in products)
////{
////    Console.WriteLine($"{product.Name} / {product.Price} TL");
////}

#endregion

#region Order By Descending  /  ThenByDescending
////var products = context.Products.Where(x => x.Id > 350).OrderByDescending(u => u.Name).ThenByDescending(o => o.Price).ToList();

////foreach (var product in products)
////{
////    Console.WriteLine($"{product.Name} / {product.Price} TL");
////}
// yüksek pricedan kücüge göre sıralıyor descending.
// thenby kisminada descending yazılabilir. tersten yazıyor. yüksekten kücüge  ThenByDescending 3590 35  . 35 sonradan gelir.  ThenBy olursa 35  3590 yazıyor.
#endregion
#endregion

////Tekil veri Getiren Sorgulamalar
#region Tekil veri Getiren Sorgulamalar

#region Single / Single Async
//toliste gerek yok zaten bir veri getiriyoruz. Db de 0 indexte baska veri girdik Idler 1 sıra kaydı. 25 te 24 geliyor.
var product = await context.Products.SingleAsync(u=>u.Id ==25);

Console.WriteLine(product.Name + " / " + product.Price);



#endregion

#region

#endregion
#endregion

public class ECommerce501DbContext : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-E30TBPJ;Database=CodeFirstECommerceDb;Trusted_Connection=True;TrustServerCertificate=Yes");
    }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }    
    public double Price { get; set; }
}
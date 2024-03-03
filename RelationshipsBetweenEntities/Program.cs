
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


Console.WriteLine();
RelationsDbContext context = new RelationsDbContext();


#region  CONCEPTS IN RELATIONAL DATA STRUCTURES ( Ilişkili veri yapılarındaki kavramlar )

#region Kavram.1 :Principle Entity(Asıl/Yönetici Varlık)
//Kendi başına varolabilen tabloyu modelleyen entity'e denir.
//Departmanlar tablosunu modellemek için kullanılan Departman entity(sınıfı)'dır.
#endregion

#region Kavram.2 :Dependent Entity(Bağımlı Varlık)
//Kendi başına var olamayan, bir başka tabloya bağımlı (ilişkisel olarak bağımlı) olan tabloyu modelleyen entity'e denir.
//Çalışan tablosunu modelleyen 'Calisan' entitysidir.
#endregion

#region Kavram.3 :Foreign Key (Bağımlı Tablodaki Yabancıl Anahtar)
//Priciple Entity ile Dependent Entity arasındaki ilişkiyi sağlayan Key'dir.
//Dependent Entity'de tanımlanır
//Principal Entity'de ki Principal KEy'e karşılık gelir
#endregion

#region Kavram.4 :Priciple Key (Yönetici sınıfındaki birincil anahtar)
//Principal Entity'deki Id'nin kendisidir. Principal Entity'nin kimliğini ifade eden kolona karşılık gelmektedir.
#endregion

//public class Calisan
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }
//    public int DepartmanId { get; set; }

//} //Dependent
//public class Departman
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//} //Principal

#endregion

#region NAVIGATION PROPERTY(yön özelliği) NEDIR?
//İlişkisel tablolar arasındaki fiziksel erişimi Entity Class'ları üzerinden sağlayan property'lerdir.
//Öğrencilerin bir okulu olur ama bir okulun birden fazla öğrencisi olabileceği için bu tanıma uygun navigation property'leri tanımlıyoruz.

//Property'lerin türü Entity olmak zorundadır ki onlara navigation prop. diyebilelim.
//İlişki türlerine göre çoka-çok yahut bire-çok ilişkileri ifade ederler.
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; } //Foreign Key

    public School School { get; set; } //Navigation Prop

} //Dependent
public class School
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Student> Students  { get; set; } //Navigation Prop

} //Principal


#endregion

#region RELATION TYPES(İLİŞKİ TÜRLERİ)

//#region ONE TO ONE
////çalışan ile adres tabloları arasındaki ilişki bire bir ilişkidir
//// Principal (parent)
//public class Student2
//{
//    public int Id { get; set; }
//    public string? Name { get; set; }

//    public StudentAddress Address { get; set; } //nav. prop
//}

//public class StudentAddress
//{
//    public int StudentAddressId { get; set; }
//    public string Address { get; set; }
//    public string City { get; set; }
//    public string State { get; set; }
//    public string Country { get; set; }

//    public int StudentId { get; set; } //foreign key
//    public Student2 Student { get; set; } //nav. prop
//}
//#endregion

//#region ONE TO MANY
//public class Student1
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public int SchoolId { get; set; } //Foreign Key

//    public School1 School { get; set; } //Navigation Prop

//} //Dependent
//public class School1
//{
//    public int Id { get; set; }
//    public string Name { get; set; }

//    public ICollection<Student1> Students { get; set; } //Navigation Prop

//}
//#endregion

//#region MANY TO MANY
//public class Post
//{
//    public int Id { get; set; }
//    public List<PostTag> PostTags { get; } = []; //nav. prop.
//}

//public class Tag
//{
//    public int Id { get; set; }
//    public List<PostTag> PostTags { get; } = []; //nav. prop.
//}

//public class PostTag
//{
//    public int PostsId { get; set; }
//    public int TagsId { get; set; }
//    public Post Post { get; set; } = null!; //nav prop
//    public Tag Tag { get; set; } = null!; //nav prop
//}
//#endregion

#endregion

#region RELATION CONFIGURING WAYS WITH EFCORE(ilişki yapılandırma yöntemleri)

#region 1.YÖNTEM : DEFAULT CONVENTIONS (Varsayılan yazım kuralları)
//Varsayılan entity kurallarını kullanarak yapılan ilişki yapılandırma yöntemleridir. PrimaryKey kolonunun EntityID, ID, Id seçilmesi gibi , Efcore bunun bir primary key olduğunu anlayacaktır.
//navigation property'lerini kullanarak ilişki şablonlarını çıkarmaktadır.
#endregion

#region 2.YÖNTEM : DATA ANNOTATIONS ATTRIBUTES( Veride ek açıklama özelliği)
//Convention dışına çıkıyorsan Foreing Key'i tanımlarken kendine özgü bir isim vermek istiyorsan data annotation 'ı kullanabilirsiniz. [Key] [ForeignKey] ...
#endregion

#region 3.YÖNTEM : FLUENT API ( Akıcı Uygulama Programlama Arabirimi)
//entity modellerindeki ilişkileri yapılandırırken daha detaylı çalışmaızı sağlayan yöntemdir.
#region a.HasOne(BİRE-...) func
//BİRE-bir ya da BİRE-çok ilişki başlatırken HasOne fonksiyonu en başta çağırılır.
#endregion

#region b.HasMany (ÇOKA-...)func
//ilgili entity'nin ilişkisel entity'e ÇOKA-BİR  ya da ÇOKA-ÇOK ilişkisini yapılandırmaya başlayan, en başta kullanılan fonksiyondur.
#endregion

#region c.WithOne
// ...-BİR şeklinde ilişki yapılanmasını istediğimizde kullanırız.
#endregion

#region d.WithMany
// ...-ÇOK şeklinde ilişki yapılanmasını istediğimizde kullanırız.
#endregion

#endregion
#endregion

#region ONE TO ONE RELATION DEEPLY


#region 1.DEFAULT CONVENTION
//Her iki entity'de nav prop ile birbirlerini tekil olarak referans ederek fiziksel bir ilişkinin olacağını ifade eder.
//one-to-one ilişki türünde dependent entity'nin hangisinin olduğunu default olarak belirleyebilmek pek kolay değildir. Bu durumda fiziksel olarak bir foreign key'e karşılık property tanımlayarak çözüme ulaşıyoruz. ör: CalisanId gibi
//public class Calisan //Principal Side
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //nav prop.

//}

//public class CalisanAdresi //Dependent Side
//{
//    public int Id { get; set; }
//    public string Adres { get; set; }

//    public int CalisanId { get; set; } //foreign key

//    public Calisan Calisan { get; set; } //nav prop.
//}
#endregion

#region 2.DATA ANNOTATION
//Navigation Prop.'ler tanımlanmak zorundadır.
//Foreign Key kolonu oluşturulursa ve adını kafama göre vermek istiyorsam [ForeignKey("Principal Class")]
//Foreign key için ek kolon üretmek istemiyorsak dependent entity'nin primary key kolonuna [Key,ForeignKey("Principal Class")] attribute'u verilir.
#region 2.1.Attribute kullanmak
//public class Calisan //Principal Side
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //nav prop.

//}

//public class CalisanAdresi //Dependent Side
//{
//    public int Id { get; set; }
//    public string Adres { get; set; }

//    [ForeignKey(nameof(Calisan))] //Ya da [ForeignKey("Calisan")]
//    public int CalisanFK { get; set; } //foreign key olduğunu EFCore'un anlayabilmesi için Data Annot. kullandık.

//    public Calisan Calisan { get; set; } //nav prop.
//}
#endregion

#region 2.2. PK ve FK tek kolon olarak tanımlamak
//public class Calisan //Principal Side
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //nav prop.

//}

//public class CalisanAdresi //Dependent Side
//{
//    [Key, ForeignKey("Calisan")] //hem Pk hem de Fk
//    public int Id { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; } //nav prop.
//}
#endregion

#endregion

#region 3.FLUENT API
//navigation prop.'lar tanımlandıktan sonra Context sınıfımızın içinde OnModelCreating metodunu override ederek bu metodun scope'ları arasında konfigüre edilir.

//public class Calisan //Principal Side
//{
//    public int Id { get; set; }
//    public string Adi { get; set; }

//    public CalisanAdresi CalisanAdresi { get; set; } //nav prop.

//}

//public class CalisanAdresi //Dependent Side
//{
//    public int Id { get; set; }
//    public string Adres { get; set; }
//    public Calisan Calisan { get; set; } //nav prop.
//}
//#endregion

//public class RelationsDbContext : DbContext
//{

//    DbSet<Calisan> Calisanlar { get; set; }

//    DbSet<CalisanAdresi> CalisanAdresleri { get; set; }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        optionsBuilder.UseSqlServer("Server=DESKTOP-P7KA77K\\SQLEXPRESS;Database=RelationsDB;User Id=sa;Password=1234; ;TrustServerCertificate=true");
//    }
//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {



//        modelBuilder.Entity<Calisan>() //principal entity'den başladık
//            .HasOne(c => c.CalisanAdresi) //Calisan entity'sinde CalisanAdresi var demek
//            .WithOne(ca => ca.Calisan)  //ca : burada calisanAdresini temsil ediyor, CalisanAdresi içinde Calisan nav. prop var demek istiyoruz.
//            .HasForeignKey<CalisanAdresi>(ca => ca.Id); //bunun sayesinde kimin dependent olduğunu ve foreign key'in kimde olacağını söylüyoruz

//        modelBuilder.Entity<CalisanAdresi>()
//           .HasKey(ca => ca.Id); //Çalışan adresi bu Primary Key'e sahip demek.Yukarıda da FK olarak vermiştik, tek kolonda hem PK Hemde FK oldu.
//    }
//}

#endregion

#endregion

#region ONE TO MANY RELATION DEEPLY
#region 1.Default Convention
//entity sınıflarında navigation property'leri oluşturduk.
//Principal Entity'nin constructer'ında Calisanlar için bir liste nesnesi oluşturduk çünkü principal üzerinden bir veri eklemek istediğimizde ve o veri ile ilişkili çok miktarda işçi nesnesi eklemek istediğimizde listemiz halihazırda oluşturulmuş olmasını istedik.



#endregion

#region 2.Data Annotation
//Navigation property'ler tanımlandıktan sonra dependent entity sınıfındaki Foreign Key kolonuna karşılık gelecek olan property'nin üzerine [ForeignKey("PrincipalClassName")] yazılır 

#endregion

#region 3.Fluent API
//navigation property'ler verildikten sonra bire çok ilişkiyi context sınıfımızın içinde virtual metot olarak bulunan OnModelCretating metodunu çağırarak ilişkiyi yazmalıyız
//Dependent Entity üzerinde FK için bir property tanımlamamıza gerek yok

class Blog
{
    public int Id { get; set; }
    public string BlogName { get; set; }

    public ICollection<Post> Postlar { get; set; }

}

class Post
{
    public int Id { get; set; }

    public int BlogId { get; set; }

    public string PostName { get; set; }

    public Blog Blog { get; set; }

}

#endregion

#endregion

#region MANY TO MANY RELATION DEEPLY
//ÇOKA ÇOK İLİŞKİ KURULURKEN ENTITY SINIFLARINI PROPERTY OLARAK İÇEREN BİR ARA TABLO OLUŞTURULUR VE 1 TO N İLİŞKİ KURULUR. BU ARA TABLO N TO N İÇİN BİR ARA KÖPRÜ GÖREVİ GÖRÜR.
//ARA TABLODAKİ İKİ ID KOLONUNUN BÜTÜNÜ UNİQUE OLMALIDIR 
//HANGİ İLİŞKİ OLUŞTURMA YÖNTEMİ SEÇİLİRSE SEÇİLSİN NAVIGATION PROPERTYLERININ ÇOĞUL OLARAK OLUŞTURULMASI ŞARTTIR.
#region 1.DEFAULT CONVENTION
//ARA TABLOYU OLUŞTURMAYA GEREK YOK , EFCORE BİZİM YERİMİZE DEFAULT CONVENTION DA ARA TABLOYU OLUŞTURACAKTIR.
//ARA TABLO KOMPOZİT PRİMARY KEY'E SAHİP OLMALI 
//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }

//    ICollection<Kitap>  Kitaplar { get; set; }
//}

//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }

//    ICollection<Yazar> Yazarlar { get; set; }


//}
#endregion

#region 2.DATA ANNOTAION
//CROSS TABLE MANUEL OLARAK OLUŞTURULMAK ZORUNDADIR
//OLUŞTURULAN CROSS TABLE İLE DİĞER ENTITYLER ARASINDA 1 TO N İLİŞKİSİ KURULMASI GEREKİR.
//CROSS TABLE'DA DATA ANNOTATION ILE COMPOSIT PRIMARY KEY'İ MANUEL KURAMIYORUZ ONMODELCREATING FONKSIYONU UZERINDEN HASKEY FONKSIYONU ILE PK BİLDİRİLİR.
//cross table DbSet olarak context sınıfı içerisinde property olarak tanımlanmaz.
//public class Yazar
//{
//    public int Id { get; set; }
//    public string YazarAdi { get; set; }
//    ICollection<KitapYazar> Kitaplar { get; set; } //nav. prop.

//}
//public class KitapYazar //DATA ANNOTATION DA CROSS TABLE MANUEL OLARAK OLUŞTURULMALI VE '1 TO N' İLİŞKİSİ ENTITY SINIFLARI İLE KURULMALI
//{

//    public int KitapId { get; set; } //Eğer isimleri KId olsaydı bunun bir Foreign key olduğunu belirtmek için [ForeignKey("Kitap")]

//    public int YazarId { get; set; }//Eğer isimleri KId olsaydı bunun bir Foreign key olduğunu belirtmek için [ForeignKey("Yazar")]

//    public Kitap Kitap { get; set; }// '1 TO N' NAVIGATION PROP
//    public Yazar Yazar { get; set; }//'1 TO N' NAVIGATION PROP
//}
//public class Kitap
//{
//    public int Id { get; set; }
//    public string KitapAdi { get; set; }

//    ICollection<KitapYazar> Yazarlar { get; set; } //nav prop.
//}
#endregion

#region 3.FLUENT API
//cross table manuel oluşturulmalı, DbSet olarak eklenmesine gerek yok, Entity sınıfları içinde çoğul nav. prop'ler verilir, HasKey ile OnModelCreating içinde Composit PK oluşturulur.
public class Yazar
{
    public int Id { get; set; }
    public string YazarAdi { get; set; }

    public ICollection<KitapYazar> Kitaplar { get; set; }
}

public class KitapYazar //DATA ANNOTATION DA CROSS TABLE MANUEL OLARAK OLUŞTURULMALI VE '1 TO N' İLİŞKİSİ ENTITY SINIFLARI İLE KURULMALI
{

    public int KitapId { get; set; } 

    public int YazarId { get; set; }

    public Kitap Kitap { get; set; }// '1 TO N' NAVIGATION PROP
    public Yazar Yazar { get; set; }//'1 TO N' NAVIGATION PROP
}
public class Kitap
{
    public int Id { get; set; }
    public string KitapAdi { get; set; }
    public ICollection<KitapYazar> Yazarlar { get; set; }

}
#endregion

#endregion

public class RelationsDbContext : DbContext
{

    DbSet<Calisan> Calisanlar { get; set; }

    DbSet<Departman> Departmanlar { get; set; }

    DbSet<Blog> Bloglar { get; set; }

    DbSet<Post> Postlar { get; set; }
    DbSet<Kitap> Kitaplar { get; set; }

    DbSet<Yazar> Yazarlar { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-P7KA77K\\SQLEXPRESS;Database=RelationsDB;User Id=sa;Password=1234; ;TrustServerCertificate=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Blog)
            .WithMany(b => b.Postlar)
            .HasForeignKey(p => p.BlogId); //burada FK tanımlamak için FK olacak prop'u gidip dependent entity sınıfnıda tanımlamak gerek
        //bire çok ilişkiler oluşturulurken foreign key prop'u nu biz oluşturmasaydık EFCore oluştururdu.

        //Data Annotation yönteminde cross table'daki Composit PK için çağırılan HasKey fonksiyonu.
        //modelBuilder.Entity<KitapYazar>()
        //    .HasKey(ky=> new {ky.YazarId,ky.KitapId});

        modelBuilder.Entity<KitapYazar>()
            .HasKey(ky=> new {ky.YazarId,ky.KitapId});

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky=>ky.Kitap)
            .WithMany(k=>k.Yazarlar)
            .HasForeignKey(ky=>ky.KitapId);

        modelBuilder.Entity<KitapYazar>()
            .HasOne(ky => ky.Yazar)
            .WithMany(y => y.Kitaplar)
            .HasForeignKey(ky => ky.YazarId);
    }

}



public class Calisan //dependent entity
{
    public int Id { get; set; }
    public int DepartmanId { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public Departman Departman { get; set; }

}

public class Departman //principal entity
{
    public Departman()
    {
        Calisanlar = new HashSet<Calisan>();
        Console.WriteLine("Calisan Listesi için liste nesnesi oluştu");
    }
    public int Id { get; set; }
    public string? DepartmanName { get; set; }

    public ICollection<Calisan> Calisanlar { get; set; }
}

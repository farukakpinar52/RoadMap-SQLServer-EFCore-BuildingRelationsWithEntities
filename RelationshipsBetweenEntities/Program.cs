// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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

public class Calisan
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int DepartmanId { get; set; }

} //Dependent
public class Departman
{
    public int Id { get; set; }
    public string? Name { get; set; }

} //Principal

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

#region ONE TO ONE
//çalışan ile adres tabloları arasındaki ilişki bire bir ilişkidir
// Principal (parent)
public class Student2
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public StudentAddress Address { get; set; } //nav. prop
}

public class StudentAddress
{
    public int StudentAddressId { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public int StudentId { get; set; } //foreign key
    public Student2 Student { get; set; } //nav. prop
}
#endregion

#region ONE TO MANY
public class Student1
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; } //Foreign Key

    public School1 School { get; set; } //Navigation Prop

} //Dependent
public class School1
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Student1> Students { get; set; } //Navigation Prop

}
#endregion

#region MANY TO MANY
public class Post
{
    public int Id { get; set; }
    public List<PostTag> PostTags { get; } = []; //nav. prop.
}

public class Tag
{
    public int Id { get; set; }
    public List<PostTag> PostTags { get; } = []; //nav. prop.
}

public class PostTag
{
    public int PostsId { get; set; }
    public int TagsId { get; set; }
    public Post Post { get; set; } = null!; //nav prop
    public Tag Tag { get; set; } = null!; //nav prop
}
#endregion

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
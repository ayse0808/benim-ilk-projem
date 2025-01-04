using PİYON_OYUNU;
using System;//Temel sistem işlevlerini sağlar.
using System.Windows.Forms;//Windows Forms uygulamaları için UI bileşenlerini ve işlevlerini içerir.


namespace PİYON_OYUNU//Bu isim alanı, projedeki diğer sınıf ve kodlarla olası çakışmaları önler.
{
    static class Program// Programın giriş noktası olan bir sınıf. static olduğu için bir nesne oluşturmadan kullanılabilir.
    {
        [STAThread]/*Bu genellikle Windows Forms veya WPF (Windows Presentation Foundation) uygulamalarında kullanıcı arayüzü
 iş parçacığının uyumluluğu ve doğru çalışması için gereklidir.Eğer [STAThread] eklenmezse:Uygulama düzgün çalışmayabilir.
 Bazı UI işlemleri hata verebilir veya beklenmedik sonuçlar doğurabilir. Özellikle Windows bileşenleriyle (örneğin, Clipboard, FileDialog) çalışırken hatalar oluşabilir.*/



        static void Main()/*Programın çalıştırıldığında çağrılan ana giriş noktasıdır.
                           Buradaki tüm kodlar uygulamanın başlangıcında çalışır.*/

        {
          Application.EnableVisualStyles();/*Windows Forms uygulamasının modern görünümlü bir tema kullanmasını sağlar.
                                           Daha iyi bir estetik için gereklidir.*/

          Application.SetCompatibleTextRenderingDefault(false);/*Metinlerin render edilme yöntemini ayarlar.
                           Genelde false olarak bırakılır ve GDI+ tabanlı metin işleme kullanılır.*/


            
            tahta formTahta = new tahta();//tahta adında bir form oluşturulur. Bu oyun tahtasını temsil eder.
            zar formZar = new zar();//zar adında başka bir form oluşturulur. Bu, zar atma işlevini temsil eder.

            
            formZar.ZarSonucuBildir += formTahta.ZarSonucunuIsle;/*zar formunda bir olaydır (event).
 Bu olay, zar sonucu bildirildiğinde tetiklenir.+= formTahta.ZarSonucunuIsle: tahta formunun ZarSonucunuIsle metodu,
 bu olaya abone olur. Böylece zar atıldığında elde edilen sonuç, tahta formuna iletilir.*/

            formTahta.Show();//her iki formuda gösterir.formTahta.Show();: "tahta" formunu ekranda gösterir.
           formZar.Show(); //formZar.Show();: "zar" formunu ekranda gösterir.

           
            Application.Run();//Tüm formlar kapatılana kadar uygulamayı çalışır durumda tut
        }
    }
}
// kısacası program cs 'te 
                    /* Uygulama çalışır.
                    tahta ve zar adında iki form oluşturulur.
                    zar formunda zar atıldığında tetiklenen bir olay tanımlanır.
                    tahta formu, bu olaya abone olarak zar sonucunu işlemek üzere ayarlanır.
                    İki form da ekrana gösterilir.
                    Kullanıcı her iki formu kapatana kadar uygulama açık kalır.*/
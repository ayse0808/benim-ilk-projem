using System.Drawing;//Renkler ve diğer grafikle ilgili işlemler için kullanılır.

using System.Windows.Forms;/*Windows Forms uygulamalarını oluşturmak için kullanılan bir kütüphane sağlar.
                           Bu kütüphane ile UI (Kullanıcı Arayüzü) elemanları yaratılabilir.*/


namespace PİYON_OYUNU  /*Kodun diğer bölümleri veya başka projelerle karışmaması için bir isim alanı(namespace) tanımlar. 
                         Kod, "PİYON_OYUNU" isim alanı içinde gruplanmıştır.*/
{
    public class tahtaSinif : Button //tahtasinif sınıfının bir button gibi davranabileceği  anlamına gelir 
                                    //ve özelleştirilmiş özelliklere sahip olacaktır
    {
        public int X { get; set; } /*X ve Y: Her bir butonun (veya "tahta parçasının") tahtadaki konumunu 
 belirten iki özellik (property). get; set; sayesinde bu özelliklere hem değer atanabilir,  hem de değerleri alınabilir.*/
        public int Y { get; set; }

        public tahtaSinif(int x, int y) /*Sınıfın yapıcı (constructor) metodu. Bir tahtaSinif nesnesi oluşturulduğunda, 
                  x ve y değerleri parametre olarak verilir ve bu değerler sınıfın X ve Y özelliklerine atanır.*/
        {
            X = x;//X = x; ve Y = y;: Yapıcı metoda verilen parametre değerleri, sınıfın X ve Y özelliklerine atanır.
            Y = y;
            this.FlatStyle = FlatStyle.Flat;/*Butonun görünümünü ayarlayan bir kod.
          FlatStyle.Flat, butona düz ve basit bir görünüm kazandırır (örneğin, 3D veya gölge efektlerini kaldırır).*/


            // tahta boyama
            if ((x + y) % 2 == 0)
                this.BackColor = Color.Gray;
            else
                this.BackColor = Color.Purple;
        }
    }
}
/* kısacası bu sınıf oyun tahtasının karelerini temsil eden butonlar oluşturmak için tasarlanmıştır
  X ve Y değerleriyle konumlandırılır ve otomatik olarak renklendirilir.*/
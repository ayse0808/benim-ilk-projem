using System;/*using Direktifleri: Programın çalışması için gerekli olan kütüphaneleri içe aktarıyoruz.
              System: Temel C# fonksiyonlarını sağlar.*/
using System.Drawing;//System.Drawing: Grafik ve renk işlemleri için kullanılır.
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;//System.Windows.Forms: Form tabanlı bir uygulama geliştirmek için kullanılır
using static System.Net.Mime.MediaTypeNames;

namespace PİYON_OYUNU
{
    public partial class tahta : Form//tahta sınıfı, bir Windows Form sınıfıdır ve uygulamanın kullanıcı arayüzünü temsil eder.
      /* bir sınıfın (veya bir yapının) birden fazla dosyaya bölünmesine izin verir.Bu, özellikle büyük projelerde,
       bir sınıfın farklı bölümlerini ayrı dosyalarda düzenlemek için kullanılır. 
       partial sınıfı bir araya getiren derleyici, bu bölünmüş parçaları tek bir sınıf olarak işler.*/
        {
        private const int rows = 7;//rows ve cols: Tahtanın 7x7 boyutunda olduğunu belirtir.
        private const int cols = 7;
        private const int cellSize = 90; //cellSize: Her bir hücrenin genişliğini ve yüksekliğini piksel olarak tanımlar.
        private bool boardCreated = false;//Tahtanın oluşturulup oluşturulmadığını kontrol eder.

        // 0 = empty, 1 = Red, 2 = Blue
        private int[,] boardState = new int[rows, cols];
        /*Bu iki boyutlu dizi, oyun tahtasının durumunu tutar. Her hücredeki değere göre, o hücrede ne olduğu belirlenir:
          0: Hücre boş (empty).
          1: Hücrede kırmızı piyon (Red) var.
          2: Hücrede mavi piyon (Blue) var.*/


        // currentPlayer: 1 = Red, 2 = Blue
        private int currentPlayer = 1;/*Şu anda hangi oyuncunun sırasının olduğunu belirler.
                           1: Kırmızı oyuncunun sırası.
                           2: Mavi oyuncunun sırası.*/



        /* Seçilen piyonu takip eder.
        Eğer bir oyuncu tahtadaki bir piyona tıklarsa, bu piyon selectedPawn değişkenine atanır.
        Eğer bir piyon seçilmemişse, değeri null olur.*/
        private PictureBox selectedPawn = null;
        private int diceResult = 0;



        /*Oyun tahtasındaki karelerin grafiksel temsillerini saklar.tahtaSinif sınıfı, 
        her bir kareyi temsil eden özel bir buton sınıfıdır.squares dizisi, bu karelerin referanslarını tutar.*/
        private tahtaSinif[,] squares;
        


        public tahta()/*Bu, tahta sınıfının yapıcı metodudur.
        Amacı: Sınıf bir nesne olarak oluşturulduğunda, başlangıç ayarlarını yapmaktır.*/



     {
      InitializeComponent();//InitializeComponent(): Form üzerindeki tüm görsel bileşenleri (butonlar, paneller, vb.) oluşturur ve başlatır.
     }







    
    public void ZarSonucunuIsle(int zarDegeri)//Bu metot, zar formundan gelen zar sonucunu işler.
                                              //Zar atıldığında bu metot çağrılır ve zarın sonucuna göre işlemler yapılır.
        {
            diceResult = zarDegeri;//Zar formundan gelen sonucu (örneğin, 4) diceResult değişkenine kaydeder.
                                   //Artık bu zar sonucu ile hareket planlanabilir.

           // Zar = 6 ise => dönüşü atla(Çapraz)
            if (diceResult == 1)
            {
                // "Çapraz (X) => dönüşü atla"
                MessageBox.Show("Çapraz (X): Dönüş atlandı. Oyuncu değiştiriliyor...");
                TogglePlayer(); //Bu, sırasını değiştirmek (oyuncuyu değiştirmek) için kullanılan bir metot çağrısıdır.
                                //Oyunda şu anki oyuncuyu bir sonraki oyuncuya geçirmek amacıyla yazılmıştır.
                  return;
            }

            // Hiçbir piyon seçilmemişse kullanıcıya sor
            if (selectedPawn == null)
            {
                MessageBox.Show($"Dice={diceResult}. Lütfen önce piyonlarınızdan birini tıklayın/seçin (Player={currentPlayer}).");
            }
            else
            {
                // Zar bilindikten sonra hamle yapılmaya çalışılıyor
                AttemptMove();//Bu metot, seçilen piyonu zar sonucuna göre hareket ettirmeye çalışır.
    //Oyuncu bir piyon seçtikten ve zar attıktan sonra, taşın geçerli bir hamle yapıp yapamayacağını kontrol eder ve hamleyi gerçekleştirir.


            }
        }

        
        private void btnBasla_Click(object sender, EventArgs e)
        {
            if (!boardCreated)//boardCreated bir boolean değişkendir ve tahtanın daha önce oluşturulup oluşturulmadığını takip eder.
            {
                CreateBoard();//Bu metot, oyun tahtasını oluşturur (örneğin: kare panelleri çizmek, grid sistemini kurmak).
                PlacePawns();//Bu metot, piyonları oyunun başlangıç pozisyonlarına yerleştirir.
                boardCreated = true;//Bu satır, tahtanın oluşturulduğunu belirtir
            }
            else
            {
                // Sıfırlamak istiyorsanız şunları yapmanız yeterlidir:
                // pnlOyunAlani.Controls.Clear();
                // boardCreated = false;
                // currentPlayer = 1;
                // selectedPawn = null;
                // Array.Clear(boardState, 0, boardState.Length);
                // CreateBoard();
                // PlacePawns();
            }
        }

      


        private void CreateBoard()
        {
            pnlOyunAlani.Controls.Clear();//Controls.Clear() ile panelin içindeki tüm alt kontroller temizlenir.
                                         //  Örneğin, daha önce oluşturulmuş kare hücreler varsa, bunlar silinir.
            squares = new tahtaSinif[rows, cols];//squares, tahtadaki kare hücrelerin referanslarını tutan bir 2 boyutlu dizi (matris) tanımlar
            //Her bir hücre, tahtaSinif türünde bir nesnedir.
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tahtaSinif cell = new tahtaSinif(j, i)////Bir tahtaSinif nesnesi (cell) oluşturulur. j(sütun) ve i(satır) bilgisi,bu hücrenin tahtadaki yerini belirtir.
                    {
                        Width = cellSize,//Hücrenin genişliği (Width) ve yüksekliği (Height) ayarlanır.
                                         // cellSize, her bir hücrenin piksel cinsinden boyutunu ifade eder
                        Height = cellSize,
                        Left = j * cellSize,//Hücrenin sol kenarının, sütun numarasına göre yatay konumu ayarlanır.
                                            // Örneğin,3.sütundaki hücrenin sol kenarı,3 * cellSize piksel olacaktır.
                        Top = (rows - 1 - i) * cellSize  //row=0'ı görsel olarak en alta yerleştirmek için
                        //(rows - 1 - i) ifadesi, satır numarasını tersine çevirir.Örneğin,en üst satırın 0.satır yerine en altta görünmesi sağlanır.
                    };
                    pnlOyunAlani.Controls.Add(cell);//Oluşturulan hücre (cell), oyun alanı paneline eklenir.
                    squares[i, j] = cell;//Bu hücre, squares matrisinde uygun konuma kaydedilir.Bu sayede her hücreye daha sonra erişilebilir.
                }
            }

            // oyun alanını temizle
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    boardState[r, c] = 0;//tahtadaki her bir karenin durumunu tutan bir 2 boyutlu dizidir.
                                         //r, satır indeksini; c, sütun indeksini temsil eder.
                }
            }

            currentPlayer = 1;  /*currentPlayer, hangi oyuncunun sırasının olduğunu tutar.
            1, kırmızı oyuncuyu temsil eder(varsayılan başlangıç durumu).Oyunun başında kırmızı oyuncu başlar.*/

            selectedPawn = null;/*seçili olan taşı temsil eder.Oyunun başında henüz hiçbir taş seçilmediği için bu değer null 
                olarak ayarlanır.Kullanıcı bir taş seçtiğinde bu değişken, ilgili taşı temsil edecek şekilde güncellenir.*/
        }
       



        private void PlacePawns()
        {
            int pawnSize = 70; /*Piyonların boyutunu belirler. cellSize(karenin boyutu) ile karşılaştırıldığında
                               biraz daha küçük ayarlanır Bu, piyonun karenin içinde ortalanmasını sağlar.*/

            for (int col = 0; col < cols; col++)//Tüm sütunlar için bir döngü başlatır.col, sütun numarasını temsil eder.Döngü her bir sütun için piyon ekler.

            {
                var piyon = new PictureBox//Yeni bir PictureBox nesnesi oluşturur. Bu nesne, tahtadaki piyonun görsel temsilidir.
                {
                    Width = pawnSize,
                    Height = pawnSize,
                    Left = col * cellSize + (cellSize - pawnSize) / 2,//col * cellSize: Piyonun sütundaki başlangıç konumu.
                              // (cellSize - pawnSize) / 2: Piyonun kare içinde ortalanması için kenar boşluğu.

                    Top = (rows - 1 - 0) * cellSize + (cellSize - pawnSize) / 2,//rows - 1 - 0: En üst satırın sırasını bulur.
                   // (rows - 1)->Alt satırın pozisyonunu yukarıya doğru çevirir.(cellSize - pawnSize) / 2: Piyonun kare içinde ortalanması için kenar boşluğu.

                    Image = Properties.Resources.PİYON1, 
                    SizeMode = PictureBoxSizeMode.StretchImage,//PictureBoxSizeMode.StretchImage: Resim, PictureBox boyutuna uyacak şekilde esnetilir.
                    BackColor = Color.Transparent,
                    Tag = $"0-{col}"  //Tag: Ek bilgi saklar."0-{col}": Piyonun satır-sütun konumunu belirler.Örneğin, sütun 3 için "0-3".
                };
                piyon.Click += PawnClicked;/*piyon.Click += PawnClicked;Bu satır, piyon nesnesine bir Click(Tıklama) olayı ekler.
Kullanıcı bir piyona tıkladığında, PawnClicked adlı metot çalıştırılır.PawnClicked, seçilen piyonu algılar ve hareket işlemlerini
başlatır.Eğer oyuncu bir taş seçmek isterse, bu işlem hangi taşın seçildiğini belirler.*/
                pnlOyunAlani.Controls.Add(piyon);/*pnlOyunAlani, oyun tahtasını temsil eden bir paneldir.Controls.Add(piyon), oluşturulan piyon nesnesini bu panelin içine ekler.
                             Yani, görsel olarak piyon oyun alanında görünür hale gelir.*/
                piyon.BringToFront();/*piyon.BringToFront();Piyonu diğer görsellerin önüne getirir.
 Eğer tahtadaki diğer kareler veya görsel elemanlar piyonun üstünde görünüyorsa, bu satır sayesinde piyon en üstkatmana taşınır.*/
                boardState[0, col] = 1; // boardState[0, col] = 1;, piyonun mantıksal olarak (satır 0, sütun col) konumuna yerleştirildiğini belirtir.
            }



            
            for (int col = 0; col < cols; col++)
            {
                var piyon = new PictureBox
                {
                    Width = pawnSize,
                    Height = pawnSize,
                    Left = col * cellSize + (cellSize - pawnSize) / 2,
                    Top = (rows - 1 - 6) * cellSize + (cellSize - pawnSize) / 2,
                    Image = Properties.Resources.PİYON2, 
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent,
                    Tag = $"6-{col}"  
                };
                piyon.Click += PawnClicked;
                pnlOyunAlani.Controls.Add(piyon);
                piyon.BringToFront();
                boardState[6, col] = 2; 
            }
        }



        
        private void PawnClicked(object sender, EventArgs e)
        {
            if (selectedPawn != null)
            {
                // eskinin seçimini kaldır
                selectedPawn.BackColor = Color.Transparent;/*Eğer bir piyona zaten tıklanıp seçim yapıldıysa (yani selectedPawn null değilse), önceki seçili piyonun seçimini iptal etmek için bir işlem yapılır.
             Bu durumda eski seçili piyona BackColor = Transparent(şeffaf) uygulanır, böylece eski seçilen piyon görünür olmaktan çıkar.*/
            }

            PictureBox clickedPawn = sender as PictureBox;/*Bu satırda, sender nesnesi (yani, tıklanan piyon) PictureBox türüne dönüştürülür.
sender: Bu olayın tetiklenmesine neden olan nesneyi belirtir, yani tıklanan piyonu temsil eder.
as: Bu anahtar kelime, sender nesnesini PictureBox türüne dönüştürmeye çalışır. Eğer başarılı olursa, clickedPawn olarak kaydedilir.
Tıklanan piyonun bir PictureBox olup olmadığını kontrol eder ve sonrasında işlemler için onu kullanır.*/

            if (clickedPawn == null) return; /*Eğer clickedPawn null ise (yani tıklanan nesne bir PictureBox değilse), fonksiyon hiçbir işlem yapmadan geri döner.
Tıklanan nesne beklenen türde PictureBox değilse, bu durumda işlem yapmamayı sağlar.*/


            string tag = clickedPawn.Tag.ToString(); /*clickedPawn.Tag, piyonun satır ve sütun bilgisini tutan özel bir etikettir. Bu etiketin değeri, "row-col" biçimindedir. Örneğin, "0-2" gibi.
ToString() ile bu etiketi bir dizi string'e dönüştürürüz.Piyonun hangi satır ve sütunda olduğunu belirlemek için kullanılır.*/
            int row = int.Parse(tag.Split('-')[0]);//Tıklanan piyonun satır bilgisini alır.
            int col = int.Parse(tag.Split('-')[1]);//tıklanan piyonun sütunun bilgisini alır

            if (boardState[row, col] == currentPlayer)/*Bu satır, piyonun bulunduğu boardState dizisinde belirtilen satır ve sütun konumundaki değeri kontrol eder.
currentPlayer, şu anki oyuncuyu temsil eder.Eğer boardState[row, col] == currentPlayer, bu piyon şu anki oyuncuya aitse, seçilebilir.
Eğer tıklanan piyon, şu anki oyuncuya aitse, piyon seçilir ve işlem yapılır.*/

            {
                //geçerli oyuncuya ait
                selectedPawn = clickedPawn;//selectedPawn, şu anda seçili olan piyonu tutar.
                selectedPawn.BackColor = Color.Yellow;/*eçilen piyon BackColor özelliği Yellow olarak ayarlanır.
                Bu, piyonun arka plan rengini sarı yaparak, kullanıcının hangi piyonun seçili olduğunu görsel olarak belirtir.*/
            }
            else
            {
                // senin piyonun değil
                MessageBox.Show("Rakibin piyonunu seçemezsiniz!");
                selectedPawn = null;
            }
        }

        // 
        //Bir zarSonucu + seçilmiş bir piyon elde ettikten sonra hamle yapmayı dene
        // 
        private void AttemptMove()
        {
            if (selectedPawn == null) return;//Eğer seçili bir piyon yoksa, fonksiyon çalışmayı durdurur ve hiçbir işlem yapılmaz.

            // geçerli konumu çıkar
            string tag = selectedPawn.Tag.ToString();//Seçili piyonu temsil eden etiketi almak, piyonu temsil eden satır ve sütun bilgilerini çözümlemek için gereklidir.
            int currentRow = int.Parse(tag.Split('-')[0]);//Seçili piyonun satır bilgisini almak için kullanılır. Bu bilgi, piyonun başlangıçta bulunduğu yeri belirlemek için gereklidir.
            int currentCol = int.Parse(tag.Split('-')[1]);// seçili piyonun sütun bilgisini almak için kullanılır

            int newRow = currentRow;/*Bu adımda, yeni satırın başlangıçta mevcut satırla aynı olduğu kabul edilir. 
     Piyon hareket etmeye başlamadan önce yeni konumu belirlenir. Ancak bu değer ilerleyen kodda hareket sırasında değişebilir.*/
            int newCol = currentCol;//Yeni sütun da başlangıçta mevcut sütunla aynı olur. Bu değer, piyonun hareketine göre değişecektir.

            // zarları çevir hareket
            // İlham kodunuz”da, zar=1 => oyuncu1 için +1 satır, vb.
            //  Ancak bunu istediğiniz "Çapraz=1 => atla" yaklaşımına uyarlayacağız.
            // Aslında ZarSonucunuIsle'da 1 => skip kullandık, o halde 2..6'yı burada eşleyelim:



            switch (diceResult)
            {
                case 2: /*Eğer zar sonucu 2 ise, piyon bir adım ileri gidecek.
             newRow += (currentPlayer == 1) ? 1 : -1;
             currentPlayer == 1, şu anki oyuncunun Kırmızı(1) olup olmadığını kontrol eder. Eğer oyuncu Kırmızı ise, piyon 1 adım yukarı gider(newRow += 1), aksi takdirde Mavi oyuncusu için piyon 1 adım aşağı gider(newRow -= 1).
             Zar sonucu 2 ise, piyon bir adım hareket eder.Kırmızı oyuncu yukarı, mavi oyuncu aşağı hareket eder.*/
                    newRow += (currentPlayer == 1) ? 1 : -1;
                    break;
                case 3: // 2 adım ileri
                    newRow += (currentPlayer == 1) ? 2 : -2;
                    break;
                case 4: // 1 adım diyagonal
                    newRow += (currentPlayer == 1) ? 1 : -1;
                    // örnek olarak sol veya sağı seçin (eşitlik veya başka bir şey kullanın)
                    newCol += (currentCol % 2 == 0) ? 1 : -1;
                    break;
                case 5: // 2 adım diyagonal
                    newRow += (currentPlayer == 1) ? 2 : -2;
                    newCol += (currentCol % 2 == 0) ? 2 : -2;
                    break;
                case 6: // L-shape => 2 forward + 1 side
                    newRow += (currentPlayer == 1) ? 2 : -2;
                    newCol += 1; // veya isterseniz -1
                    break;
            }

            if (IsValidMove(currentRow, currentCol, newRow, newCol))//Eğer hareket geçerli ise, MovePawn fonksiyonu çağrılır. Bu fonksiyon, piyonu eski konumdan yeni konuma taşır.
                //Burada, piyonu başlangıç satır ve sütunundan yeni satır ve sütuna taşıyan işlemler yapılır.

            {
                MovePawn(currentRow, currentCol, newRow, newCol);
            }
            else
            {
                MessageBox.Show("GEÇERSİZ HAREKET!");
            }
        }
private bool IsValidMove(int r0, int c0, int r1, int c1)/*IsValidMove fonksiyonu, dört parametre alır:
    r0, c0: Taşın mevcut(başlangıç) satır ve sütununu belirtir.r1, c1: Taşın hedef (yeni) satır ve sütununu belirtir.
Bu fonksiyon, taşın başlangıç konumundan hedef konumuna geçişinin geçerli olup olmadığını kontrol eder ve bu kontrolü sağlayan bir bool (doğru/yanlış) değer döndürür.*/
        {
            // TAHTA SINIRLARI
            if (r1 < 0 || r1 >= rows || c1 < 0 || c1 >= cols)/*Bu satır, taşın hedef konumunun tahta sınırları içinde olup olmadığını kontrol eder. Eğer hedef konum tahtanın dışındaysa geçersiz kabul edilir.
     r1 < 0 || r1 >= rows: Hedef satır, 0'dan küçük veya tahtadaki satır sayısı (rows) kadar büyükse geçersizdir.
     c1 < 0 || c1 >= cols: Hedef sütun, 0'dan küçük veya tahtadaki sütun sayısı (cols) kadar büyükse geçersizdir.
     Eğer herhangi bir sınır ihlali varsa, fonksiyon false döndürür.*/

                return false;

            // Oraya taşınmak için boş olmalı
            if (boardState[r1, c1] != 0)
                return false;

            // Temel ileri mantık: Kırmızı aşağı doğru hareket edemez, Mavi yukarı doğru hareket edemez
            // (eğer kesinlikle ileri gitmek istiyorsan)
            if (currentPlayer == 1 && r1 <= r0) return false;
            if (currentPlayer == 2 && r1 >= r0) return false;

            // Aksi takdirde, zar tabanlı harekete güvenin veya isterseniz daha fazla kontrol ekleyin
            return true;
        }




        //
        // Gerçek taşıma işlemini gerçekleştirin => boardState'i güncelleyin, PictureBox'ı taşıyın
        // 
        private void MovePawn(int oldRow, int oldCol, int newRow, int newCol)
        {
            // 1) tahta durumunu güncelle
            boardState[newRow, newCol] = currentPlayer;//boardState[newRow, newCol] = currentPlayer; satırı, yeni konumda (yeni satır ve sütun) piyonun oyun sırasında hangi oyuncuya ait olduğunu belirtiyor.
           
            boardState[oldRow, oldCol] = 0; // boardState[oldRow, oldCol] = 0; satırı ise, eski konumdaki piyonun yerini boş(0) olarak işaretliyor.

            // 2) picturboxu taşımak
            /*selectedPawn.Top ve selectedPawn.Left, PictureBox'ın yeni koordinatlarını hesaplamak için kullanılıyor. Bu hesaplamalar, görsel olarak piyonun doğru şekilde taşınmasını sağlıyor.
selectedPawn.Tag = $"{newRow}-{newCol}"; satırı, piyonun konumunu güncelliyor, yani Tag özelliği, piyonu tanımlayan satır ve sütun bilgilerini içeriyor.
selectedPawn.BackColor = Color.Transparent; satırı ise, seçili piyonun arka plan rengini şeffaf yaparak, görselde bir değişiklik olmadan hareket etmesini sağlıyor.*/
            selectedPawn.Top = (rows - 1 - newRow) * cellSize + (cellSize - selectedPawn.Height) / 2;
            selectedPawn.Left = newCol * cellSize + (cellSize - selectedPawn.Width) / 2;
            selectedPawn.Tag = $"{newRow}-{newCol}";
            selectedPawn.BackColor = Color.Transparent;

            // anında kazanmayı kontrol edin
            CheckForWin(newRow);

            // oyuncuyu değiştir
            TogglePlayer();
            selectedPawn = null; // seçimleri sıfırla
            diceResult = 0; /*zarları sıfırlaselectedPawn = null; satırı, seçili piyon bilgisini sıfırlıyor. Böylece bir sonraki hamlede yeni bir piyon seçilebilir.
            diceResult = 0; satırı ise zar sonucunu sıfırlıyor.Bu, zarın yeniden atılması gerektiği anlamına gelir.*/
        }



        // 
        // If Red reaches row=6 or Blue reaches row=0 => KAZANDI
        // 
        private void CheckForWin(int newRow)/*Bu fonksiyon, bir oyuncunun kazanıp kazanmadığını kontrol etmek için yazılmıştır.
 Hedef, her oyuncunun taşının belirli bir satıra ulaşmasını sağlamak ve o zaman kazanıp kazanmadığını görmek.
  currentPlayer == 1: Şu anki oyuncunun Kırmızı oyuncu(Player 1) olup olmadığını kontrol eder.newRow == 6: Eğer Kırmızı oyuncu ve taşın yeni satırı 6'ya (oyunun sonuna) ulaştıysa, bu durumda Kırmızı oyuncu kazanmış demektir.*/
            {
                if (currentPlayer == 1 && newRow == 6)
            {
                MessageBox.Show("KIRMIZI (OYUNCU 1) KAZANDI!");
                // SIFIRLAYABİLİR VEYA KAPATABİLİRSİNİZ
            }
            else if (currentPlayer == 2 && newRow == 0)
            {
                MessageBox.Show("MAVİ (OYUNCU 2) KAZANDI!");
                // SIFIRLAYABİLİR VEYA KAPATABİLİRSİNİZ
            }
        }

        // -------------------------------------------------------------------------
        private void TogglePlayer()//Bu satır fonksiyonun başını belirtir. TogglePlayer fonksiyonu, oyuncuların sırayla oynayabilmesi için her çağrıldığında şu anki oyuncuyu değiştirir.
        {
            currentPlayer = (currentPlayer == 1) ? 2 : 1;/* currentPlayer == 1: Bu ifade, şu anki oyuncunun Kırmızı oyuncu (Player 1) olup olmadığını kontrol eder.
? 2 : 1: Bu kısım bir ternary operator (üçlü operatör) kullanarak oyuncu geçişini yapar.
Eğer şu anki oyuncu Kırmızı (Player 1) ise, bu durumda Mavi (Player 2)'ye geçilecektir (yani currentPlayer 2 olur).
Eğer şu anki oyuncu Mavi (Player 2) ise, bu durumda Kırmızı (Player 1)'e geçilecektir (yani currentPlayer 1 olur).
Bu satırın amacı, sıradaki oyuncuyu değiştirmektir. Bu geçiş, her iki oyuncunun sırayla oynayabilmesi için gereklidir.*/

            MessageBox.Show($"Şimdi Oyuncu {currentPlayer} sırası.");

        }
    }
}

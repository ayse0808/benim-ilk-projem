using PİYON_OYUNU.Properties;
using System;
using System.Windows.Forms;

namespace PİYON_OYUNU
{
    public partial class zar : Form
    {
        //Zarları birkaç kez “döndüreceğiz”, ardından son bir yüz seçeceğiz [1..6].
        private Random rng = new Random();
        private int diceRollCount = 0;
        private int finalDiceValue = 0;

        // Son zar değerini tahta formuna bildiriyoruz
        public event Action<int> ZarSonucuBildir;

        public zar()
        {
            InitializeComponent();
        }

        private void btnfırlat_Click(object sender, EventArgs e)
        {
            diceRollCount = 0;
            tmrzaman.Enabled = true; // animasyona başla
        }

        private void tmrzaman_Tick(object sender, EventArgs e)
        {
            if (diceRollCount >= 8)
            {
                // yuvarlama tamamlandı
                tmrzaman.Enabled = false;
                ShowDiceResult(finalDiceValue);
                ZarSonucuBildir?.Invoke(finalDiceValue);
            }
            else
            {
                // rastgele zar yüzünü canlandırma
                finalDiceValue = rng.Next(1, 7); // [1..6]
                ShowDiceFace(finalDiceValue);
                diceRollCount++;
            }
        }

        private void ShowDiceFace(int value)
        {
            //Her yüzü bir kaynak görüntüsüyle eşleyin. Adları gerektiği gibi ayarlayın.
            switch (value)
            {
                case 1:
                    pbxzar.Image = Resources.paint3; 
                    break;
                case 2:
                    pbxzar.Image = Resources.paint1; 
                    break;
                case 3:
                    pbxzar.Image = Resources.paint2;
                    break;
                case 4:
                    pbxzar.Image = Resources.paint5;
                    break;
                case 5:
                    pbxzar.Image = Resources.paint6;
                    break;
                case 6:
                    pbxzar.Image = Resources.paint4;
                    break;
            }
        }

        private void ShowDiceResult(int value)
        {
            
            switch (value)
            {
                case 1:
                    MessageBox.Show("zar=1 => çapraz veya dönüşü atla.");
                    break;
                case 2:
                    MessageBox.Show("zar=2 =>  1 adım ileri.");
                    break;
                case 3:
                    MessageBox.Show("zar=3 =>  2 adım ileri");
                    break;
                case 4:
                    MessageBox.Show("zar=4 => 1 adım çapraz.");
                    break;
                case 5:
                    MessageBox.Show("zar=5 => 2 adım çapraz.");
                    break;
                case 6:
                    MessageBox.Show("zar=6 => L gidin");
                    break;
            }
        }
    }
}

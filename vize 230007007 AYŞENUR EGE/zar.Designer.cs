namespace PİYON_OYUNU
{
    partial class zar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pbxzar;
        private System.Windows.Forms.Button btnfırlat;
        private System.Windows.Forms.Timer tmrzaman;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pbxzar = new System.Windows.Forms.PictureBox();
            this.btnfırlat = new System.Windows.Forms.Button();
            this.tmrzaman = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbxzar)).BeginInit();
            this.SuspendLayout();
            // 
            // pbxzar
            // 
            this.pbxzar.Location = new System.Drawing.Point(30, 30);
            this.pbxzar.Name = "pbxzar";
            this.pbxzar.Size = new System.Drawing.Size(200, 200);
            this.pbxzar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxzar.TabIndex = 0;
            this.pbxzar.TabStop = false;
            // 
            // btnfırlat
            // 
            this.btnfırlat.Location = new System.Drawing.Point(60, 250);
            this.btnfırlat.Name = "btnfırlat";
            this.btnfırlat.Size = new System.Drawing.Size(140, 35);
            this.btnfırlat.TabIndex = 1;
            this.btnfırlat.Text = "FIRLAT!";
            this.btnfırlat.UseVisualStyleBackColor = true;
            this.btnfırlat.Click += new System.EventHandler(this.btnfırlat_Click);
            // 
            // tmrzaman
            // 
            this.tmrzaman.Interval = 150;
            this.tmrzaman.Tick += new System.EventHandler(this.tmrzaman_Tick);
            // 
            // zar
            // 
            this.ClientSize = new System.Drawing.Size(400, 320);
            this.Controls.Add(this.btnfırlat);
            this.Controls.Add(this.pbxzar);
            this.Name = "zar";
            this.Text = "Zar Atma";
            ((System.ComponentModel.ISupportInitialize)(this.pbxzar)).EndInit();
            this.ResumeLayout(false);

        }
    }
}

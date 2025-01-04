namespace PİYON_OYUNU
{
    partial class tahta
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlOyunAlani;
        private System.Windows.Forms.Button btnBasla;

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
            this.pnlOyunAlani = new System.Windows.Forms.Panel();
            this.btnBasla = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlOyunAlani
            // 
            this.pnlOyunAlani.Location = new System.Drawing.Point(12, 12);
            this.pnlOyunAlani.Name = "pnlOyunAlani";
            this.pnlOyunAlani.Size = new System.Drawing.Size(630, 630);
            this.pnlOyunAlani.TabIndex = 0;
            // 
            // btnBasla
            // 
            this.btnBasla.Location = new System.Drawing.Point(660, 50);
            this.btnBasla.Name = "btnBasla";
            this.btnBasla.Size = new System.Drawing.Size(96, 34);
            this.btnBasla.TabIndex = 1;
            this.btnBasla.Text = "Başla";
            this.btnBasla.UseVisualStyleBackColor = true;
            this.btnBasla.Click += new System.EventHandler(this.btnBasla_Click);
            // 
            // tahta
            // 
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.btnBasla);
            this.Controls.Add(this.pnlOyunAlani);
            this.Name = "tahta";
            this.Text = "Piyon Oyunu - Tahta";
            this.ResumeLayout(false);

        }
    }
}

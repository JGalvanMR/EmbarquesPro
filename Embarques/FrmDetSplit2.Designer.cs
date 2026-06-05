namespace Embarques
{
    partial class FrmDetSplit2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetSplit2));
            this.Logo = new System.Windows.Forms.PictureBox();
            this.LblNoSplit = new System.Windows.Forms.Label();
            this.DGSplit = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblPed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.FOLIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cajas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DGDet = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDet)).BeginInit();
            this.SuspendLayout();
            // 
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(16, 18);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(89, 89);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 31;
            this.Logo.TabStop = false;
            // 
            // LblNoSplit
            // 
            this.LblNoSplit.AutoSize = true;
            this.LblNoSplit.BackColor = System.Drawing.Color.Transparent;
            this.LblNoSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNoSplit.ForeColor = System.Drawing.Color.Aqua;
            this.LblNoSplit.Location = new System.Drawing.Point(186, 99);
            this.LblNoSplit.Name = "LblNoSplit";
            this.LblNoSplit.Size = new System.Drawing.Size(277, 24);
            this.LblNoSplit.TabIndex = 28;
            this.LblNoSplit.Text = "NO. DE SPLIT GENERADOS";
            // 
            // DGSplit
            // 
            this.DGSplit.AllowUserToAddRows = false;
            this.DGSplit.AllowUserToDeleteRows = false;
            this.DGSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSplit.Location = new System.Drawing.Point(12, 127);
            this.DGSplit.Name = "DGSplit";
            this.DGSplit.ReadOnly = true;
            this.DGSplit.Size = new System.Drawing.Size(588, 486);
            this.DGSplit.TabIndex = 26;
            this.DGSplit.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGSplit_CellMouseClick);
            this.DGSplit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DGSplit_KeyUp);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.LblPed);
            this.panel1.Location = new System.Drawing.Point(190, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 37);
            this.panel1.TabIndex = 32;
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(1, 6);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(241, 20);
            this.LblPed.TabIndex = 24;
            this.LblPed.Text = "NO. DE SPLIT GENERADOS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(788, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 24);
            this.label1.TabIndex = 29;
            this.label1.Text = "PRODUCTOS DEL SPLIT";
            // 
            // FOLIO
            // 
            this.FOLIO.HeaderText = "Folio";
            this.FOLIO.Name = "FOLIO";
            this.FOLIO.ReadOnly = true;
            this.FOLIO.Width = 70;
            // 
            // Tiempo
            // 
            this.Tiempo.HeaderText = "Tiempo";
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.ReadOnly = true;
            this.Tiempo.Width = 70;
            // 
            // Cajas
            // 
            this.Cajas.HeaderText = "Cajas";
            this.Cajas.Name = "Cajas";
            this.Cajas.ReadOnly = true;
            this.Cajas.Width = 40;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Producto";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 330;
            // 
            // DGDet
            // 
            this.DGDet.AllowUserToAddRows = false;
            this.DGDet.AllowUserToDeleteRows = false;
            this.DGDet.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DGDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Cajas,
            this.Tiempo,
            this.FOLIO});
            this.DGDet.Location = new System.Drawing.Point(606, 127);
            this.DGDet.Name = "DGDet";
            this.DGDet.ReadOnly = true;
            this.DGDet.RowHeadersVisible = false;
            this.DGDet.Size = new System.Drawing.Size(518, 486);
            this.DGDet.TabIndex = 27;
            // 
            // FrmDetSplit2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 625);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblNoSplit);
            this.Controls.Add(this.DGDet);
            this.Controls.Add(this.DGSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDetSplit2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDetSplit2";
            this.Load += new System.EventHandler(this.FrmDetSplit2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label LblNoSplit;
        private System.Windows.Forms.DataGridView DGSplit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FOLIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiempo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cajas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridView DGDet;

    }
}
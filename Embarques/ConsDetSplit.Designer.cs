namespace Embarques
{
    partial class ConsDetSplit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsDetSplit));
            this.DGSplit = new System.Windows.Forms.DataGridView();
            this.DGDet = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cajas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tiempo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LblNoSplit = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblPed = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.lblresponsable = new System.Windows.Forms.Label();
            this.respimp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDet)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // DGSplit
            // 
            this.DGSplit.AllowUserToAddRows = false;
            this.DGSplit.AllowUserToDeleteRows = false;
            this.DGSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSplit.Location = new System.Drawing.Point(12, 121);
            this.DGSplit.Name = "DGSplit";
            this.DGSplit.ReadOnly = true;
            this.DGSplit.Size = new System.Drawing.Size(469, 486);
            this.DGSplit.TabIndex = 0;
            this.DGSplit.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGSplit_CellMouseClick);
            this.DGSplit.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DGSplit_KeyUp);
            // 
            // DGDet
            // 
            this.DGDet.AllowUserToAddRows = false;
            this.DGDet.AllowUserToDeleteRows = false;
            this.DGDet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Cajas,
            this.Tiempo});
            this.DGDet.Location = new System.Drawing.Point(519, 120);
            this.DGDet.Name = "DGDet";
            this.DGDet.ReadOnly = true;
            this.DGDet.Size = new System.Drawing.Size(497, 486);
            this.DGDet.TabIndex = 1;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Producto";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 350;
            // 
            // Cajas
            // 
            this.Cajas.HeaderText = "Cajas";
            this.Cajas.Name = "Cajas";
            this.Cajas.ReadOnly = true;
            this.Cajas.Width = 40;
            // 
            // Tiempo
            // 
            this.Tiempo.HeaderText = "Tiempo";
            this.Tiempo.Name = "Tiempo";
            this.Tiempo.ReadOnly = true;
            this.Tiempo.Width = 70;
            // 
            // LblNoSplit
            // 
            this.LblNoSplit.AutoSize = true;
            this.LblNoSplit.BackColor = System.Drawing.Color.Transparent;
            this.LblNoSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNoSplit.ForeColor = System.Drawing.Color.Aqua;
            this.LblNoSplit.Location = new System.Drawing.Point(186, 93);
            this.LblNoSplit.Name = "LblNoSplit";
            this.LblNoSplit.Size = new System.Drawing.Size(277, 24);
            this.LblNoSplit.TabIndex = 21;
            this.LblNoSplit.Text = "NO. DE SPLIT GENERADOS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(620, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 24);
            this.label1.TabIndex = 22;
            this.label1.Text = "PRODUCTOS DEL SPLIT";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.LblPed);
            this.panel1.Location = new System.Drawing.Point(219, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(611, 37);
            this.panel1.TabIndex = 24;
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
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(16, 12);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(89, 89);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 25;
            this.Logo.TabStop = false;
            // 
            // lblresponsable
            // 
            this.lblresponsable.AutoSize = true;
            this.lblresponsable.BackColor = System.Drawing.Color.Transparent;
            this.lblresponsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblresponsable.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblresponsable.Location = new System.Drawing.Point(659, 62);
            this.lblresponsable.Name = "lblresponsable";
            this.lblresponsable.Size = new System.Drawing.Size(241, 20);
            this.lblresponsable.TabIndex = 25;
            this.lblresponsable.Text = "NO. DE SPLIT GENERADOS";
            // 
            // respimp
            // 
            this.respimp.AutoSize = true;
            this.respimp.BackColor = System.Drawing.Color.Transparent;
            this.respimp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respimp.ForeColor = System.Drawing.Color.Red;
            this.respimp.Location = new System.Drawing.Point(158, 62);
            this.respimp.Name = "respimp";
            this.respimp.Size = new System.Drawing.Size(241, 20);
            this.respimp.TabIndex = 25;
            this.respimp.Text = "NO. DE SPLIT GENERADOS";
            // 
            // ConsDetSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 628);
            this.Controls.Add(this.respimp);
            this.Controls.Add(this.lblresponsable);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblNoSplit);
            this.Controls.Add(this.DGDet);
            this.Controls.Add(this.DGSplit);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsDetSplit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONTROL DE TIEMPOS DE EMBARQUES POR SPLIT";
            this.Load += new System.EventHandler(this.ConsDetSplit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDet)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGSplit;
        private System.Windows.Forms.DataGridView DGDet;
        private System.Windows.Forms.Label LblNoSplit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cajas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tiempo;
        private System.Windows.Forms.Label lblresponsable;
        private System.Windows.Forms.Label respimp;
    }
}
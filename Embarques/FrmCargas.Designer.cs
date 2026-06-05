namespace Embarques
{
    partial class FrmCargas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCargas));
            this.Logo = new System.Windows.Forms.PictureBox();
            this.LblFec = new System.Windows.Forms.Label();
            this.DGDatos = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(7, 7);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(82, 86);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 29;
            this.Logo.TabStop = false;
            // 
            // LblFec
            // 
            this.LblFec.AutoSize = true;
            this.LblFec.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblFec.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblFec.Location = new System.Drawing.Point(120, 67);
            this.LblFec.Name = "LblFec";
            this.LblFec.Size = new System.Drawing.Size(99, 26);
            this.LblFec.TabIndex = 31;
            this.LblFec.Text = "FECHA:";
            // 
            // DGDatos
            // 
            this.DGDatos.AllowUserToAddRows = false;
            this.DGDatos.AllowUserToDeleteRows = false;
            this.DGDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDatos.Location = new System.Drawing.Point(4, 103);
            this.DGDatos.Name = "DGDatos";
            this.DGDatos.ReadOnly = true;
            this.DGDatos.RowHeadersVisible = false;
            this.DGDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDatos.Size = new System.Drawing.Size(980, 517);
            this.DGDatos.TabIndex = 32;
            this.DGDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDatos_CellDoubleClick);
            // 
            // FrmCargas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 627);
            this.Controls.Add(this.DGDatos);
            this.Controls.Add(this.LblFec);
            this.Controls.Add(this.Logo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCargas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE DE CARGAS";
            this.Load += new System.EventHandler(this.FrmCargas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label LblFec;
        private System.Windows.Forms.DataGridView DGDatos;
    }
}
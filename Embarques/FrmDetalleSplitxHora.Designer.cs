namespace Embarques
{
    partial class FrmDetalleSplitxHora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDetalleSplitxHora));
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblPed = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LblNoSplit = new System.Windows.Forms.Label();
            this.DGSplit = new System.Windows.Forms.DataGridView();
            this.responsable = new System.Windows.Forms.Label();
            this.horaactual = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.LblPed);
            this.panel1.Location = new System.Drawing.Point(190, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(448, 37);
            this.panel1.TabIndex = 38;
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
            this.Logo.Location = new System.Drawing.Point(16, 11);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(89, 89);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 37;
            this.Logo.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(595, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(243, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "PRODUCTOS DEL SPLIT";
            // 
            // LblNoSplit
            // 
            this.LblNoSplit.AutoSize = true;
            this.LblNoSplit.BackColor = System.Drawing.Color.Transparent;
            this.LblNoSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNoSplit.ForeColor = System.Drawing.Color.Aqua;
            this.LblNoSplit.Location = new System.Drawing.Point(186, 92);
            this.LblNoSplit.Name = "LblNoSplit";
            this.LblNoSplit.Size = new System.Drawing.Size(277, 24);
            this.LblNoSplit.TabIndex = 35;
            this.LblNoSplit.Text = "NO. DE SPLIT GENERADOS";
            // 
            // DGSplit
            // 
            this.DGSplit.AllowUserToAddRows = false;
            this.DGSplit.AllowUserToDeleteRows = false;
            this.DGSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSplit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9});
            this.DGSplit.Location = new System.Drawing.Point(12, 120);
            this.DGSplit.Name = "DGSplit";
            this.DGSplit.ReadOnly = true;
            this.DGSplit.Size = new System.Drawing.Size(956, 486);
            this.DGSplit.TabIndex = 33;
            // 
            // responsable
            // 
            this.responsable.AutoSize = true;
            this.responsable.BackColor = System.Drawing.Color.Transparent;
            this.responsable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.responsable.ForeColor = System.Drawing.Color.Red;
            this.responsable.Location = new System.Drawing.Point(691, 9);
            this.responsable.Name = "responsable";
            this.responsable.Size = new System.Drawing.Size(277, 24);
            this.responsable.TabIndex = 35;
            this.responsable.Text = "NO. DE SPLIT GENERADOS";
            // 
            // horaactual
            // 
            this.horaactual.AutoSize = true;
            this.horaactual.BackColor = System.Drawing.Color.Transparent;
            this.horaactual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.horaactual.ForeColor = System.Drawing.Color.Blue;
            this.horaactual.Location = new System.Drawing.Point(691, 33);
            this.horaactual.Name = "horaactual";
            this.horaactual.Size = new System.Drawing.Size(277, 24);
            this.horaactual.TabIndex = 35;
            this.horaactual.Text = "NO. DE SPLIT GENERADOS";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "hora";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "emb-folio";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Recibo";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Producto";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Tarima";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Caja";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Estado";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "No Split";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Verde/Blanca";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // FrmDetalleSplitxHora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 618);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.horaactual);
            this.Controls.Add(this.responsable);
            this.Controls.Add(this.LblNoSplit);
            this.Controls.Add(this.DGSplit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmDetalleSplitxHora";
            this.Text = "FrmDetalleSplitxHora";
            this.Load += new System.EventHandler(this.FrmDetalleSplitxHora_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblNoSplit;
        private System.Windows.Forms.DataGridView DGSplit;
        private System.Windows.Forms.Label responsable;
        private System.Windows.Forms.Label horaactual;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    }
}
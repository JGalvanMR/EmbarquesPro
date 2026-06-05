namespace Embarques
{
    partial class ConsulPedPen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsulPedPen));
            this.TxtTotS = new System.Windows.Forms.TextBox();
            this.TxtTotP = new System.Windows.Forms.TextBox();
            this.LblPed = new System.Windows.Forms.Label();
            this.DGDetPed = new System.Windows.Forms.DataGridView();
            this.CmbPed = new System.Windows.Forms.ComboBox();
            this.LblDetPed = new System.Windows.Forms.Label();
            this.BtnAce = new System.Windows.Forms.Button();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.BtnProdPen = new System.Windows.Forms.Button();
            this.LblProdPen = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtTotS
            // 
            this.TxtTotS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotS.ForeColor = System.Drawing.Color.Red;
            this.TxtTotS.Location = new System.Drawing.Point(739, 65);
            this.TxtTotS.Name = "TxtTotS";
            this.TxtTotS.ReadOnly = true;
            this.TxtTotS.Size = new System.Drawing.Size(83, 26);
            this.TxtTotS.TabIndex = 25;
            this.TxtTotS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTotP
            // 
            this.TxtTotP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotP.ForeColor = System.Drawing.Color.Red;
            this.TxtTotP.Location = new System.Drawing.Point(662, 65);
            this.TxtTotP.Name = "TxtTotP";
            this.TxtTotP.ReadOnly = true;
            this.TxtTotP.Size = new System.Drawing.Size(72, 26);
            this.TxtTotP.TabIndex = 24;
            this.TxtTotP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(132, 3);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(103, 26);
            this.LblPed.TabIndex = 23;
            this.LblPed.Text = "PEDIDO";
            // 
            // DGDetPed
            // 
            this.DGDetPed.AllowUserToAddRows = false;
            this.DGDetPed.AllowUserToDeleteRows = false;
            this.DGDetPed.AllowUserToOrderColumns = true;
            this.DGDetPed.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.DGDetPed.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGDetPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDetPed.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGDetPed.Location = new System.Drawing.Point(7, 96);
            this.DGDetPed.Name = "DGDetPed";
            this.DGDetPed.ReadOnly = true;
            this.DGDetPed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetPed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetPed.Size = new System.Drawing.Size(817, 514);
            this.DGDetPed.TabIndex = 22;
            // 
            // CmbPed
            // 
            this.CmbPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPed.FormattingEnabled = true;
            this.CmbPed.Location = new System.Drawing.Point(525, 7);
            this.CmbPed.Name = "CmbPed";
            this.CmbPed.Size = new System.Drawing.Size(121, 28);
            this.CmbPed.TabIndex = 27;
            // 
            // LblDetPed
            // 
            this.LblDetPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDetPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblDetPed.Location = new System.Drawing.Point(116, 36);
            this.LblDetPed.Name = "LblDetPed";
            this.LblDetPed.Size = new System.Drawing.Size(463, 56);
            this.LblDetPed.TabIndex = 29;
            this.LblDetPed.Text = "PEDIDO";
            // 
            // BtnAce
            // 
            this.BtnAce.Image = global::Embarques.Properties.Resources.CHECKMRK;
            this.BtnAce.Location = new System.Drawing.Point(646, 4);
            this.BtnAce.Name = "BtnAce";
            this.BtnAce.Size = new System.Drawing.Size(35, 33);
            this.BtnAce.TabIndex = 28;
            this.BtnAce.UseVisualStyleBackColor = true;
            this.BtnAce.Click += new System.EventHandler(this.BtnAce_Click);
            // 
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(7, 6);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(82, 86);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 26;
            this.Logo.TabStop = false;
            // 
            // BtnProdPen
            // 
            this.BtnProdPen.Location = new System.Drawing.Point(708, 14);
            this.BtnProdPen.Name = "BtnProdPen";
            this.BtnProdPen.Size = new System.Drawing.Size(116, 23);
            this.BtnProdPen.TabIndex = 33;
            this.BtnProdPen.Text = "PROD PENDIENTE";
            this.BtnProdPen.UseVisualStyleBackColor = true;
            this.BtnProdPen.Click += new System.EventHandler(this.BtnProdPen_Click);
            // 
            // LblProdPen
            // 
            this.LblProdPen.AutoSize = true;
            this.LblProdPen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProdPen.ForeColor = System.Drawing.Color.Red;
            this.LblProdPen.Location = new System.Drawing.Point(249, 68);
            this.LblProdPen.Name = "LblProdPen";
            this.LblProdPen.Size = new System.Drawing.Size(212, 20);
            this.LblProdPen.TabIndex = 34;
            this.LblProdPen.Text = "PRODUCTO PENDIENTE";
            this.LblProdPen.Visible = false;
            // 
            // ConsulPedPen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(833, 616);
            this.Controls.Add(this.LblProdPen);
            this.Controls.Add(this.BtnProdPen);
            this.Controls.Add(this.LblDetPed);
            this.Controls.Add(this.BtnAce);
            this.Controls.Add(this.CmbPed);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.TxtTotS);
            this.Controls.Add(this.TxtTotP);
            this.Controls.Add(this.LblPed);
            this.Controls.Add(this.DGDetPed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsulPedPen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTA DE PRODUCTOS PEDIDO VS EXISTENCIA";
            this.Load += new System.EventHandler(this.ConsulPedPen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.TextBox TxtTotS;
        private System.Windows.Forms.TextBox TxtTotP;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.DataGridView DGDetPed;
        private System.Windows.Forms.Button BtnAce;
        private System.Windows.Forms.ComboBox CmbPed;
        private System.Windows.Forms.Label LblDetPed;
        private System.Windows.Forms.Button BtnProdPen;
        private System.Windows.Forms.Label LblProdPen;
    }
}
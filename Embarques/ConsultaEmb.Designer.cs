namespace Embarques
{
    partial class ConsultaEmb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultaEmb));
            this.DGDetPed = new System.Windows.Forms.DataGridView();
            this.LblPed = new System.Windows.Forms.Label();
            this.TxtTotP = new System.Windows.Forms.TextBox();
            this.TxtTotS = new System.Windows.Forms.TextBox();
            this.LblCliente = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.BtnSplit = new System.Windows.Forms.Button();
            this.BtnImp = new System.Windows.Forms.Button();
            this.LblObs = new System.Windows.Forms.Label();
            this.TxtTotSpl = new System.Windows.Forms.TextBox();
            this.LblTotSpl = new System.Windows.Forms.Label();
            this.PbxQR = new System.Windows.Forms.PictureBox();
            this.btnGPS = new System.Windows.Forms.Button();
            this.LblTar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxQR)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDetPed
            // 
            this.DGDetPed.AllowUserToAddRows = false;
            this.DGDetPed.AllowUserToDeleteRows = false;
            this.DGDetPed.AllowUserToOrderColumns = true;
            this.DGDetPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDetPed.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGDetPed.Location = new System.Drawing.Point(9, 102);
            this.DGDetPed.Name = "DGDetPed";
            this.DGDetPed.ReadOnly = true;
            this.DGDetPed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetPed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetPed.Size = new System.Drawing.Size(961, 514);
            this.DGDetPed.TabIndex = 0;
            this.DGDetPed.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDetPed_CellDoubleClick);
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(118, 71);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(86, 24);
            this.LblPed.TabIndex = 15;
            this.LblPed.Text = "PEDIDO";
            // 
            // TxtTotP
            // 
            this.TxtTotP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotP.ForeColor = System.Drawing.Color.Red;
            this.TxtTotP.Location = new System.Drawing.Point(664, 71);
            this.TxtTotP.Name = "TxtTotP";
            this.TxtTotP.ReadOnly = true;
            this.TxtTotP.Size = new System.Drawing.Size(72, 26);
            this.TxtTotP.TabIndex = 18;
            this.TxtTotP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTotS
            // 
            this.TxtTotS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotS.ForeColor = System.Drawing.Color.Red;
            this.TxtTotS.Location = new System.Drawing.Point(742, 71);
            this.TxtTotS.Name = "TxtTotS";
            this.TxtTotS.ReadOnly = true;
            this.TxtTotS.Size = new System.Drawing.Size(67, 26);
            this.TxtTotS.TabIndex = 19;
            this.TxtTotS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblCliente
            // 
            this.LblCliente.AutoSize = true;
            this.LblCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblCliente.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblCliente.Location = new System.Drawing.Point(120, 12);
            this.LblCliente.Name = "LblCliente";
            this.LblCliente.Size = new System.Drawing.Size(72, 17);
            this.LblCliente.TabIndex = 20;
            this.LblCliente.Text = "CLIENTE";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(9, 12);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(82, 86);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 21;
            this.Logo.TabStop = false;
            // 
            // BtnSplit
            // 
            this.BtnSplit.Image = global::Embarques.Properties.Resources.cajas2;
            this.BtnSplit.Location = new System.Drawing.Point(742, 9);
            this.BtnSplit.Name = "BtnSplit";
            this.BtnSplit.Size = new System.Drawing.Size(64, 54);
            this.BtnSplit.TabIndex = 17;
            this.BtnSplit.UseVisualStyleBackColor = true;
            this.BtnSplit.Click += new System.EventHandler(this.BtnSplit_Click);
            // 
            // BtnImp
            // 
            this.BtnImp.Image = global::Embarques.Properties.Resources.botonImprimir;
            this.BtnImp.Location = new System.Drawing.Point(667, 8);
            this.BtnImp.Name = "BtnImp";
            this.BtnImp.Size = new System.Drawing.Size(65, 56);
            this.BtnImp.TabIndex = 16;
            this.BtnImp.UseVisualStyleBackColor = true;
            this.BtnImp.Click += new System.EventHandler(this.BtnImp_Click);
            // 
            // LblObs
            // 
            this.LblObs.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblObs.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblObs.Location = new System.Drawing.Point(119, 29);
            this.LblObs.Name = "LblObs";
            this.LblObs.Size = new System.Drawing.Size(539, 43);
            this.LblObs.TabIndex = 22;
            this.LblObs.Text = "PEDIDO";
            // 
            // TxtTotSpl
            // 
            this.TxtTotSpl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotSpl.ForeColor = System.Drawing.Color.Red;
            this.TxtTotSpl.Location = new System.Drawing.Point(816, 71);
            this.TxtTotSpl.Name = "TxtTotSpl";
            this.TxtTotSpl.ReadOnly = true;
            this.TxtTotSpl.Size = new System.Drawing.Size(70, 26);
            this.TxtTotSpl.TabIndex = 23;
            this.TxtTotSpl.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LblTotSpl
            // 
            this.LblTotSpl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotSpl.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblTotSpl.Location = new System.Drawing.Point(819, 52);
            this.LblTotSpl.Name = "LblTotSpl";
            this.LblTotSpl.Size = new System.Drawing.Size(64, 17);
            this.LblTotSpl.TabIndex = 24;
            this.LblTotSpl.Text = "0/0";
            this.LblTotSpl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PbxQR
            // 
            this.PbxQR.Location = new System.Drawing.Point(937, 9);
            this.PbxQR.Name = "PbxQR";
            this.PbxQR.Size = new System.Drawing.Size(33, 32);
            this.PbxQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PbxQR.TabIndex = 25;
            this.PbxQR.TabStop = false;
            // 
            // btnGPS
            // 
            this.btnGPS.Image = global::Embarques.Properties.Resources.Buscar_Mini;
            this.btnGPS.Location = new System.Drawing.Point(831, 7);
            this.btnGPS.Name = "btnGPS";
            this.btnGPS.Size = new System.Drawing.Size(41, 42);
            this.btnGPS.TabIndex = 16;
            this.btnGPS.UseVisualStyleBackColor = true;
            this.btnGPS.Click += new System.EventHandler(this.btnGPS_Click);
            // 
            // LblTar
            // 
            this.LblTar.AutoSize = true;
            this.LblTar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTar.ForeColor = System.Drawing.Color.Blue;
            this.LblTar.Location = new System.Drawing.Point(491, 83);
            this.LblTar.Name = "LblTar";
            this.LblTar.Size = new System.Drawing.Size(78, 13);
            this.LblTar.TabIndex = 26;
            this.LblTar.Text = "Tot Tarimas:";
            // 
            // ConsultaEmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 621);
            this.Controls.Add(this.LblTar);
            this.Controls.Add(this.PbxQR);
            this.Controls.Add(this.LblTotSpl);
            this.Controls.Add(this.TxtTotSpl);
            this.Controls.Add(this.LblObs);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.LblCliente);
            this.Controls.Add(this.TxtTotS);
            this.Controls.Add(this.TxtTotP);
            this.Controls.Add(this.BtnSplit);
            this.Controls.Add(this.btnGPS);
            this.Controls.Add(this.BtnImp);
            this.Controls.Add(this.LblPed);
            this.Controls.Add(this.DGDetPed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsultaEmb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTA DETALLE DEL PEDIDO";
            this.Load += new System.EventHandler(this.ConsultaEmb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbxQR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDetPed;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.Button BtnImp;
        private System.Windows.Forms.Button BtnSplit;
        private System.Windows.Forms.TextBox TxtTotP;
        private System.Windows.Forms.TextBox TxtTotS;
        private System.Windows.Forms.Label LblCliente;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label LblObs;
        private System.Windows.Forms.TextBox TxtTotSpl;
        private System.Windows.Forms.Label LblTotSpl;
        private System.Windows.Forms.PictureBox PbxQR;
        private System.Windows.Forms.Button btnGPS;
        private System.Windows.Forms.Label LblTar;
    }
}
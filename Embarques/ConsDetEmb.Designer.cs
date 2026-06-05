namespace Embarques
{
    partial class ConsDetEmb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsDetEmb));
            this.DGDetPed = new System.Windows.Forms.DataGridView();
            this.DGLotes = new System.Windows.Forms.DataGridView();
            this.CmbPed = new System.Windows.Forms.ComboBox();
            this.BtnAce = new System.Windows.Forms.Button();
            this.LblPed = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnImprimir = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.TxtPeso = new System.Windows.Forms.TextBox();
            this.TxtTotS = new System.Windows.Forms.TextBox();
            this.TxtTotP = new System.Windows.Forms.TextBox();
            this.TxtPeso1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEMP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UBICA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHACAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PESO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PESOXCJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPOREC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PesoCaja = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSCC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGLotes)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGDetPed
            // 
            this.DGDetPed.AllowUserToAddRows = false;
            this.DGDetPed.AllowUserToDeleteRows = false;
            this.DGDetPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetPed.Dock = System.Windows.Forms.DockStyle.Left;
            this.DGDetPed.Location = new System.Drawing.Point(0, 77);
            this.DGDetPed.Name = "DGDetPed";
            this.DGDetPed.ReadOnly = true;
            this.DGDetPed.RowHeadersVisible = false;
            this.DGDetPed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetPed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetPed.Size = new System.Drawing.Size(532, 497);
            this.DGDetPed.TabIndex = 0;
            this.DGDetPed.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDetPed_CellClick);
            this.DGDetPed.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DGDetPed_KeyUp);
            // 
            // DGLotes
            // 
            this.DGLotes.AllowUserToAddRows = false;
            this.DGLotes.AllowUserToDeleteRows = false;
            this.DGLotes.AllowUserToOrderColumns = true;
            this.DGLotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGLotes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LOTE,
            this.CANT,
            this.TEMP,
            this.UBICA,
            this.FECHACAD,
            this.PESO,
            this.PESOXCJ,
            this.TIPOREC,
            this.PesoCaja,
            this.SSCC});
            this.DGLotes.Dock = System.Windows.Forms.DockStyle.Right;
            this.DGLotes.Location = new System.Drawing.Point(538, 77);
            this.DGLotes.Name = "DGLotes";
            this.DGLotes.ReadOnly = true;
            this.DGLotes.RowHeadersVisible = false;
            this.DGLotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGLotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGLotes.Size = new System.Drawing.Size(723, 497);
            this.DGLotes.TabIndex = 1;
            this.DGLotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGLotes_CellDoubleClick);
            // 
            // CmbPed
            // 
            this.CmbPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbPed.FormattingEnabled = true;
            this.CmbPed.Location = new System.Drawing.Point(576, 11);
            this.CmbPed.Name = "CmbPed";
            this.CmbPed.Size = new System.Drawing.Size(155, 28);
            this.CmbPed.TabIndex = 2;
            // 
            // BtnAce
            // 
            this.BtnAce.Image = global::Embarques.Properties.Resources.CHECKMRK;
            this.BtnAce.Location = new System.Drawing.Point(737, 9);
            this.BtnAce.Name = "BtnAce";
            this.BtnAce.Size = new System.Drawing.Size(35, 33);
            this.BtnAce.TabIndex = 3;
            this.BtnAce.UseVisualStyleBackColor = true;
            this.BtnAce.Click += new System.EventHandler(this.BtnAce_Click);
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(95, 12);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(103, 26);
            this.LblPed.TabIndex = 16;
            this.LblPed.Text = "PEDIDO";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.BtnImprimir);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.TxtPeso);
            this.panel1.Controls.Add(this.LblPed);
            this.panel1.Controls.Add(this.CmbPed);
            this.panel1.Controls.Add(this.BtnAce);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1261, 77);
            this.panel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(942, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 26);
            this.label3.TabIndex = 119;
            this.label3.Text = "PESO BRUTO";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LimeGreen;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Location = new System.Drawing.Point(373, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(315, 18);
            this.label1.TabIndex = 117;
            this.label1.Text = "Generando Reporte ......";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Visible = false;
            // 
            // BtnImprimir
            // 
            this.BtnImprimir.AutoSize = true;
            this.BtnImprimir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BtnImprimir.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnImprimir.Font = new System.Drawing.Font("Lucida Sans Unicode", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImprimir.ForeColor = System.Drawing.Color.Black;
            this.BtnImprimir.Image = global::Embarques.Properties.Resources.ExcelMini;
            this.BtnImprimir.Location = new System.Drawing.Point(0, 0);
            this.BtnImprimir.Name = "BtnImprimir";
            this.BtnImprimir.Size = new System.Drawing.Size(56, 49);
            this.BtnImprimir.TabIndex = 111;
            this.BtnImprimir.UseVisualStyleBackColor = false;
            this.BtnImprimir.Click += new System.EventHandler(this.BtnImprimir_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 49);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1261, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 116;
            this.progressBar1.Visible = false;
            // 
            // TxtPeso
            // 
            this.TxtPeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPeso.Location = new System.Drawing.Point(858, 12);
            this.TxtPeso.Name = "TxtPeso";
            this.TxtPeso.Size = new System.Drawing.Size(78, 26);
            this.TxtPeso.TabIndex = 0;
            this.TxtPeso.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTotS
            // 
            this.TxtTotS.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotS.ForeColor = System.Drawing.Color.Red;
            this.TxtTotS.Location = new System.Drawing.Point(444, 12);
            this.TxtTotS.Name = "TxtTotS";
            this.TxtTotS.ReadOnly = true;
            this.TxtTotS.Size = new System.Drawing.Size(78, 26);
            this.TxtTotS.TabIndex = 21;
            this.TxtTotS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtTotP
            // 
            this.TxtTotP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotP.ForeColor = System.Drawing.Color.Red;
            this.TxtTotP.Location = new System.Drawing.Point(360, 12);
            this.TxtTotP.Name = "TxtTotP";
            this.TxtTotP.ReadOnly = true;
            this.TxtTotP.Size = new System.Drawing.Size(78, 26);
            this.TxtTotP.TabIndex = 20;
            this.TxtTotP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TxtPeso1
            // 
            this.TxtPeso1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPeso1.Location = new System.Drawing.Point(818, 12);
            this.TxtPeso1.Name = "TxtPeso1";
            this.TxtPeso1.ReadOnly = true;
            this.TxtPeso1.Size = new System.Drawing.Size(78, 26);
            this.TxtPeso1.TabIndex = 1;
            this.TxtPeso1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(734, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 26);
            this.label2.TabIndex = 118;
            this.label2.Text = "PESO X PRODUCTO";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.TxtTotS);
            this.panel2.Controls.Add(this.TxtPeso1);
            this.panel2.Controls.Add(this.TxtTotP);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 574);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1261, 50);
            this.panel2.TabIndex = 119;
            // 
            // LOTE
            // 
            this.LOTE.HeaderText = "LOTE";
            this.LOTE.Name = "LOTE";
            this.LOTE.ReadOnly = true;
            this.LOTE.Width = 150;
            // 
            // CANT
            // 
            this.CANT.HeaderText = "CANT";
            this.CANT.Name = "CANT";
            this.CANT.ReadOnly = true;
            this.CANT.Width = 40;
            // 
            // TEMP
            // 
            this.TEMP.HeaderText = "TEMP";
            this.TEMP.Name = "TEMP";
            this.TEMP.ReadOnly = true;
            this.TEMP.Width = 40;
            // 
            // UBICA
            // 
            this.UBICA.HeaderText = "UBICA";
            this.UBICA.Name = "UBICA";
            this.UBICA.ReadOnly = true;
            this.UBICA.Width = 40;
            // 
            // FECHACAD
            // 
            this.FECHACAD.HeaderText = "FECHA CAD";
            this.FECHACAD.Name = "FECHACAD";
            this.FECHACAD.ReadOnly = true;
            this.FECHACAD.Width = 80;
            // 
            // PESO
            // 
            this.PESO.HeaderText = "PESO";
            this.PESO.Name = "PESO";
            this.PESO.ReadOnly = true;
            this.PESO.Width = 70;
            // 
            // PESOXCJ
            // 
            this.PESOXCJ.HeaderText = "PESO PROD X CAJA";
            this.PESOXCJ.Name = "PESOXCJ";
            this.PESOXCJ.ReadOnly = true;
            this.PESOXCJ.Width = 70;
            // 
            // TIPOREC
            // 
            this.TIPOREC.HeaderText = "TIPOREC";
            this.TIPOREC.Name = "TIPOREC";
            this.TIPOREC.ReadOnly = true;
            this.TIPOREC.Visible = false;
            // 
            // PesoCaja
            // 
            this.PesoCaja.HeaderText = "PESO X CAJA";
            this.PesoCaja.Name = "PesoCaja";
            this.PesoCaja.ReadOnly = true;
            this.PesoCaja.Width = 65;
            // 
            // SSCC
            // 
            this.SSCC.HeaderText = "SSCC";
            this.SSCC.Name = "SSCC";
            this.SSCC.ReadOnly = true;
            this.SSCC.Width = 140;
            // 
            // ConsDetEmb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 624);
            this.Controls.Add(this.DGLotes);
            this.Controls.Add(this.DGDetPed);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsDetEmb";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTA DE PRODUCTOS SURTIDOS POR PEDIDO";
            this.Load += new System.EventHandler(this.ConsDetEmb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGLotes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDetPed;
        private System.Windows.Forms.DataGridView DGLotes;
        private System.Windows.Forms.ComboBox CmbPed;
        private System.Windows.Forms.Button BtnAce;
        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtTotS;
        private System.Windows.Forms.TextBox TxtTotP;
        private System.Windows.Forms.TextBox TxtPeso;
        private System.Windows.Forms.TextBox TxtPeso1;
        private System.Windows.Forms.Button BtnImprimir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEMP;
        private System.Windows.Forms.DataGridViewTextBoxColumn UBICA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHACAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PESOXCJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPOREC;
        private System.Windows.Forms.DataGridViewTextBoxColumn PesoCaja;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSCC;
    }
}
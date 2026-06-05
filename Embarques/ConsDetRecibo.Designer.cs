namespace Embarques
{
    partial class ConsDetRecibo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsDetRecibo));
            this.DGDetTar = new System.Windows.Forms.DataGridView();
            this.TARIMA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FECHACAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SURTIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RECIBO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRLLEGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIEMPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TEMPSAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblEnf = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LblProd = new System.Windows.Forms.Label();
            this.TxtRecibo = new System.Windows.Forms.TextBox();
            this.LblOrd = new System.Windows.Forms.Label();
            this.DGDetEmb = new System.Windows.Forms.DataGridView();
            this.DGDetTarPtp = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetTar)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetEmb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetTarPtp)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDetTar
            // 
            this.DGDetTar.AllowUserToAddRows = false;
            this.DGDetTar.AllowUserToDeleteRows = false;
            this.DGDetTar.AllowUserToOrderColumns = true;
            this.DGDetTar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetTar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TARIMA,
            this.FECHACAD,
            this.CANT,
            this.SURTIDO,
            this.TIPO,
            this.LOTE,
            this.RECIBO,
            this.HRLLEGO,
            this.HRENT,
            this.TIEMPO,
            this.TEMPSAL});
            this.DGDetTar.Location = new System.Drawing.Point(12, 121);
            this.DGDetTar.Name = "DGDetTar";
            this.DGDetTar.ReadOnly = true;
            this.DGDetTar.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetTar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetTar.Size = new System.Drawing.Size(982, 247);
            this.DGDetTar.TabIndex = 0;
            // 
            // TARIMA
            // 
            this.TARIMA.HeaderText = "TARIMA";
            this.TARIMA.Name = "TARIMA";
            this.TARIMA.ReadOnly = true;
            // 
            // FECHACAD
            // 
            this.FECHACAD.HeaderText = "FECHA CAD";
            this.FECHACAD.Name = "FECHACAD";
            this.FECHACAD.ReadOnly = true;
            // 
            // CANT
            // 
            this.CANT.HeaderText = "CANT";
            this.CANT.Name = "CANT";
            this.CANT.ReadOnly = true;
            // 
            // SURTIDO
            // 
            this.SURTIDO.HeaderText = "SURTIDO";
            this.SURTIDO.Name = "SURTIDO";
            this.SURTIDO.ReadOnly = true;
            // 
            // TIPO
            // 
            this.TIPO.HeaderText = "TIPO";
            this.TIPO.Name = "TIPO";
            this.TIPO.ReadOnly = true;
            // 
            // LOTE
            // 
            this.LOTE.HeaderText = "LOTE CONTROL";
            this.LOTE.Name = "LOTE";
            this.LOTE.ReadOnly = true;
            // 
            // RECIBO
            // 
            this.RECIBO.HeaderText = "RECIBO";
            this.RECIBO.Name = "RECIBO";
            this.RECIBO.ReadOnly = true;
            // 
            // HRLLEGO
            // 
            this.HRLLEGO.HeaderText = "HR LLEGO";
            this.HRLLEGO.Name = "HRLLEGO";
            this.HRLLEGO.ReadOnly = true;
            // 
            // HRENT
            // 
            this.HRENT.HeaderText = "HR ENT.";
            this.HRENT.Name = "HRENT";
            this.HRENT.ReadOnly = true;
            // 
            // TIEMPO
            // 
            this.TIEMPO.HeaderText = "TIEMPO";
            this.TIEMPO.Name = "TIEMPO";
            this.TIEMPO.ReadOnly = true;
            // 
            // TEMPSAL
            // 
            this.TEMPSAL.HeaderText = "TEMP SAL";
            this.TEMPSAL.Name = "TEMPSAL";
            this.TEMPSAL.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.LblEnf);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LblProd);
            this.panel1.Controls.Add(this.TxtRecibo);
            this.panel1.Controls.Add(this.LblOrd);
            this.panel1.Location = new System.Drawing.Point(12, 15);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 100);
            this.panel1.TabIndex = 2;
            // 
            // LblEnf
            // 
            this.LblEnf.AutoSize = true;
            this.LblEnf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblEnf.ForeColor = System.Drawing.Color.Blue;
            this.LblEnf.Location = new System.Drawing.Point(690, 83);
            this.LblEnf.Name = "LblEnf";
            this.LblEnf.Size = new System.Drawing.Size(139, 13);
            this.LblEnf.TabIndex = 28;
            this.LblEnf.Text = "E N F R I A M I E N T O";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(624, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(350, 50);
            this.label4.TabIndex = 27;
            this.label4.Text = "KG/CJA";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(825, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "KG/CJA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(117, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "DATOSRECIBO";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(117, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "DATOSRECIBO";
            // 
            // LblProd
            // 
            this.LblProd.AutoSize = true;
            this.LblProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblProd.ForeColor = System.Drawing.Color.Red;
            this.LblProd.Location = new System.Drawing.Point(117, 4);
            this.LblProd.Name = "LblProd";
            this.LblProd.Size = new System.Drawing.Size(77, 20);
            this.LblProd.TabIndex = 23;
            this.LblProd.Text = "ORDEN:";
            // 
            // TxtRecibo
            // 
            this.TxtRecibo.BackColor = System.Drawing.Color.Black;
            this.TxtRecibo.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRecibo.ForeColor = System.Drawing.Color.Yellow;
            this.TxtRecibo.Location = new System.Drawing.Point(7, 36);
            this.TxtRecibo.Name = "TxtRecibo";
            this.TxtRecibo.Size = new System.Drawing.Size(100, 35);
            this.TxtRecibo.TabIndex = 22;
            // 
            // LblOrd
            // 
            this.LblOrd.AutoSize = true;
            this.LblOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblOrd.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblOrd.Location = new System.Drawing.Point(3, 13);
            this.LblOrd.Name = "LblOrd";
            this.LblOrd.Size = new System.Drawing.Size(77, 20);
            this.LblOrd.TabIndex = 21;
            this.LblOrd.Text = "ORDEN:";
            // 
            // DGDetEmb
            // 
            this.DGDetEmb.AllowUserToAddRows = false;
            this.DGDetEmb.AllowUserToDeleteRows = false;
            this.DGDetEmb.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetEmb.Location = new System.Drawing.Point(12, 374);
            this.DGDetEmb.Name = "DGDetEmb";
            this.DGDetEmb.ReadOnly = true;
            this.DGDetEmb.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetEmb.Size = new System.Drawing.Size(982, 236);
            this.DGDetEmb.TabIndex = 3;
            // 
            // DGDetTarPtp
            // 
            this.DGDetTarPtp.AllowUserToAddRows = false;
            this.DGDetTarPtp.AllowUserToDeleteRows = false;
            this.DGDetTarPtp.AllowUserToOrderColumns = true;
            this.DGDetTarPtp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetTarPtp.Location = new System.Drawing.Point(13, 124);
            this.DGDetTarPtp.Name = "DGDetTarPtp";
            this.DGDetTarPtp.ReadOnly = true;
            this.DGDetTarPtp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetTarPtp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetTarPtp.Size = new System.Drawing.Size(982, 247);
            this.DGDetTarPtp.TabIndex = 4;
            this.DGDetTarPtp.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDetTarPtp_CellDoubleClick);
            // 
            // ConsDetRecibo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1011, 631);
            this.Controls.Add(this.DGDetTarPtp);
            this.Controls.Add(this.DGDetEmb);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGDetTar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsDetRecibo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONSULTA DETALLE DEL RECIBO";
            this.Load += new System.EventHandler(this.ConsDetRecibo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDetTar)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetEmb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetTarPtp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDetTar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtRecibo;
        private System.Windows.Forms.Label LblOrd;
        private System.Windows.Forms.Label LblProd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DGDetEmb;
        private System.Windows.Forms.DataGridView DGDetTarPtp;
        private System.Windows.Forms.DataGridViewTextBoxColumn TARIMA;
        private System.Windows.Forms.DataGridViewTextBoxColumn FECHACAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CANT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SURTIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn RECIBO;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRLLEGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRENT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIEMPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TEMPSAL;
        private System.Windows.Forms.Label LblEnf;
    }
}
namespace Embarques
{
    partial class FrmCapPesoBas
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCapPesoBas));
			this.panel3 = new System.Windows.Forms.Panel();
			this.BtnSalir = new System.Windows.Forms.Button();
			this.BtnGrabar = new System.Windows.Forms.Button();
			this.DtFE = new System.Windows.Forms.DateTimePicker();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtPlacaCaja = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.TxtHrEnt = new System.Windows.Forms.TextBox();
			this.TxtPlaca = new System.Windows.Forms.TextBox();
			this.TxtDest = new System.Windows.Forms.TextBox();
			this.TxtCho = new System.Windows.Forms.TextBox();
			this.TxtTrans = new System.Windows.Forms.TextBox();
			this.TxtHrLlego = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.TxtPeso = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel3.Controls.Add(this.BtnSalir);
			this.panel3.Controls.Add(this.BtnGrabar);
			this.panel3.Controls.Add(this.DtFE);
			this.panel3.Location = new System.Drawing.Point(13, 12);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(819, 63);
			this.panel3.TabIndex = 24;
			// 
			// BtnSalir
			// 
			this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnSalir.Image = global::Embarques.Properties.Resources.BtnSalir_Mini;
			this.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnSalir.Location = new System.Drawing.Point(722, 8);
			this.BtnSalir.Name = "BtnSalir";
			this.BtnSalir.Size = new System.Drawing.Size(77, 42);
			this.BtnSalir.TabIndex = 32;
			this.BtnSalir.Text = "SALIR";
			this.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnSalir.UseVisualStyleBackColor = true;
			this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
			// 
			// BtnGrabar
			// 
			this.BtnGrabar.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.BtnGrabar.Image = global::Embarques.Properties.Resources.BtnGuardarMini;
			this.BtnGrabar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BtnGrabar.Location = new System.Drawing.Point(362, 8);
			this.BtnGrabar.Name = "BtnGrabar";
			this.BtnGrabar.Size = new System.Drawing.Size(91, 42);
			this.BtnGrabar.TabIndex = 31;
			this.BtnGrabar.Text = "GUARDAR";
			this.BtnGrabar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BtnGrabar.UseVisualStyleBackColor = true;
			this.BtnGrabar.Click += new System.EventHandler(this.BtnGrabar_Click);
			// 
			// DtFE
			// 
			this.DtFE.Enabled = false;
			this.DtFE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.DtFE.Location = new System.Drawing.Point(18, 19);
			this.DtFE.Name = "DtFE";
			this.DtFE.Size = new System.Drawing.Size(96, 20);
			this.DtFE.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.txtPlacaCaja);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.TxtHrEnt);
			this.panel2.Controls.Add(this.TxtPlaca);
			this.panel2.Controls.Add(this.TxtDest);
			this.panel2.Controls.Add(this.TxtCho);
			this.panel2.Controls.Add(this.TxtTrans);
			this.panel2.Controls.Add(this.TxtHrLlego);
			this.panel2.Controls.Add(this.label16);
			this.panel2.Controls.Add(this.label15);
			this.panel2.Controls.Add(this.label14);
			this.panel2.Controls.Add(this.label13);
			this.panel2.Controls.Add(this.label12);
			this.panel2.Controls.Add(this.label11);
			this.panel2.Location = new System.Drawing.Point(11, 88);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(822, 87);
			this.panel2.TabIndex = 23;
			// 
			// txtPlacaCaja
			// 
			this.txtPlacaCaja.Enabled = false;
			this.txtPlacaCaja.Location = new System.Drawing.Point(522, 49);
			this.txtPlacaCaja.Name = "txtPlacaCaja";
			this.txtPlacaCaja.Size = new System.Drawing.Size(85, 20);
			this.txtPlacaCaja.TabIndex = 33;
			this.txtPlacaCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Black;
			this.label3.Location = new System.Drawing.Point(431, 53);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 13);
			this.label3.TabIndex = 32;
			this.label3.Text = "PLACA CAJA:";
			// 
			// TxtHrEnt
			// 
			this.TxtHrEnt.Enabled = false;
			this.TxtHrEnt.Location = new System.Drawing.Point(724, 49);
			this.TxtHrEnt.Name = "TxtHrEnt";
			this.TxtHrEnt.Size = new System.Drawing.Size(85, 20);
			this.TxtHrEnt.TabIndex = 32;
			this.TxtHrEnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtPlaca
			// 
			this.TxtPlaca.Enabled = false;
			this.TxtPlaca.Location = new System.Drawing.Point(339, 49);
			this.TxtPlaca.Name = "TxtPlaca";
			this.TxtPlaca.Size = new System.Drawing.Size(85, 20);
			this.TxtPlaca.TabIndex = 31;
			this.TxtPlaca.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtDest
			// 
			this.TxtDest.Enabled = false;
			this.TxtDest.Location = new System.Drawing.Point(80, 49);
			this.TxtDest.Name = "TxtDest";
			this.TxtDest.Size = new System.Drawing.Size(125, 20);
			this.TxtDest.TabIndex = 30;
			this.TxtDest.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtCho
			// 
			this.TxtCho.Enabled = false;
			this.TxtCho.Location = new System.Drawing.Point(590, 8);
			this.TxtCho.Name = "TxtCho";
			this.TxtCho.Size = new System.Drawing.Size(212, 20);
			this.TxtCho.TabIndex = 29;
			// 
			// TxtTrans
			// 
			this.TxtTrans.Enabled = false;
			this.TxtTrans.Location = new System.Drawing.Point(364, 8);
			this.TxtTrans.Name = "TxtTrans";
			this.TxtTrans.Size = new System.Drawing.Size(121, 20);
			this.TxtTrans.TabIndex = 28;
			this.TxtTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TxtHrLlego
			// 
			this.TxtHrLlego.Enabled = false;
			this.TxtHrLlego.Location = new System.Drawing.Point(145, 8);
			this.TxtHrLlego.Name = "TxtHrLlego";
			this.TxtHrLlego.Size = new System.Drawing.Size(85, 20);
			this.TxtHrLlego.TabIndex = 3;
			this.TxtHrLlego.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.ForeColor = System.Drawing.Color.Black;
			this.label16.Location = new System.Drawing.Point(614, 53);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(103, 13);
			this.label16.TabIndex = 27;
			this.label16.Text = "HORA ENTRADA:";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.ForeColor = System.Drawing.Color.Black;
			this.label15.Location = new System.Drawing.Point(211, 53);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(103, 13);
			this.label15.TabIndex = 26;
			this.label15.Text = "PLACA TRAILER:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.Black;
			this.label14.Location = new System.Drawing.Point(11, 53);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(63, 13);
			this.label14.TabIndex = 25;
			this.label14.Text = "DESTINO:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Black;
			this.label13.Location = new System.Drawing.Point(508, 12);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(59, 13);
			this.label13.TabIndex = 24;
			this.label13.Text = "CHOFER:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Black;
			this.label12.Location = new System.Drawing.Point(253, 12);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(88, 13);
			this.label12.TabIndex = 23;
			this.label12.Text = "TRANSPORTE:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.Black;
			this.label11.Location = new System.Drawing.Point(18, 12);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(104, 13);
			this.label11.TabIndex = 22;
			this.label11.Text = "HORA LLEGADA:";
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.TxtPeso);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Location = new System.Drawing.Point(232, 198);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(380, 125);
			this.panel1.TabIndex = 25;
			// 
			// TxtPeso
			// 
			this.TxtPeso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.TxtPeso.Location = new System.Drawing.Point(167, 69);
			this.TxtPeso.Name = "TxtPeso";
			this.TxtPeso.Size = new System.Drawing.Size(121, 29);
			this.TxtPeso.TabIndex = 33;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Black;
			this.label2.Location = new System.Drawing.Point(27, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(322, 20);
			this.label2.TabIndex = 29;
			this.label2.Text = "CAPTURA EL PESO DE LA BASCULA:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Black;
			this.label1.Location = new System.Drawing.Point(88, 71);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 24);
			this.label1.TabIndex = 28;
			this.label1.Text = "PESO:";
			// 
			// FrmCapPesoBas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(845, 335);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmCapPesoBas";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CONTROL DE EMBARQUES (CAPTURA EL PESO DE LA BASCULA)";
			this.Load += new System.EventHandler(this.FrmCapPesoBas_Load);
			this.panel3.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.Button BtnGrabar;
        private System.Windows.Forms.DateTimePicker DtFE;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox TxtHrEnt;
        private System.Windows.Forms.TextBox TxtPlaca;
        private System.Windows.Forms.TextBox TxtDest;
        private System.Windows.Forms.TextBox TxtCho;
        private System.Windows.Forms.TextBox TxtTrans;
        private System.Windows.Forms.TextBox TxtHrLlego;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox TxtPeso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPlacaCaja;
		private System.Windows.Forms.Label label3;
	}
}
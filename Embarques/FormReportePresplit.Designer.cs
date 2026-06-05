namespace Embarques
{
    partial class FormReportePresplit
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
            this.label4 = new System.Windows.Forms.Label();
            this.BtnEmbdia = new System.Windows.Forms.PictureBox();
            this.DGSplit = new System.Windows.Forms.DataGridView();
            this.BtnRep = new System.Windows.Forms.Button();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.DtFE = new System.Windows.Forms.DateTimePicker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TxtPedPen1 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.LblTotalVerde = new System.Windows.Forms.TextBox();
            this.LblTotalAmarillo = new System.Windows.Forms.TextBox();
            this.LblTotalRojo = new System.Windows.Forms.TextBox();
            this.LblTotalAzul = new System.Windows.Forms.TextBox();
            this.LblTotalNaranja = new System.Windows.Forms.TextBox();
            this.LblTotalBlanca = new System.Windows.Forms.TextBox();
            this.LblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEmbdia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(425, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 20);
            this.label4.TabIndex = 93;
            this.label4.Text = "REPORTE PRESPLIT";
            // 
            // BtnEmbdia
            // 
            this.BtnEmbdia.Image = global::Embarques.Properties.Resources.aplicar;
            this.BtnEmbdia.Location = new System.Drawing.Point(120, 16);
            this.BtnEmbdia.Name = "BtnEmbdia";
            this.BtnEmbdia.Size = new System.Drawing.Size(32, 31);
            this.BtnEmbdia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnEmbdia.TabIndex = 127;
            this.BtnEmbdia.TabStop = false;
            this.BtnEmbdia.Click += new System.EventHandler(this.BtnEmbdia_Click);
            // 
            // DGSplit
            // 
            this.DGSplit.AllowUserToAddRows = false;
            this.DGSplit.AllowUserToDeleteRows = false;
            this.DGSplit.AllowUserToOrderColumns = true;
            this.DGSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSplit.Location = new System.Drawing.Point(12, 94);
            this.DGSplit.Name = "DGSplit";
            this.DGSplit.ReadOnly = true;
            this.DGSplit.Size = new System.Drawing.Size(1044, 434);
            this.DGSplit.TabIndex = 128;
            // 
            // BtnRep
            // 
            this.BtnRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRep.Image = global::Embarques.Properties.Resources.Excel;
            this.BtnRep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRep.Location = new System.Drawing.Point(736, 12);
            this.BtnRep.Name = "BtnRep";
            this.BtnRep.Size = new System.Drawing.Size(98, 42);
            this.BtnRep.TabIndex = 126;
            this.BtnRep.Text = "REPORTE";
            this.BtnRep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRep.UseVisualStyleBackColor = true;
            this.BtnRep.Click += new System.EventHandler(this.BtnRep_Click);
            // 
            // BtnSalir
            // 
            this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalir.Image = global::Embarques.Properties.Resources.BtnSalir_Mini;
            this.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSalir.Location = new System.Drawing.Point(933, 12);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(97, 40);
            this.BtnSalir.TabIndex = 125;
            this.BtnSalir.Text = "SALIR";
            this.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // DtFE
            // 
            this.DtFE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtFE.Location = new System.Drawing.Point(18, 22);
            this.DtFE.Name = "DtFE";
            this.DtFE.Size = new System.Drawing.Size(96, 20);
            this.DtFE.TabIndex = 124;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 269);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1034, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 129;
            this.progressBar1.Visible = false;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.LimeGreen;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Navy;
            this.label32.Location = new System.Drawing.Point(377, 274);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(315, 18);
            this.label32.TabIndex = 130;
            this.label32.Text = "Generando Reporte ......";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label32.Visible = false;
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.Black;
            this.textBox3.Location = new System.Drawing.Point(920, 534);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(110, 20);
            this.textBox3.TabIndex = 134;
            this.textBox3.Text = "Activo Leido Presplit";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Red;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.Location = new System.Drawing.Point(385, 534);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(66, 20);
            this.textBox2.TabIndex = 133;
            this.textBox2.Text = "No Cargado";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Yellow;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(211, 534);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(133, 20);
            this.textBox1.TabIndex = 132;
            this.textBox1.Text = "Leido Presplit - Reimpreso";
            // 
            // TxtPedPen1
            // 
            this.TxtPedPen1.BackColor = System.Drawing.Color.LawnGreen;
            this.TxtPedPen1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPedPen1.Location = new System.Drawing.Point(12, 534);
            this.TxtPedPen1.Name = "TxtPedPen1";
            this.TxtPedPen1.ReadOnly = true;
            this.TxtPedPen1.Size = new System.Drawing.Size(148, 20);
            this.TxtPedPen1.TabIndex = 131;
            this.TxtPedPen1.Text = "PreSplit Leido en Camionetas";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(672, 534);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(209, 20);
            this.textBox4.TabIndex = 132;
            this.textBox4.Text = "Leido En Camionetas Pero no en Presplit";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Cyan;
            this.textBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(493, 534);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(136, 20);
            this.textBox5.TabIndex = 132;
            this.textBox5.Text = "Presplit Leido DESTIEMPO";
            // 
            // LblTotalVerde
            // 
            this.LblTotalVerde.BackColor = System.Drawing.Color.LawnGreen;
            this.LblTotalVerde.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalVerde.Location = new System.Drawing.Point(18, 68);
            this.LblTotalVerde.Name = "LblTotalVerde";
            this.LblTotalVerde.ReadOnly = true;
            this.LblTotalVerde.Size = new System.Drawing.Size(60, 20);
            this.LblTotalVerde.TabIndex = 131;
            this.LblTotalVerde.Text = "Total: 0";
            // 
            // LblTotalAmarillo
            // 
            this.LblTotalAmarillo.BackColor = System.Drawing.Color.Yellow;
            this.LblTotalAmarillo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalAmarillo.Location = new System.Drawing.Point(232, 68);
            this.LblTotalAmarillo.Name = "LblTotalAmarillo";
            this.LblTotalAmarillo.ReadOnly = true;
            this.LblTotalAmarillo.Size = new System.Drawing.Size(60, 20);
            this.LblTotalAmarillo.TabIndex = 132;
            this.LblTotalAmarillo.Text = "Total: 0";
            // 
            // LblTotalRojo
            // 
            this.LblTotalRojo.BackColor = System.Drawing.Color.Red;
            this.LblTotalRojo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalRojo.Location = new System.Drawing.Point(385, 68);
            this.LblTotalRojo.Name = "LblTotalRojo";
            this.LblTotalRojo.ReadOnly = true;
            this.LblTotalRojo.Size = new System.Drawing.Size(60, 20);
            this.LblTotalRojo.TabIndex = 133;
            this.LblTotalRojo.Text = "Total: 0";
            // 
            // LblTotalAzul
            // 
            this.LblTotalAzul.BackColor = System.Drawing.Color.Cyan;
            this.LblTotalAzul.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalAzul.Location = new System.Drawing.Point(544, 68);
            this.LblTotalAzul.Name = "LblTotalAzul";
            this.LblTotalAzul.ReadOnly = true;
            this.LblTotalAzul.Size = new System.Drawing.Size(60, 20);
            this.LblTotalAzul.TabIndex = 132;
            this.LblTotalAzul.Text = "Total: 0";
            // 
            // LblTotalNaranja
            // 
            this.LblTotalNaranja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.LblTotalNaranja.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalNaranja.Location = new System.Drawing.Point(736, 68);
            this.LblTotalNaranja.Name = "LblTotalNaranja";
            this.LblTotalNaranja.ReadOnly = true;
            this.LblTotalNaranja.Size = new System.Drawing.Size(60, 20);
            this.LblTotalNaranja.TabIndex = 132;
            this.LblTotalNaranja.Text = "Total: 0";
            // 
            // LblTotalBlanca
            // 
            this.LblTotalBlanca.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LblTotalBlanca.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalBlanca.ForeColor = System.Drawing.Color.Black;
            this.LblTotalBlanca.Location = new System.Drawing.Point(951, 68);
            this.LblTotalBlanca.Name = "LblTotalBlanca";
            this.LblTotalBlanca.ReadOnly = true;
            this.LblTotalBlanca.Size = new System.Drawing.Size(60, 20);
            this.LblTotalBlanca.TabIndex = 134;
            this.LblTotalBlanca.Text = "Total: 0";
            // 
            // LblTotal
            // 
            this.LblTotal.AutoSize = true;
            this.LblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LblTotal.Location = new System.Drawing.Point(250, 49);
            this.LblTotal.Name = "LblTotal";
            this.LblTotal.Size = new System.Drawing.Size(142, 16);
            this.LblTotal.TabIndex = 93;
            this.LblTotal.Text = "REPORTE PRESPLIT";
            // 
            // FormReportePresplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 566);
            this.ControlBox = false;
            this.Controls.Add(this.LblTotalBlanca);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.LblTotalRojo);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.LblTotalAzul);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.LblTotalNaranja);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.LblTotalAmarillo);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.LblTotalVerde);
            this.Controls.Add(this.TxtPedPen1);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.BtnEmbdia);
            this.Controls.Add(this.DGSplit);
            this.Controls.Add(this.BtnRep);
            this.Controls.Add(this.BtnSalir);
            this.Controls.Add(this.DtFE);
            this.Controls.Add(this.LblTotal);
            this.Controls.Add(this.label4);
            this.Name = "FormReportePresplit";
            this.Text = "FormReportePresplit";
            this.Load += new System.EventHandler(this.FormReportePresplit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BtnEmbdia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox BtnEmbdia;
        private System.Windows.Forms.DataGridView DGSplit;
        private System.Windows.Forms.Button BtnRep;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.DateTimePicker DtFE;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TxtPedPen1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox LblTotalVerde;
        private System.Windows.Forms.TextBox LblTotalAmarillo;
        private System.Windows.Forms.TextBox LblTotalRojo;
        private System.Windows.Forms.TextBox LblTotalAzul;
        private System.Windows.Forms.TextBox LblTotalNaranja;
        private System.Windows.Forms.TextBox LblTotalBlanca;
        private System.Windows.Forms.Label LblTotal;
    }
}
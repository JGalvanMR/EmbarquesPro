namespace Embarques
{
    partial class FrmRepSplitPed
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRepSplitPed));
            this.label32 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.DGSplit = new System.Windows.Forms.DataGridView();
            this.NOTRAILER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRANSPORTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESTINO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PEDIDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRCAP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRCARGA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIEMPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOSPLIT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRINI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HRFIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIEMPO2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RESPONSABLE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CONSE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.TxtTot = new System.Windows.Forms.TextBox();
            this.LblNoSplit = new System.Windows.Forms.Label();
            this.BtnEmbdia = new System.Windows.Forms.PictureBox();
            this.BtnRep = new System.Windows.Forms.Button();
            this.BtnSalir = new System.Windows.Forms.Button();
            this.DtFE = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEmbdia)).BeginInit();
            this.SuspendLayout();
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.LimeGreen;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Navy;
            this.label32.Location = new System.Drawing.Point(394, 118);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(315, 18);
            this.label32.TabIndex = 125;
            this.label32.Text = "Generando Reporte ......";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label32.Visible = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(9, 110);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(1034, 28);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 124;
            this.progressBar1.Visible = false;
            // 
            // DGSplit
            // 
            this.DGSplit.AllowUserToAddRows = false;
            this.DGSplit.AllowUserToDeleteRows = false;
            this.DGSplit.AllowUserToOrderColumns = true;
            this.DGSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGSplit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NOTRAILER,
            this.TRANSPORTE,
            this.DESTINO,
            this.PEDIDO,
            this.HRCAP,
            this.HRCARGA,
            this.TIEMPO,
            this.NOSPLIT,
            this.HRINI,
            this.HRFIN,
            this.TIEMPO2,
            this.RESPONSABLE,
            this.CONSE});
            this.DGSplit.Location = new System.Drawing.Point(9, 77);
            this.DGSplit.Name = "DGSplit";
            this.DGSplit.ReadOnly = true;
            this.DGSplit.Size = new System.Drawing.Size(1044, 475);
            this.DGSplit.TabIndex = 123;
            this.DGSplit.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGSplit_CellDoubleClick);
            // 
            // NOTRAILER
            // 
            this.NOTRAILER.HeaderText = "PLACA";
            this.NOTRAILER.Name = "NOTRAILER";
            this.NOTRAILER.ReadOnly = true;
            this.NOTRAILER.Width = 80;
            // 
            // TRANSPORTE
            // 
            this.TRANSPORTE.HeaderText = "TRANSPORTE";
            this.TRANSPORTE.Name = "TRANSPORTE";
            this.TRANSPORTE.ReadOnly = true;
            // 
            // DESTINO
            // 
            this.DESTINO.HeaderText = "DESTINO";
            this.DESTINO.Name = "DESTINO";
            this.DESTINO.ReadOnly = true;
            // 
            // PEDIDO
            // 
            this.PEDIDO.HeaderText = "PEDIDO";
            this.PEDIDO.Name = "PEDIDO";
            this.PEDIDO.ReadOnly = true;
            this.PEDIDO.Width = 80;
            // 
            // HRCAP
            // 
            this.HRCAP.HeaderText = "HR. CAP";
            this.HRCAP.Name = "HRCAP";
            this.HRCAP.ReadOnly = true;
            this.HRCAP.Width = 60;
            // 
            // HRCARGA
            // 
            this.HRCARGA.HeaderText = "HR. CARGA";
            this.HRCARGA.Name = "HRCARGA";
            this.HRCARGA.ReadOnly = true;
            this.HRCARGA.Width = 65;
            // 
            // TIEMPO
            // 
            this.TIEMPO.HeaderText = "TIEMPO";
            this.TIEMPO.Name = "TIEMPO";
            this.TIEMPO.ReadOnly = true;
            this.TIEMPO.Width = 60;
            // 
            // NOSPLIT
            // 
            this.NOSPLIT.HeaderText = "No. SPLIT";
            this.NOSPLIT.Name = "NOSPLIT";
            this.NOSPLIT.ReadOnly = true;
            this.NOSPLIT.Width = 45;
            // 
            // HRINI
            // 
            this.HRINI.HeaderText = "HR. INI";
            this.HRINI.Name = "HRINI";
            this.HRINI.ReadOnly = true;
            this.HRINI.Width = 70;
            // 
            // HRFIN
            // 
            this.HRFIN.HeaderText = "HR. FIN";
            this.HRFIN.Name = "HRFIN";
            this.HRFIN.ReadOnly = true;
            this.HRFIN.Width = 70;
            // 
            // TIEMPO2
            // 
            this.TIEMPO2.HeaderText = "TIEMPO";
            this.TIEMPO2.Name = "TIEMPO2";
            this.TIEMPO2.ReadOnly = true;
            this.TIEMPO2.Width = 60;
            // 
            // RESPONSABLE
            // 
            this.RESPONSABLE.HeaderText = "RESPONSABLE";
            this.RESPONSABLE.Name = "RESPONSABLE";
            this.RESPONSABLE.ReadOnly = true;
            this.RESPONSABLE.Width = 205;
            // 
            // CONSE
            // 
            this.CONSE.HeaderText = "CONSE";
            this.CONSE.Name = "CONSE";
            this.CONSE.ReadOnly = true;
            this.CONSE.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.TxtTot);
            this.panel3.Controls.Add(this.LblNoSplit);
            this.panel3.Controls.Add(this.BtnEmbdia);
            this.panel3.Controls.Add(this.BtnRep);
            this.panel3.Controls.Add(this.BtnSalir);
            this.panel3.Controls.Add(this.DtFE);
            this.panel3.Location = new System.Drawing.Point(9, 8);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1042, 63);
            this.panel3.TabIndex = 122;
            // 
            // TxtTot
            // 
            this.TxtTot.Enabled = false;
            this.TxtTot.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTot.Location = new System.Drawing.Point(317, 25);
            this.TxtTot.Name = "TxtTot";
            this.TxtTot.Size = new System.Drawing.Size(64, 29);
            this.TxtTot.TabIndex = 36;
            this.TxtTot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LblNoSplit
            // 
            this.LblNoSplit.AutoSize = true;
            this.LblNoSplit.BackColor = System.Drawing.Color.Transparent;
            this.LblNoSplit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNoSplit.ForeColor = System.Drawing.Color.Navy;
            this.LblNoSplit.Location = new System.Drawing.Point(192, 30);
            this.LblNoSplit.Name = "LblNoSplit";
            this.LblNoSplit.Size = new System.Drawing.Size(123, 20);
            this.LblNoSplit.TabIndex = 35;
            this.LblNoSplit.Text = "TOTAL SPLIT:";
            // 
            // BtnEmbdia
            // 
            this.BtnEmbdia.Image = global::Embarques.Properties.Resources.aplicar;
            this.BtnEmbdia.Location = new System.Drawing.Point(117, 14);
            this.BtnEmbdia.Name = "BtnEmbdia";
            this.BtnEmbdia.Size = new System.Drawing.Size(32, 31);
            this.BtnEmbdia.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BtnEmbdia.TabIndex = 34;
            this.BtnEmbdia.TabStop = false;
            this.toolTip1.SetToolTip(this.BtnEmbdia, "GENERAR LA INFORMACION");
            this.BtnEmbdia.Click += new System.EventHandler(this.BtnEmbdia_Click);
            // 
            // BtnRep
            // 
            this.BtnRep.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRep.Image = global::Embarques.Properties.Resources.Excel;
            this.BtnRep.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRep.Location = new System.Drawing.Point(733, 10);
            this.BtnRep.Name = "BtnRep";
            this.BtnRep.Size = new System.Drawing.Size(98, 42);
            this.BtnRep.TabIndex = 33;
            this.BtnRep.Text = "REPORTE";
            this.BtnRep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.BtnRep, "EXPORTAR A EXCEL");
            this.BtnRep.UseVisualStyleBackColor = true;
            this.BtnRep.Click += new System.EventHandler(this.BtnRep_Click);
            // 
            // BtnSalir
            // 
            this.BtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSalir.Image = global::Embarques.Properties.Resources.BtnSalir_Mini;
            this.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnSalir.Location = new System.Drawing.Point(930, 10);
            this.BtnSalir.Name = "BtnSalir";
            this.BtnSalir.Size = new System.Drawing.Size(97, 40);
            this.BtnSalir.TabIndex = 32;
            this.BtnSalir.Text = "SALIR";
            this.BtnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.BtnSalir, "SALIR DEL REPORTE");
            this.BtnSalir.UseVisualStyleBackColor = true;
            this.BtnSalir.Click += new System.EventHandler(this.BtnSalir_Click);
            // 
            // DtFE
            // 
            this.DtFE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtFE.Location = new System.Drawing.Point(15, 20);
            this.DtFE.Name = "DtFE";
            this.DtFE.Size = new System.Drawing.Size(96, 20);
            this.DtFE.TabIndex = 0;
            // 
            // FrmRepSplitPed
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 556);
            this.ControlBox = false;
            this.Controls.Add(this.label32);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.DGSplit);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRepSplitPed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CONTROL DE EMBARQUES (REPORTE DE SPLIT POR PEDIDO)";
            this.Load += new System.EventHandler(this.FrmRepSplitPed_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGSplit)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BtnEmbdia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.DataGridView DGSplit;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox TxtTot;
        private System.Windows.Forms.Label LblNoSplit;
        private System.Windows.Forms.PictureBox BtnEmbdia;
        private System.Windows.Forms.Button BtnRep;
        private System.Windows.Forms.Button BtnSalir;
        private System.Windows.Forms.DateTimePicker DtFE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOTRAILER;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRANSPORTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESTINO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PEDIDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRCAP;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRCARGA;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIEMPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOSPLIT;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRINI;
        private System.Windows.Forms.DataGridViewTextBoxColumn HRFIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIEMPO2;
        private System.Windows.Forms.DataGridViewTextBoxColumn RESPONSABLE;
        private System.Windows.Forms.DataGridViewTextBoxColumn CONSE;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
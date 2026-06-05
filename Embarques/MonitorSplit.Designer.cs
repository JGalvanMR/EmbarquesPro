namespace Embarques
{
    partial class MonitorSplit
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorSplit));
			this.label1 = new System.Windows.Forms.Label();
			this.DGDatos = new System.Windows.Forms.DataGridView();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.DT = new System.Windows.Forms.DateTimePicker();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.BtnAcep = new System.Windows.Forms.Button();
			this.lblGenerandoReporte = new System.Windows.Forms.Label();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.DGDatos)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(15, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(58, 22);
			this.label1.TabIndex = 12;
			this.label1.Text = "label1";
			// 
			// DGDatos
			// 
			this.DGDatos.AllowUserToAddRows = false;
			this.DGDatos.AllowUserToDeleteRows = false;
			this.DGDatos.AllowUserToResizeColumns = false;
			this.DGDatos.AllowUserToResizeRows = false;
			this.DGDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DGDatos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.DGDatos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.DGDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGDatos.Location = new System.Drawing.Point(12, 45);
			this.DGDatos.Name = "DGDatos";
			this.DGDatos.ReadOnly = true;
			this.DGDatos.RowHeadersVisible = false;
			this.DGDatos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.DGDatos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DGDatos.Size = new System.Drawing.Size(1346, 524);
			this.DGDatos.TabIndex = 11;
			this.DGDatos.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DGDatos_CellBeginEdit);
			this.DGDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDatos_CellDoubleClick);
			this.DGDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGDatos_CellFormatting);
			this.DGDatos.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DGDatos_DataError);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// DT
			// 
			this.DT.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.DT.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.DT.Location = new System.Drawing.Point(1139, 13);
			this.DT.Name = "DT";
			this.DT.Size = new System.Drawing.Size(96, 20);
			this.DT.TabIndex = 14;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(1057, 13);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(76, 22);
			this.label3.TabIndex = 12;
			this.label3.Text = "FECHA:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(645, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(138, 17);
			this.label2.TabIndex = 13;
			this.label2.Text = "MONITOR DE SPLIT";
			// 
			// BtnAcep
			// 
			this.BtnAcep.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BtnAcep.Image = global::Embarques.Properties.Resources.CHECKMRK;
			this.BtnAcep.Location = new System.Drawing.Point(1241, 11);
			this.BtnAcep.Name = "BtnAcep";
			this.BtnAcep.Size = new System.Drawing.Size(28, 22);
			this.BtnAcep.TabIndex = 15;
			this.BtnAcep.UseVisualStyleBackColor = true;
			this.BtnAcep.Click += new System.EventHandler(this.BtnAcep_Click);
			// 
			// lblGenerandoReporte
			// 
			this.lblGenerandoReporte.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lblGenerandoReporte.AutoSize = true;
			this.lblGenerandoReporte.BackColor = System.Drawing.SystemColors.AppWorkspace;
			this.lblGenerandoReporte.Font = new System.Drawing.Font("AmsiPro-Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGenerandoReporte.ForeColor = System.Drawing.Color.Blue;
			this.lblGenerandoReporte.Location = new System.Drawing.Point(542, 254);
			this.lblGenerandoReporte.Name = "lblGenerandoReporte";
			this.lblGenerandoReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblGenerandoReporte.Size = new System.Drawing.Size(286, 32);
			this.lblGenerandoReporte.TabIndex = 123;
			this.lblGenerandoReporte.Text = "Cargando Información......";
			this.lblGenerandoReporte.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblGenerandoReporte.Visible = false;
			// 
			// progressBar2
			// 
			this.progressBar2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar2.BackColor = System.Drawing.SystemColors.MenuHighlight;
			this.progressBar2.Location = new System.Drawing.Point(460, 289);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(451, 25);
			this.progressBar2.Step = 1;
			this.progressBar2.TabIndex = 122;
			this.progressBar2.Tag = "";
			this.progressBar2.Visible = false;
			// 
			// MonitorSplit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1370, 581);
			this.Controls.Add(this.lblGenerandoReporte);
			this.Controls.Add(this.progressBar2);
			this.Controls.Add(this.BtnAcep);
			this.Controls.Add(this.DT);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.DGDatos);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MonitorSplit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "MonitorSplit";
			this.Load += new System.EventHandler(this.MonitorSplit_Load);
			((System.ComponentModel.ISupportInitialize)(this.DGDatos)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGDatos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button BtnAcep;
        private System.Windows.Forms.DateTimePicker DT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblGenerandoReporte;
		private System.Windows.Forms.ProgressBar progressBar2;
	}
}
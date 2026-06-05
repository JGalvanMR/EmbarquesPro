namespace Embarques
{
    partial class ImprimirSplit
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			this.label4 = new System.Windows.Forms.Label();
			this.DGDatos = new System.Windows.Forms.DataGridView();
			this.Emb_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.split = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cajas = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.impreso = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Emergency = new System.Windows.Forms.Button();
			this.BtnImp = new System.Windows.Forms.Button();
			this.CodeQR = new System.Windows.Forms.PictureBox();
			this.Code128 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.DGDatos)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.CodeQR)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Code128)).BeginInit();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label4.Location = new System.Drawing.Point(73, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(307, 20);
			this.label4.TabIndex = 92;
			this.label4.Text = "LIBERACION DE SPLIT POR ARMADOR";
			// 
			// DGDatos
			// 
			this.DGDatos.AllowUserToAddRows = false;
			this.DGDatos.AllowUserToDeleteRows = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGDatos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.DGDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DGDatos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Emb_folio,
            this.split,
            this.cajas,
            this.estado,
            this.impreso});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.DGDatos.DefaultCellStyle = dataGridViewCellStyle2;
			this.DGDatos.Location = new System.Drawing.Point(12, 68);
			this.DGDatos.Name = "DGDatos";
			this.DGDatos.ReadOnly = true;
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.DGDatos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.DGDatos.Size = new System.Drawing.Size(459, 394);
			this.DGDatos.TabIndex = 90;
			this.DGDatos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDatos_CellDoubleClick);
			this.DGDatos.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGDatos_CellFormatting);
			// 
			// Emb_folio
			// 
			this.Emb_folio.HeaderText = "Emb_folio";
			this.Emb_folio.Name = "Emb_folio";
			this.Emb_folio.ReadOnly = true;
			// 
			// split
			// 
			this.split.HeaderText = "No. Split";
			this.split.Name = "split";
			this.split.ReadOnly = true;
			// 
			// cajas
			// 
			this.cajas.HeaderText = "No.Cajas";
			this.cajas.Name = "cajas";
			this.cajas.ReadOnly = true;
			// 
			// estado
			// 
			this.estado.HeaderText = "estado";
			this.estado.Name = "estado";
			this.estado.ReadOnly = true;
			// 
			// impreso
			// 
			this.impreso.HeaderText = "impreso";
			this.impreso.Name = "impreso";
			this.impreso.ReadOnly = true;
			this.impreso.Visible = false;
			// 
			// Emergency
			// 
			this.Emergency.Image = global::Embarques.Properties.Resources.botonImprimirEmergencia;
			this.Emergency.Location = new System.Drawing.Point(386, 6);
			this.Emergency.Name = "Emergency";
			this.Emergency.Size = new System.Drawing.Size(61, 56);
			this.Emergency.TabIndex = 91;
			this.Emergency.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.Emergency.UseVisualStyleBackColor = true;
			this.Emergency.Click += new System.EventHandler(this.Emergency_Click);
			// 
			// BtnImp
			// 
			this.BtnImp.Image = global::Embarques.Properties.Resources.botonImprimir;
			this.BtnImp.Location = new System.Drawing.Point(410, 6);
			this.BtnImp.Name = "BtnImp";
			this.BtnImp.Size = new System.Drawing.Size(61, 56);
			this.BtnImp.TabIndex = 91;
			this.BtnImp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.BtnImp.UseVisualStyleBackColor = true;
			this.BtnImp.Click += new System.EventHandler(this.BtnImp_Click);
			// 
			// CodeQR
			// 
			this.CodeQR.Location = new System.Drawing.Point(12, 6);
			this.CodeQR.Name = "CodeQR";
			this.CodeQR.Size = new System.Drawing.Size(98, 86);
			this.CodeQR.TabIndex = 93;
			this.CodeQR.TabStop = false;
			this.CodeQR.Visible = false;
			// 
			// Code128
			// 
			this.Code128.Location = new System.Drawing.Point(135, 6);
			this.Code128.Name = "Code128";
			this.Code128.Size = new System.Drawing.Size(98, 86);
			this.Code128.TabIndex = 93;
			this.Code128.TabStop = false;
			this.Code128.Visible = false;
			// 
			// ImprimirSplit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(483, 506);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.Emergency);
			this.Controls.Add(this.BtnImp);
			this.Controls.Add(this.DGDatos);
			this.Controls.Add(this.Code128);
			this.Controls.Add(this.CodeQR);
			this.Name = "ImprimirSplit";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ImprimirSplit";
			this.Load += new System.EventHandler(this.ImprimirSplit_Load);
			((System.ComponentModel.ISupportInitialize)(this.DGDatos)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.CodeQR)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Code128)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BtnImp;
        private System.Windows.Forms.DataGridView DGDatos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Emb_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn split;
        private System.Windows.Forms.DataGridViewTextBoxColumn cajas;
        private System.Windows.Forms.DataGridViewTextBoxColumn estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn impreso;
        private System.Windows.Forms.Button Emergency;
        private System.Windows.Forms.PictureBox CodeQR;
        private System.Windows.Forms.PictureBox Code128;
    }
}
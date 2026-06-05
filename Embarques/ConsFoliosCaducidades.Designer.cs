namespace Embarques
{
    partial class ConsFoliosCaducidades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsFoliosCaducidades));
            this.DGDatos = new System.Windows.Forms.DataGridView();
            this.LblPed = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDatos
            // 
            this.DGDatos.AllowUserToAddRows = false;
            this.DGDatos.AllowUserToDeleteRows = false;
            this.DGDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDatos.DefaultCellStyle = dataGridViewCellStyle1;
            this.DGDatos.Location = new System.Drawing.Point(12, 69);
            this.DGDatos.Name = "DGDatos";
            this.DGDatos.ReadOnly = true;
            this.DGDatos.RowHeadersVisible = false;
            this.DGDatos.Size = new System.Drawing.Size(920, 493);
            this.DGDatos.TabIndex = 23;
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(12, 33);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(124, 24);
            this.LblPed.TabIndex = 24;
            this.LblPed.Text = "PRODUCTO";
            // 
            // ConsFoliosCaducidades
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 569);
            this.Controls.Add(this.LblPed);
            this.Controls.Add(this.DGDatos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsFoliosCaducidades";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Folios con Fecha de Caducidad";
            this.Load += new System.EventHandler(this.ConsFoliosCaducidades_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDatos;
        private System.Windows.Forms.Label LblPed;
    }
}
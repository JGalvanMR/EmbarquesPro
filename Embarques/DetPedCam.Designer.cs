namespace Embarques
{
    partial class DetPedCam
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LblPed = new System.Windows.Forms.Label();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.LblDetPed = new System.Windows.Forms.Label();
            this.DGDetPed = new System.Windows.Forms.DataGridView();
            this.PEDCAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMPED = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMCLI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMNOM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMCAN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAMSUR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORCE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).BeginInit();
            this.SuspendLayout();
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(151, 9);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(103, 26);
            this.LblPed.TabIndex = 32;
            this.LblPed.Text = "PEDIDO";
            // 
            // Logo
            // 
            this.Logo.Image = global::Embarques.Properties.Resources.logo;
            this.Logo.Location = new System.Drawing.Point(12, 9);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(82, 86);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabIndex = 38;
            this.Logo.TabStop = false;
            // 
            // LblDetPed
            // 
            this.LblDetPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblDetPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblDetPed.Location = new System.Drawing.Point(126, 42);
            this.LblDetPed.Name = "LblDetPed";
            this.LblDetPed.Size = new System.Drawing.Size(402, 58);
            this.LblDetPed.TabIndex = 37;
            this.LblDetPed.Text = "PEDIDO";
            // 
            // DGDetPed
            // 
            this.DGDetPed.AllowUserToAddRows = false;
            this.DGDetPed.AllowUserToDeleteRows = false;
            this.DGDetPed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetPed.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CAMPED,
            this.CAMCLI,
            this.CAMNOM,
            this.CAMCAN,
            this.CAMSUR,
            this.PORCE});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDetPed.DefaultCellStyle = dataGridViewCellStyle4;
            this.DGDetPed.Location = new System.Drawing.Point(11, 102);
            this.DGDetPed.Name = "DGDetPed";
            this.DGDetPed.ReadOnly = true;
            this.DGDetPed.RowHeadersVisible = false;
            this.DGDetPed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGDetPed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetPed.Size = new System.Drawing.Size(811, 495);
            this.DGDetPed.TabIndex = 39;
            this.DGDetPed.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDetPed_CellDoubleClick);
            // 
            // PEDCAM
            // 
            this.PEDCAM.HeaderText = "PEDIDO";
            this.PEDCAM.Name = "PEDCAM";
            // 
            // CAMPED
            // 
            this.CAMPED.HeaderText = "PEDIDO";
            this.CAMPED.Name = "CAMPED";
            this.CAMPED.ReadOnly = true;
            // 
            // CAMCLI
            // 
            this.CAMCLI.HeaderText = "CLIENTE";
            this.CAMCLI.Name = "CAMCLI";
            this.CAMCLI.ReadOnly = true;
            // 
            // CAMNOM
            // 
            this.CAMNOM.HeaderText = "NOMBRE";
            this.CAMNOM.Name = "CAMNOM";
            this.CAMNOM.ReadOnly = true;
            this.CAMNOM.Width = 380;
            // 
            // CAMCAN
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CAMCAN.DefaultCellStyle = dataGridViewCellStyle1;
            this.CAMCAN.HeaderText = "CANTIDAD";
            this.CAMCAN.Name = "CAMCAN";
            this.CAMCAN.ReadOnly = true;
            this.CAMCAN.Width = 70;
            // 
            // CAMSUR
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CAMSUR.DefaultCellStyle = dataGridViewCellStyle2;
            this.CAMSUR.HeaderText = "SURTIDO";
            this.CAMSUR.Name = "CAMSUR";
            this.CAMSUR.ReadOnly = true;
            this.CAMSUR.Width = 70;
            // 
            // PORCE
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PORCE.DefaultCellStyle = dataGridViewCellStyle3;
            this.PORCE.HeaderText = "PORCE";
            this.PORCE.Name = "PORCE";
            this.PORCE.ReadOnly = true;
            this.PORCE.Width = 60;
            // 
            // DetPedCam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 603);
            this.Controls.Add(this.DGDetPed);
            this.Controls.Add(this.Logo);
            this.Controls.Add(this.LblDetPed);
            this.Controls.Add(this.LblPed);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetPedCam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETALLE DE PEDIDOS X CAMIONETA";
            this.Load += new System.EventHandler(this.DetPedCam_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetPed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.Label LblDetPed;
        private System.Windows.Forms.DataGridView DGDetPed;
        private System.Windows.Forms.DataGridViewTextBoxColumn PEDCAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMPED;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMCLI;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMNOM;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMCAN;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAMSUR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PORCE;
    }
}
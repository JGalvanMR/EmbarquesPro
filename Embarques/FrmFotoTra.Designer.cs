namespace Embarques
{
    partial class FrmFotoTra
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFotoTra));
            this.LblPed = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LblImg1 = new System.Windows.Forms.Label();
            this.LblImg2 = new System.Windows.Forms.Label();
            this.LblImg3 = new System.Windows.Forms.Label();
            this.Pbx1 = new System.Windows.Forms.PictureBox();
            this.Pbx2 = new System.Windows.Forms.PictureBox();
            this.Pbx3 = new System.Windows.Forms.PictureBox();
            this.Save1 = new System.Windows.Forms.PictureBox();
            this.Save2 = new System.Windows.Forms.PictureBox();
            this.Save3 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.ImgZoom = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgZoom)).BeginInit();
            this.SuspendLayout();
            // 
            // LblPed
            // 
            this.LblPed.AutoSize = true;
            this.LblPed.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPed.ForeColor = System.Drawing.Color.DarkBlue;
            this.LblPed.Location = new System.Drawing.Point(13, 28);
            this.LblPed.Name = "LblPed";
            this.LblPed.Size = new System.Drawing.Size(65, 18);
            this.LblPed.TabIndex = 25;
            this.LblPed.Text = "PLACA:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(354, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 18);
            this.label1.TabIndex = 26;
            this.label1.Text = "TEMPERATURA:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(197, 292);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 18);
            this.label2.TabIndex = 27;
            this.label2.Text = "TRANSPORTE:";
            // 
            // LblImg1
            // 
            this.LblImg1.AutoSize = true;
            this.LblImg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImg1.ForeColor = System.Drawing.Color.Black;
            this.LblImg1.Location = new System.Drawing.Point(92, 36);
            this.LblImg1.Name = "LblImg1";
            this.LblImg1.Size = new System.Drawing.Size(26, 9);
            this.LblImg1.TabIndex = 28;
            this.LblImg1.Text = "Img1";
            // 
            // LblImg2
            // 
            this.LblImg2.AutoSize = true;
            this.LblImg2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImg2.ForeColor = System.Drawing.Color.Black;
            this.LblImg2.Location = new System.Drawing.Point(491, 36);
            this.LblImg2.Name = "LblImg2";
            this.LblImg2.Size = new System.Drawing.Size(26, 9);
            this.LblImg2.TabIndex = 29;
            this.LblImg2.Text = "Img2";
            // 
            // LblImg3
            // 
            this.LblImg3.AutoSize = true;
            this.LblImg3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblImg3.ForeColor = System.Drawing.Color.Black;
            this.LblImg3.Location = new System.Drawing.Point(323, 298);
            this.LblImg3.Name = "LblImg3";
            this.LblImg3.Size = new System.Drawing.Size(26, 9);
            this.LblImg3.TabIndex = 30;
            this.LblImg3.Text = "Img3";
            // 
            // Pbx1
            // 
            this.Pbx1.Location = new System.Drawing.Point(12, 48);
            this.Pbx1.Name = "Pbx1";
            this.Pbx1.Size = new System.Drawing.Size(294, 225);
            this.Pbx1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pbx1.TabIndex = 31;
            this.Pbx1.TabStop = false;
            this.toolTip1.SetToolTip(this.Pbx1, "Doble click para Maximizar");
            this.Pbx1.DoubleClick += new System.EventHandler(this.Pbx1_DoubleClick);
            // 
            // Pbx2
            // 
            this.Pbx2.Location = new System.Drawing.Point(357, 48);
            this.Pbx2.Name = "Pbx2";
            this.Pbx2.Size = new System.Drawing.Size(294, 225);
            this.Pbx2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pbx2.TabIndex = 32;
            this.Pbx2.TabStop = false;
            this.toolTip1.SetToolTip(this.Pbx2, "Doble click para Maximizar");
            this.Pbx2.DoubleClick += new System.EventHandler(this.Pbx2_DoubleClick);
            // 
            // Pbx3
            // 
            this.Pbx3.Location = new System.Drawing.Point(204, 312);
            this.Pbx3.Name = "Pbx3";
            this.Pbx3.Size = new System.Drawing.Size(294, 225);
            this.Pbx3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Pbx3.TabIndex = 33;
            this.Pbx3.TabStop = false;
            this.toolTip1.SetToolTip(this.Pbx3, "Doble click para Maximizar");
            this.Pbx3.DoubleClick += new System.EventHandler(this.Pbx3_DoubleClick);
            // 
            // Save1
            // 
            this.Save1.Image = global::Embarques.Properties.Resources.BtnGuardarMini;
            this.Save1.Location = new System.Drawing.Point(12, 276);
            this.Save1.Name = "Save1";
            this.Save1.Size = new System.Drawing.Size(32, 33);
            this.Save1.TabIndex = 34;
            this.Save1.TabStop = false;
            this.toolTip1.SetToolTip(this.Save1, "Click para Guardar la Imagen");
            this.Save1.Click += new System.EventHandler(this.Save1_Click);
            // 
            // Save2
            // 
            this.Save2.Image = global::Embarques.Properties.Resources.BtnGuardarMini;
            this.Save2.Location = new System.Drawing.Point(619, 277);
            this.Save2.Name = "Save2";
            this.Save2.Size = new System.Drawing.Size(32, 33);
            this.Save2.TabIndex = 35;
            this.Save2.TabStop = false;
            this.toolTip1.SetToolTip(this.Save2, "Click para Guardar la Imagen");
            this.Save2.Click += new System.EventHandler(this.Save2_Click);
            // 
            // Save3
            // 
            this.Save3.Image = global::Embarques.Properties.Resources.BtnGuardarMini;
            this.Save3.Location = new System.Drawing.Point(502, 504);
            this.Save3.Name = "Save3";
            this.Save3.Size = new System.Drawing.Size(32, 33);
            this.Save3.TabIndex = 36;
            this.Save3.TabStop = false;
            this.toolTip1.SetToolTip(this.Save3, "Click para Guardar la Imagen");
            this.Save3.Click += new System.EventHandler(this.Save3_Click);
            // 
            // ImgZoom
            // 
            this.ImgZoom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ImgZoom.Location = new System.Drawing.Point(12, 49);
            this.ImgZoom.Name = "ImgZoom";
            this.ImgZoom.Size = new System.Drawing.Size(138, 96);
            this.ImgZoom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ImgZoom.TabIndex = 37;
            this.ImgZoom.TabStop = false;
            this.toolTip1.SetToolTip(this.ImgZoom, "Doble click para cerrar");
            this.ImgZoom.Visible = false;
            this.ImgZoom.DoubleClick += new System.EventHandler(this.ImgZoom_DoubleClick);
            // 
            // FrmFotoTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 558);
            this.Controls.Add(this.ImgZoom);
            this.Controls.Add(this.Save3);
            this.Controls.Add(this.Save2);
            this.Controls.Add(this.Save1);
            this.Controls.Add(this.Pbx3);
            this.Controls.Add(this.Pbx2);
            this.Controls.Add(this.Pbx1);
            this.Controls.Add(this.LblImg3);
            this.Controls.Add(this.LblImg2);
            this.Controls.Add(this.LblImg1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LblPed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFotoTra";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imagenes del Transporte";
            this.Load += new System.EventHandler(this.FrmFotoTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Pbx1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pbx3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Save3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImgZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblPed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LblImg1;
        private System.Windows.Forms.Label LblImg2;
        private System.Windows.Forms.Label LblImg3;
        private System.Windows.Forms.PictureBox Pbx1;
        private System.Windows.Forms.PictureBox Pbx2;
        private System.Windows.Forms.PictureBox Pbx3;
        private System.Windows.Forms.PictureBox Save1;
        private System.Windows.Forms.PictureBox Save2;
        private System.Windows.Forms.PictureBox Save3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox ImgZoom;
    }
}
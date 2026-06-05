namespace Embarques
{
    partial class Auth
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Auth));
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.textemb = new System.Windows.Forms.TextBox();
			this.emba = new System.Windows.Forms.Label();
			this.pass = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(190, 75);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Cancelar";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(109, 75);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Ok";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// textemb
			// 
			this.textemb.Location = new System.Drawing.Point(84, 49);
			this.textemb.Name = "textemb";
			this.textemb.Size = new System.Drawing.Size(181, 20);
			this.textemb.TabIndex = 6;
			// 
			// emba
			// 
			this.emba.AutoSize = true;
			this.emba.Location = new System.Drawing.Point(14, 55);
			this.emba.Name = "emba";
			this.emba.Size = new System.Drawing.Size(58, 13);
			this.emba.TabIndex = 3;
			this.emba.Text = "Embarque:";
			// 
			// pass
			// 
			this.pass.Location = new System.Drawing.Point(84, 12);
			this.pass.Name = "pass";
			this.pass.PasswordChar = '*';
			this.pass.Size = new System.Drawing.Size(181, 20);
			this.pass.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Contraseña:";
			// 
			// Auth
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(275, 112);
			this.ControlBox = false;
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textemb);
			this.Controls.Add(this.emba);
			this.Controls.Add(this.pass);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Auth";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Auth";
			this.Load += new System.EventHandler(this.Auth_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textemb;
        private System.Windows.Forms.Label emba;
        private System.Windows.Forms.TextBox pass;
        private System.Windows.Forms.Label label1;
    }
}
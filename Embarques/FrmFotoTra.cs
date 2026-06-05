using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
///using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Embarques
{
    public partial class FrmFotoTra : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        public FrmFotoTra()
        {
            InitializeComponent();
        }

        private void FrmFotoTra_Load(object sender, EventArgs e)
        {
            thisConnecion.Open();
            string Cadena = "SELECT * FROM TB_FOTOS_TRAILER WHERE FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                LblImg1.Text = Info["FOTO_PLACA"].ToString().Trim();
                if (File.Exists(LblImg1.Text))
                    Pbx1.ImageLocation = LblImg1.Text; 
                LblImg2.Text = Info["FOTO_TEMP"].ToString().Trim();
                if (File.Exists(LblImg2.Text))
                    Pbx2.ImageLocation = LblImg2.Text; 
                LblImg3.Text = Info["FOTO_TRANS"].ToString().Trim();
                if (File.Exists(LblImg3.Text))
                    Pbx3.ImageLocation = LblImg3.Text; 
            }
            thisConnecion.Close();
        }

        private void Save1_Click(object sender, EventArgs e)
        {
            Copiar("ImgPla", LblImg1.Text.Trim());
        }

        private void Save2_Click(object sender, EventArgs e)
        {
            Copiar("ImgTemp", LblImg2.Text.Trim());
        }

        private void Save3_Click(object sender, EventArgs e)
        {
            Copiar("ImgTrans", LblImg3.Text.Trim());
        }

        private void Copiar(string Ima, string Arch)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();

            SaveFile.Filter = "Jpg File (*.jpg)|*.jpg";
            SaveFile.FileName = Ima;
            if (SaveFile.ShowDialog() == DialogResult.OK)
                File.Copy(Arch, SaveFile.FileName.ToString()); 
        }

        private void Pbx2_DoubleClick(object sender, EventArgs e)
        {
            ZoomImg(LblImg2.Text);
        }

        private void ImgZoom_DoubleClick(object sender, EventArgs e)
        {
            ImgZoom.Height = 242;
            ImgZoom.Width = 306;
            ImgZoom.Visible = false;
        }

        private void Pbx1_DoubleClick(object sender, EventArgs e)
        {
            ZoomImg(LblImg1.Text);
        }

        private void Pbx3_DoubleClick(object sender, EventArgs e)
        {
            ZoomImg(LblImg3.Text);
        }

        private void ZoomImg(string Arch)
        {
            ImgZoom.Location = new Point(12, 12);
            ImgZoom.Height = 530;
            ImgZoom.Width = 680;
            ImgZoom.ImageLocation = Arch;
            ImgZoom.Visible = true;
        }
       
    }
}

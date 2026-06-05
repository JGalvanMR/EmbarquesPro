using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Embarques
{
    public partial class FrmCapPesoBas : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        public FrmCapPesoBas()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }
        public string Tipo;
        

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            thisConnecion.Open();
            //thisConnecionDBGAB.Open();
            string Cadena = "Update TB_MSTR_TRAILER SET PESOBASCULA = '" + TxtPeso.Text + "'" +      
                            " WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion; //DBGAB
            cmd.ExecuteNonQuery();
            MessageBox.Show("DATOS GRABADOS!!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BtnGrabar.Enabled = false;
            thisConnecion.Close();
            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "M", "7.1", TxtPlaca.Text, TxtCho.Text + " " + TxtPlaca.Text + " " + TxtPeso.Text, "SIPGAB");
            //thisConnecionDBGAB.Close();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void FrmCapPesoBas_Load(object sender, EventArgs e)
        {
            if (Tipo == "CAMIONETAS")
            {
                getCamioneta();
            }
            else
            {
                getTrailer();
            }
        }

        private void getTrailer()
        {
            string mfec = Program.MyGlobal.PubFecEmb;
            DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
            thisConnecion.Open();
            string Cadena = "SELECT HORAREGVIG,HORAENT,CHOFER,DESTINO,TRANSPORTE,NO_TRAILER,PLACA,PESOBASCULA FROM TB_MSTR_TRAILER WHERE FECHA = '" + mfec + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "' ORDER BY FECHA,CONSE ";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                TxtCho.Text = Info["Chofer"].ToString();
                TxtDest.Text = Info["Destino"].ToString();
                TxtHrEnt.Text = Info["HoraEnt"].ToString();
                TxtHrLlego.Text = Info["HoraRegVig"].ToString();
                TxtPlaca.Text = Info["NO_TRAILER"].ToString();
                TxtTrans.Text = Info["Transporte"].ToString();
                TxtPeso.Text = Info["PESOBASCULA"].ToString();
                txtPlacaCaja.Text = Info["PLACA"].ToString();
            }
            thisConnecion.Close();
        }

        private void getCamioneta()
        {
            label15.Text = "PLACA CAMIONETA";
            string mfec = Program.MyGlobal.PubFecEmb;
            DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
            thisConnecion.Open();
            string Cadena = "SELECT SUBSTRING(A.hora_trailer,12,24) AS HORATRAILER,SUBSTRING(A.HORAINI,12,24) AS HORAINI,A.CHOFER,A.DESTINO,A.TRANSPORTE,A.NO_TRAILER,B.PLACAS,A.PESOBASCULA FROM TB_MSTR_TRAILER A INNER JOIN tb_cat_vehiculos B ON A.no_trailer = B.clave WHERE FECHA = '" + mfec + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "' ORDER BY FECHA,CONSE ";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                TxtCho.Text = Info["CHOFER"].ToString().Trim();
                TxtDest.Text = Info["DESTINO"].ToString().Trim();
                TxtHrEnt.Text = Info["HORATRAILER"].ToString().Trim();
                TxtHrLlego.Text = Info["HORAINI"].ToString().Trim();
                TxtPlaca.Text = Info["NO_TRAILER"].ToString().Trim();
                TxtTrans.Text = Info["TRANSPORTE"].ToString().Trim();
                TxtPeso.Text = Info["PESOBASCULA"].ToString().Trim();
                txtPlacaCaja.Text = Info["PLACAS"].ToString();
            }
            thisConnecion.Close();
        }
    }
}

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
    public partial class FrmCapPntRev : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        public FrmCapPntRev()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCapPntRev_Load(object sender, EventArgs e)
        {
            string mfec = Program.MyGlobal.PubFecEmb;
            DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
            thisConnecion.Open();
            string Cadena = "SELECT HORAREGVIG,HORAENT,CHOFER,DESTINO,TRANSPORTE,NO_TRAILER,PLACA,concepto5a, concepto5b, concepto5c, concepto5d, concepto5e, concepto5f, concepto5g, concepto5h, concepto5l, pesoEje1, pesoEje2, pesoEje3 FROM TB_MSTR_TRAILER WHERE FECHA = '" + mfec + "' AND NO_TRAILER = '"+ Program.MyGlobal.PubNoTrailer +"' ORDER BY FECHA,CONSE ";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            
            while (Info.Read())
            {
                TxtCho.Text = Info["Chofer"].ToString();
                TxtDest.Text = Info["Destino"].ToString();
                TxtHrEnt.Text=Info["HoraEnt"].ToString();
                TxtHrLlego.Text = Info["HoraRegVig"].ToString();
                TxtPlaca.Text = Info["PLACA"].ToString();
                TxtTrans.Text = Info["Transporte"].ToString();
                Cmb5a.SelectedIndex = Opc(Info["Concepto5a"].ToString());
                Cmb5b.SelectedIndex = Opc(Info["Concepto5b"].ToString());
                Cmb5c.SelectedIndex = Opc(Info["Concepto5c"].ToString());
                Cmb5d.SelectedIndex = Opc(Info["Concepto5d"].ToString());
                Cmb5e.SelectedIndex = Opc(Info["Concepto5e"].ToString());
                Cmb5f.SelectedIndex = Opc(Info["Concepto5f"].ToString());
                Cmb5g.SelectedIndex = Opc(Info["Concepto5g"].ToString());
                Cmb5h.SelectedIndex = Opc(Info["Concepto5h"].ToString());
                Cmb5i.SelectedIndex = Opc(Info["Concepto5L"].ToString());
                txtp1.Text = Info["pesoEje1"].ToString();
                txtp2.Text = Info["pesoEje2"].ToString();
                txtp3.Text = Info["pesoEje3"].ToString();
            }
            thisConnecion.Close();

            if (txtp1.Text.Trim().Length == 0)
            {
                txtp1.Text = "0";
            }
            if (txtp2.Text.Trim().Length == 0)
            {
                txtp2.Text = "0";
            }
            if (txtp3.Text.Trim().Length == 0)
            {
                txtp3.Text = "0";
            }
            
        }

        private int Opc(string Cad)
        {
            int Sel = 0;
            if (Cad.Trim() == "BIEN")
                Sel = 0;
            else
                if (Cad.Trim() == "MAL")
                    Sel = 1;
                else
                    Sel = 2;
            return Sel;
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            thisConnecion.Open();
            //thisConnecionDBGAB.Open();
            string Cadena = "Update TB_MSTR_TRAILER SET CONCEPTO5A = '" + Cmb5a.Text + "', CONCEPTO5B = '" + Cmb5b.Text + "', CONCEPTO5C = '" + Cmb5c.Text + "', CONCEPTO5D = '" + Cmb5d.Text + "', CONCEPTO5E = '" + Cmb5e.Text + "'," +
                            "CONCEPTO5F = '" + Cmb5f.Text + "',CONCEPTO5G = '" + Cmb5g.Text + "',CONCEPTO5H = '" + Cmb5h.Text + "',CONCEPTO5L = '" + Cmb5i.Text + "'" +
                            ",pesoEje1 = '" + txtp1.Text + "',pesoEje2 = '" + txtp2.Text + "',pesoEje3 = '" + txtp3.Text + "' WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
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
            //thisConnecionDBGAB.Close();
            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "7.1", TxtPlaca.Text, TxtCho.Text + " " + TxtPlaca.Text, "SIPGAB");
        }

        
    }
}

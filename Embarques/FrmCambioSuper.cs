using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Embarques
{
    public partial class FrmCambioSuper : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        int AndenOri = 0;

        public FrmCambioSuper()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmCambioSuper_Load(object sender, EventArgs e)
        {
            string mfec = Program.MyGlobal.PubFecEmb;
            DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
            thisConnecion.Open();
            string Cadena = "SELECT TURNO,RESPONSABLE,CHOFER,DESTINO,NO_TRAILER,HORAINI,ANDEN FROM TB_MSTR_TRAILER WHERE FECHA = '" + mfec + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "' ORDER BY FECHA,CONSE ";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                TxtCho.Text = Info["Chofer"].ToString();
                TxtDest.Text = Info["Destino"].ToString();
                TxtHrIniCar.Text = Info["HoraIni"].ToString();
                TxtTurno.Text = Info["Turno"].ToString();
                TxtPlaca.Text = Info["no_trailer"].ToString();
                TxtRespon.Text = Info["responsable"].ToString();
                NumUpAnden.Value = Convert.ToInt16(Info["Anden"].ToString());
                AndenOri = Convert.ToInt16(Info["Anden"].ToString());
            }
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("Select NOMBRE, NOMCORTO from TB_RESPONSABLE WHERE ESTATUS = 'A' order by NOMBRE  ", thisConnecion);
            da.Fill(ds, "Ranchos");
            CmbRes.DataSource = ds.Tables[0].DefaultView;
            CmbRes.ValueMember = "NOMCORTO";
            CmbRes.DisplayMember = "NOMBRE";
            CmbRes.Text = TxtRespon.Text;
            thisConnecion.Close();
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            thisConnecion.Open();
            if (NumUpAnden.Value != AndenOri)
            {
                string ActAnden = ValidaAnden(NumUpAnden.Value.ToString());
                if (ActAnden == "S")
                {
                    MessageBox.Show("Anden se Encuentra Ocupado!!" + NumUpAnden.Value.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    thisConnecion.Close();
                    return;
                }
            }
            //thisConnecionDBGAB.Open();
            string Cadena = "Update TB_MSTR_TRAILER SET TURNO = '" + NumUp.Value.ToString().Trim() + "', RESPONSABLE = '" + CmbRes.Text + "', ANDEN = '"+ NumUpAnden.Value.ToString() +"'" +
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
            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "M", "7.1", "Cambio de Supervisor "+ CmbRes.Text.Trim() +  TxtPlaca.Text, "Turno " + TxtTurno.Text  , "SIPGAB");
        }

        private string ValidaAnden(string Anden)
        {
            String cad = "N";
            String Cadena = "Select Anden From tb_mstr_trailer Where fecha = '" + Program.MyGlobal.PubFecEmb +
                            "' and horafin = '--:--' and horaini <> '--:--' order by anden";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            DataSet ds = new DataSet();
            da.Fill(ds, "Andenes");
            DataTable Andenes = ds.Tables["Andenes"];
            foreach (DataRow row in Andenes.Select("Anden = '" + Anden + "'"))
                cad = "S";
            return cad;
        }
    }
}

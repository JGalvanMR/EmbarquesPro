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
    public partial class Auth : Form
    {
        SqlConnection thisConnection = new SqlConnection(Utilerias.Class1.ConnectionString);
        SqlCommand cmnd2;
        SqlDataReader reader2;
        public static string usuario;
        public int OPCION = 0;

        public Auth()
        {
            InitializeComponent();
        }

            
        private void button1_Click_1(object sender, EventArgs e)
        {
            usuario = "";
            string clave = "";
            thisConnection.Open();
            cmnd2 = thisConnection.CreateCommand();
            cmnd2.CommandText = "SELECT TOP 1 usuario, clave  FROM Tb_Autoriza_Reetiquetado where password = '" + pass.Text.Trim() + "' and obs = 'A' And clave = '03'";
            reader2 = cmnd2.ExecuteReader();
            while (reader2.Read())
            {
                usuario = reader2.GetSqlString(0).Value.ToString();
                clave = reader2.GetSqlString(1).Value.ToString();
            }
            reader2.Close();
            thisConnection.Close();

           
            if (usuario.Trim() != "")
            {
                string embarque = textemb.Text;
                this.Visible = false;
                ImprimirSplit FrReIm = new ImprimirSplit(usuario, embarque);
                FrReIm.ShowDialog(this);
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña Incorrecta, Vuelva a intentarlo");
                pass.Text = "";
            }
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void Auth_Load(object sender, EventArgs e)
		{

		}
	}
}

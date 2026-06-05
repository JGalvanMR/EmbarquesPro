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
    public partial class Autoriza : Form
    {
        SqlConnection thisConnection = new SqlConnection(Utilerias.Class1.ConnectionString);
        SqlCommand cmnd2;
        SqlDataReader reader2;
        public static string usuario;
        public int OPCION = 0;

        public Autoriza()
        {
            InitializeComponent();
        }

            
        private void button1_Click_1(object sender, EventArgs e)
        {

            if (pass.Text.Trim() == "santi" || pass.Text.Trim() == "AHDZ" || pass.Text.Trim() == "LOGISTICA" || pass.Text.Trim() == "ISW")
            {
                MonitorSplit ConsPed = new MonitorSplit();
                ConsPed.ShowDialog(this);
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

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
    public partial class ConsulPedPen : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DetPed = new DataTable();
        DataTable DetPed2 = new DataTable();
        public DataTable Inven = new DataTable();
        DataTable Pedidos = new DataTable();
        public string Opcion = "";
        string MTip = "";
            
        public ConsulPedPen()
        {
            InitializeComponent();
        }

        private void ConsulPedPen_Load(object sender, EventArgs e)
        {
            thisConnecion.Open();
            //string Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,B.prod_clave,B.pdn_num_unidades FROM TB_MSTR_PEDIDOS_NAL A, tb_det_pedidos B WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "' AND A.PDN_FOLIO = B.PDN_FOLIO AND A.PDN_TIPO = B.PDN_TIPO " +
            //                "UNION " +
            //                "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,B.prod_clave,B.pdn_num_unidades FROM TB_MSTR_PEDIDOS_NAL A, tb_det_pedidos B WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "' AND A.PDN_FOLIO = B.PDN_FOLIO AND A.PDN_TIPO = B.PDN_TIPO " +
            //                "ORDER BY PDN_FOLIO";
            string Cadena = "";
            if (Opcion == "")
              Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO,PDN_TIPO FROM TB_MSTR_PEDIDOS_NAL A WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "'" +
                            "UNION " +
                            "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO,PDN_TIPO FROM TB_MSTR_PEDIDOS_EXP A WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "'" +
                            "ORDER BY PDN_FOLIO";
            else
                Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO,PDN_TIPO FROM TB_MSTR_PEDIDOS_NAL A WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.pdn_folio = '" + Program.MyGlobal.PubNoTrailer + "'" +
                            "UNION " +
                            "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO,PDN_TIPO FROM TB_MSTR_PEDIDOS_EXP A WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.pdn_folio = '" + Program.MyGlobal.PubNoTrailer + "'" +
                            "ORDER BY PDN_FOLIO";
            //string Cadena = "SELECT EMB_FOLIO FROM TB_MSTR_EMBARQUE WHERE HORA_TRAILER = '" + Program.MyGlobal.PubFecEmb + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "' ORDER BY EMB_FOLIO";
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "Pedi");
            Pedidos = ds1.Tables["Pedi"];
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            int T = 0;
            if (Info.HasRows == false)
            {
                MessageBox.Show("NO ENCONTRE INFORMACION PARA ESTE PEDIDO", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            while (Info.Read())
            {
                CmbPed.Items.Add(Info["pdn_folio"].ToString());
                T++;
            }
            CmbPed.SelectedIndex = 0;
            string Mped = CmbPed.Text.ToString();
            MTip = "";
            LblPed.Text = "Pedido: " + Mped;
            foreach (DataRow Row in Pedidos.Select("Pdn_Folio = '" + Mped + "'"))
            {
                LblDetPed.Text = "Cliente: " + Row["Cnte_Clave"].ToString() + " " + Fn_TraeNomCli(Row["Cnte_Clave"].ToString()) + " " + System.Environment.NewLine +
                                 Row["pdn_observacion"].ToString().Trim() + System.Environment.NewLine +
                                 Row["pdn_pedsigma"].ToString().Trim() + System.Environment.NewLine +
                                 "Elaboro: " + Row["pdn_ELABORO"].ToString().Trim();
                MTip = Row["PDN_TIPO"].ToString();
            }
            thisConnecion.Close();
            CreaTable();
            if (T == 1)
            {
                CmbPed.Enabled = false;
                LLenaPed(Mped, MTip);
            }
            else
            {
                BtnProdPen_Click(sender, e);
            }
            //DGDetEmbCap.DataSource = DetEmb;
            
        }

        private void LLenaPed(string pedido, string tipo)
        {
            thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            DetPed.Rows.Clear();
            string Cadena = "";
            //Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + pedido + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + pedido + "' AND A.PROD_CLAVE = B.PROD_CLAVE and pdn_tipo = '"+ tipo +"' ORDER BY B.PROD_NOMBRE";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                //foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]) - Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                    T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]);
                    //T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                    TotS = TotS + T; // Convert.ToInt32(row["SURTIDO"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                DetPed.Rows.Add(Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]), T);
            }
            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("#,###");
            TxtTotS.Text = TotS.ToString("#,###");
            DGDetPed.DataSource = DetPed;
            FormatoSalida();
            LblPed.Text = "Pedido: " + CmbPed.Text.ToString();
        }

        private void anterio()
        {
            CreaTable();
            LblPed.Text = "PEDIDO " + Program.MyGlobal.NoPedido;
            string Cadena = "";
            
            thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Program.MyGlobal.NoPedido.ToString() + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                //foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]);
                    TotS = TotS + T; // Convert.ToInt32(row["SURTIDO"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                DetPed.Rows.Add(Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]), T);
            }
            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("###,###");
            TxtTotS.Text = TotS.ToString("###,###");
            DGDetPed.DataSource = DetPed;
            FormatoSalida();
        }

        private void CreaTable()
        {
            DetPed.Columns.Add("PROD_CLAVE", typeof(string));     //0
            DetPed.Columns.Add("PROD_NOMBRE", typeof(string));      //1
            DetPed.Columns.Add("PEDIDO", typeof(Int32));       //2
            DetPed.Columns.Add("EXISTENCIA", typeof(Int32));    //3
            DetPed2.Columns.Add("FOLIO", typeof(string));     //0
            DetPed2.Columns.Add("PROD_CLAVE", typeof(string));     //0
            DetPed2.Columns.Add("PROD_NOMBRE", typeof(string));      //1
            DetPed2.Columns.Add("PEDIDO", typeof(Int32));       //2
            DetPed2.Columns.Add("EXISTENCIA", typeof(Int32));    //3
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToInt32(DetPed.Rows[i]["PEDIDO"]) >= Convert.ToInt32(DetPed.Rows[i]["EXISTENCIA"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            }
            DGDetPed.Columns[0].HeaderText = "PRODUCTO";
            DGDetPed.Columns[1].HeaderText = "NOMBRE";
            DGDetPed.Columns[0].Width = 120;
            DGDetPed.Columns[1].Width = 500;
            DGDetPed.Columns[2].Width = 70;
            DGDetPed.Columns[3].Width = 80;
            DGDetPed.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[2].DefaultCellStyle.Format = "###,###";
            DGDetPed.Columns[3].DefaultCellStyle.Format = "###,###";
       }

        private void FormatoSalida2()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToInt32(DetPed2.Rows[i]["PEDIDO"]) > Convert.ToInt32(DetPed2.Rows[i]["EXISTENCIA"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            }
            DGDetPed.Columns[0].HeaderText = "FOLIO";
            DGDetPed.Columns[1].HeaderText = "PRODUCTO";
            DGDetPed.Columns[2].HeaderText = "NOMBRE";
            DGDetPed.Columns[0].Width = 65;
            DGDetPed.Columns[1].Width = 120;
            DGDetPed.Columns[2].Width = 450;
            DGDetPed.Columns[3].Width = 65;
            DGDetPed.Columns[4].Width = 73;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[3].DefaultCellStyle.Format = "###,###";
            DGDetPed.Columns[4].DefaultCellStyle.Format = "###,###";
        }

        private void BtnAce_Click(object sender, EventArgs e)
        {
            LblProdPen.Visible = false;
            string mtip = "";
            foreach (DataRow Row in Pedidos.Select("Pdn_Folio = '" + CmbPed.Text.ToString() + "'"))
            {
                LblDetPed.Text = "Cliente: " + Row["Cnte_Clave"].ToString() + " " + Fn_TraeNomCli(Row["Cnte_Clave"].ToString()) + " " + System.Environment.NewLine +
                                 Row["pdn_observacion"].ToString().Trim() + System.Environment.NewLine +
                                 Row["pdn_pedsigma"].ToString().Trim() + System.Environment.NewLine +
                                 "Elaboro: " + Row["pdn_ELABORO"].ToString().Trim();
                mtip = Row["pdn_tipo"].ToString();
            }
            LLenaPed(CmbPed.Text.ToString(), mtip);
        }

        public string Fn_TraeNomCli(string var_cli)
        {
            string Cerrar = "F";
            if (thisConnecion.State == System.Data.ConnectionState.Closed)
            {
                thisConnecion.Open();
                Cerrar = "T";
            }
            string cad = "";
            string Cadena = "SELECT CNTE_NOMBRE FROM tb_cat_CLIENTE WHERE cnte_clave = '" + var_cli + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cad = Convert.ToString(cmd.ExecuteScalar());
            if (Cerrar == "T")
              thisConnecion.Close();
            return cad;
        }

        private void LLenaProdPed(string pedido, string mtip)
        {
            thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            string Cadena = "";
            //Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + pedido + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + pedido + "' AND A.PDN_TIPO = '"+ mtip  + "'  AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                //foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]) - Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                    T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]);
                    //T = Convert.ToInt32(row["CANTIDAD"]) - Convert.ToInt32(row["SURTIDO"]) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                    TotS = TotS + T; // Convert.ToInt32(row["SURTIDO"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                int jj = Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                string cp = Ped["PROD_CLAVE"].ToString();
                string Nom = Ped["PROD_NOMBRE"].ToString();
                //if (T < Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]))
                if (T < 0)
                  DetPed2.Rows.Add(pedido, Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]), T);
            }
            thisConnecion.Close();
            //TxtTotP.Text = TotP.ToString("#,###");
            //TxtTotS.Text = TotS.ToString("#,###");
            DGDetPed.DataSource = DetPed2;
            //FormatoSalida();
            //LblPed.Text = "Pedido: " + CmbPed.Text.ToString();
        }

        private void BtnProdPen_Click(object sender, EventArgs e)
        {
           LblProdPen.Visible = true;
           DetPed2.Rows.Clear();
           for (int i = 0; i < CmbPed.Items.Count; i++)
               LLenaProdPed(CmbPed.Items[i].ToString(), MTip);
           FormatoSalida2();
        }
    }
}

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
    public partial class ConsPedSur : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DetPed = new DataTable();
        DataTable DetPed2 = new DataTable();
        public DataTable Inven = new DataTable();
        DataTable Pedidos = new DataTable();
        public string Tipo = "";
        public string PedidoOrigen = "";
        public int Opcion = 0;
        public string Camioneta = "";

        public ConsPedSur()
        {
            InitializeComponent();
        }

        private void ConsPedSur_Load(object sender, EventArgs e)
        {
            thisConnecion.Open();
            //string Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,B.prod_clave,B.pdn_num_unidades FROM TB_MSTR_PEDIDOS_NAL A, tb_det_pedidos B WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "' AND A.PDN_FOLIO = B.PDN_FOLIO AND A.PDN_TIPO = B.PDN_TIPO " +
            //                "UNION " +
            //                "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,B.prod_clave,B.pdn_num_unidades FROM TB_MSTR_PEDIDOS_NAL A, tb_det_pedidos B WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "' AND A.PDN_FOLIO = B.PDN_FOLIO AND A.PDN_TIPO = B.PDN_TIPO " +
            //                "ORDER BY PDN_FOLIO";

            string Cadena = "";
            if (Opcion == 0)
            {
                if (Program.MyGlobal.TipoPed == "NAL")
                {
                    Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO, pdn_pedorigen, pdn_tipo FROM TB_MSTR_PEDIDOS_NAL A WHERE a.pdn_estatus <> 'C' and (A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "') or (pdn_pedorigen = '" + PedidoOrigen + "' or pdn_folio = '" + PedidoOrigen + "') ORDER BY PDN_FOLIO";
                }
                else if (Program.MyGlobal.TipoPed == "EXP")
                {
                    Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO, pdn_pedorigen, pdn_tipo FROM TB_MSTR_PEDIDOS_EXP A WHERE a.pdn_estatus <> 'C' and (A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND A.placacaja = '" + Program.MyGlobal.PubNoTrailer + "') or (pdn_pedorigen = '" + PedidoOrigen + "' or pdn_folio = '" + PedidoOrigen + "') ORDER BY PDN_FOLIO";
                }

            }
            else
                if (Opcion == 1)
            {
                Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO, pdn_pedorigen,pdn_tipo FROM TB_MSTR_PEDIDOS_NAL A, TB_MSTR_FACTURAS_NAL B " +
                         "WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND B.CVE_AUTO = '" + Camioneta + "' AND A.PDN_FOLIO = B.PDN_FOLIO and a.pdn_estatus <> 'C' " +
                         "ORDER BY PDN_FOLIO";
            }
            else
                    if (Opcion == 2)
            {
                Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.CNTE_CLAVE,A.pdn_pedsigma,A.pdn_observacion,A.PDN_ELABORO, pdn_pedorigen,pdn_tipo FROM TB_MSTR_PEDIDOS_NAL A, TB_MSTR_FACTURAS_NAL B " +
                     "WHERE A.PDN_FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND B.CVE_AUTO = '" + Camioneta + "' AND A.PDN_FOLIO = B.PDN_FOLIO  and a.pdn_estatus <> 'C' and A.PDN_FOLIO = '" + PedidoOrigen + "' " +
                     "ORDER BY PDN_FOLIO";
            }
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
            string placatra = "";
            while (Info.Read())
            {
                placatra = Info["PLACACAJA"].ToString();
                CmbPed.Items.Add(Info["PDN_FOLIO"].ToString());
                T++;
            }
            CmbPed.SelectedIndex = 0;
            string Mped = CmbPed.Text.ToString();
            LblPed.Text = "Pedido: " + Mped;
            string mtipo = "";
            foreach (DataRow Row in Pedidos.Select("PDN_FOLIO = '" + Mped + "'"))
            {
                LblDetPed.Text = "Cliente: " + Row["CNTE_CLAVE"].ToString() + " " + Fn_TraeNomCli(Row["CNTE_CLAVE"].ToString()) + " " + System.Environment.NewLine +
                                 Row["pdn_observacion"].ToString().Trim() + System.Environment.NewLine +
                                 Row["pdn_pedsigma"].ToString().Trim() + System.Environment.NewLine +
                                 "Elaboro: " + Row["PDN_ELABORO"].ToString().Trim();
                mtipo = Row["pdn_tipo"].ToString().Trim();
            }
            thisConnecion.Close();
            CreaTable();
            if (T == 1)
            {
                CmbPed.Enabled = false;
                LLenaPed(Mped, mtipo);
                FormatoSalida();
            }
            else
                BtnProdPen_Click(sender, e);
            //DGDetEmbCap.DataSource = DetEmb;
            if (placatra != "")
            {

                string CadenaAdicional = "SELECT * FROM tb_det_pend_embarque WHERE hora_trailer = '" + Program.MyGlobal.PubFecEmb + "' AND no_trailer = '" + placatra + "'  AND estatus = 'A'";
                DataSet dsadicional = new DataSet();
                SqlDataAdapter daadicional = new SqlDataAdapter(Cadena, thisConnecion);
                daadicional.Fill(ds1, "Pedi");
                SqlCommand cmdadicional;
                cmdadicional = new SqlCommand(CadenaAdicional);
                cmdadicional.Connection = thisConnecion;
                SqlDataReader InfoAdicional;
                thisConnecion.Open();
                InfoAdicional = cmdadicional.ExecuteReader();
                if (InfoAdicional.HasRows == true)
                {
                    lbladicional.Visible = true;
                }
                thisConnecion.Close();
            }
        }

        private void LLenaPed(string Mpedido, string mTip)
        {
            LblPed.Text = "Pedido: " + Mpedido;
            DataTable Surtido = new DataTable();
            //LblCliente.Text = Program.MyGlobal.CveCliente;
            if (DetPed.Rows.Count > 0)
                DetPed.Rows.Clear(); //DGDetPed.DataSource = "";
            string Cadena = "";
            thisConnecion.Open(); //DBGAB
            Mpedido = Mpedido.ToString().Trim().PadLeft(6, '0');
            Cadena = " SELECT PROD_CLAVE, SUM(CAJAS) AS SURTIDO FROM tb_det_embarque  WHERE emb_folio = '" + Mpedido + "' AND ESTATUS <> 'C' GROUP BY PROD_CLAVE";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];
            Cadena = " SELECT PROD_CLAVE, SUM(CAJAS) AS SURTIDO FROM tb_det_split  WHERE emb_folio = '" + Mpedido + "' AND ESTATUS = 'A' GROUP BY PROD_CLAVE";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da.Fill(ds, "SPLIT");
            DataTable Split = ds.Tables["SPLIT"];
            //thisConnecion.Close(); //DBGAB
            //thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Mpedido + "' and a.pdn_tipo = '" + mTip + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            DataTable DETPED = new DataTable();
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "DETPED");
            DETPED = ds.Tables["DETPED"];
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            int Exis = 0;
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["SURTIDO"]);
                    TotS = TotS + Convert.ToInt32(row["SURTIDO"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]));
                    //Exis = (Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]))) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]) - T;
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"])) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                }
                Int32 TSplit = 0;
                foreach (DataRow Row in Split.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    TSplit = Convert.ToInt32(Row["Surtido"]);
                }
                DetPed.Rows.Add(Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]).ToString("#,###"), T.ToString("#,##0"), TSplit.ToString("#,###"), Exis.ToString("#,##0"));
            }
            Exis = 0;
            foreach (DataRow row in Surtido.Rows)
            {
                string hay = "N";
                foreach (DataRow row1 in DETPED.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
                {
                    hay = "S";
                }
                if (hay == "N")
                {
                    int T = Convert.ToInt32(row["SURTIDO"]);
                    TotS = TotS + Convert.ToInt32(row["SURTIDO"]);
                    foreach (DataRow row2 in Inven.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
                    {
                        Exis = Convert.ToInt32(row2["CANTIDAD"]) - (Convert.ToInt32(row2["SURTIDO"]));
                    }
                    Int32 TSplit = 0;
                    foreach (DataRow Row in Split.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                    {
                        TSplit = Convert.ToInt32(Row["Surtido"]);
                    }
                    DetPed.Rows.Add(row["PROD_CLAVE"].ToString(), TraeProd(row["PROD_CLAVE"].ToString()), 0, T.ToString("#,##0"), TSplit.ToString("#,###"), Exis.ToString("#,##0"));
                }
            }

            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("#,###");
            TxtTotS.Text = TotS.ToString("#,###");
            LblAvance.Text = ((Convert.ToDecimal(TotS)) / (Convert.ToDecimal(TotP))).ToString("##0 %");
            DGDetPed.DataSource = DetPed;
        }

        private void CreaTable()
        {
            DetPed.Columns.Add("PROD_CLAVE", typeof(string));  //0
            DetPed.Columns.Add("PROD_NOMBRE", typeof(string)); //1
            DetPed.Columns.Add("PEDIDO", typeof(string));      //2
            DetPed.Columns.Add("SURTIDO", typeof(string));     //3
            DetPed.Columns.Add("SPLIT", typeof(string));     //3
            DetPed.Columns.Add("EXISTENCIA", typeof(string));     //3
            DetPed2.Columns.Add("FOLIO", typeof(string));  //0
            DetPed2.Columns.Add("PROD_CLAVE", typeof(string));  //0
            DetPed2.Columns.Add("PROD_NOMBRE", typeof(string)); //1
            DetPed2.Columns.Add("PEDIDO", typeof(string));      //2
            DetPed2.Columns.Add("SURTIDO", typeof(string));     //3
            DetPed2.Columns.Add("SPLIT", typeof(string));     //3
            DetPed2.Columns.Add("EXISTENCIA", typeof(string));
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToString(DetPed.Rows[i]["PEDIDO"]) != Convert.ToString(DetPed.Rows[i]["SURTIDO"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                if ((Convert.ToDecimal(DetPed.Rows[i]["PEDIDO"]) - Convert.ToDecimal(DetPed.Rows[i]["SURTIDO"])) > Convert.ToDecimal(DetPed.Rows[i]["EXISTENCIA"]))
                    DGDetPed.Rows[i].Cells["EXISTENCIA"].Style.BackColor = System.Drawing.Color.Red;
            }
            DGDetPed.Columns[0].HeaderText = "PRODUCTO";
            DGDetPed.Columns[1].HeaderText = "NOMBRE";
            DGDetPed.Columns[1].HeaderText = "SPLIT ARMADO";
            DGDetPed.Columns[0].Width = 120;
            DGDetPed.Columns[1].Width = 500;
            DGDetPed.Columns[2].Width = 70;
            DGDetPed.Columns[3].Width = 70;
            DGDetPed.Columns[4].Width = 70;
            DGDetPed.Columns[5].Width = 80;
            DGDetPed.Columns[0].Visible = false;
            DGDetPed.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (Tipo == "C")
                DGDetPed.Columns[5].Visible = false;
        }

        private void FormatoSalida2()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToString(DetPed2.Rows[i]["PEDIDO"]) != Convert.ToString(DetPed2.Rows[i]["SURTIDO"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                if ((Convert.ToDecimal(DetPed2.Rows[i]["PEDIDO"]) - Convert.ToDecimal(DetPed2.Rows[i]["SURTIDO"])) > Convert.ToDecimal(DetPed2.Rows[i]["EXISTENCIA"]))
                    DGDetPed.Rows[i].Cells["EXISTENCIA"].Style.BackColor = System.Drawing.Color.Red;
            }
            DGDetPed.Columns[0].HeaderText = "FOLIO";
            DGDetPed.Columns[1].HeaderText = "PRODUCTO";
            DGDetPed.Columns[2].HeaderText = "NOMBRE";
            DGDetPed.Columns[0].Width = 70;
            DGDetPed.Columns[1].Width = 120;
            DGDetPed.Columns[2].Width = 450;
            DGDetPed.Columns[3].Width = 60;
            DGDetPed.Columns[4].Width = 60;
            DGDetPed.Columns[5].Width = 60;
            DGDetPed.Columns[6].Width = 80;
            DGDetPed.Columns[1].Visible = false;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (Tipo == "C")
                DGDetPed.Columns[6].Visible = false;
        }

        private void BtnAce_Click(object sender, EventArgs e)
        {
            LblProdPen.Visible = false;
            string mtipo = "";
            foreach (DataRow Row in Pedidos.Select("PDN_FOLIO = '" + CmbPed.Text.ToString() + "'"))
            {
                mtipo = Row["pdn_tipo"].ToString().Trim();
            }
            LLenaPed(CmbPed.Text.ToString(), mtipo);
            FormatoSalida();
            foreach (DataRow Row in Pedidos.Select("Pdn_Folio = '" + CmbPed.Text.ToString() + "'"))
            {
                LblDetPed.Text = "Cliente: " + Row["Cnte_Clave"].ToString() + " " + Fn_TraeNomCli(Row["Cnte_Clave"].ToString()).Trim() + " " + System.Environment.NewLine +
                                 Row["pdn_observacion"].ToString().Trim() + System.Environment.NewLine +
                                 Row["pdn_pedsigma"].ToString().Trim() + System.Environment.NewLine +
                                 "Elaboro: " + Row["pdn_ELABORO"].ToString().Trim();
            }
        }

        private string TraeProd(string Cve)
        {
            string cad = "";
            string Cadena = "SELECT PROD_NOMBRE FROM tb_cat_PRODUCTO WHERE PROD_clave = '" + Cve + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cad = Convert.ToString(cmd.ExecuteScalar());
            return cad;
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

        public void ProdPendiente(string Mpedido)
        {
            LblPed.Text = "Pedido: " + Mpedido;
            DataTable Surtido = new DataTable();
            //LblCliente.Text = Program.MyGlobal.CveCliente;
            //if (DetPed.Rows.Count > 0)
            //    DetPed.Rows.Clear(); //DGDetPed.DataSource = "";
            string Cadena = "";
            thisConnecion.Open(); //DBGAB
            Mpedido = Mpedido.ToString().Trim().PadLeft(6, '0');
            Cadena = " SELECT PROD_CLAVE, SUM(CAJAS) AS SURTIDO FROM tb_det_embarque  WHERE emb_folio = '" + Mpedido + "' AND ESTATUS <> 'C' GROUP BY PROD_CLAVE";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];
            Cadena = " SELECT PROD_CLAVE, SUM(CAJAS) AS SURTIDO FROM tb_det_split  WHERE emb_folio = '" + Mpedido + "' AND ESTATUS = 'A' GROUP BY PROD_CLAVE";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da.Fill(ds, "SPLIT");
            DataTable Split = ds.Tables["SPLIT"];
            //thisConnecion.Close(); //DBGAB
            //thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            string Mtip = (Convert.ToInt32(Mpedido) > 200000) ? "NAL" : "EXP";
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Mpedido + "' AND A.PROD_CLAVE = B.PROD_CLAVE AND A.PDN_TIPO = '" + Mtip + "'ORDER BY B.PROD_NOMBRE";
            DataTable DETPED = new DataTable();
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "DETPED");
            DETPED = ds.Tables["DETPED"];
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            int Exis = 0;
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["SURTIDO"]);
                    TotS = TotS + Convert.ToInt32(row["SURTIDO"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]));
                    //Exis = (Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]))) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]) - T;
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"])) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                }
                Int32 TSplit = 0;
                foreach (DataRow Row in Split.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    TSplit = Convert.ToInt32(Row["Surtido"]);
                }
                if (Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]) != T)
                    DetPed2.Rows.Add(Mpedido, Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]).ToString("#,###"), T.ToString("#,##0"), TSplit.ToString("#,###"), Exis.ToString("#,##0"));
            }
            //Exis = 0;
            //foreach (DataRow row in Surtido.Rows)
            //{
            //    string hay = "N";
            //    foreach (DataRow row1 in DETPED.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
            //    {
            //        hay = "S";
            //    }
            //    if (hay == "N")
            //    {
            //        int T = Convert.ToInt32(row["SURTIDO"]);
            //        TotS = TotS + Convert.ToInt32(row["SURTIDO"]);
            //        foreach (DataRow row2 in Inven.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
            //        {
            //            Exis = Convert.ToInt32(row2["CANTIDAD"]) - (Convert.ToInt32(row2["SURTIDO"]));
            //        }
            //        Int32 TSplit = 0;
            //        foreach (DataRow Row in Split.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
            //        {
            //            TSplit = Convert.ToInt32(Row["Surtido"]);
            //        }
            //        DetPed.Rows.Add(row["PROD_CLAVE"].ToString(), TraeProd(row["PROD_CLAVE"].ToString()), 0, T.ToString("#,##0"), TSplit.ToString("#,###"), Exis.ToString("#,##0"));
            //    }
            //}

            thisConnecion.Close();
            //TxtTotP.Text = TotP.ToString("#,###");
            //TxtTotS.Text = TotS.ToString("#,###");
            //LblAvance.Text = ((Convert.ToDecimal(TotS)) / (Convert.ToDecimal(TotP))).ToString("##0 %");
            DGDetPed.DataSource = DetPed2;
        }

        private void BtnProdPen_Click(object sender, EventArgs e)
        {
            LblProdPen.Visible = true;
            DetPed2.Rows.Clear();
            for (int i = 0; i < CmbPed.Items.Count; i++)
                ProdPendiente(CmbPed.Items[i].ToString());
            FormatoSalida2();
        }

    }
}

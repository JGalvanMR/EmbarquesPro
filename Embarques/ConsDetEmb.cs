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
using Microsoft.VisualBasic.Logging;

namespace Embarques
{
    public partial class ConsDetEmb : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DetPed = new DataTable();
        DataTable DetLotes = new DataTable();
        DataTable PesoProd = new DataTable();
        DataTable Pedidos = new DataTable();
        string pedidofolio = "";

        public ConsDetEmb(string pedidopasado)
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
            pedidofolio = pedidopasado;
        }

        private void ConsDetEmb_Load(object sender, EventArgs e)
        {
            int posicion_pedido  = 0;
            thisConnecion.Open();
            string Cadena = "SELECT EMB_FOLIO,emb_tipo, fcn_folio FROM TB_MSTR_EMBARQUE LEFT JOIN tb_mstr_facturas_nal ON CONVERT(int, EMB_FOLIO) = PDN_FOLIO  WHERE HORA_TRAILER = '" + Program.MyGlobal.PubFecEmb + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "' ORDER BY EMB_FOLIO";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "Pedidos");
            Pedidos = ds.Tables["Pedidos"];
            int T = 0;
            while (Info.Read())
            {
                CmbPed.Items.Add(Info["Emb_folio"].ToString() + " - " + Info["fcn_folio"].ToString());
               if (pedidofolio.Trim() == Info["Emb_folio"].ToString().Trim())
               {
                   posicion_pedido = T;
               }
               T++;
            }
            CmbPed.SelectedIndex = posicion_pedido;
            string Mped = CmbPed.Text.ToString().Substring(0,6);
            string Factura = "";
            try {
                Factura = CmbPed.Text.Replace(Mped + " - ", "");
            }catch{
            
            }
            LblPed.Text = "Pedido: " + Mped + " Factura: " + Factura;
            if (T == 1)
                CmbPed.Enabled = false;
            //DGDetEmbCap.DataSource = DetEmb;
            Cadena = "SELECT a.prod_clave, a.prod_nombre, a.prod_presentacion, B.env_peso " +
                     "FROM tb_cat_producto A, tb_cat_envases B " +
                     "WHERE A.prod_presentacion = b.env_clave ";

            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PesoProd");
            PesoProd = ds.Tables["PesoProd"];
            thisConnecion.Close();
            TxtPeso.Text = Fn_Peso(Mped, Program.MyGlobal.TipoPed).ToString("##,##0.00");
            CreaTable();
            BusTipo(Mped);
            LLenaPed(Mped, Factura);
            FormatoSalida();
        }
        
        private void LLenaPed(string Mpedido, string factura)
        {
            LblPed.Text = "Pedido: "+Mpedido+ " Factura: " + factura;
            TxtPeso.Text = Fn_Peso(Mpedido, Program.MyGlobal.TipoPed).ToString("##,##0.00");
            DataTable Surtido = new DataTable();
            //LblCliente.Text = Program.MyGlobal.CveCliente;
            if (DetPed.Rows.Count > 0)
                DetPed.Rows.Clear(); //DGDetPed.DataSource = "";
            string Cadena = "";
            //thisConnecionDBGAB.Open();
            thisConnecion.Open(); 
            //Cadena = "SELECT * FROM TB_PED_EMBARQUE A WHERE EMB_FOLIO = '" + Mpedido + "' AND NALEXP = '" + Program.MyGlobal.TipoPed + "'";
            Cadena = "SELECT a.emb_folio, a.prod_clave, SUM(a.cajas) AS cant_sur, b.prod_nombre FROM TB_DET_EMBARQUE A, TB_CAT_PRODUCTO B WHERE A.estatus != 'C' and EMB_FOLIO = '" + Mpedido + "' AND EMB_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE" +
                     " GROUP BY A.emb_folio,A.PROD_CLAVE,b.prod_nombre ORDER BY A.emb_folio,A.PROD_CLAVE"; 

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];

            //thisConnecionDBGAB.Close();
            //thisConnecion.Open();
            Cadena = "SELECT * FROM tb_det_embarque WHERE EMB_FOLIO = '" + Mpedido + "' AND EMB_TIPO = '" + Program.MyGlobal.TipoPed + "' and estatus != 'C'  ORDER BY NO_LOTE";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "LOTES");
            DetLotes = ds.Tables["LOTES"];
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            if (Program.MyGlobal.TipoPed != "TRA")
              Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Mpedido + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            else
                Cadena = "SELECT A.prod_clave, A.emb_unidades AS pdn_num_unidades, B.PROD_NOMBRE FROM tb_det_ordenes_emb A, TB_CAT_PRODUCTO B WHERE A.EMB_FOLIO = '" + Mpedido + "' AND A.EMB_TIPO = 'MAQ' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            DataTable DETPED = new DataTable();
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "DETPED");
            DETPED = ds.Tables["DETPED"];
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            while (Ped.Read())
            {
                int T = 0;
                foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["cant_sur"]);
                    TotS = TotS + Convert.ToInt32(row["cant_sur"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                DetPed.Rows.Add(Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]).ToString("#,###"), T.ToString("#,###"));
            }
            foreach (DataRow row in Surtido.Rows)
            {
                string hay = "N";
                foreach (DataRow row1 in DETPED.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
                {
                    hay = "S";
                }
                if (hay == "N")
                {
                    int T = Convert.ToInt32(row["cant_sur"]);
                    TotS = TotS + Convert.ToInt32(row["cant_sur"]);
                    DetPed.Rows.Add(row["PROD_CLAVE"].ToString(), row["PROD_NOMBRE"].ToString(), 0, T.ToString("#,###"));
                }
            }

            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("#,###");
            TxtTotS.Text = TotS.ToString("#,###");
            DGDetPed.DataSource = DetPed;
        }

        private void CreaTable()
        {
            DetPed.Columns.Add("PROD_CLAVE", typeof(string));  //0
            DetPed.Columns.Add("PROD_NOMBRE", typeof(string)); //1
            DetPed.Columns.Add("PEDIDO", typeof(string));      //2
            DetPed.Columns.Add("SURTIDO", typeof(string));     //3
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToString(DetPed.Rows[i]["PEDIDO"]) != Convert.ToString(DetPed.Rows[i]["SURTIDO"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            }
            DGDetPed.Columns[0].HeaderText = "PRODUCTO";
            DGDetPed.Columns[1].HeaderText = "NOMBRE";
            DGDetPed.Columns[0].Width = 120;
            DGDetPed.Columns[1].Width = 330;
            DGDetPed.Columns[2].Width = 50;
            DGDetPed.Columns[3].Width = 60;
            DGDetPed.Columns[0].Visible = false;
            DGDetPed.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void DGDetPed_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DetEti();
        }


        private void DetEti()
        {
            DGLotes.Rows.Clear();
            string Mprod = DGDetPed.CurrentRow.Cells["prod_CLAVE"].Value.ToString();
            string mREC = "", Mlote = "";
            decimal Mpe = 0, Mpeso = 0, MpxCja = 0;
            foreach (DataRow row in DetLotes.Select("prod_clave = '" + Mprod + "'"))
            {
                if (mREC != row["No_lote"].ToString().Substring(0, 6))
                {
                    mREC = row["No_lote"].ToString().Substring(0, 6);
                    Mpe = (Fn_PesoXRecibo(mREC, Mprod, row["tipo_rec"].ToString(), Convert.ToInt32(row["CAJAS"]),"PB"));
                    MpxCja = (Fn_PesoXRecibo(mREC, Mprod, row["tipo_rec"].ToString(), Convert.ToInt32(row["CAJAS"]), "PN"));
                    //Mpe = Mpe/Convert.ToInt32(row["CAJAS"]);
                }
                Mpeso = Mpeso + (Mpe * Convert.ToDecimal(row["CAJAS"]));
                //Mpeso = Mpeso + Mpe;
                Mlote = row["recibo"].ToString() + row["prod_clave"].ToString();
                if (row["TIPO_REC"].ToString() == "PTP")
                    Mlote = Mlote + row["TARIMA"].ToString().Trim().PadLeft(3, ' ');
                else
                    Mlote = Mlote + row["TARIMA"].ToString().Trim().PadLeft(2, '0');
                DGLotes.Rows.Add(Mlote, row["CAJAS"].ToString(), row["TEMP"].ToString(), Convert.ToInt16(row["SECCION"]), row["FEC_CAD"].ToString(), (Mpe * Convert.ToDecimal(row["CAJAS"])).ToString("##,##0.00"), Mpe.ToString("##0.00"), row["tipo_rec"].ToString(), MpxCja.ToString("##0.00"), LoteSSCC(row["recibo"].ToString(), row["tarima"].ToString(), row["TIPO_REC"].ToString()));
            }
            TxtPeso1.Text = Mpeso.ToString("##,##0.00");
        }

        private void BtnAce_Click(object sender, EventArgs e)
        {
            string Mped = CmbPed.Text.ToString().Substring(0, 6);
            string Factura = "";
            try
            {
                Factura = CmbPed.Text.Replace(Mped + " - ", "");
            }
            catch
            {

            }


            BusTipo(Mped.ToString());
            LLenaPed(Mped, Factura);
            FormatoSalida();
        }

        public decimal Fn_Peso(string var_folio, string var_tipo)
        {
            decimal mKilos = 0;
            Int32 Mtar = 0, TotTar = 0;
            thisConnecion.Open();
            string Cadena = "SELECT A.EMB_FOLIO,A.PROD_CLAVE,A.NO_LOTE,A.CAJAS,A.TARIMA,A.SECCION,B.RPT_FECHA,C.env_clave,C.rptd_peso_bruto,C.rptd_tara,C.rptd_tarimas,C.rptd_cantidad FROM TB_DET_EMBARQUE A, TB_MSTR_RECEPCION_PT B,tb_det_recepcion_pt C WHERE " +
                            " A.ESTATUS <> 'C' AND A.TIPO_REC = 'PTC' AND A.EMB_FOLIO = '" + var_folio + "' AND A.EMB_TIPO = '" + var_tipo + "' AND A.RECIBO = B.RPT_RECIBO AND A.RECIBO = C.RPT_RECIBO AND A.PROD_CLAVE = C.PROD_CLAVE ";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                //mKilos = mKilos + ((Convert.ToDecimal(Info["rptd_peso_bruto"]) - Convert.ToDecimal(Info["rptd_tara"]) - (Convert.ToDecimal(Info["rptd_tarimas"]) * 20)) / Convert.ToDecimal(Info["rptd_cantidad"])) * Convert.ToDecimal(Info["CAJAS"]);
                mKilos = mKilos + ((Convert.ToDecimal(Info["rptd_peso_bruto"]) - Convert.ToDecimal(Info["rptd_tara"])) / Convert.ToDecimal(Info["rptd_cantidad"])) * Convert.ToDecimal(Info["CAJAS"]);
                DateTime mF = Convert.ToDateTime(Program.MyGlobal.PubFecEmb.Substring(0, 10));//Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
                if (Convert.ToString(Info["PROD_CLAVE"]) == "02002ML00" || Convert.ToString(Info["PROD_CLAVE"]) == "02002BROFR" || Convert.ToString(Info["PROD_CLAVE"]) == "02BRCO2025")
                    mKilos = mKilos + (Fn_PesoHielo(Convert.ToDecimal(8.5), Convert.ToDateTime(Info["RPT_FECHA"]), mF) * Convert.ToDecimal(Info["CAJAS"])) ;
                if (Info["PROD_CLAVE"].ToString() == "02002BRHEB")
                    mKilos = mKilos + Fn_PesoHielo(Convert.ToDecimal(4), Convert.ToDateTime(Info["RPT_FECHA"]), mF);
                //if (Convert.ToInt32(Info["SECCION"]) > Mtar)
                //    Mtar = Convert.ToInt32(Info["SECCION"]);
            }
            Cadena = "SELECT A.EMB_FOLIO,A.PROD_CLAVE,A.NO_LOTE,A.CAJAS,A.TARIMA,A.SECCION,B.PROD_PESO_VAR,B.FODP_UNIDADES,C.PROD_PRESENTACION,D.ENV_PESO,E.HRP_PESO_NETO,E. HRP_NUM_UNIDADES,E.hrp_fecha FROM TB_DET_EMBARQUE A, TB_DET_FINAL_ODP B, TB_CAT_PRODUCTO C,tb_cat_envases D, TB_HIST_RECEPCION E WHERE " +
                            " A.ESTATUS <> 'C' AND A.TIPO_REC = 'PTP' AND A.EMB_FOLIO = '" + var_folio + "' AND A.EMB_TIPO = '" + var_tipo + "' AND A.RECIBO = B.ORDP_FOLIO AND A.PROD_CLAVE = B.PROD_CLAVE AND B.PROD_CLAVE = C.PROD_CLAVE AND C.PROD_PRESENTACION = D.ENV_CLAVE  " +
                            " AND A.PROD_CLAVE = E.PROD_CLAVE AND A.RECIBO = E.HRP_RECIBO order by A.seccion";
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            Info = cmd.ExecuteReader();
            while (Info.Read())
            {
                decimal Mpe = 0;
                if (Convert.ToDecimal(Info["PROD_PESO_VAR"]) > 0)
                    Mpe = Convert.ToDecimal(Info["PROD_PESO_VAR"]) * Convert.ToDecimal(Info["CAJAS"]);
                else
                    Mpe = Convert.ToDecimal(Info["ENV_PESO"]) * Convert.ToDecimal(Info["CAJAS"]);

                foreach (DataRow row in PesoProd.Select("Prod_Clave ='" + Info["PROD_CLAVE"].ToString() + "'"))
                    Mpe = (Convert.ToDecimal(row["env_peso"]) == 0) ? 1 : Convert.ToDecimal(row["env_peso"]);
                
                //mKilos = mKilos + ((Convert.ToDecimal(Info["HRP_PESO_NETO"]) / Convert.ToDecimal(Info["HRP_NUM_UNIDADES"])) * Convert.ToDecimal(Info["CAJAS"]) + Mpe);
                mKilos = mKilos + ((Convert.ToDecimal(Info["HRP_PESO_NETO"]) / Convert.ToDecimal(Info["HRP_NUM_UNIDADES"])) * Convert.ToDecimal(Info["CAJAS"])) + (Mpe * Convert.ToDecimal(Info["CAJAS"]));
                if (Info["PROD_CLAVE"].ToString() == "02002ML00" || Info["PROD_CLAVE"].ToString() == "02002BROFR" || Info["PROD_CLAVE"].ToString() == "02BRCO2025")
                    mKilos = mKilos + (Fn_PesoHielo(Convert.ToDecimal(8.5), Convert.ToDateTime(Info["HRP_FECHA"]), Convert.ToDateTime(Program.MyGlobal.PubFecEmb)) * Convert.ToDecimal(Info["CAJAS"]));
                if (Info["PROD_CLAVE"].ToString() == "02002BRHEB")
                    mKilos = mKilos + Fn_PesoHielo(Convert.ToDecimal(4), Convert.ToDateTime(Info["HRP_FECHA"]), Convert.ToDateTime(Program.MyGlobal.PubFecEmb));
                if (Convert.ToInt32(Info["SECCION"]) != Mtar)
                {
                    TotTar++;
                    Mtar = Convert.ToInt32(Info["SECCION"]);
                }
            }
            mKilos = mKilos + (TotTar * 20);
            thisConnecion.Close();
            return mKilos;
        }

        public decimal Fn_PesoXRecibo (string mRecibo, string Mprod, string TipoRec, int MCant, string TipoPeso)
        {
            decimal mKilos = 0;
            string Cadena = "";
            SqlCommand cmd;
            SqlDataReader Info;
            thisConnecion.Open();
            if (TipoRec == "PTC")
            {
                Cadena = "SELECT A.*, B.ENV_PESO,C.RPT_FECHA FROM tb_det_recepcion_pt A, tb_cat_envases B, TB_MSTR_RECEPCION_PT C  WHERE " +
                                " A.RPT_RECIBO = '" + mRecibo + "' AND A.PROD_CLAVE = '" + Mprod + "' AND A.ENV_CLAVE = B.ENV_CLAVE AND A.RPT_RECIBO = C.RPT_RECIBO ";
                
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    decimal p_b = Convert.ToDecimal(Info["rptd_peso_bruto"]);
                    decimal t = Convert.ToDecimal(Info["rptd_tara"]);
                    decimal can = Convert.ToDecimal(Info["rptd_cantidad"]);
                    decimal tar = Convert.ToDecimal(Info["rptd_tarimas"]);
                    decimal peso_env = 0;
                    if (TipoPeso == "PB") // PesoBruto
                        peso_env = Convert.ToDecimal(Info["env_peso"]);


                    mKilos = Convert.ToDecimal(p_b) - Convert.ToDecimal(t) - (Convert.ToDecimal(can) * peso_env) - (Convert.ToDecimal(tar) * 20);
                    mKilos = mKilos / Convert.ToInt32(can);

                    //mKilos = mKilos + ((Convert.ToDecimal(Info["rptd_peso_bruto"]) - Convert.ToDecimal(Info["rptd_tara"]) - (Convert.ToDecimal(Info["rptd_tarimas"]) * 20) -  (Convert.ToDecimal(Info["ENV_PESO"]) * Convert.ToDecimal(Info["rptd_cantidad"]))) / Convert.ToDecimal(Info["rptd_cantidad"])) * Convert.ToDecimal(MCant);
                    //mKilos = mKilos + ((Convert.ToDecimal(Info["rptd_peso_bruto"]) - Convert.ToDecimal(Info["rptd_tara"])) / Convert.ToDecimal(Info["rptd_cantidad"])) * Convert.ToDecimal(MCant);
                    DateTime mF = Convert.ToDateTime(Program.MyGlobal.PubFecEmb.Substring(0, 10));  //Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
                    if (Convert.ToString(Info["PROD_CLAVE"]) == "02002ML00" || Convert.ToString(Info["PROD_CLAVE"]) == "02002BROFR" || Convert.ToString(Info["PROD_CLAVE"]) == "02BRCO2025")
                        mKilos = mKilos + (Fn_PesoHielo(Convert.ToDecimal(8.5), Convert.ToDateTime(Info["RPT_FECHA"]), mF) * MCant) ;
                    if (Info["PROD_CLAVE"].ToString() == "02002BRHEB")
                        mKilos = mKilos + Fn_PesoHielo(Convert.ToDecimal(4), Convert.ToDateTime(Info["RPT_FECHA"]), mF);
                }
            }
            else
            {
                Cadena = "SELECT B.PROD_CLAVE, C.PROD_NOMBRE, B.PROD_PESO_VAR,B.FODP_UNIDADES,C.PROD_PRESENTACION,D.ENV_PESO,E.HRP_PESO_NETO,E.HRP_NUM_UNIDADES,E.hrp_fecha FROM TB_DET_FINAL_ODP B, TB_CAT_PRODUCTO C,tb_cat_envases D, TB_HIST_RECEPCION E " +
                                " WHERE B.PROD_CLAVE = '" + Mprod + "' AND B.ORDP_FOLIO = '" + mRecibo + "' AND B.PROD_CLAVE = C.PROD_CLAVE AND C.PROD_PRESENTACION = D.ENV_CLAVE AND B.ORDP_FOLIO = E.hrp_recibo AND B.PROD_CLAVE = E.PROD_CLAVE";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    decimal Mpe = 0;
                    if (Convert.ToDecimal(Info["PROD_PESO_VAR"]) > 0)
                        Mpe = Convert.ToDecimal(Info["PROD_PESO_VAR"]) * MCant;
                    else
                        Mpe = Convert.ToDecimal(Info["ENV_PESO"]) * MCant;

                    foreach (DataRow row in PesoProd.Select("Prod_Clave ='" + Info["PROD_CLAVE"].ToString() + "'"))
                        Mpe = (Convert.ToDecimal(row["env_peso"]) == 0) ? 1 : Convert.ToDecimal(row["env_peso"]);

                    if (TipoPeso != "PB") // Peso Bruto
                        Mpe = 0;
                    decimal Muni = Convert.ToDecimal(Info["HRP_NUM_UNIDADES"]);
                    mKilos = mKilos + ((Convert.ToDecimal(Info["HRP_PESO_NETO"]) / Muni)) + (Mpe*Muni); // +Mpe;
                    //MKILOS = MKILOS + ((HRP_PESO_NETO / HRP_NUM_UNIDADES) * D) + (var_dec_env_peso * D)
                    if (Info["PROD_CLAVE"].ToString().Trim() == "02002ML00" || Info["PROD_CLAVE"].ToString() == "02002BROFR" || Info["PROD_CLAVE"].ToString() == "02BRCO2025")
                        mKilos = mKilos + (Fn_PesoHielo(Convert.ToDecimal(8.5), Convert.ToDateTime(Info["HRP_FECHA"]), Convert.ToDateTime(Program.MyGlobal.PubFecEmb)) * MCant);
                    if (Info["PROD_CLAVE"].ToString() == "02002BRHEB")
                        mKilos = mKilos + Fn_PesoHielo(Convert.ToDecimal(4), Convert.ToDateTime(Info["HRP_FECHA"]), Convert.ToDateTime(Program.MyGlobal.PubFecEmb));
                }
            }
            thisConnecion.Close();
            return mKilos;
        }
        public decimal Fn_PesoHielo(decimal peso, DateTime FecRec, DateTime FecEmb)
        {
            int MDIA = FecEmb.Subtract(FecRec).Days;
            decimal Mpeso = 0;
            switch (MDIA)
            {
                case 0:
                    Mpeso = peso;
                    break;
                case 1:
                    Mpeso = (peso * 85) / 100;
                    break;
                case 2:
                    Mpeso = (peso * 75) / 100;
                    break;
                case 3:
                    Mpeso = (peso * 50) / 100;
                    break;
                case 4:
                    Mpeso = (peso * 35) / 100;
                    break;
                case 5:
                    Mpeso = (peso * 20) / 100;
                    break;
                case 6:
                    Mpeso = (peso * 10) / 100;
                    break;
                default:
                    Mpeso = peso;
                    break;
            }
            return Mpeso;
        }

        private void DGLotes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           //if (DGLotes.CurrentRow.Cells[""])
            int pos = DGLotes.CurrentRow.Cells["LOTE"].Value.ToString().IndexOf(" ");
            if (pos > 6 || pos == -1)
              pos = 6;  
            Program.MyGlobal.PubRecibo = DGLotes.CurrentRow.Cells["LOTE"].Value.ToString().Substring(0, pos);
            Program.MyGlobal.PubTipoRec = DGLotes.CurrentRow.Cells["TIPOREC"].Value.ToString();
            Program.MyGlobal.PubProd = DGDetPed.CurrentRow.Cells["PROD_CLAVE"].Value.ToString() + " " +DGDetPed.CurrentRow.Cells["PROD_NOMBRE"].Value.ToString();
            Program.MyGlobal.PubCveProd = DGDetPed.CurrentRow.Cells["PROD_CLAVE"].Value.ToString();
            ConsDetRecibo ConsRecibo = new ConsDetRecibo();
            ConsRecibo.ShowDialog(this); 

        }

        void ReporteExcel()
        {
            label1.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Maximum = DGDetPed.Rows.Count - 1;
            String ruta = @"C:\Reportes\RepOrdProdMP.xls";
            if (!File.Exists(ruta))
            {
                var fileStream = File.Create(ruta);
            }

            Excel.Range r;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add();

            //int count = MiDataGrid.ColumnCount;
            excel.Cells[1, 1] = "Comercializadora GAB s.a. de c.v.";
            excel.Range[excel.Cells[1, 1], excel.Cells[1, 7]].Merge();
            excel.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[2, 1] = "REPORTE DE DESGLOSE DE LOTES POR PEDIDO";
            excel.Range[excel.Cells[1, 1], excel.Cells[1, 7]].Merge();
            excel.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //r = excelWorksheet.get_Range("A1", "R1");
            //r.Font.Bold = true;
            excel.Cells[3, 3] = "Fecha: " + System.DateTime.Now.ToString();
            excel.Cells[4, 3] = "PEDIDO: " + LblPed.Text; 
            //excel.Cells[5, 3] = "Reporte del " + Program.MyGlobal.PubFI + " Al " + Program.MyGlobal.PubFF;
            r = excel.get_Range("A1", "F5");
            r.Font.Bold = true;
            excel.Cells[6, 4] = "PEDIDO"; excel.Cells[6, 5] = "SURTIDO";
            excel.Cells[7, 1] = "CODIGO"; excel.Cells[7, 2] = "PRODUCTO"; excel.Cells[7, 3] = "LOTE O RECIBO"; excel.Cells[7, 4] = "CAJAS"; excel.Cells[7, 5] = "TEMP"; excel.Cells[7, 6] = "FECHA CAD";
            r = excel.Range[excel.Cells[6, 1], excel.Cells[7, 6]];
            r.Font.Bold = true;
            //for (i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    dgc = dataGridView1.Rows[i].Cells[0];
            //    celda = ((String)dgc.Value) + "\r\n";
            //    textBox1.Text += celda.Replace(".", ",");
            //}

            //agrega las filas a excel

            int i = 8;
            foreach (DataRow Prod in DetPed.Rows)
            {
                string Mprod = Prod["prod_CLAVE"].ToString();
                excel.Cells[i, 1] = Mprod;
                excel.Cells[i, 2] = Prod["prod_nombre"].ToString();
                excel.Cells[i, 4] = Prod["pedido"].ToString();
                excel.Cells[i, 5] = Prod["surtido"].ToString();
                r = excel.Range[excel.Cells[i, 1], excel.Cells[i, 5]];
                r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                i++;
                foreach (DataRow row in DetLotes.Select("prod_clave = '" + Mprod + "'"))
                {
                    //excel.Cells[i, 1] = Mprod ;
                    //excel.Cells[i, 2] = Prod["prod_nombre"].ToString();
                    excel.Cells[i, 3] = row["No_lote"].ToString();
                    excel.Cells[i, 4] = row["CAJAS"].ToString();
                    excel.Cells[i, 5] = row["TEMP"].ToString();
                    excel.Cells[i, 6] = row["FEC_CAD"].ToString();
                    //DGLotes.Rows.Add(row["No_lote"].ToString(), row["CAJAS"].ToString(), row["TEMP"].ToString(), Convert.ToInt16(row["SECCION"]), row["FEC_CAD"].ToString(), (Mpe * Convert.ToDecimal(row["CAJAS"])).ToString("##,##0.00"), Mpe.ToString("##0.00"), row["tipo_rec"].ToString());
                    i++;
                }
                progressBar1.PerformStep();
            }
            
            
            //    //decimal Porce = (fila / MiGrid.Rows.Count);
            //    label1.Text = "Exportando Información ..  " + fila.ToString() + "/" + MiGrid.Rows.Count.ToString(); // + " " + Porce.ToString();
            //    label1.Update();
            //}

            //r = excelWorksheet.Range[excelWorksheet.Cells[MiGrid.RowCount + 7, 1], excelWorksheet.Cells[MiGrid.RowCount + 7, 11]];
            //r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
            //r = excelWorksheet.Range[excelWorksheet.Cells[MiGrid.RowCount + 7, 1], excelWorksheet.Cells[MiGrid.RowCount + 7, 8]];
            //r.Font.Bold = true;

            //excel.PageSetup.PrintTitleRows = "$1:$4";
            //r = excel.Range[excel.Cells[MiGrid.RowCount + 6, 1], excel.Cells[MiGrid.RowCount + 6, 13]];
            //r.HorizontalAlignment = Excel.XlVAlign.xlVAlignJustify;
            excel.Columns.AutoFit();
            excel.Rows.AutoFit();
            progressBar1.Minimum = 0;
            progressBar1.Visible = false;
            label1.Visible = false;
            MessageBox.Show("Archivo Generado Con Exito!!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            excel.Visible = true;

        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            ReporteExcel();
        }

        private void DGDetPed_KeyUp(object sender, KeyEventArgs e)
        {
            DetEti();
        }

        private void BusTipo(string Mped)
        {
            foreach (DataRow row in Pedidos.Select("Emb_folio = '" + Mped + "'"))
                Program.MyGlobal.TipoPed = Convert.ToString(row["emb_tipo"]);

        }

        private string LoteSSCC(string Lot, string Tar, String Tipo)
        {
            thisConnecion.Open();
            string Cadena = "Select id_pallet from tb_det_trazabilidad Where recibo= '" + Lot + "' and Tarima = '" + Tar + "' and Tipo = '" + Tipo + "'";
            SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
            string Pallet = "0000796631"+Convert.ToString(cmd.ExecuteScalar()).Trim();
            Pallet = Pallet + ((Pallet.Trim().Length == 17) ? DigitoControlSSCC(Pallet) : "0");
            thisConnecion.Close();
            return Pallet;
        }

        private string DigitoControlSSCC(string SSCC)
        {
            Int32 SumDig = 0;
            Int32 DigMul = 3;
            for (int i = 1; i <= 17; i++)
            {
                DigMul = 3;
                if ((i % 2) == 0)
                    DigMul = 1;
                SumDig += Convert.ToInt32(SSCC.Substring(i - 1, 1)) * DigMul;
            }
            int DecSup = SumDig - (SumDig % 10) + 10;
            return (DecSup - SumDig).ToString(); 
        }

    }
}

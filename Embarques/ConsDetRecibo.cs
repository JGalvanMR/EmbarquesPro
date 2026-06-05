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
    public partial class ConsDetRecibo : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DetPed = new DataTable();
        DataTable Facturas = new DataTable();
        DataTable DetEmb = new DataTable();
        string TipoRec = "";

        public ConsDetRecibo()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }

        private void ConsDetRecibo_Load(object sender, EventArgs e)
        {
            CreaTabla();
            string Cadena = "";
            TxtRecibo.Text = Program.MyGlobal.PubRecibo;
            LblProd.Text = Program.MyGlobal.PubProd;
            thisConnecion.Open();
            if (Program.MyGlobal.PubTipoRec == "PTC")
            {

                Cadena = "SELECT A.prov_clave,A.RCH_CLAVE,A.TBL_CLAVE,A.VARI_CLAVE,A.LIN_CLAVE,A.RPT_FECHA, A.RPT_VIAJE, A.RPT_HORA, A.RPT_OBSERVACIONES, B.rptd_peso_bruto, B.rptd_tara, B.rptd_tarimas, B.rptd_cantidad FROM TB_MSTR_RECEPCION_PT A,  tb_det_recepcion_pt B WHERE A.rpt_recibo = '" + Program.MyGlobal.PubRecibo + "' AND A.RPT_RECIBO = B.RPT_RECIBO AND PROD_CLAVE = '" + Program.MyGlobal.PubCveProd + "'";
                SqlCommand cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader Ped;
                Ped = cmnd2.ExecuteReader();
                string MPROV = "", MRAN = "", MTAB = "", MVAR = "", MLIN = "", kgcja = "";
                while (Ped.Read())
                {
                    label1.Text = "Fecha: " + Convert.ToDateTime(Ped["RPT_FECHA"]).ToString("dd/MM/yyyy") + " Viaje: " + Ped["RPT_VIAJE"].ToString().Trim() + " Hora: " + Ped["RPT_HORA"].ToString();
                    label2.Text = "OBS: " + Ped["RPT_OBSERVACIONES"].ToString().Trim();
                    kgcja = "   KG/CJA = " + ((Convert.ToDecimal(Ped["rptd_peso_bruto"]) - Convert.ToDecimal(Ped["rptd_tara"]) - (Convert.ToDecimal(Ped["rptd_tarimas"])*20)) / Convert.ToDecimal(Ped["rptd_cantidad"])).ToString("##0.00");
                    MPROV = Ped["PROV_CLAVE"].ToString();
                    MRAN = Ped["RCH_CLAVE"].ToString();
                    MTAB = Ped["TBL_CLAVE"].ToString();
                    MVAR = Ped["VARI_CLAVE"].ToString();
                    MLIN = Ped["LIN_CLAVE"].ToString();
                }
                Cadena = "SELECT PROV_NOMBRE FROM tb_cat_proveedor WHERE PROV_CLAVE = '" + MPROV + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                label4.Text = "Prov: "+ Convert.ToString(cmnd2.ExecuteScalar()).Trim();
                Cadena = "SELECT RCH_NOMBRE FROM tb_cat_ranchos WHERE PROV_CLAVE = '" + MPROV + "' AND RCH_CLAVE = '" + MRAN + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                label4.Text = label4.Text + "  Ran: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim();
                Cadena = "SELECT TBL_NOMBRE FROM tb_cat_tablas WHERE PROV_CLAVE = '" + MPROV + "' AND RCH_CLAVE = '" + MRAN + "' AND TBL_CLAVE = '" + MTAB + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                label4.Text = label4.Text + "  Tab: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim();
                Cadena = "SELECT VARI_NOMBRE FROM tb_cat_VARIEDAD WHERE VARI_CLAVE = '" + MVAR + "' AND LIN_CLAVE = '" + MLIN + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                label4.Text = label4.Text + "  VARIEDAD: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim();
                label4.Text = label4.Text + "  " + kgcja;
                Cadena = "SELECT TARIMA,FECHA_CAD,ETIQUETA,SURTIDO,TIPO,PTI_CLAVE,RECIBO FROM TB_DET_TRAZABILIDAD " +
                     " WHERE RECIBO = '" + Program.MyGlobal.PubRecibo + "' AND TIPO = '" + Program.MyGlobal.PubTipoRec + "' AND PROD_CLAVE = '"+ Program.MyGlobal.PubCveProd  +"'";
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
                da1.Fill(ds1, "TARIMAS");
                DataTable TARIMAS = new DataTable() ;
                TARIMAS  = ds1.Tables["TARIMAS"];
                //DGDetTar.DataSource = TARIMAS;
                Cadena = "SELECT A.HORA_LLEGO,A.HORA_ENT,A.TEMP_SAL,B.TARIMA FROM TB_MSTR_TUBO A, TB_DET_TUBO B " +
                     " WHERE B.FOLIO = '" + Program.MyGlobal.PubRecibo + "' AND B.TIPO = '" + Program.MyGlobal.PubTipoRec + "' AND B.CVE_PROD = '" + Program.MyGlobal.PubCveProd + "'" +
                     " AND B.FOLIO = A.FOLIO AND B.HORA_ENT = A.HORA_ENT" ;
                ds1 = new DataSet();
                da1 = new SqlDataAdapter(Cadena, thisConnecion);
                da1.Fill(ds1, "TUBO");
                DataTable TUBO = new DataTable();
                TUBO = ds1.Tables["TUBO"];
                string hrllego = "", hrEnt = "",tmSal = "", tiempo = "";
                int tp = 0, ts = 0;
                foreach (DataRow Row in TARIMAS.Rows)
                {
                    foreach (DataRow row1 in TUBO.Select("TARIMA = '" + Row["TARIMA"].ToString() + "'"))
                    {
                        hrllego = row1["HORA_LLEGO"].ToString();
                        hrEnt = row1["HORA_ENT"].ToString();
                        tmSal = row1["TEMP_SAL"].ToString();
                        tiempo = Fn_Tiempo(hrllego, hrEnt);
                    }
                    DGDetTar.Rows.Add(Row["TARIMA"].ToString(), Row["FECHA_CAD"].ToString(), Row["ETIQUETA"].ToString(), Row["SURTIDO"].ToString(), Row["TIPO"].ToString(), Row["PTI_CLAVE"].ToString(),Row["RECIBO"].ToString(),
                                      hrllego,hrEnt,tiempo,tmSal);
                    tp = tp + Convert.ToInt32(Row["ETIQUETA"]);
                    ts = ts + (Convert.ToInt32(Row["ETIQUETA"]) - Convert.ToInt32(Row["SURTIDO"]) > Convert.ToInt32(Row["ETIQUETA"]) ? 0 : Convert.ToInt32(Row["SURTIDO"]) - Convert.ToInt32(Row["SURTIDO"])); 
                    //ts = ts + Convert.ToInt32(Row["SURTIDO"]) > Convert.ToInt32(Row["ETIQUETA"]) ? Convert.ToInt32(Row["ETIQUETA"]) : Convert.ToInt32(Row["SURTIDO"]);
                }

                label3.Text = "Cajas Prod: " + tp.ToString("###,##0") + System.Environment.NewLine + "X Surtir: " + ts.ToString("###,##0");
                //DGDetTar.DataSource = TARIMAS;
                DGDetTarPtp.Visible = false;
                DGDetTar.Visible = true;
            }
            else
            {
                LblEnf.Visible = false;
                Cadena = "SELECT ORDP_FECHA, ORDP_LINEA, ordp_responsable, ordp_autorizo, ORDP_TURNO FROM TB_MSTR_ORDENES_PROD WHERE ORDP_FOLIO = '" + Program.MyGlobal.PubRecibo + "'";
                SqlCommand cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader Ped;
                Ped = cmnd2.ExecuteReader();
                while (Ped.Read())
                {
                    label1.Text = "Fecha: " + Convert.ToDateTime(Ped["ORDP_FECHA"]).ToString("dd/MM/yyyy");
                    label2.Text = "TURNO: " + Ped["ORDP_TURNO"].ToString(); 
                    label3.Text = "LINEA: "+Ped["ORDP_LINEA"].ToString() + "Resp: "+ Ped["ordp_responsable"].ToString().Trim()+ System.Environment.NewLine+"Autorizo "+Ped["ordp_autorizo"].ToString().Trim();
                    label4.Text = "";
                }
                Cadena = "SELECT TARIMA,NUM_LOTE,NUM_CAJAS,CAJAS_SUR,LOTE_CONTROL,RECIBO,HORA,SUPERVISOR,INSPECTOR FROM TB_DET_ETI_FINAL " +
                     " WHERE FOLIO = '" + Program.MyGlobal.PubRecibo + "' AND CVE_PROD = '" + Program.MyGlobal.PubCveProd + "'";
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
                da1.Fill(ds1, "TARIMAS");
                DataTable TARIMAS = new DataTable();
                TARIMAS = ds1.Tables["TARIMAS"];
                int tp = 0, ts = 0;
                foreach (DataRow Row in TARIMAS.Rows)
                {
                    tp = tp + Convert.ToInt32(Row["NUM_CAJAS"]);
                    ts = ts + Convert.ToInt32(Row["NUM_CAJAS"]) - Convert.ToInt32(Row["CAJAS_SUR"]);
                }
                label3.Text = "Cajas Prod: " + tp.ToString("###,##0") + System.Environment.NewLine + "X Surtir: " + ts.ToString("###,##0");
                DGDetTarPtp.DataSource = TARIMAS;
                DGDetTarPtp.Visible = true;
                DGDetTar.Visible = false;
            }

            Cadena = "SELECT A.EMB_FOLIO, B.FCN_FOLIO, B.FCN_FECHA, C.CNTE_NOMBRE, A.CAJAS, A.TEMP,A.TARIMA,A.FEC_CAD,B.PROV_CLAVE " +
                     "FROM tb_det_embarque A, TB_MSTR_FACTURAS_NAL B, TB_CAT_CLIENTE C  " +
                     "WHERE A.RECIBO = '" + Program.MyGlobal.PubRecibo + "' AND A.PROD_CLAVE = '" + Program.MyGlobal.PubCveProd + "' AND A.emb_folio = B.pdn_folio AND B.CNTE_CLAVE = C.CNTE_CLAVE   ORDER BY A.NO_LOTE";
            Cadena = "SELECT A.EMB_FOLIO, B.FCN_FOLIO, B.FCN_FECHA, C.CNTE_NOMBRE, A.CAJAS, A.TEMP,A.TARIMA,A.FEC_CAD,B.PROV_CLAVE " +
                     "FROM tb_det_embarque A, TB_MSTR_FACTURAS_NAL B, TB_CAT_CLIENTE C " +
                     "WHERE A.RECIBO = '" + Program.MyGlobal.PubRecibo + "' AND A.PROD_CLAVE = '" + Program.MyGlobal.PubCveProd + "' AND CAST(A.emb_folio AS INT) = CAST(B.pdn_folio AS INT) " +
                     "AND B.cnte_clave = C.cnte_clave AND (A.emb_tipo != B.fcn_tipo  OR A.emb_tipo != B.fcn_tipo) ";

            Cadena = "SELECT B.EMB_FOLIO, A.cajas, A.temp, A.tarima, A.fec_cad, A.prod_clave, b.no_trailer, c.responsable, B.EMB_TIPO, B.HORA_TRAILER, C.TRANSPORTE " +
                            " FROM dbo.tb_det_embarque A, tb_mstr_embarque B, tb_mstr_trailer C" +
                            " WHERE A.recibo = '" + Program.MyGlobal.PubRecibo + "' AND A.tipo_rec = '" + Program.MyGlobal.PubTipoRec + "' AND A.PROD_CLAVE = '" + Program.MyGlobal.PubCveProd + "' AND A.emb_folio = B.emb_folio and A.Estatus != 'C' " +
                            " AND B.no_trailer = C.no_trailer AND B.hora_trailer = C.hora_trailer ORDER BY A.EMB_folio,A.prod_clave ";

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "FACTURAS");
            Facturas  = ds.Tables["FACTURAS"];
            //DGDetEmb.DataSource = Facturas;
            thisConnecion.Close();
            foreach (DataRow Row1 in Facturas.Rows) // Emb2.Select("prod_clave = '" + Prod + "'"))
            {
                string Fac = Fn_Factura(Row1["EMB_FOLIO"].ToString(), Row1["EMB_TIPO"].ToString());
                string Cli = Fn_TraeNomCli(Row1["EMB_FOLIO"].ToString(), Row1["EMB_TIPO"].ToString());
                string Ped = Row1["EMB_FOLIO"].ToString().Trim();
                DetEmb.Rows.Add(Row1["EMB_FOLIO"], Fac, Row1["HORA_TRAILER"], Cli, Row1["cajas"], Row1["temp"], Row1["tarima"], Row1["fec_cad"], Row1["TRANSPORTE"], Row1["responsable"], Row1["no_trailer"]);
            }
            DGDetEmb.DataSource = DetEmb;
            FormatoSalida();
        }

        private void FormatoSalida()
        {
            //for (int i = 0; i < DGDetTar.Rows.Count; i++)
            //{
            //    if (Convert.ToString(DetPed.Rows[i]["PEDIDO"]) != Convert.ToString(DetPed.Rows[i]["SURTIDO"]))
            //        DGDetTar.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            //}
            DGDetTar.Columns[0].HeaderText = "TARIMA";
            DGDetTar.Columns[1].HeaderText = "FECHA CAD";
            DGDetTar.Columns[2].HeaderText = "CANT";
            DGDetTar.Columns[3].HeaderText = "SURTIDO";
            DGDetTar.Columns[4].HeaderText = "TIPO";
            DGDetTar.Columns[5].HeaderText = "LOTE CONTROL";
            DGDetTar.Columns[6].HeaderText = "RECIBO";
            DGDetTar.Columns[0].Width = 60;
            DGDetTar.Columns[1].Width = 90;
            DGDetTar.Columns[2].Width = 40;
            DGDetTar.Columns[3].Width = 60;
            DGDetTar.Columns[4].Width = 50;
            DGDetTar.Columns[5].Width = 180;
            DGDetTar.Columns[6].Width = 80;
            DGDetTar.Columns[7].Width = 80;
            DGDetTar.Columns[8].Width = 80;
            DGDetTar.Columns[9].Width = 80;
            DGDetTar.Columns[10].Width = 90;
            DGDetTar.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGDetTar.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetTar.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetTar.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetEmb.Columns[0].HeaderText = "O. VENTA";
            DGDetEmb.Columns[1].HeaderText = "FACTURA";
            DGDetEmb.Columns[2].HeaderText = "FECHA";
            DGDetEmb.Columns[3].HeaderText = "CLIENTE";
            DGDetEmb.Columns[4].HeaderText = "CAJAS";
            DGDetEmb.Columns[5].HeaderText = "TEMP";
            DGDetEmb.Columns[6].HeaderText = "TARIMA";
            DGDetEmb.Columns[7].HeaderText = "FEC. CAD";
            DGDetEmb.Columns[8].HeaderText = "TRANSPORTE";
            if (DGDetTarPtp.Rows.Count > 0)
            {
                DGDetTarPtp.Columns[0].Width = 40;
                DGDetTarPtp.Columns[1].Width = 80;
                DGDetTarPtp.Columns[2].Width = 60;
                DGDetTarPtp.Columns[3].Width = 60;
                DGDetTarPtp.Columns[4].Width = 150;
                DGDetTarPtp.Columns[5].Width = 60;
                DGDetTarPtp.Columns[6].Width = 60;
                DGDetTarPtp.Columns[7].Width = 150;
                DGDetTarPtp.Columns[8].Width = 150;
                DGDetTarPtp.Columns[1].HeaderText = "FECHA CAD";
                DGDetTarPtp.Columns[2].HeaderText = "CANT";
                DGDetTarPtp.Columns[3].HeaderText = "SURTIDO";
            }
            DGDetEmb.Columns[0].Width = 60;
            DGDetEmb.Columns[1].Width = 60;
            DGDetEmb.Columns[2].Width = 80;
            DGDetEmb.Columns[3].Width = 250;
            DGDetEmb.Columns[4].Width = 60;
            DGDetEmb.Columns[5].Width = 60;
            DGDetEmb.Columns[6].Width = 60;
        }

        public string Fn_Tiempo(string TI, string TF)
        {
            string cad = "";
            decimal T1 = (Convert.ToInt32(TI.Substring(0, 2)) * 60) + Convert.ToInt32(TI.Substring(3, 2));   //(VAL(SUBSTR(TB_MSTR_EMBARQUE.HORA_INI,1,2))*60)+VAL(SUBSTR(TB_MSTR_EMBARQUE.HORA_INI,4,2))
            decimal MHI = Convert.ToInt32(TI.Substring(0, 2));  //VAL(SUBSTR(TB_MSTR_EMBARQUE.HORA_INI,1,2))
            decimal MHF = Convert.ToInt32(TF.Substring(0, 2)); //VAL(SUBSTR(TB_MSTR_EMBARQUE.HORA_FIN,1,2))
            decimal T2 = 0;
            decimal T3 = 0, T4 = 0;
            if (MHF < MHI)
                T2 = ((MHF + 12) * 60) + Convert.ToInt32(TF.Substring(3, 2));    //IIF(MHF < MHI,MHF+12,MHF)*60+VAL(SUBSTR(TB_MSTR_EMBARQUE.HORA_FIN,4,2))
            else
                T2 = (MHF * 60) + Convert.ToInt32(TF.Substring(3, 2));
            if (Math.Floor(Convert.ToDecimal(((T2 - T1) / 60))) >= 1)
                T3 = Math.Floor(Convert.ToDecimal(((T2 - T1) / 60)));    //=IIF(INT((T2-T1)/60)>=1,INT((T2-T1)/60),0)
            if (T3 >= 1)
                T4 = Convert.ToDecimal(((T2 - T1) / 60) - T3);
            else  //IIF(T3>=1,((T2-T1)/60)-T3,((T2-T1)/60))
                T4 = (T2 - T1) / 60;

            decimal T5 = Math.Round(T4 * 60, 2);
            if (T3 > 0)  //cad = IIF(T3>0,STR(T3,2)+':'+STR(T5,2)+' HRS',STR(T5,2)+' MIN')
                cad = T3.ToString().Trim().PadLeft(2, '0') + ':' + T5.ToString().Trim().PadLeft(2, '0') + " HRS";
            else
                if (T5 == 0)
                    cad = "00 MIN";
                else
                    cad = T5.ToString().Trim().PadLeft(2, '0') + " MIN";
            return cad;
        }

        private void DGDetTarPtp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGDetTarPtp.CurrentCell.ColumnIndex == 5) // RECIBO DE MATERIA PRIMA
            {
                //MessageBox.Show("Entre");
                InfoRecibo(DGDetTarPtp.CurrentRow.Cells["RECIBO"].Value.ToString());
            }

        }

        private void InfoRecibo(string Recibo)
        {
            thisConnecion.Open();
            string Mrec = Recibo;  //DGDatos.CurrentRow.Cells["RECIBO"].Value.ToString();
            string Cadena = "";
            Cadena = "SELECT * FROM TB_DET_PROD_ODP WHERE ORDP_FOLIO = '" + Program.MyGlobal.PubRecibo + "' and RMP_RECIBO = '" + Mrec + "'";
            SqlCommand cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            string MRem = "";
            string Tipo = "";
            while (Ped.Read())
            {
                Tipo = Ped["RMP_TIPO"].ToString();
            }
            if (Tipo == "REM")
            {
                string NewRec = BuscaReem(Mrec).Trim();
                if (NewRec != Mrec)
                {
                    MRem = Mrec;
                    Mrec = NewRec;
                }

            }
            if (TipoRec == "PTC")
                Cadena = "SELECT prov_clave, RCH_CLAVE, TBL_CLAVE,RPT_CODIGO,RPT_FECHA,RPT_observaciones,vari_clave,lin_clave FROM TB_MSTR_RECEPCION_PT WHERE RPT_RECIBO = '" + Mrec + "'";
            else
                if (Tipo.Trim() == "MP")
                    Cadena = "SELECT prov_clave, RCH_CLAVE, TBL_CLAVE, Mp_Cve_fecha,RMP_FECHA,rmp_observaciones,vari_clave,lin_clave TBL_CLAVE  FROM TB_MSTR_RECEPCION_MP WHERE RMP_RECIBO = '" + Mrec + "'";
                else
                    Cadena = "SELECT prov_clave, RCH_CLAVE, TBL_CLAVE, Mp_Cve_fecha,RMP_FECHA,rmp_observaciones,vari_clave,lin_clave TBL_CLAVE  FROM TB_MSTR_RECEPCION_ESPARRAGO WHERE RMP_RECIBO = '" + Mrec + "'";
            //Cadena = "SELECT * FROM TB_MSTR_RECEPCION_MP WHERE RMP_RECIBO = '" +Mrec + "'";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Reci;
            Reci = cmnd2.ExecuteReader();
            string MPROV = "", MRAN = "", MTAB = "", mLOT = "", mFEC = "", mOBS = "", mVar = "", mLin = "";
            while (Reci.Read())
            {
                //MPROV= Reci["prov_clave"].ToString ();
                //MRAN= Reci["RCH_CLAVE"].ToString();
                //MTAB = Reci["TBL_CLAVE"].ToString();
                //mLOT = Reci["Mp_Cve_fecha"].ToString();
                // mFEC = Reci["RMP_FECHA"].ToString();
                // mOBS = Reci["rmp_observaciones"].ToString();
                MPROV = Reci[0].ToString();
                MRAN = Reci[1].ToString();
                MTAB = Reci[2].ToString();
                mLOT = Reci[3].ToString();
                mFEC = Reci[4].ToString();
                mOBS = Reci[5].ToString();
                mVar = Reci[6].ToString();
                mLin = Reci[7].ToString();
            }
            string DetaRec = "";
            DetaRec = (MRem.Trim().Length > 0) ? "Recibo Reempaque: " + MRem + System.Environment.NewLine : "";
            DetaRec = DetaRec + "Recibo: " + Mrec + "  Lote: " + mLOT + System.Environment.NewLine;
            DetaRec = DetaRec + "Fecha:" + mFEC + System.Environment.NewLine;
            Cadena = "SELECT PROV_NOMBRE FROM tb_cat_proveedor WHERE PROV_CLAVE = '" + MPROV + "'";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            DetaRec = DetaRec + "Prov: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim() + System.Environment.NewLine;
            Cadena = "SELECT RCH_NOMBRE FROM tb_cat_ranchos WHERE PROV_CLAVE = '" + MPROV + "' AND RCH_CLAVE = '" + MRAN + "'";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            DetaRec = DetaRec + "Ran: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim() + System.Environment.NewLine;
            Cadena = "SELECT TBL_NOMBRE FROM tb_cat_tablas WHERE PROV_CLAVE = '" + MPROV + "' AND RCH_CLAVE = '" + MRAN + "' AND TBL_CLAVE = '" + MTAB + "'";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            DetaRec = DetaRec + "Tab: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim() + System.Environment.NewLine + System.Environment.NewLine;
            Cadena = "SELECT vari_nombre FROM tb_cat_variedad WHERE vari_clave = '" + mVar + "' AND LIN_CLAVE = '" + mLin + "'";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            DetaRec = DetaRec + "Var: " + Convert.ToString(cmnd2.ExecuteScalar()).Trim() + System.Environment.NewLine + System.Environment.NewLine;
            DetaRec = DetaRec + mOBS;
            MessageBox.Show(DetaRec, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            thisConnecion.Close();
        }

        public string BuscaReemAnt(string Rec)
        {
            string Cadena = "SELECT * FROM TB_DET_PROD_TAR WHERE ORDE_FOLIO = '" + Rec + "'";
            SqlCommand cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Rempa;
            Rempa = cmnd2.ExecuteReader();
            string hay = "N";
            string Mrec = "";
            while (Rempa.Read())
            {
                string MTAR = Rempa["TARIMA"].ToString();
                string MORDP = Rempa["RMP_RECIBO"].ToString();
                string MPROD = Rempa["PROD_CLAVE"].ToString();
                Cadena = "SELECT * FROM TB_DET_ETI_FINAL WHERE FOLIO = '" + MORDP + "' AND CVE_PROD = '" + MPROD + "' AND TARIMA = '" + MTAR + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader tar;
                tar = cmnd2.ExecuteReader();
                while (tar.Read())
                {
                    //MRem = Mrec;
                    Mrec = tar["RECIBO"].ToString();
                    if (Convert.ToInt32(Mrec) < 100000)
                    {
                        Mrec = BuscaReem(Mrec).Trim();
                    }
                    hay = "S";
                }
                if (hay == "S")
                    break;
            }
            return Mrec;
        }

        public string BuscaReem(string Rec)
        {
            string Cadena = "SELECT A.*, B.rmp_tipo FROM TB_DET_PROD_TAR A, tb_det_prod_ode B WHERE A.ORDE_FOLIO = '" + Rec + "' AND A.orde_folio = B.orde_folio";
            SqlCommand cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Rempa;
            Rempa = cmnd2.ExecuteReader();
            string hay = "N";
            string Mrec = "";
            while (Rempa.Read())
            {
                string MTAR = Rempa["TARIMA"].ToString();
                string MORDP = Rempa["RMP_RECIBO"].ToString();
                string MPROD = Rempa["PROD_CLAVE"].ToString();
                string MTIP = Rempa["RMP_TIPO"].ToString();
                TipoRec = Rempa["RMP_TIPO"].ToString();
                //if (Convert.ToInt32(MORDP) > 170000)
                //{
                //    Mrec = MORDP;
                //    break;
                //}
                if (MTIP == "PTP")
                    Cadena = "SELECT * FROM TB_DET_ETI_FINAL WHERE FOLIO = '" + MORDP + "' AND CVE_PROD = '" + MPROD + "' AND TARIMA = '" + MTAR + "'";
                else
                    Cadena = "SELECT RPT_RECIBO AS RECIBO FROM TB_MSTR_RECEPCION_PT WHERE RPT_RECIBO = '" + MORDP + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader tar;
                tar = cmnd2.ExecuteReader();
                while (tar.Read())
                {
                    //MRem = Mrec;
                    Mrec = tar["RECIBO"].ToString();
                    if (Convert.ToInt32(Mrec) < 60000)
                    {
                        Mrec = BuscaReem(Mrec).Trim();
                    }
                    hay = "S";
                }
                if (hay == "S")
                    break;
            }
            return Mrec;
        }

        public string Fn_Factura(string Mped, string Tipo)
        {
            thisConnecion.Open();
            int NewP = Convert.ToInt32(Mped);
            Mped = NewP.ToString();
            string cad = "";
            string Cadena = "";
            if (Tipo == "EXP")
                Cadena = "SELECT FCN_FOLIO FROM TB_MSTR_FACTURAS_NAL WHERE PDN_FOLIO = '" + Mped + "' AND FCN_LUGAR = 'EXP'";
            else
                Cadena = "SELECT FCN_FOLIO FROM TB_MSTR_FACTURAS_NAL WHERE PDN_FOLIO = '" + Mped + "' AND FCN_LUGAR <> 'EXP'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cad = Convert.ToString(cmd.ExecuteScalar());
            thisConnecion.Close();
            return cad;
        }

        public string Fn_TraeNomCli(string var_folio, string var_tipo)
        {
            thisConnecion.Open();
            string cad = "", bd = "";
            if (var_tipo == "NAL")
                bd = "TB_MSTR_PEDIDOS_NAL A";
            else
                bd = "TB_MSTR_PEDIDOS_EXP A";
            string Cadena = "SELECT B.cnte_nombre FROM " + bd + ", tb_cat_CLIENTE B WHERE PDN_FOLIO = '" + var_folio + "' AND A.CNTE_CLAVE = B.CNTE_CLAVE";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cad = Convert.ToString(cmd.ExecuteScalar());
            thisConnecion.Close();
            return cad;
        }

        private void CreaTabla()
        {
            //DGDetEmb.Rows.Add(Row1["EMB_FOLIO"], Fac, Row1["HORA_TRAILER"], Cli, Row1["cajas"], Row1["temp"], Row1["tarima"], Row1["fec_cad"], Row1["TRANSPORTE"], Row1["responsable"], Row1["no_trailer"]);
            DetEmb.Columns.Add("OrdVenta");
            DetEmb.Columns.Add("Factura");
            DetEmb.Columns.Add("Fecha");
            DetEmb.Columns.Add("Cliente");
            DetEmb.Columns.Add("Cajas");
            DetEmb.Columns.Add("Temp");
            DetEmb.Columns.Add("Tarima");
            DetEmb.Columns.Add("Fec Cad");
            DetEmb.Columns.Add("Transporte");
            DetEmb.Columns.Add("Responsable");
            DetEmb.Columns.Add("Trailer");
        }
 
       
    }
}

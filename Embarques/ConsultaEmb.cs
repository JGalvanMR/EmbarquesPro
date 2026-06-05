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
    public partial class ConsultaEmb : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DetPed = new DataTable();
        public DataTable Inven = new DataTable();
        public string Obs = "";
        public string SplXCar = "";

        public ConsultaEmb()
        {
            InitializeComponent();
        }

        private void ConsultaEmb_Load(object sender, EventArgs e)
        {
            anterior();
            CreaQr(Program.MyGlobal.NoPedido.ToString());
        }

        private void anterior()
        {
            CreaTable();
            DataTable Surtido = new DataTable();
            LblCliente.Text = Program.MyGlobal.CveCliente;
            LblPed.Text = "PEDIDO " + Program.MyGlobal.NoPedido;
            LblObs.Text = Obs;
            LblTotSpl.Text = SplXCar;
            string Cadena = "";
            string Folio = (Program.MyGlobal.TipoPed == "EXP") ? "0" + Program.MyGlobal.NoPedido.ToString() : Program.MyGlobal.NoPedido.ToString();
            thisConnecion.Open(); //DBGAB
            Cadena = "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '" + Folio + "' AND NALEXP = '" + Program.MyGlobal.TipoPed + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];
            Cadena = " SELECT PROD_CLAVE, SUM(CAJAS) AS SURTIDO FROM tb_det_split  WHERE emb_folio = '" + Folio + "' AND ESTATUS = 'A' GROUP BY PROD_CLAVE";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "SPLIT");
            DataTable Split = ds.Tables["SPLIT"];
            thisConnecion.Close(); //DBGAB
            thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0, TotSp = 0;
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE, B.prod_num_tarimas FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Program.MyGlobal.NoPedido.ToString() + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();
            //= SQLEXEC(gnHandle, "SELECT * FROM TB_PED_EMBARQUE WHERE EMB_FOLIO = '"+MFAC+"' and NALEXP = '"+MTIP+"'",'DETPEDIDOS')
            decimal TotTar = 0;
            while (Ped.Read())
            {
                int T = 0;
                foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    T = Convert.ToInt32(row["cant_sur"]);
                    TotS = TotS + Convert.ToInt32(row["cant_sur"]);
                }
                TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                Int32 TSplit = 0, Exis = 0;
                foreach (DataRow Row in Split.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    TSplit = Convert.ToInt32(Row["Surtido"]);
                    TotSp += TSplit;
                }
                foreach (DataRow row in Inven.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                {
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]));
                    //Exis = (Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"]))) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]) - T;
                    Exis = Convert.ToInt32(row["CANTIDAD"]) - (Convert.ToInt32(row["SURTIDO"])) + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                }
                DetPed.Rows.Add(Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]).ToString("#,###"), T.ToString("#,###"), TSplit.ToString("#,###"), Exis.ToString("#,##0"));
                string nom1 = Ped["PROD_CLAVE"].ToString();
                int num1 = Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                int num2 = Convert.ToInt32(Ped["prod_num_tarimas"]);
                TotTar = TotTar + (Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]) / Convert.ToInt32(Ped["prod_num_tarimas"]));
            }
            LblTar.Text = "Tot. Tarimas: " + TotTar.ToString("##.0");
            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("#,###");
            TxtTotS.Text = TotS.ToString("#,###");
            TxtTotSpl.Text = TotSp.ToString("#,##0");
            DGDetPed.DataSource = DetPed;
            FormatoSalida();
        }

        private void CreaTable()
        {
            DetPed.Columns.Add("PROD_CLAVE", typeof(string));     //0
            DetPed.Columns.Add("PROD_NOMBRE", typeof(string));      //1
            DetPed.Columns.Add("PEDIDO", typeof(string));       //2
            DetPed.Columns.Add("SURTIDO", typeof(string));    //3
            DetPed.Columns.Add("SPLIT", typeof(string));    //4
            DetPed.Columns.Add("EXISTENCIA", typeof(string));    //5
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                decimal mExis = (DetPed.Rows[i]["EXISTENCIA"].ToString().Trim().Length == 0) ? 0 : Convert.ToDecimal(DetPed.Rows[i]["EXISTENCIA"]);
                if (Convert.ToString(DetPed.Rows[i]["PEDIDO"]) != Convert.ToString(DetPed.Rows[i]["SURTIDO"]))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                if (Convert.ToInt32(DetPed.Rows[i]["PEDIDO"].ToString().Replace(",", "")) > mExis)
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
            }
            DGDetPed.Columns[0].HeaderText = "PRODUCTO";
            DGDetPed.Columns[1].HeaderText = "NOMBRE";
            DGDetPed.Columns[4].HeaderText = "SPLIT ARMADO";
            DGDetPed.Columns[0].Width = 120;
            DGDetPed.Columns[1].Width = 500;
            DGDetPed.Columns[2].Width = 70;
            DGDetPed.Columns[3].Width = 70;
            DGDetPed.Columns[4].Width = 70;
            DGDetPed.Columns[5].Width = 80;
            DGDetPed.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDetPed.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void LLenaPed(string Mpedido)
        {
            LblPed.Text = "Pedido: " + Mpedido;
            //TxtPeso.Text = Fn_Peso(Mpedido, Program.MyGlobal.TipoPed).ToString("##,##0.00");
            DataTable Surtido = new DataTable();
            //LblCliente.Text = Program.MyGlobal.CveCliente;
            if (DetPed.Rows.Count > 0)
                DetPed.Rows.Clear(); //DGDetPed.DataSource = "";
            string Cadena = "";
            thisConnecion.Open(); //DBGAB
            Cadena = "SELECT * FROM TB_PED_EMBARQUE A WHERE EMB_FOLIO = '" + Mpedido + "' AND NALEXP = '" + Program.MyGlobal.TipoPed + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];

            thisConnecion.Close(); //DBGAB
            thisConnecion.Open();
            SqlCommand cmnd2;
            Int32 TotS = 0, TotP = 0;
            Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B WHERE A.PDN_FOLIO = '" + Mpedido + "' AND A.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND A.PROD_CLAVE = B.PROD_CLAVE ORDER BY B.PROD_NOMBRE";
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
                    DetPed.Rows.Add(row["PROD_CLAVE"].ToString(), "", 0, T.ToString("#,###"));
                }
            }

            thisConnecion.Close();
            TxtTotP.Text = TotP.ToString("#,###");
            TxtTotS.Text = TotS.ToString("#,###");
            DGDetPed.DataSource = DetPed;
        }

        public string getNombreTarima(string pdn_tipotar)
        {
            string nombre_tarima = "";

            string Cadena = "SELECT ISNULL(NULLIF(t.Nom_Tarima, ''), e.emp_nombre) AS NombreFinal FROM Tb_Cat_Tarima t FULL OUTER JOIN tb_cat_empaques e ON t.Id_Tarima = e.emp_clave WHERE ISNULL(t.Id_Tarima, e.emp_clave) = '" + pdn_tipotar + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            nombre_tarima = Convert.ToString(cmd.ExecuteScalar());

            return nombre_tarima.Trim();
        }
        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            decimal sumataraprox = 0, sumacomp = 0, sumasplit = 0, decimales = 0, enteros = 0;
            Int32 sumapedido = 0;
            String drawString1 = " ", drawString2 = " ", drawString3 = " ", drawString4 = " ", drawobs = " ", drawryan = " ";
            String drawLinea = " ", drawProd = " ", drawPedi = " ", sumatar = " ", taraprox = " ", sumcomp = " ", sumdeci = " ", drawcajas = " ", drawtaraprox = " ", drawobsprod = " ";
            String drawstring8 = " ", drawstring9 = " ", drawstring10 = " ", drawstring11 = " ", drawstring12 = " ";
            DateTime dt = DateTime.Now;
            //int cont = 148, cont1 = 165;
            int cont = 148, cont1 = 160, cont2 = 160;
            thisConnecion.Open();
            SqlCommand cmnd2;
            string Cadena = "";
            string DB = "";
            if (Program.MyGlobal.TipoPed == "NAL")
                DB = "TB_MSTR_PEDIDOS_NAL";
            else
                DB = "TB_MSTR_PEDIDOS_EXP";
            Cadena = "SELECT A.*,B.PDN_MONTO_TRANSPORTE,B.CVE_SUBCLI,B.CNTE_CLAVE,B.PROV_CLAVE,B.PDN_TRANSPORTE,B.PDN_OBSERVACION,B.PDN_PEDSIGMA,B.ryandigital,B.PDN_SELLO,C.PROD_NOMBRE,C.prod_num_tarimas FROM " + DB + " B, TB_DET_PEDIDOS A, TB_CAT_PRODUCTO C WHERE B.PDN_FOLIO = '" + Program.MyGlobal.NoPedido.ToString() + "' AND B.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND  B.PDN_FOLIO = A.PDN_FOLIO AND B.PDN_TIPO = A.PDN_TIPO AND A.PROD_CLAVE = C.PROD_CLAVE ORDER BY C.PROD_NOMBRE";
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = Cadena;
            SqlDataReader Ped;
            Ped = cmnd2.ExecuteReader();

            //SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            //SqlCommand cmd = new SqlCommand(Cadena);
            //cmd.Connection = thisConnecion;
            // Ped = cmd.ExecuteReader();

            string sub_cli = "", TxtCve = "", Nomcli = "", txttalonemb = "";
            string MnomProv = "", PedObs = "", PedSigma = "";
            string ryandigital = "", PedSello = "";

            string tarima = "", subcli = "";

            //foreach (DataRow row in Cat_Prov.Select("prov_clave = '" + Ped["PROV_CLAVE"].ToString() + "'"))
            MnomProv = Program.MyGlobal.CveCliente;  // DGPedidos.CurrentRow.Cells["NOMPROV"].Value.ToString(); //row["prov_nombre"].ToString();
            Nomcli = Program.MyGlobal.CveProv;   //DGPedidos.CurrentRow.Cells["NOMCLI"].Value.ToString();
            // Create font and brush. 
            Font drawFont = new Font("Arial", 8);//encabezado
            Font drawFont1 = new Font("PF Barcode 39", 25);//código de barras
            Font drawFont3 = new Font("Arial", 5);//tarima
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing. 
            PointF drawPoint = new PointF(10.0F, 10.0F);//encabezado
            PointF drawPoint1 = new PointF(620.0F, 20.0F);//codigo de barras            
            PointF drawPoint2 = new PointF(00.0f, 42.0f);//linea1
            PointF drawPoint3 = new PointF(0.0f, 55.0f);//datos1
            PointF drawpoint4 = new PointF(450.0F, 55.0F);
            PointF drawpoint5 = new PointF(0.0F, 68.0F);
            PointF drawpoint6 = new PointF(540.0F, 68.0F);
            PointF drawpoint7 = new PointF(0.0F, 82.0F);
            PointF drawpoint8 = new PointF(540.0F, 82.0F);
            PointF drawpoint9 = new PointF(0.0F, 105.0F);
            PointF drawPoint4 = new PointF(00.0f, 90.0f);//linea2
            PointF drawPoint5 = new PointF(00.0f, 130.0f);//linea3
            drawString2 = "_________________________________________________________________________________________________________________________________________";

            #region
            decimal resulta = 0;
            while (Ped.Read())
            {
                tarima = getNombreTarima(Ped["pdn_tipotar"].ToString().Trim());
                subcli = Ped["pdn_subcli"].ToString().Trim();

                sub_cli = Ped["CVE_SUBCLI"].ToString();
                TxtCve = Ped["CNTE_CLAVE"].ToString();
                //Nomcli = Ped["CNTE_NOMBRE"].ToString();
                txttalonemb = Ped["pdn_transporte"].ToString();
                PedObs = Ped["pdn_observacion"].ToString();
                PedSigma = Ped["pdn_pedsigma"].ToString();
                ryandigital = Ped["ryandigital"].ToString();
                PedSello = Ped["pdn_sello"].ToString();
                if (Convert.ToInt32(Ped["prod_num_tarimas"]) != 0)
                    resulta = Convert.ToDecimal(Ped["pdn_num_unidades"]) / Convert.ToInt32(Ped["prod_num_tarimas"]);

                drawLinea = string.Format("{0}", Convert.ToString(Ped["lin_CLAVE"]));
                drawProd = string.Format("{0}\t{1}", Convert.ToString(Ped["prod_clave"]), Convert.ToString(Ped["prod_nombre"]));
                drawPedi = string.Format("|               |                     |    \t\t            |\r\n");
                //drawPedi = string.Format("|               |                     |    \t\t\t\r\n");
                drawcajas = (Convert.ToDecimal(Ped["PDN_NUM_UNIDADES"])).ToString("#,###,###");
                drawtaraprox = resulta.ToString("0.00");

                if (Convert.ToString(Ped["pdn_observaciones"]).ToString().Trim().Length > 30)
                {
                    String cadena = Convert.ToString(Ped["pdn_observaciones"]).Substring(0, 30);
                    PointF drawPointobs = new PointF(620.0F, cont);
                    String cadena2 = Convert.ToString(Ped["pdn_observaciones"]).Substring(30);
                    PointF drawPointobs2 = new PointF(620.0F, cont + 13);
                    e.Graphics.DrawString(cadena, drawFont, drawBrush, drawPointobs);
                    e.Graphics.DrawString(cadena2, drawFont, drawBrush, drawPointobs2);
                }
                else
                {
                    drawobsprod = Convert.ToString(Ped["pdn_observaciones"]);
                    PointF drawPointobs = new PointF(620.0F, cont);
                    e.Graphics.DrawString(drawobsprod, drawFont, drawBrush, drawPointobs);
                }

                String pedimento = string.Format("{0,15}", Convert.ToString(Ped["pdn_pedimento"]));
                String fecpedimento = Convert.ToString(Ped["pdn_fechap"]);
                String aduan = Convert.ToString(Ped["pdn_aduana"]);

                PointF drawPointlin = new PointF(0.0F, cont);
                PointF drawPointProd = new PointF(30.0f, cont);
                PointF drawPointPedi = new PointF(453.0F, cont);
                PointF drawPointcajas = new PointF(470.0F, cont);
                PointF drawPointtar = new PointF(530.0F, cont);
                PointF drawpedi = new PointF(30.0f, cont + 13);
                PointF drawfecpedi = new PointF(100.0F, cont + 13);
                PointF drawadua = new PointF(170.0F, cont + 13);

                PointF drawtar = new PointF(720.0F, cont);
                PointF drawsubcli = new PointF(720.0F, cont + 13);

                e.Graphics.DrawImage(PbxQR.Image, 10, 10);
                e.Graphics.DrawString(drawLinea, drawFont, drawBrush, drawPointlin);
                e.Graphics.DrawString(drawProd, drawFont, drawBrush, drawPointProd);
                e.Graphics.DrawString(drawPedi, drawFont, drawBrush, drawPointPedi);
                e.Graphics.DrawString(drawcajas, drawFont, drawBrush, drawPointcajas);
                e.Graphics.DrawString(drawtaraprox, drawFont, drawBrush, drawPointtar);
                e.Graphics.DrawString(pedimento, drawFont, drawBrush, drawpedi);
                e.Graphics.DrawString(fecpedimento, drawFont, drawBrush, drawfecpedi);
                e.Graphics.DrawString(aduan, drawFont, drawBrush, drawadua);

                e.Graphics.DrawString(tarima, drawFont3, drawBrush, drawtar);
                e.Graphics.DrawString(subcli, drawFont3, drawBrush, drawsubcli);

                cont = cont + 30;

                PointF drawPoint7 = new PointF(0.0F, cont1);
                e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint7);
                cont1 = cont1 + 30;
                cont2 = cont2 + 30;
                sumapedido = sumapedido + Convert.ToInt32(Ped["pdn_num_unidades"]);
                sumataraprox = sumataraprox + resulta;
                enteros = Math.Truncate(resulta);
                sumacomp = sumacomp + enteros;
                decimales = enteros - resulta;
                sumasplit = sumasplit + decimales;
                Tope++;
                if (Tope > 25)
                {
                    e.HasMorePages = true;
                    Tope = 0;
                }
                else
                    e.HasMorePages = false;
            }//for

            String drawString;

            if (Program.MyGlobal.TipoPed == "NAL")
            {
                drawString = "\t\t\t\t\tComercializadora GAB, S.A. de C.V.\r\n " +
                                           "\t\t\t\tORDEN DE VENTA NACIONAL/REPORTE DE EMBARQUES\r\n\r\n";
            }
            else
            {
                drawString = "\t\t\t\t\tComercializadora GAB, S.A. de C.V.\r\n " +
                                           "\t\t\t\tORDEN DE VENTA EXPORTACIÓN/REPORTE DE EMBARQUES\r\n\r\n";
            }

            drawString1 = "*" + Program.MyGlobal.NoPedido + "*\r\n";
            //drawString1 = "*" + txtfolio.Text + "*\r\n";

            drawString3 = "\tCLIENTE: " + TxtCve.Trim() + " " + Nomcli.Trim() + "";
            drawString4 = "\t\tFOLIO: " + Program.MyGlobal.NoPedido + "\t FECHA: " + DateTime.Now.ToShortDateString() + "\r\n";
            drawstring8 = "\t" + sub_cli.Trim() + "";
            drawstring9 = "HORA DE EMISION: " + dt.ToString("T").Trim() + "\r\n";
            drawstring10 = "\tTRANSPORTISTA: " + MnomProv.Trim() + "\t CARTA PORTE: " + txttalonemb.Trim() + "";
            drawstring11 = "IRAPUATO GTO.\r\n\r\n";
            drawstring12 = "\tDETALLE DE LA ORDEN DE VENTA\r\n" +
                                "Linea     Producto  \t\t\t\t\t\t\t                 |  Pedido  |  Tar. Aprox.  |  Obs  \t\t\t|  Tarima\r\n\r\n";

            e.Graphics.DrawString(drawString1, drawFont1, drawBrush, drawPoint1);//código de barras            
            e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);//encabezado            
            e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint2);//linea1
            e.Graphics.DrawString(drawString3, drawFont, drawBrush, drawPoint3);//datos1
            e.Graphics.DrawString(drawString4, drawFont, drawBrush, drawpoint4);//
            e.Graphics.DrawString(drawstring8, drawFont, drawBrush, drawpoint5);//
            e.Graphics.DrawString(drawstring9, drawFont, drawBrush, drawpoint6);//
            e.Graphics.DrawString(drawstring10, drawFont, drawBrush, drawpoint7);//
            e.Graphics.DrawString(drawstring11, drawFont, drawBrush, drawpoint8);//
            e.Graphics.DrawString(drawstring12, drawFont, drawBrush, drawpoint9);//
            e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint4);//linea2
            e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint5);//linea3                       

            sumatar = sumapedido.ToString("#,###,###");//suma de tarimas
            taraprox = "Tarimas Aprox : " + sumataraprox.ToString("0.00");//tarimas aproximadas
            sumcomp = "    Completas : " + sumacomp.ToString("0.00");//tarimas completas
            sumdeci = "        Split : " + (sumasplit * -1).ToString("0.00");//decimales tarimas
            drawobs = "\t" + PedObs.Trim();//txt observaciones
            drawPedi = "\t" + PedSigma.Trim();  // txt pedido sigma


            drawryan = "Ryan Digital : " + ryandigital + "";
            String drawString5 = "\tCargo : __________________________ \t Destino : __________________________ \t Sello : " + PedSello.Trim() + " \r\n";
            String drawString6 = "\tMe comprometo a mantener la temperatua a : ______________ F \t Nombre del chofer : ______________________ \tFirma Chofer : __________\r\n";
            //String drawchofer = txtnomtrans.Text;
            //if (txtnomtrans.Text == " ")
            //{
            //    drawchofer = " ";
            //}

            //else
            //{
            //    int limite = txtnomtrans.TextLength;
            //    limite = limite - 20;
            //    if (limite > 0)
            //        drawchofer = drawchofer.Remove(20, limite);
            //}
            String drawString7 = "\tTERMOGRAFO : _________________ \t Empezo a cargar (horas) : __________ \t Termino (horas) : __________";
            PointF drawsumatar = new PointF(470.0f, cont1);//suma de tarimas              
            PointF drawaproxtar = new PointF(600.0f, cont1);//tarimas aproximadas
            cont1 = cont1 + 10;
            PointF drawcomptar = new PointF(610.0f, cont1);//tarimas completas
            cont1 = cont1 + 10;
            PointF drawdecitar = new PointF(620.0f, cont1);//tarimas completas
            PointF drawryandig = new PointF(553.0f, 1000.0f);//ryan
            PointF drawobser = new PointF(0.00f, 995.0f);//txt observaciones
            PointF drawsigma = new PointF(0.00f, 1005.0f);//txt pedidosigma
            PointF drawstring5 = new PointF(0.00f, 1020.0f);//string5
            PointF drawchofername = new PointF(505.0f, 1035.0f);//nombre chofer
            PointF drawstring6 = new PointF(0.00f, 1035.0f);//string6
            PointF drawstring7 = new PointF(0.00f, 1050.0f);//string7            
            e.Graphics.DrawString(sumatar, drawFont, drawBrush, drawsumatar);//suma de tarimas
            e.Graphics.DrawString(taraprox, drawFont, drawBrush, drawaproxtar);//tarimas aproximadas
            e.Graphics.DrawString(sumcomp, drawFont, drawBrush, drawcomptar);//tarimas completas
            e.Graphics.DrawString(sumdeci, drawFont, drawBrush, drawdecitar);//tarimas completas
            e.Graphics.DrawString(drawryan, drawFont, drawBrush, drawryandig);//ryan
            e.Graphics.DrawString(drawobs, drawFont, drawBrush, drawobser);//txt observaciones
            e.Graphics.DrawString(drawPedi, drawFont, drawBrush, drawsigma);//txt pedido sigma
            e.Graphics.DrawString(drawString5, drawFont, drawBrush, drawstring5);//string5
            //e.Graphics.DrawString(drawchofer, drawFont, drawBrush, drawchofername);//nombre chofer
            e.Graphics.DrawString(drawString6, drawFont, drawBrush, drawstring6);//string6
            e.Graphics.DrawString(drawString7, drawFont, drawBrush, drawstring7);//string7
            cont1 = 160;
            #endregion
            sumataraprox = sumacomp = sumasplit = decimales = enteros = 0;
            sumapedido = 0;
            e.HasMorePages = false;
            thisConnecion.Close();

        }

        public int Tope = 0;

        private void BtnImp_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Ingrese Contraseña: ", "Imprimir Orden De Venta");

            if (promptValue.Trim().Length > 0)
            {

                string usuario = "";
                string clave = "";
                thisConnecion.Open();
                SqlCommand cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = "SELECT TOP 1 usuario, clave  FROM Tb_Autoriza_Reetiquetado where password = '" + promptValue.Trim() + "' and obs = 'A' And clave = '03'";
                SqlDataReader reader2 = cmnd2.ExecuteReader();
                while (reader2.Read())
                {
                    usuario = reader2.GetSqlString(0).Value.ToString();
                    clave = reader2.GetSqlString(1).Value.ToString();
                }
                reader2.Close();
                thisConnecion.Close();


                if (usuario.Trim() != "")
                {

                    PrintDocument pd = new PrintDocument();
                    //Program.MyGlobal.NoPedido = Convert.ToInt32(DGPedidos.CurrentRow.Cells["PEDIDO"].Value);
                    //Program.MyGlobal.TipoPed = DGPedidos.CurrentRow.Cells["TIPO"].Value.ToString();
                    Tope = 0;
                    pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                    //if (Utilerias.Nombre_impresora == null)
                    //{
                    //    Utilerias.Nombre_impresora = "Cannon iR2220/iR3320 PCL6";
                    //}
                    ////else
                    ////{
                    //pd.PrinterSettings.PrinterName = Utilerias.Nombre_impresora;
                    //}
                    pd.Print();
                    thisConnecion.Open();
                    string Cadena = "";
                    if (Program.MyGlobal.TipoPed.Trim() == "NAL")
                        Cadena = "Update tb_mstr_pedidos_nal set pdn_impreso = 'S', pdn_fechaimp = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "' WHERE pdn_folio = '" + Program.MyGlobal.NoPedido.ToString() + "' AND PDN_TIPO = '" + Program.MyGlobal.TipoPed + "'";
                    else
                        Cadena = "Update tb_mstr_pedidos_exp set pdn_impreso = 'S', pdn_fechaimp = '" + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "' WHERE pdn_folio = '" + Program.MyGlobal.NoPedido.ToString() + "' AND PDN_TIPO = '" + Program.MyGlobal.TipoPed + "'";
                    SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
                    cmd.ExecuteNonQuery();

                    Cadena = "INSERT INTO  tb_det_acceso_celulares ( fecha, imei, nom_usu, sistema, folio, version, estado) " +
                                    "VALUES(GETDATE(),'7.1','" + usuario + "','MonEmba','" + Program.MyGlobal.NoPedido.ToString().Trim() + "','','I')";
                    cmd = new SqlCommand(Cadena, thisConnecion);
                    cmd.ExecuteNonQuery();

                    thisConnecion.Close();

                }
                else
                {
                    MessageBox.Show("Contraseña Incorrecta, Vuelva a intentarlo");
                }

            }
        }

        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 200,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = "Contraseña: ", Width = 100 };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, PasswordChar = '*', Width = 100 };
                Button confirmation = new Button() { Text = "Ok", Left = 50, Width = 100, Top = 80, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }



        private void BtnSplit_Click(object sender, EventArgs e)
        {
            ConsDetSplit DetSplit = new ConsDetSplit();
            DetSplit.ShowDialog(this);
        }

        private void CmbPed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DGDetPed_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5) // Campo de Existencia
            {
                ConsFoliosCaducidades frmCad = new ConsFoliosCaducidades();
                frmCad.mNom = DGDetPed.CurrentRow.Cells["prod_nombre"].Value.ToString().Trim();
                frmCad.mProd = DGDetPed.CurrentRow.Cells["prod_clave"].Value.ToString().Trim();
                frmCad.ShowDialog(this);
            }
        }



        private void CreaQr(string Folio)
        {
            BarcodeLib.Barcode.QRCode qrbarcode = new BarcodeLib.Barcode.QRCode();

            // Select QR Code data encoding type: numeric, alphanumeric, byte, and Kanji to select from.
            qrbarcode.Encoding = BarcodeLib.Barcode.QRCodeEncoding.Auto;
            qrbarcode.Data = Folio;

            // Adjusting QR Code barcode module size and quiet zones on four sides.
            qrbarcode.ModuleSize = 1;
            qrbarcode.LeftMargin = 1; //12
            qrbarcode.RightMargin = 1; //12 
            qrbarcode.TopMargin = 1; //12
            qrbarcode.BottomMargin = 1; //12

            // Select QR Code Version (Symbol Size), available from V1 to V40, i.e. 21 x 21 to 177 x 177 modules.
            qrbarcode.Version = BarcodeLib.Barcode.QRCodeVersion.V1;

            // Set QR-Code bar code Reed Solomon Error Correction Level: L(7%), M (15%), Q(25%), H(30%)
            qrbarcode.ECL = BarcodeLib.Barcode.QRCodeErrorCorrectionLevel.H; //L
            qrbarcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;

            // More barcode settings here, like ECI, FNC1, Structure Append, etc.

            // save barcode image into your system
            //if (File.Exists(@"c:/reportes/qrcode.png"))
            //    File.Delete(@"c:/reportes/qrcode.png");
            //qrbarcode.drawBarcode(@"c:/reportes/qrcode.png");

            // Generate QR Code barcode & output to byte array
            byte[] barcodeInBytes = qrbarcode.drawBarcodeAsBytes();

            MemoryStream ms = new MemoryStream(barcodeInBytes, 0, barcodeInBytes.Length);
            ms.Write(barcodeInBytes, 0, barcodeInBytes.Length);
            Image newImage = Image.FromStream(ms, true);//Exception occurs here
            PbxQR.Image = newImage;


            // Generate QR Code barcode to Graphics object
            //Graphics graphics =  ... ;
            //barcode.drawBarcode(graphics);

            // Generate QR Code barcode and output to HttpResponse object
            //HttpResponse response = ...;
            //qrbarcode.drawBarcode(response);

            // Generate QR Code barcode and output to Stream object
            //Stream stream = "@c:\reporte\qr";
            //qrbarcode.drawBarcode(stream);
        }

        private void btnGPS_Click(object sender, EventArgs e)
        {
            Geolocalizacion geolo = new Geolocalizacion();
            geolo.ShowDialog(this);
        }
    }
}

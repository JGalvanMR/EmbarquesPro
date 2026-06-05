using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;

namespace Embarques
{
    public partial class autorizacioncliente : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        string ordenventa = "";
        string tipoventa = "";
        string prov_nombre = "";
        string cliente_nombre = "";
        int Tope = 0;
        string pdn_origen = "";
        DataTable Acumulado = new DataTable();

        public autorizacioncliente(string folio, string tipo, string prov_nom, string clien_nom)
        {
            string tb = "tb_mstr_pedidos_nal";
            InitializeComponent();
            ordenventa = folio;
            tipoventa = tipo;
            prov_nombre = prov_nom;
            cliente_nombre = clien_nom;

            pdn_origen = ordenventa;
            Tope = 0;
            if (tipoventa != "NAL")
            {
                tb = "tb_mstr_pedidos_exp";
            }

            CreaTablas();

            thisConnecion.Open();
            SqlCommand cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = "SELECT pdn_pedorigen FROM " + tb + " WHERE pdn_folio = '" + folio + "'";
            SqlDataReader reader2 = cmnd2.ExecuteReader();
            while (reader2.Read())
            {
                string pedidoorigen = reader2.GetSqlInt32(0).Value.ToString();
                if ((pedidoorigen.Trim().Length > 0) && (pedidoorigen.ToString().Trim() != "0"))
                {
                    pdn_origen = reader2.GetSqlString(0).Value.ToString();
                }
            }
            reader2.Close();
            thisConnecion.Close();

            cmbcliente.Items.Add("Seleccione un Cliente");
            thisConnecion.Open();
            cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = "select cnte_clave FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + pdn_origen + "' OR pdn_pedorigen = '" + pdn_origen + "' and PDN_ESTATUS != 'C' UNION select cnte_clave FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pdn_origen + "' OR pdn_pedorigen = '" + pdn_origen + "' and PDN_ESTATUS != 'C' GROUP BY cnte_clave ORDER BY cnte_clave";
            reader2 = cmnd2.ExecuteReader();
            while (reader2.Read())
            {
                cmbcliente.Items.Add(reader2.GetSqlString(0).Value.ToString());

            }
            reader2.Close();
            thisConnecion.Close();
            cmbcliente.SelectedItem = "Seleccione un Cliente";
        }

        private void CreaTablas()
        {
            Acumulado.Columns.Add("lin_clave", typeof(string)); //0
            Acumulado.Columns.Add("prod_clave", typeof(string));     //1
            Acumulado.Columns.Add("prod_nombre", typeof(string));
            Acumulado.Columns.Add("pdn_num_unidades", typeof(string)); //2
            Acumulado.Columns.Add("prod_num_tarimas", typeof(string));     //3
            Acumulado.Columns.Add("observaciones", typeof(string));    //4
            Acumulado.Columns.Add("pdn_pedimento", typeof(string));    //5
            Acumulado.Columns.Add("pdn_fechap", typeof(string));    //6
            Acumulado.Columns.Add("pdn_aduana", typeof(string));    //7
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cmbcliente.Text == "")
            {
                MessageBox.Show("No se ha seleccionado el cliente");
            }
            else
            {
                if (txtpass.Text.Trim().Length > 0)
                {
                    string usuario = "";
                    string clave = "";
                    thisConnecion.Open();
                    SqlCommand cmnd2 = thisConnecion.CreateCommand();
                    cmnd2.CommandText = "SELECT TOP 1 usuario, clave  FROM Tb_Autoriza_Reetiquetado where password = '" + txtpass.Text.Trim() + "' and obs = 'A' And clave = '03'";
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
                        Program.MyGlobal.NoPedido = Convert.ToInt32(ordenventa);
                        Program.MyGlobal.TipoPed = tipoventa;
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
                                        "VALUES(GETDATE(),'7.1','" + usuario + "','MonEmba','" + pdn_origen.ToString().Trim() + "','','I')";
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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
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
            Acumulado.Rows.Clear();
            //traer todas las ordenes del cliente
            //Cadena = "SELECT A.*,B.PDN_MONTO_TRANSPORTE,B.CVE_SUBCLI,B.CNTE_CLAVE,B.PROV_CLAVE,B.PDN_TRANSPORTE,B.PDN_OBSERVACION,B.PDN_PEDSIGMA,B.ryandigital,B.PDN_SELLO,C.PROD_NOMBRE,C.prod_num_tarimas FROM " + DB + " B, TB_DET_PEDIDOS A, TB_CAT_PRODUCTO C WHERE B.PDN_FOLIO = '" + Program.MyGlobal.NoPedido.ToString() + "' AND B.PDN_TIPO = '" + Program.MyGlobal.TipoPed + "' AND  B.PDN_FOLIO = A.PDN_FOLIO AND B.PDN_TIPO = A.PDN_TIPO AND A.PROD_CLAVE = C.PROD_CLAVE ORDER BY C.PROD_NOMBRE";
            string Ordenes = "";
            thisConnecion.Open();
            SqlCommand cmnd2 = thisConnecion.CreateCommand();
            cmnd2.CommandText = "select pdn_folio FROM tb_mstr_pedidos_nal WHERE pdn_folio = '" + pdn_origen + "' OR pdn_pedorigen = '" + pdn_origen + "' and PDN_ESTATUS != 'C' and cnte_clave = '" + cmbcliente.SelectedItem.ToString().Trim() + "'  UNION select pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_folio = '" + pdn_origen + "' OR pdn_pedorigen = '" + pdn_origen + "' and PDN_ESTATUS != 'C' and cnte_clave = '" + cmbcliente.SelectedItem.ToString().Trim() + "' ORDER BY pdn_folio";
            SqlDataReader reader2 = cmnd2.ExecuteReader();
            while (reader2.Read())
            {
                string ordenac = reader2.GetSqlInt32(0).Value.ToString();
                Ordenes = Ordenes + ordenac.ToString() + ",";
            }
            reader2.Close();
            thisConnecion.Close();

            Ordenes = Ordenes.TrimEnd(',');

            decimal sumataraprox = 0, sumacomp = 0, sumasplit = 0, decimales = 0, enteros = 0;
            Int32 sumapedido = 0;
            String drawString1 = " ", drawString2 = " ", drawString3 = " ", drawString4 = " ", drawobs = " ", drawryan = " ";
            String drawLinea = " ", drawProd = " ", drawPedi = " ", sumatar = " ", taraprox = " ", sumcomp = " ", sumdeci = " ", drawcajas = " ", drawtaraprox = " ", drawobsprod = " ";
            String drawstring8 = " ", drawstring9 = " ", drawstring10 = " ", drawstring11 = " ", drawstring12 = " ";
            DateTime dt = DateTime.Now;
            //int cont = 148, cont1 = 165;
            int cont = 148, cont1 = 160, cont2 = 160;
            thisConnecion.Open();
            string Cadena = "";
            string Cadena1 = "";
            string Cadena2 = "";
            string DB = "", TBD = "";


            string sub_cli = "", TxtCve = "", Nomcli = "", txttalonemb = "";
            string MnomProv = "", PedObs = "", PedSigma = "";
            string ryandigital = "", PedSello = "";

            string tarima = "", subcli = "";

            string[] ordenesventa = Ordenes.Split(',');
            foreach (var orden in ordenesventa)
            {
                DB = "NAL";
                TBD = "Tb_Mstr_Pedidos_Nal";
                if (Convert.ToInt32(orden) < 400000)
                {
                    DB = "EXP";
                    TBD = "Tb_Mstr_pedidos_Exp";
                }
                Cadena = "SELECT A.lin_clave, A.prod_clave, A.pdn_num_unidades, A.pdn_observaciones, A.pdn_pedimento, A.pdn_fechap, A.pdn_aduana, C.PROD_NOMBRE,C.prod_num_tarimas FROM  TB_DET_PEDIDOS A, TB_CAT_PRODUCTO C WHERE A.PDN_FOLIO = " + orden.ToString() + " AND A.PDN_TIPO = '" + DB + "' AND A.PROD_CLAVE = C.PROD_CLAVE ORDER BY C.PROD_NOMBRE";

                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader Ped;
                Ped = cmnd2.ExecuteReader();
                while (Ped.Read())
                {
                    int encontrado = 0;
                    DataRow[] result = Acumulado.Select("prod_clave = '" + Ped["prod_clave"].ToString().Trim() + "'");
                    foreach (DataRow row in result)
                    {
                        encontrado = 1;
                        row[3] = (Convert.ToInt32(row[3]) + Convert.ToInt32(Ped["pdn_num_unidades"].ToString().Trim()));
                        row[5] = row[5].ToString() + Ped["pdn_observaciones"].ToString().Trim() + " ";
                    }

                    if (encontrado == 0)
                    {
                        Acumulado.Rows.Add(Ped["lin_clave"].ToString().Trim(), Ped["prod_clave"].ToString().Trim(), Ped["PROD_NOMBRE"].ToString().Trim(), Ped["pdn_num_unidades"].ToString().Trim().Replace(".000", ""), Ped["prod_num_tarimas"].ToString().Trim().Replace(".00", ""), Ped["pdn_observaciones"].ToString().Trim(), Ped["pdn_pedimento"].ToString().Trim(), Ped["pdn_fechap"].ToString().Trim(), Ped["pdn_aduana"].ToString().Trim());
                    }
                }


                Cadena = "SELECT PDN_MONTO_TRANSPORTE, CVE_SUBCLI, CNTE_CLAVE, PROV_CLAVE, PDN_TRANSPORTE, PDN_OBSERVACION, PDN_PEDSIGMA, ryandigital, PDN_SELLO FROM " + TBD + " WHERE PDN_FOLIO = '" + orden + "' AND PDN_TIPO = '" + DB + "'";
                cmnd2 = thisConnecion.CreateCommand();
                cmnd2.CommandText = Cadena;
                SqlDataReader Info;
                Info = cmnd2.ExecuteReader();

                while (Info.Read())
                {
                    TxtCve = Info["CNTE_CLAVE"].ToString();
                    MnomProv = prov_nombre; //row["prov_nombre"].ToString();
                    Nomcli = cliente_nombre;
                    //Nomcli = Ped["CNTE_NOMBRE"].ToString();
                    txttalonemb = Info["pdn_transporte"].ToString();
                    PedObs = PedObs + "orden: " + orden + " " + Info["pdn_observacion"].ToString() + " ";
                    PedSigma = Info["pdn_pedsigma"].ToString();
                    ryandigital = Info["ryandigital"].ToString();
                    PedSello = Info["pdn_sello"].ToString();
                }



            }


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


            foreach (DataRow row in Acumulado.Rows)
            {
                tarima = getNombreTarima(row["pdn_tipotar"].ToString().Trim());
                subcli = row["pdn_subcli"].ToString().Trim();

                if (Convert.ToInt32(row["prod_num_tarimas"]) != 0)
                    resulta = Convert.ToDecimal(row["pdn_num_unidades"]) / Convert.ToInt32(row["prod_num_tarimas"]);

                drawLinea = string.Format("{0}", Convert.ToString(row["lin_CLAVE"]));
                drawProd = string.Format("{0}\t{1}", Convert.ToString(row["prod_clave"]), Convert.ToString(row["prod_nombre"]));
                drawPedi = string.Format("|               |                     |    \t\t            |\r\n");
                //drawPedi = string.Format("|               |                     |    \t\t\t\r\n");
                drawcajas = (Convert.ToDecimal(row["pdn_num_unidades"])).ToString("#,###,###");
                drawtaraprox = resulta.ToString("0.00");

                if (Convert.ToString(row["observaciones"]).ToString().Trim().Length > 30)
                {
                    String cadena = Convert.ToString(row["observaciones"]).Substring(0, 30);
                    PointF drawPointobs = new PointF(620.0F, cont);
                    String cadena2 = Convert.ToString(row["observaciones"]).Substring(30);
                    PointF drawPointobs2 = new PointF(620.0F, cont + 13);
                    e.Graphics.DrawString(cadena, drawFont, drawBrush, drawPointobs);
                    e.Graphics.DrawString(cadena2, drawFont, drawBrush, drawPointobs2);
                }
                else
                {
                    drawobsprod = Convert.ToString(row["observaciones"]);
                    PointF drawPointobs = new PointF(620.0F, cont);
                    e.Graphics.DrawString(drawobsprod, drawFont, drawBrush, drawPointobs);
                }

                String pedimento = string.Format("{0,15}", Convert.ToString(row["pdn_pedimento"]));
                String fecpedimento = Convert.ToString(row["pdn_fechap"]);
                String aduan = Convert.ToString(row["pdn_aduana"]);



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
                sumapedido = sumapedido + Convert.ToInt32(row["pdn_num_unidades"]);
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

                e.HasMorePages = false;
            }

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

        private void autorizacioncliente_Load(object sender, EventArgs e)
        {

        }
    }
}

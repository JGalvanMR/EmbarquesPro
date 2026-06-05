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
    public partial class FrmRepSplitResp : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable Datos = new DataTable();
        DataTable DetSplit = new DataTable();
        ConsDetSplit Frm1 = new ConsDetSplit();
        Form1 Fr1 = new Form1();
        FrmDetSplit2 Fun = new FrmDetSplit2();
        DataTable Split = new DataTable();

        public FrmRepSplitResp()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
               
        private void Llena()
        {
            DGSplit.Rows.Clear();
            string mfec = DtFE.Value.ToShortDateString();
            string mfechant = DtFE.Value.AddDays(-1).ToShortDateString();
            string mfechsig = DtFE.Value.AddDays(+1).ToShortDateString();
            //MessageBox.Show("LLega aqui antes");
            DateTime mfechini = DateTime.Now;
            DateTime mfechfin = DateTime.Now;
            try
            {
                mfechini = Convert.ToDateTime(mfec + " 04:00:00 a. m.");
                mfechfin = Convert.ToDateTime(mfechsig + " 04:00:00 a. m.");
            }
            catch {
                mfechini = Convert.ToDateTime(mfec + " 04:00:00 a.m.");
                mfechfin = Convert.ToDateTime(mfechsig + " 04:00:00 a.m.");
            }
            
            det_fech_split.Text = "Reporte de Split Por Armador de " + mfechini.ToString("dd/MM/yyyy hh:mm tt") + " Al " + mfechfin.ToString("dd/MM/yyyy hh:mm tt");
            det_fech_split.Visible = true;
            //MessageBox.Show("LLega aqui");
            DataTable PedSplit = new DataTable();
            /*thisConnecion.Open();
            string Cadena = "SELECT pdn_folio,CNTE_CLAVE,PROV_CLAVE,PDN_HORA, PDN_FECHA, pdn_pedorigen FROM TB_MSTR_PEDIDOS_NAL WHERE PDN_FECHA IN ('" + mfec + "', '" + mfechsig + "') ORDER BY PDN_FOLIO";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PedSplit");
            PedSplit = ds.Tables["PedSplit"];
            thisConnecion.Close();*/
            thisConnecion.Open(); //DBGAB
            //Cadena = "SELECT A.*,B.hora_ini FROM tb_det_split A LEFT JOIN tb_mstr_embarque B ON A.EMB_FOLIO = B.EMB_FOLIO WHERE SUBSTRING(A.FECHA,1,10) = '" + mfec + "' ORDER BY A.NOM_CAPSPLIT,A.EMB_FOLIO,A.TARIMA,A.FECHA";
            //string Cadena = "SELECT A.*,B.hora_ini, B.fecha_cap FROM tb_det_split A, tb_mstr_embarque B WHERE FECHA LIKE '" + mfec + "%' AND FECHA LIKE '" + mfechsig + "%' OR A.EMB_FOLIO = B.EMB_FOLIO ORDER BY A.NOM_CAPSPLIT,A.EMB_FOLIO,A.TARIMA,A.FECHA"; //
            string Cadena = "SELECT A.*,B.hora_ini, B.fecha_cap FROM tb_det_split A LEFT JOIN tb_mstr_embarque B ON A.EMB_FOLIO = B.EMB_FOLIO WHERE FECHA LIKE '" + mfec + "%' OR FECHA LIKE '" + mfechsig + "%' ORDER BY A.NOM_CAPSPLIT,A.EMB_FOLIO,A.TARIMA,A.FECHA";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "DetSplit");
            DetSplit = ds.Tables["DetSplit"];
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion; //DBGAB
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            int mConse = 0, TotSplit = 0;
            Int32 mreg = 0, CJS = 0, NPROD = 1, ini = 1;
            string A = "", B = "", C = "", D = "", E = "", TI = "", MPROD = "", CapSplit = "";
            while (Info.Read())
            {
                DateTime fechaactual = DateTime.Now;
                try
                {
                    fechaactual = Convert.ToDateTime(Info["FECHA"].ToString().Replace("a.m.", "a. m.").Replace("p.m.", "p. m."));
                }
                catch {
                    fechaactual = Convert.ToDateTime(Info["FECHA"].ToString());
                }
                if (fechaactual > mfechini && fechaactual < mfechfin) {

                    string hay = "N";
                    string Mped = Info["Emb_folio"].ToString();
                    string MRes = Info["NOM_CAPSPLIT"].ToString();
                    string mHiPED = Info["hora_ini"].ToString();
                    string mfechemb = Info["fecha_cap"].ToString();
                    string pdn_emb_hora = "";
                    try
                    {
                        try
                        {
                            pdn_emb_hora = Convert.ToDateTime(Info["fecha_cap"].ToString()).ToString("dd/MM/yyyy") + " " + mHiPED.Replace("a.m.", "a. m.").Replace("p.m.", "p. m.");
                        }
                        catch {
                            pdn_emb_hora = Convert.ToDateTime(Info["fecha_cap"].ToString()).ToString("dd/MM/yyyy") + " " + mHiPED;
                        }
                        
                        mHiPED = pdn_emb_hora;
                    }
                    catch {
                        
                    }
                    
                    
                    int i = 0;
                    foreach (DataGridViewRow row in DGSplit.Rows)
                    {
                        if (DGSplit.Rows[i].Cells["PEDIDO"].Value.ToString() == Mped && DGSplit.Rows[i].Cells["RESPONSABLE"].Value.ToString() == MRes)
                        {
                            hay = "S";
                            break;
                        }
                        i++;
                    }
                    if (hay == "N")
                    {
                        string BusPed = "N";

                        //thisConnecion.Open(); //DBGAB
                        //Cadena = "SELECT A.*,B.hora_ini FROM tb_det_split A LEFT JOIN tb_mstr_embarque B ON A.EMB_FOLIO = B.EMB_FOLIO WHERE SUBSTRING(A.FECHA,1,10) = '" + mfec + "' ORDER BY A.NOM_CAPSPLIT,A.EMB_FOLIO,A.TARIMA,A.FECHA";
                        Cadena = "SELECT pdn_folio,CNTE_CLAVE,PROV_CLAVE,PDN_HORA, PDN_FECHA, pdn_pedorigen FROM TB_MSTR_PEDIDOS_NAL WHERE PDN_FOLIO = '" + Mped + "'";
                        cmd = new SqlCommand(Cadena);
                        cmd.Connection = thisConnecion; //DBGAB
                        SqlDataReader mstrpedido;
                        mstrpedido = cmd.ExecuteReader();
                        while (mstrpedido.Read())
                        {
                            BusPed = "S";

                            string Tmp2 = "";
                            string pdn_hora = Convert.ToDateTime(mstrpedido["PDN_FECHA"].ToString()).ToString("dd/MM/yyyy") + " " + Fn_ConvertHTtoampm(mstrpedido["PDN_HORA"].ToString()).ToString().Replace("a.m.", "a. m.").Replace("p.m.", "p. m.");  
                            string HrCap = pdn_hora;
                            try
                            {
                                
                                DateTime timuno = Convert.ToDateTime(pdn_hora);
                                DateTime timdos = Convert.ToDateTime(pdn_emb_hora);
                                TimeSpan span = new TimeSpan(0, 0, 0, 0, 0);
                                TimeSpan tiemp = timdos - timuno;

                                var cx = tiemp.CompareTo(span);


                                if (cx > 0)
                                {
                                    Tmp2 = tiemp.ToString();
                                }

                            }
                            catch { 
                            
                            }

                            

                            //Tmp2 = (HrCap.Trim().Length > 0 && mHiPED.Trim().Length > 0) ? Frm1.Fn_Tiempo(HrCap, mHiPED) : "";
                            DGSplit.Rows.Add(mstrpedido["PROV_CLAVE"].ToString(), mstrpedido["CNTE_CLAVE"].ToString(), Mped, HrCap, mHiPED, Tmp2, "", "", "", "", "", MRes, mConse);
                            mConse++;
                        }
                        if (BusPed == "N")
                        {

                            DGSplit.Rows.Add("", "", Mped, "", mHiPED, "", "", "", "", "", "", MRes, mConse);
                            mConse++;
                        }
                        int Ini = 0, nprod = 0, nsplit = 0;
                        string mprod = "", msplit = "", Hini = "", Hfin = "", fechaini = "", fechafin = "";
                        foreach (DataRow row1 in DetSplit.Select("EMB_FOLIO = '" + Mped + "' AND NOM_CAPSPLIT = '" + MRes + "'"))
                        {
                            Mped = Convert.ToInt32(Mped).ToString();
                            if (Ini == 0)
                            {
                                //Cadena = "select TOP (1) fecha_cap FROM Tb_Det_Etiqueta WHERE emb_folio = '" + Mped + "' AND Eti_Recibo = '" + row1["no_lote"].ToString().Trim() + "' AND Eti_Producto = '" + row1["prod_clave"].ToString().Trim() + "' AND Eti_TarIni = '" + Convert.ToInt32(row1["TARINI"].ToString().Trim()) + "' And Split = '" + row1["tarima"].ToString().Trim() + "' Order By fecha_cap ASC";
                                Cadena = "select TOP (1) fecha_cap FROM Tb_Det_Etiqueta WHERE emb_folio = '" + Mped + "' AND Split = '" + row1["tarima"].ToString().Trim() + "' Order By fecha_cap ASC";
                                cmd = new SqlCommand(Cadena, thisConnecion);
                                string Surt = Convert.ToDateTime(cmd.ExecuteScalar()).ToString("hh:mm tt");
                                fechaini = cmd.ExecuteScalar().ToString();

                                row1["hora"] = Surt;
                                Hini = row1["hora"].ToString();
                                Ini = 1;
                            }
                            if (msplit != row1["tarima"].ToString())
                            {
                                msplit = row1["tarima"].ToString();
                                nsplit++;
                            }
                            if (mprod != row1["PROD_CLAVE"].ToString())
                            {
                                mprod = row1["PROD_CLAVE"].ToString();
                                nprod++;
                            }
                            Hfin = row1["hora"].ToString();
                            fechafin = row1["FECHA"].ToString();
                        }
                        TotSplit = TotSplit + nsplit;
                        string HI = (Hini.Trim().Length > 5) ? Frm1.Fn_CONVIERTEHR(Hini) : "";
                        string HF = (Hfin.Trim().Length > 5) ? Frm1.Fn_CONVIERTEHR(Hfin) : "";
                        DGSplit.Rows[mConse - 1].Cells["HRINI"].Value = Hini;
                        DGSplit.Rows[mConse - 1].Cells["HRFIN"].Value = Hfin;
                        
                        DateTime fechauno = DateTime.Now;
                        DateTime fechados = DateTime.Now;

                        try
                        {
                            fechauno = Convert.ToDateTime(fechafin.Replace("a.m.", "a. m.").Replace("p.m.", "p. m."));
                            fechados = Convert.ToDateTime(fechaini.Replace("a.m.", "a. m.").Replace("p.m.", "p. m."));

                        }
                        catch {
                            fechauno = Convert.ToDateTime(fechafin);
                            fechados = Convert.ToDateTime(fechaini);
                        
                        
                        }


                        TimeSpan tiempo = fechauno - fechados;

                        string dif = tiempo.ToString();
                        
                        //DGSplit.Rows[mConse-1].Cells["TIEMPO2"].Value = (HI.Trim().Length > 0 && HF.Trim().Length > 0) ? Frm1.Fn_Tiempo(Hini, Hfin) : "";
                        DGSplit.Rows[mConse - 1].Cells["TIEMPO2"].Value = dif;
                        DGSplit.Rows[mConse - 1].Cells["NOSPLIT"].Value = nsplit.ToString();
                        DGSplit.Rows[mConse - 1].Cells["NOPROD"].Value = nprod.ToString();
                    }
                    if (ini == 1)
                    {
                        A = Info["EMB_FOLIO"].ToString();
                        B = Info["FECHA"].ToString(); // FECHA
                        C = Info["TARIMA"].ToString(); // NOSPLIT
                        D = Info["HORA"].ToString(); // TIEMPO
                        E = "";
                        TI = Info["HORA"].ToString(); //TIEMPO
                        MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                        CapSplit = Info["nom_capsplit"].ToString();
                    }
                    mreg++;
                    if (A != Info["EMB_FOLIO"].ToString() || C != Info["TARIMA"].ToString())
                    {
                        Datos.Rows.Add(C, D, E, Fun.Fn_Tiempo(D, E), CJS, NPROD, CapSplit, A);
                        A = Info["EMB_FOLIO"].ToString();
                        B = Info["FECHA"].ToString(); // FECHA
                        C = Info["TARIMA"].ToString(); // NOSPLIT
                        D = Info["HORA"].ToString(); // TIEMPO
                        CapSplit = Info["nom_capsplit"].ToString();
                        mreg = 1;
                        CJS = 0;
                        MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                        NPROD = 1;
                    }
                    ini++;
                    E = Info["HORA"].ToString();
                    CJS = CJS + Convert.ToInt32(Info["CAJAS"]); //CAJAS
                    if (MPROD != Info["PROD_CLAVE"].ToString()) //CVE_PROD
                    {
                        MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                        NPROD++;
                    }
                
                }

            }
            Datos.Rows.Add(C, D, E, Fun.Fn_Tiempo(D, E), CJS, NPROD, CapSplit,A);
            thisConnecion.Close(); //DBGAB
            TxtTot.Text = TotSplit.ToString("#,###");
            Datos.DefaultView.Sort = "Responsable, Pedido, NoSplit ASC";
            Datos = Datos.DefaultView.ToTable(true);
            DetSplit.DefaultView.Sort = "NOM_CAPSPLIT, Emb_Folio, Tarima ASC";
            DetSplit = DetSplit.DefaultView.ToTable(true);
        }

        private void BtnEmbdia_Click(object sender, EventArgs e)
        {
            DGSplit.Visible = true;
            DGSpliPen.Visible = false;
            Llena();
        }

        private void CreaTable()
        {
            Datos.Columns.Add("NoSplit", typeof(string));     //0
            Datos.Columns.Add("HoraIni", typeof(string));      //1
            Datos.Columns.Add("HoraFin", typeof(string));       //2
            Datos.Columns.Add("Tiempo", typeof(string));    //3
            Datos.Columns.Add("Cajas", typeof(string));       //4
            Datos.Columns.Add("Productos", typeof(string)); //5  
            Datos.Columns.Add("Responsable", typeof(string)); //6
            Datos.Columns.Add("Pedido", typeof(string)); //7
        }

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            ConsDetSplit DetSplit = new ConsDetSplit();
            DetSplit.ShowDialog(this); 
        }

        private void DGSplit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGSplit.CurrentCell.ColumnIndex == 6)
            {
                Program.MyGlobal.PubPedido = DGSplit.CurrentRow.Cells["PEDIDO"].Value.ToString();
                Program.MyGlobal.PubResp = DGSplit.CurrentRow.Cells["RESPONSABLE"].Value.ToString();
                Program.MyGlobal.fechadepedido = DtFE.Value.ToShortDateString();
                FrmDetSplit2 DetSplit = new FrmDetSplit2();
                DetSplit.ShowDialog(this);
            }

        }

        void ReporteExcel()
        {
            label32.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Maximum = Datos.Rows.Count - 1;
            Excel.Range r;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add();

            excel.Cells[1, 1] = "Comercializadora GAB s.a. de c.v.";
            excel.Range[excel.Cells[1, 1], excel.Cells[1, 7]].Merge();
            excel.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[2, 1] = "LOGISTICA DE TRAILERS";
            excel.Range[excel.Cells[2, 1], excel.Cells[2, 7]].Merge();
            excel.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[3, 1] = "Control de Tiempos de Embarques por Split";
            excel.Range[excel.Cells[3, 1], excel.Cells[3, 7]].Merge();
            excel.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            r = excel.Range[excel.Cells[3, 1], excel.Cells[3, 7]];
            r.Font.Size = 13;
            //r = excelWorksheet.get_Range("A1", "R1");
            //r.Font.Bold = true;
            excel.Cells[5, 2] = "Fecha: " + DtFE.Value.ToShortDateString();
            r = excel.get_Range("A1", "F5");
            r.Font.Bold = true;
            //excel.Cells[6, 4] = "PEDIDO"; excel.Cells[6, 5] = "SURTIDO";
            			 				

            excel.Cells[7, 1] = "Fecha"; excel.Cells[7, 2] = "Responsable"; excel.Cells[7, 3] = "Pedido"; excel.Cells[7, 4] = "Cant Split"; excel.Cells[7, 5] = "# Cajas"; excel.Cells[7, 6] = "# Productos";
            excel.Cells[7, 7] = "Hr. Inicial"; excel.Cells[7, 8] = "Hr. Final"; 
            r = excel.Range[excel.Cells[7, 1], excel.Cells[7, 9]];
            r.Font.Bold = true;
            int i = 8;
            int j = 0;
            foreach (DataRow row in Datos.Rows)
            {
                //No.	Fecha	Turno	Sup. Carga	Temp	Hr. Llegó	Hr. Entro	Hr. Salio	Tiempo Total	Chofer	Destino	Ini. Carga	Fin. Carga	Tiempo Carga	Anden	Transporte	Placa Caja	Placa Trailer	Radio
                excel.Cells[i, 1] = DtFE.Value.ToShortDateString();
                excel.Cells[i, 2] = row["RESPONSABLE"].ToString();
                excel.Cells[i, 3] = row ["PEDIDO"].ToString();
                excel.Cells[i, 4] = 1;
                excel.Cells[i, 5] = row["CAJAS"].ToString();
                excel.Cells[i, 6] = row["productos"].ToString();
                excel.Cells[i, 7] = row["HORAINI"].ToString();
                excel.Cells[i, 8] = row["HORAFIN"].ToString();
                //r = excel.Range[excel.Cells[i, 1], excel.Cells[i, 5]];
                //r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                i++;
                j++;
                progressBar1.PerformStep();
            }
            excel.Columns.AutoFit();
            excel.Rows.AutoFit();
            progressBar1.Minimum = 0;
            progressBar1.Visible = false;
            label32.Visible = false;
            MessageBox.Show("Archivo Generado Con Exito!!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            excel.Visible = true;
        }

        void ReporteExcel2()
        {
            label32.Visible = true;
            progressBar1.Visible = true;
            progressBar1.Maximum = Datos.Rows.Count - 1;
            Excel.Range r;

            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add();

            excel.Cells[1, 1] = "Comercializadora GAB s.a. de c.v.";
            excel.Range[excel.Cells[1, 1], excel.Cells[1, 7]].Merge();
            excel.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[2, 1] = "LOGISTICA DE TRAILERS";
            excel.Range[excel.Cells[2, 1], excel.Cells[2, 7]].Merge();
            excel.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[3, 1] = "Control de Tiempos de Embarques por Split";
            excel.Range[excel.Cells[3, 1], excel.Cells[3, 7]].Merge();
            excel.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            r = excel.Range[excel.Cells[3, 1], excel.Cells[3, 7]];
            r.Font.Size = 13;
            //r = excelWorksheet.get_Range("A1", "R1");
            //r.Font.Bold = true;
            excel.Cells[5, 2] = "Fecha: " + DtFE.Value.ToShortDateString();
            r = excel.get_Range("A1", "F5");
            r.Font.Bold = true;
            //excel.Cells[6, 4] = "PEDIDO"; excel.Cells[6, 5] = "SURTIDO";
            excel.Cells[7, 1] = "Responsable"; excel.Cells[7, 2] = "Transporte"; excel.Cells[7, 3] = "Destino"; excel.Cells[7, 4] = "Pedido"; excel.Cells[7, 5] = "Hr. Captura"; excel.Cells[7, 6] = "Hr. Carga";
            excel.Cells[7, 7] = "Tiempo Carga"; excel.Cells[7, 8] = "No. Splits"; excel.Cells[7, 9] = "Hr. Inicial"; excel.Cells[7, 10] = "Hr. Final"; excel.Cells[7, 11] = "Tiempo";
            r = excel.Range[excel.Cells[7, 1], excel.Cells[7, 11]];
            r.Font.Bold = true;
            int i = 8, j = 0;
            foreach (DataGridViewRow row in DGSplit.Rows)
            {
                //No.	Fecha	Turno	Sup. Carga	Temp	Hr. Llegó	Hr. Entro	Hr. Salio	Tiempo Total	Chofer	Destino	Ini. Carga	Fin. Carga	Tiempo Carga	Anden	Transporte	Placa Caja	Placa Trailer	Radio
                excel.Cells[i, 1] = DGSplit.Rows[j].Cells["RESPONSABLE"].Value.ToString();
                excel.Cells[i, 2] = DGSplit.Rows[j].Cells["TRANSPORTE"].Value.ToString();
                excel.Cells[i, 3] = DGSplit.Rows[j].Cells["DESTINO"].Value.ToString();
                excel.Cells[i, 4] = DGSplit.Rows[j].Cells["PEDIDO"].Value.ToString();
                excel.Cells[i, 5] = DGSplit.Rows[j].Cells["HRCAP"].Value.ToString();
                excel.Cells[i, 6] = DGSplit.Rows[j].Cells["HRCARGA"].Value.ToString();
                excel.Cells[i, 7] = DGSplit.Rows[j].Cells["TIEMPO"].Value.ToString();
                excel.Cells[i, 8] = DGSplit.Rows[j].Cells["NOSPLIT"].Value.ToString();
                excel.Cells[i, 9] = DGSplit.Rows[j].Cells["HRINI"].Value.ToString();
                excel.Cells[i, 10] = DGSplit.Rows[j].Cells["HRFIN"].Value.ToString();
                excel.Cells[i, 11] = DGSplit.Rows[j].Cells["TIEMPO2"].Value.ToString();
                //for (int k = 1; k < 11; k++)
                //    excel.Cells[i, k + 1] = DGSplit.Rows[j].Cells[k].Value.ToString();
                string Mped = DGSplit.Rows[j].Cells["PEDIDO"].Value.ToString();
                string MRes = DGSplit.Rows[j].Cells["RESPONSABLE"].Value.ToString();
                foreach (DataRow row1 in Datos.Select("PEDIDO = '" + Mped + "' AND RESPONSABLE = '" + MRes + "'"))
                {
                    i++;
                    excel.Cells[i, 8] = row1["NoSplit"].ToString();
                    excel.Cells[i, 9] = row1["HORAINI"].ToString();
                    excel.Cells[i, 10] = row1["HORAFIN"].ToString();
                    excel.Cells[i, 11] = row1["TIEMPO"].ToString();
                    excel.Cells[i, 12] = row1["CAJAS"].ToString();
                    excel.Cells[i, 13] = row1["PRODUCTOS"].ToString();
                    string MTar = row1["NoSplit"].ToString();
                    foreach (DataRow row2 in DetSplit.Select("EMB_FOLIO = '" + Mped + "' AND TARIMA = '" + MTar + "'"))
                    {
                        i++;
                        excel.Cells[i, 12] = row2["nom_prod"].ToString();
                        excel.Cells[i, 13] = row2["CAJAS"].ToString();
                        excel.Cells[i, 14] = row2["hora"].ToString();
                    }
                }
                //r = excel.Range[excel.Cells[i, 1], excel.Cells[i, 5]];
                //r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                i++;
                j++;
                progressBar1.PerformStep();
            }
            excel.Columns.AutoFit();
            excel.Rows.AutoFit();
            progressBar1.Minimum = 0;
            progressBar1.Visible = false;
            label32.Visible = false;
            MessageBox.Show("Archivo Generado Con Exito!!!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            excel.Visible = true;
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {
            ReporteExcel();
        }

        public string Fn_ConvertHTtoampm(string HORA)
        {
            string cad = "";
            Int32 H = 0;
            if (HORA.Trim().Length < 5)
                cad = "";
            else
            {
                H = Convert.ToInt32(HORA.Substring(0, 2));
                if (H > 12)
                    cad = (H - 12).ToString().Trim().PadLeft(2, '0') + HORA.Substring(2, 3) + " p.m.";
                else
                    cad = (H).ToString().Trim().PadLeft(2, '0') + HORA.Substring(2, 3) + " a.m.";
            }
            return cad;
        }

        private void FrmRepSplitResp_Load(object sender, EventArgs e)
        {
            CreaTable();
        }

        private void BtnRep_Click(object sender, EventArgs e)
        {
            ReporteExcel2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            det_fech_split.Visible = false;
            DGSplit.Visible = false;
            DGSpliPen.Visible = true;
            thisConnecion.Open();
            string Cadena = "SELECT emb_folio, tarima, no_lote, TARINI, cajas, prod_clave, nom_prod, FECHA , NOM_CAPSPLIT" +
                            " FROM tb_det_split WHERE estatus = 'A' ORDER BY NOM_PROD ";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            DataSet ds = new DataSet();
            da.Fill(ds, "Split");
            DataTable Split = ds.Tables["Split"];
            DGSpliPen.DataSource = Split;
            DGSpliPen.Location = new Point(12,81);
            DGSpliPen.Size = new Size(1044, 475);
            DGSpliPen.Columns[0].HeaderText = "PEDIDO";
            DGSpliPen.Columns[1].HeaderText = "No. Split";
            DGSpliPen.Columns[2].HeaderText = "FOLIO";
            DGSpliPen.Columns[3].HeaderText = "TARIMA";
            DGSpliPen.Columns[4].HeaderText = "CAJAS";
            DGSpliPen.Columns[5].HeaderText = "CVE PROD";
            DGSpliPen.Columns[6].HeaderText = "NOMBRE";
            DGSpliPen.Columns[7].HeaderText = "FECHA";
            DGSpliPen.Columns[8].HeaderText = "RESPONSABLE";
            DGSpliPen.Columns[0].Width = 70;
            DGSpliPen.Columns[1].Width = 40;
            DGSpliPen.Columns[2].Width = 70;
            DGSpliPen.Columns[3].Width = 50;
            DGSpliPen.Columns[4].Width = 60;
            DGSpliPen.Columns[5].Width = 90;
            DGSpliPen.Columns[6].Width = 300;
            DGSpliPen.Columns[7].Width = 150;
            DGSpliPen.Columns[8].Width = 150;
            Split.DefaultView.Sort = "emb_folio,tarima desc";
            DataTable Orden = Split.DefaultView.ToTable(true);
            int Tot = 0, tar = 0;
            string folio = "";
            foreach (DataRow row in Orden.Rows)
            {
                if (folio != row["emb_folio"].ToString().Trim() || tar != Convert.ToInt16(row["tarima"]))
                {
                    folio = row["emb_folio"].ToString().Trim();
                    tar = Convert.ToInt16(row["tarima"]);
                    Tot++;
                }
            }
            TxtTot.Text = Tot.ToString();
            thisConnecion.Close();
            Formato();
        }

        private void Formato()
        {
            int i = 0, Fol = 0, Colores = 1, Tot = 0, tar = 0;
            foreach (DataGridViewRow Row in DGSpliPen.Rows)
            {
                if (Fol != Convert.ToInt32(DGSpliPen.Rows[i].Cells["emb_folio"].Value) || tar != Convert.ToInt16(DGSpliPen.Rows[i].Cells["tarima"].Value))
                {
                    if (Colores == 1)
                        Colores = 2;
                    else
                        Colores = 1;
                    Fol = Convert.ToInt32(DGSpliPen.Rows[i].Cells["emb_folio"].Value);
                    tar = Convert.ToInt16(Convert.ToInt16(DGSpliPen.Rows[i].Cells["tarima"].Value));
                    Tot++;
                }
                //if ((Convert.ToInt32(DGDatos.Rows[i].Cells["ORDEN"].Value) % 2) == 0)
                if (Colores == 1)
                    DGSpliPen.Rows[i].DefaultCellStyle.BackColor = Color.White;
                else
                    DGSpliPen.Rows[i].DefaultCellStyle.BackColor = Color.Cyan;
                i++;
            }
            //LblCant.Text = Tot.ToString("###");
            //DGSpliPen.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
        }
        
    }
}

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
    public partial class FrmRepSplitPed : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable Datos = new DataTable();
        DataTable DetSplit = new DataTable();
        DataTable Pedidos = new DataTable();
        ConsDetSplit Frm1 = new ConsDetSplit();
        Form1 Fr1 = new Form1();
        FrmDetSplit2 Fun = new FrmDetSplit2();
        FrmRepSplitResp fun2 = new FrmRepSplitResp();
        DataTable Split = new DataTable();

        public FrmRepSplitPed()
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
            DataTable PedSplit = new DataTable();
            thisConnecion.Open();
            string Cadena = "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.PDN_HORA FROM TB_MSTR_PEDIDOS_NAL A WHERE A.PDN_FECHA = '" + mfec + "' AND ltrim(A.placacaja) <> '' " + //  
                            "UNION " +
                            "SELECT A.PDN_FOLIO,A.PDN_FECHA,A.PLACACAJA,A.PDN_HORA FROM TB_MSTR_PEDIDOS_EXP A WHERE A.PDN_FECHA = '" + mfec + "' AND ltrim(A.placacaja) <> '' " + // AND ltrim(A.placacaja) <> ''
                            "ORDER BY PDN_FOLIO";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PedSplit");
            PedSplit = ds.Tables["PedSplit"];
            thisConnecion.Close();
            //thisConnecion.Open(); //DBGAB 
            Cadena = "SELECT A.EMB_FOLIO,A.FECHA_CAP,A.EMB_TIPO,A.HORA_INI,A.HORA_FIN,A.NO_TRAILER,B.DESTINO,B.TRANSPORTE,B.RESPONSABLE FROM tb_mstr_embarque A, TB_MSTR_TRAILER B WHERE A.FECHA_CAP = '" + mfec + "' AND A.NO_TRAILER=B.NO_TRAILER AND A.FECHA_CAP = B.FECHA AND B.CONSE > 0 ORDER BY EMB_FOLIO"; //
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "Pedidos");
            Pedidos = ds.Tables["Pedidos"];
            Cadena = "SELECT A.*,B.* FROM tb_det_split A, tb_mstr_embarque B WHERE B.FECHA_CAP = '" + mfec + "' AND A.EMB_FOLIO = B.EMB_FOLIO ORDER BY A.EMB_FOLIO, A.TARIMA"; //
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "DetSplit");
            DetSplit = ds.Tables["DetSplit"];
            thisConnecion.Close();
            //thisConnecionDBGAB.Close();
            //dataGridView1.DataSource = PedSplit;
            int mConse = 0, TotSplit = 0;
            Int32 mreg = 0, CJS = 0, NPROD = 1, ini = 1;
            string A = "", B = "", C = "", D = "", E = "", TI = "", MPROD = "", CapSplit = "";
            foreach (DataRow row in Pedidos.Rows)
            {
                string Mped = row["Emb_folio"].ToString();
                string mHiPED = row["hora_ini"].ToString();
                string Trans = row["TRANSPORTE"].ToString();
                string Dest = row["DESTINO"].ToString();
                string Trailer = row["NO_TRAILER"].ToString();
                string MRes = ""; // row["NOM_CAPSPLIT"].ToString();
                string HrCap = "", Tmp2 = "";
                foreach (DataRow row1 in PedSplit.Select("PDN_FOLIO = '" + Mped + "'"))
                {
                    HrCap = fun2.Fn_ConvertHTtoampm(row1["PDN_HORA"].ToString());
                    Tmp2 = (HrCap.Trim().Length > 0 && mHiPED.Trim().Length > 0) ? Frm1.Fn_Tiempo(HrCap, mHiPED) : "";
                }
                DGSplit.Rows.Add(Trailer, Trans, Dest, Mped, HrCap, mHiPED, Tmp2, "", "", "", "", MRes, mConse);
                mConse++;
                int Ini = 0, nprod = 0, nsplit = 0;
                string mprod = "", msplit = "", Hini = "", Hfin = "", mRes = "";
                foreach (DataRow row1 in DetSplit.Select("EMB_FOLIO = '" + Mped + "'"))
                {
                    if (Ini == 0)
                    {
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
                    mRes = row1["NOM_CAPSPLIT"].ToString();
                }
                TotSplit = TotSplit + nsplit;
                string HI = (Hini.Trim().Length > 5) ? Frm1.Fn_CONVIERTEHR(Hini) : "";
                string HF = (Hfin.Trim().Length > 5) ? Frm1.Fn_CONVIERTEHR(Hfin) : "";
                DGSplit.Rows[mConse - 1].Cells["HRINI"].Value = Hini;
                DGSplit.Rows[mConse - 1].Cells["HRFIN"].Value = Hfin;
                DGSplit.Rows[mConse - 1].Cells["TIEMPO2"].Value = (HI.Trim().Length > 0 && HF.Trim().Length > 0) ? Frm1.Fn_Tiempo(Hini, Hfin) : "";
                DGSplit.Rows[mConse - 1].Cells["NOSPLIT"].Value = nsplit.ToString();
                DGSplit.Rows[mConse - 1].Cells["RESPONSABLE"].Value = mRes ;
            }
            foreach (DataRow row in DetSplit.Rows)
            {
                if (ini == 1)
                {
                    A = row["EMB_FOLIO"].ToString();
                    B = row["FECHA"].ToString(); // FECHA
                    C = row["TARIMA"].ToString(); // NOSPLIT
                    D = row["HORA"].ToString(); // TIEMPO
                    E = "";
                    TI = row["HORA"].ToString(); //TIEMPO
                    MPROD = row["PROD_CLAVE"].ToString(); //CVE_PROD
                    CapSplit = row["nom_capsplit"].ToString();
                }
                mreg++;
                if (A != row["EMB_FOLIO"].ToString() || C != row["TARIMA"].ToString())
                {
                    Datos.Rows.Add(C, D, E, Fun.Fn_Tiempo(D, E), CJS, NPROD, CapSplit,A);
                    A = row["EMB_FOLIO"].ToString();
                    B = row["FECHA"].ToString(); // FECHA
                    C = row["TARIMA"].ToString(); // NOSPLIT
                    D = row["HORA"].ToString(); // TIEMPO
                    CapSplit = row["nom_capsplit"].ToString();
                    mreg = 1;
                    CJS = 0;
                    MPROD = row["PROD_CLAVE"].ToString(); //CVE_PROD
                    NPROD = 1;
                }
                ini++;
                E = row["HORA"].ToString();
                CJS = CJS + Convert.ToInt32(row["CAJAS"]); //CAJAS
                if (MPROD != row["PROD_CLAVE"].ToString()) //CVE_PROD
                {
                    MPROD = row["PROD_CLAVE"].ToString(); //CVE_PROD
                    NPROD++;
                }
            }
            Datos.Rows.Add(C, D, E, Fun.Fn_Tiempo(D, E), CJS, NPROD, CapSplit,A);
            //thisConnecionDBGAB.Close();
            TxtTot.Text = TotSplit.ToString("#,###");
            Datos.DefaultView.Sort = "Responsable, Pedido, NoSplit ASC";
            Datos = Datos.DefaultView.ToTable(true);
            DetSplit.DefaultView.Sort = "NOM_CAPSPLIT, Emb_Folio, Tarima ASC";
            DetSplit = DetSplit.DefaultView.ToTable(true);
            TxtTot.Text = TotSplit.ToString();
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

        private void BtnEmbdia_Click(object sender, EventArgs e)
        {
            Llena();
        }

        private void FrmRepSplitPed_Load(object sender, EventArgs e)
        {
            CreaTable();
        }

        private void DGSplit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGSplit.CurrentCell.ColumnIndex == 7)
            {
                Program.MyGlobal.PubPedido = DGSplit.CurrentRow.Cells["PEDIDO"].Value.ToString();
                Program.MyGlobal.TipoPed = "NAL";
                Program.MyGlobal.PubResp = DGSplit.CurrentRow.Cells["RESPONSABLE"].Value.ToString();
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
            excel.Cells[3, 1] = "Control de Tiempos de Embarques de Pedidos por Split";
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
            excel.Cells[7, 1] = "Placa"; excel.Cells[7, 2] = "Transporte"; excel.Cells[7, 3] = "Destino"; excel.Cells[7, 4] = "Pedido"; excel.Cells[7, 5] = "Hr. Captura"; excel.Cells[7, 6] = "Hr. Carga";
            excel.Cells[7, 7] = "Tiempo Carga"; excel.Cells[7, 8] = "No. Splits"; excel.Cells[7, 9] = "Hr. Inicial"; excel.Cells[7, 10] = "Hr. Final"; excel.Cells[7, 11] = "Tiempo"; excel.Cells[7, 12] = "Responsable";
            r = excel.Range[excel.Cells[7, 1], excel.Cells[7, 12]];
            r.Font.Bold = true;
            int i = 8, j = 0;
            foreach (DataGridViewRow row in DGSplit.Rows)
            {
                //No.	Fecha	Turno	Sup. Carga	Temp	Hr. Llegó	Hr. Entro	Hr. Salio	Tiempo Total	Chofer	Destino	Ini. Carga	Fin. Carga	Tiempo Carga	Anden	Transporte	Placa Caja	Placa Trailer	Radio
                excel.Cells[i, 1] = DGSplit.Rows[j].Cells["NOTRAILER"].Value.ToString();
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
                //excel.Cells[i, 12] = DGSplit.Rows[j].Cells["RESPONSABLE"].Value.ToString();
                //for (int k = 1; k < 11; k++)
                //    excel.Cells[i, k + 1] = DGSplit.Rows[j].Cells[k].Value.ToString();
                string Mped = DGSplit.Rows[j].Cells["PEDIDO"].Value.ToString();
                string MRes = DGSplit.Rows[j].Cells["RESPONSABLE"].Value.ToString();
                foreach (DataRow row1 in Datos.Select("PEDIDO = '" + Mped + "'"))
                {
                    i++;
                    excel.Cells[i, 8] = row1["NoSplit"].ToString();
                    excel.Cells[i, 9] = row1["HORAINI"].ToString();
                    excel.Cells[i, 10] = row1["HORAFIN"].ToString();
                    excel.Cells[i, 11] = row1["TIEMPO"].ToString();
                    excel.Cells[i, 12] = row1["CAJAS"].ToString();
                    excel.Cells[i, 13] = row1["PRODUCTOS"].ToString();
                    excel.Cells[i, 14] = row1["RESPONSABLE"].ToString();
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

        private void BtnRep_Click(object sender, EventArgs e)
        {
            ReporteExcel();
        }
    }
}

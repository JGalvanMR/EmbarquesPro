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
    public partial class FormReportePresplit : Form
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

        int totalv = 0;
        int totalb = 0;
        int totala = 0;
        int totalr = 0;
        int totaln = 0;
        int totalaz = 0;

        public FormReportePresplit()
        {
            InitializeComponent();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Llena()
        {
            totalv = 0;
            totalb = 0;
            totala = 0;
            totalr = 0;
            totaln = 0;
            totalaz = 0;

            Datos.Rows.Clear();
            //DGSplit.Rows.Clear();
            string mfec = DtFE.Value.ToShortDateString();
            DataTable PedSplit = new DataTable();
            DataTable PedSplitLeido = new DataTable();
            thisConnecion.Open();
            string Cadena = "SELECT * FROM Tb_Det_Etiqueta_Presplit AS A INNER JOIN tb_cat_producto ON A.Eti_Producto = tb_cat_producto.prod_clave WHERE (A.fecha_cap > '" + Convert.ToDateTime(mfec).ToString("dd/MM/yyyy") + "') AND (A.fecha_cap < '" + Convert.ToDateTime(mfec).AddDays(1).ToString("dd/MM/yyyy") + " 4:00:00') Order by Split";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PedSplit");
            PedSplit = ds.Tables["PedSplit"];

            Cadena = "SELECT * FROM Tb_Det_Etiqueta INNER JOIN tb_cat_producto ON Tb_Det_Etiqueta.Eti_Producto = tb_cat_producto.prod_clave WHERE (fecha_cap > '" + Convert.ToDateTime(mfec).ToString("dd/MM/yyyy") + " 14:00:00') AND (fecha_cap < '" + Convert.ToDateTime(mfec).AddDays(1).ToString("dd/MM/yyyy") + " 8:00:00') AND (Cve_Camioneta <> '')";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "PedSplitLeido");
            PedSplitLeido = ds.Tables["PedSplitLeido"];
            thisConnecion.Close();
            foreach (DataRow row in PedSplit.Rows)
            {
                DataRow[] result = PedSplitLeido.Select("Eti_Lectura = '" + row["Eti_Lectura"].ToString().Trim() + "'");
                if (result.Count() > 0)
                {
                    if (row["Estatus"].ToString().Trim() == "C")
                    {
                        Datos.Rows.Add(row["fecha_cap"].ToString().Trim(), row["Eti_Lectura"].ToString().Trim(), row["Eti_Recibo"].ToString().Trim(), row["Eti_Producto"].ToString().Trim(), row["prod_nombre"].ToString().Trim(), row["Eti_TarIni"].ToString().Trim(), row["Eti_Caja"].ToString().Trim(), row["Split"].ToString().Trim(), "D", row["responsable"].ToString().Trim(), "");
                    }
                    else
                    {
                        Datos.Rows.Add(row["fecha_cap"].ToString().Trim(), row["Eti_Lectura"].ToString().Trim(), row["Eti_Recibo"].ToString().Trim(), row["Eti_Producto"].ToString().Trim(), row["prod_nombre"].ToString().Trim(), row["Eti_TarIni"].ToString().Trim(), row["Eti_Caja"].ToString().Trim(), row["Split"].ToString().Trim(), row["Estatus"].ToString().Trim(), row["responsable"].ToString().Trim(), "");
                    }
                }
                else {
                    Datos.Rows.Add(row["fecha_cap"].ToString().Trim(), row["Eti_Lectura"].ToString().Trim(), row["Eti_Recibo"].ToString().Trim(), row["Eti_Producto"].ToString().Trim(), row["prod_nombre"].ToString().Trim(), row["Eti_TarIni"].ToString().Trim(), row["Eti_Caja"].ToString().Trim(), row["Split"].ToString().Trim(), row["Estatus"].ToString().Trim(), row["responsable"].ToString().Trim(), "");
                }
            }

            thisConnecion.Open();
            //Cadena = "SELECT * FROM Tb_Det_Etiqueta WHERE (fecha_cap > '" + Convert.ToDateTime(mfec).ToString("dd/MM/yyyy") + " 14:00:00') AND (fecha_cap < '" + Convert.ToDateTime(mfec).AddDays(1).ToString("dd/MM/yyyy") + " 4:00:00') AND (Cve_Camioneta <> '')";
            
            thisConnecion.Close();
            foreach (DataRow row in PedSplitLeido.Rows)
            {
                DataRow[] result = PedSplit.Select("Eti_Lectura = '" + row["Eti_Lectura"].ToString().Trim() + "'");
                if (result.Count() == 0) {
                    Datos.Rows.Add(row["fecha_cap"].ToString().Trim(), row["Eti_Lectura"].ToString().Trim(), row["Eti_Recibo"].ToString().Trim(), row["Eti_Producto"].ToString().Trim(), row["prod_nombre"].ToString().Trim(), row["Eti_TarIni"].ToString().Trim(), row["Eti_Caja"].ToString().Trim(), row["Split"].ToString().Trim(), "N", "", "");
         
                }

            }

            DGSplit.DataSource = Datos;

            foreach (DataGridViewRow row in DGSplit.Rows)
            {
                if (Convert.ToString(row.Cells[8].Value) == "S")
                {
                    row.DefaultCellStyle.BackColor = Color.SpringGreen;
                    totalv = totalv + 1;
                   
                }
                else if (Convert.ToString(row.Cells[8].Value) == "A")
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                    totalb = totalb + 1;
                    
                }
                else if (Convert.ToString(row.Cells[8].Value) == "N")
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                    totaln = totaln + 1;
                }
                else if (Convert.ToString(row.Cells[8].Value) == "D")
                {
                    row.DefaultCellStyle.BackColor = Color.Aqua;
                    totalaz = totalaz + 1;
                }
                else
                {
                    if (Convert.ToString(row.Cells[10].Value).Trim() == "R")
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        totala = totala + 1; 
                    }
                    else {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        totalr = totalr + 1;
                    }
                }
            }

            LblTotalVerde.Text = "Total: " + totalv.ToString();
            LblTotalAmarillo.Text = "Total: " + totala.ToString();
            LblTotalBlanca.Text = "Total: " + totalb.ToString();
            LblTotalAzul.Text = "Total: " + totalaz.ToString();
            LblTotalRojo.Text = "Total: " + totalr.ToString();
            LblTotalNaranja.Text = "Total: " + totaln.ToString();


            

            //SELECT SUM(pdn_num_unidades) FROM tb_mstr_pedidos_nal a, tb_det_pedidos b, tb_cat_producto c WHERE a.pdn_fecha BETWEEN '28/09/2022' AND '28/09/2022' AND a.prov_clave = 'MRLUCKY' AND a.pdn_folio = B.pdn_folio AND a.pdn_tipo = B.pdn_tipo  AND b.prod_clave = C.prod_clave  and a.pdn_estatus != 'C'

            thisConnecion.Open();
            Cadena = "SELECT SUM(pdn_num_unidades) FROM tb_mstr_pedidos_nal a, tb_det_pedidos b, tb_cat_producto c WHERE a.pdn_fecha BETWEEN '" + Convert.ToDateTime(mfec).ToString("dd/MM/yyyy") + "' AND '" + Convert.ToDateTime(mfec).ToString("dd/MM/yyyy") + "' AND a.prov_clave = 'MRLUCKY' AND a.pdn_folio = B.pdn_folio AND a.pdn_tipo = B.pdn_tipo  AND b.prod_clave = C.prod_clave  and a.pdn_estatus != 'C'";
            SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
            string valor_pedido = "";
            try
            {
                valor_pedido = Convert.ToString(Convert.ToInt32(cmd.ExecuteScalar()));
            }
            catch
            {
                //4152 3136 4075 2403
            }
            thisConnecion.Close();

            LblTotal.Text = "Total Pedido = " + valor_pedido + " - Total Presplit = " + PedSplit.Rows.Count.ToString() + " - Total Split Camionetas = " + PedSplitLeido.Rows.Count.ToString();

        }

        private void CreaTable()
        {
            Datos.Columns.Add("fecha cap", typeof(string));     //0
            Datos.Columns.Add("Lectura", typeof(string));      //1
            Datos.Columns.Add("Recibo", typeof(string));       //2
            Datos.Columns.Add("Producto", typeof(string));    //3
            Datos.Columns.Add("Nombre", typeof(string));
            Datos.Columns.Add("Tarima", typeof(string));       //4
            Datos.Columns.Add("Caja", typeof(string)); //5  
            Datos.Columns.Add("PreSplit", typeof(string)); //6
            Datos.Columns.Add("Estatus", typeof(string)); //7
            Datos.Columns.Add("Responsable", typeof(string)); //7
            Datos.Columns.Add("Reetiquetado", typeof(string)); //7
        }

        private void BtnEmbdia_Click(object sender, EventArgs e)
        {
            Llena();
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
            excel.Cells[2, 1] = "LOGISTICA DE CARGA CAMIONETAS";
            excel.Range[excel.Cells[2, 1], excel.Cells[2, 7]].Merge();
            excel.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[3, 1] = "Control de PreSplit Leido";
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
            excel.Cells[7, 1] = "Fecha Hora"; excel.Cells[7, 2] = "Lectura"; excel.Cells[7, 3] = "Recibo"; excel.Cells[7, 4] = "Producto"; excel.Cells[7, 5] = "Nombre"; excel.Cells[7, 6] = "Tarima"; excel.Cells[7, 7] = "Caja";
            excel.Cells[7, 8] = "No. PreSplit"; excel.Cells[7, 9] = "Estado"; excel.Cells[7, 10] = "Responsable"; excel.Cells[7, 11] = "Reetiquetado";
            r = excel.Range[excel.Cells[7, 1], excel.Cells[7, 11]];
            r.Font.Bold = true;
            int i = 8, j = 0;
            foreach (DataGridViewRow row in DGSplit.Rows)
            {
                
                excel.Cells[i, 1] = DGSplit.Rows[j].Cells["fecha cap"].Value.ToString();
                excel.Cells[i, 2] = DGSplit.Rows[j].Cells["Lectura"].Value.ToString();
                excel.Cells[i, 3] = DGSplit.Rows[j].Cells["Recibo"].Value.ToString();
                excel.Cells[i, 4] = DGSplit.Rows[j].Cells["Producto"].Value.ToString();
                excel.Cells[i, 5] = DGSplit.Rows[j].Cells["Nombre"].Value.ToString();
                excel.Cells[i, 6] = DGSplit.Rows[j].Cells["Tarima"].Value.ToString();
                excel.Cells[i, 7] = DGSplit.Rows[j].Cells["Caja"].Value.ToString();
                excel.Cells[i, 8] = DGSplit.Rows[j].Cells["PreSplit"].Value.ToString();
                excel.Cells[i, 9] = DGSplit.Rows[j].Cells["Estatus"].Value.ToString();
                excel.Cells[i, 10] = DGSplit.Rows[j].Cells["Responsable"].Value.ToString();
                excel.Cells[i, 11] = DGSplit.Rows[j].Cells["Reetiquetado"].Value.ToString();
                
                progressBar1.PerformStep();
                r = excel.Range[excel.Cells[i, 1], excel.Cells[i, 11]];
                if (DGSplit.Rows[j].Cells["Estatus"].Value.ToString().Trim() == "S")
                {
                    r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LawnGreen);
                }
                else  if (DGSplit.Rows[j].Cells["Estatus"].Value.ToString().Trim() == "A")
                {
                    r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                }
                else if (DGSplit.Rows[j].Cells["Estatus"].Value.ToString().Trim() == "N")
                {
                    r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                }
                else if (DGSplit.Rows[j].Cells["Estatus"].Value.ToString().Trim() == "D")
                {
                    r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Aqua);
                }
                else{
                    if (DGSplit.Rows[j].Cells["Reetiquetado"].Value.ToString().Trim() == "R")
                    {
                        r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                    }
                    else {
                        r.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
                    }
                }
                
                i++;
                j++;
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

        private void FormReportePresplit_Load(object sender, EventArgs e)
        {
            CreaTable();
            Llena();
        }
    }
}


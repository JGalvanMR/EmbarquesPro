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
    public partial class FrmDetalleSplitxHora : Form
    {
        private string hora = null;
        private string Armador = null;
        private DateTime fechaini = DateTime.Now;
        private DateTime fechafin = DateTime.Now;
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);

        public FrmDetalleSplitxHora()
        {
            InitializeComponent();
        }

        public FrmDetalleSplitxHora(string hora, string armador, DateTime fechaini, DateTime fechafin)
            : this()
        {
            this.hora = hora;
            this.Armador = armador;
            this.fechaini = fechaini;
            this.fechafin = fechafin;
        }

        private void FrmDetalleSplitxHora_Load(object sender, EventArgs e)
        {
            responsable.Text = Armador.Trim();
            horaactual.Text = "Horario de Lectura: " + hora;
            LblPed.Text = "Detalle de Split Generados Al " + fechaini.ToString("dd/MM/yyyy");
            hora = hora.Replace(" - ", "*"); 
            string[] horas = hora.Split('*');
            if (Convert.ToInt32(horas[0]) >= 0 && Convert.ToInt32(horas[0]) < 0)
            {
                fechaini = fechafin;
            }

            DateTime fec1 = Convert.ToDateTime(fechaini.ToShortDateString() + " " + horas[0] + ":00");
            DateTime fec2 = Convert.ToDateTime(fechaini.ToShortDateString() + " " + horas[1] + ":00");


            thisConnecion.Open();
            //string Cadena = "SELECT A.NUM_CAJAS, A.HORA, B.ORDP_FECHA, B.ORDP_LINEA, B.ORDP_TURNO " +
            //                "FROM TB_DET_ETI_FINAL A, TB_MSTR_ORDENES_PROD B " +
            //                "WHERE B.ORDP_FECHA >= '" + Fecha.ToShortDateString()  + "' AND B.ORDP_FECHA <= '" + Fecha.ToShortDateString()  + "' " +
            //                "AND B.ORDP_FOLIO = A.FOLIO AND B.ORDP_LINEA = '" + Linea  + "' AND B.ORDP_TURNO = '" + Turno  + "' " +
            //                "ORDER BY B.ORDP_LINEA, B.ORDP_TURNO,A.HORA ";
            string cadena = "SELECT * FROM Tb_Det_Etiqueta INNER JOIN tb_det_split AS B ON Tb_Det_Etiqueta.emb_folio = B.emb_folio  AND Tb_Det_Etiqueta.Eti_Producto = B.prod_clave AND Tb_Det_Etiqueta.Eti_Recibo = no_lote AND Tb_Det_Etiqueta.Eti_TarIni = CAST(TARINI AS INT) AND Tb_Det_Etiqueta.split = tarima  WHERE (B.NOM_CAPSPLIT = '" + Armador.Trim() + "') AND (Tb_Det_Etiqueta.fecha_cap >= '" + fec1.ToString("dd/MM/yyyy HH:mm") + "') AND (Tb_Det_Etiqueta.fecha_cap <= '" + fec2.ToString("dd/MM/yyyy HH:mm") + "') order by fecha_cap ASC";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cadena, thisConnecion);
            da.Fill(ds, "Info");
            DataTable Info = ds.Tables["INFO"];
            thisConnecion.Close();

            Info.DefaultView.Sort = "fecCap ASC";

            int armador = 0;
            int TotCaj = 0;
            foreach (DataRow Row in Info.Rows)
            {
                //fechaCap, emb_folio, Eti_Recibo, Eti_Producto, Eti_TarIni, Eti_Caja, Estatus, Split, Tipo_Captura
                DGSplit.Rows.Add(Row["fecCap"].ToString(), Row["emb_folio"].ToString(), Row["Eti_Recibo"].ToString(), Row["Eti_Producto"].ToString(), Row["Eti_TarIni"].ToString(), Row["Eti_Caja"].ToString(), Row["Estatus"].ToString(), Row["Split"].ToString(), Row["Tipo_Captura"].ToString());
            }
            label1.Text = "TOTAL DE CAJAS LEIDAS: " + Info.Rows.Count;


            DGSplit.Sort(DGSplit.Columns[0], ListSortDirection.Ascending);

            foreach (DataGridViewRow row in DGSplit.Rows)
            {
                if (row.Cells[6].Value.ToString() == "C")
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else if (row.Cells[8].Value.ToString() == "B")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                }else if (row.Cells[8].Value.ToString() == "V")
                {
                    row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                    row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                }
            }


        }
    }
}

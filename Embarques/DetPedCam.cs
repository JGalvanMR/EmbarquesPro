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
    public partial class DetPedCam : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);

        public string Cveauto = "";
        public string Fecha = "";
        public string Chofer = "";
        DataTable Pedido = new DataTable();
        DataTable Embarque = new DataTable();
        DataTable Cliente = new DataTable();
        public DataTable Inventario = new DataTable();

        public DetPedCam()
        {
            InitializeComponent();
        }

        private void DetPedCam_Load(object sender, EventArgs e)
        {
            LblPed.Text = "Camioneta: " + Cveauto;
            LblDetPed.Text = Chofer;
            thisConnecion.Open();
            string Cadena = "SELECT SUM(pdn_num_unidades) AS CANTI,A.PDN_FOLIO, a.cnte_clave FROM TB_MSTR_PEDIDOS_NAL A, tb_det_pedidos B, TB_MSTR_FACTURAS_NAL C WHERE A.pdn_folio = B.pdn_folio AND A.pdn_tipo = B.pdn_tipo AND PDN_FECHA = '" +
                             Fecha + "' AND C.CVE_AUTO = '" + Cveauto + "' and a.prov_clave = 'MRLUCKY' and a.pdn_estatus <> 'C' AND C.PDN_FOLIO = A.PDN_FOLIO GROUP BY C.CVE_AUTO,A.PDN_FOLIO,a.cnte_clave";
            SqlDataAdapter da = new SqlDataAdapter(Cadena,thisConnecion);
            DataSet ds = new DataSet();
            da.Fill(ds, "Pedido");
            Pedido = ds.Tables["Pedido"];

            Cadena = "SELECT SUM(A.cajas) AS SURTI,b.PDN_FOLIO, b.cnte_clave FROM tb_det_embarque A, tb_mstr_facturas_nal B, tb_mstr_embarque C" +
                     " WHERE B.cve_auto = '" + Cveauto + "' AND B.fcn_fecha = '" + Fecha + "' AND A.ESTATUS != 'C' AND B.pdn_folio = C.EMB_FOLIO AND C.emb_folio = A.emb_folio AND C.emb_tipo = A.emb_tipo GROUP BY B.CVE_AUTO,B.pdn_folio, b.cnte_clave ";
            da = new SqlDataAdapter(Cadena, thisConnecion);
            ds = new DataSet();
            da.Fill(ds,"Embarque");
            Embarque = ds.Tables["Embarque"];
            Cadena = "Select CNTE_CLAVE,CNTE_NOMBRE FROM TB_CAT_CLIENTE ORDER BY CNTE_CLAVE";
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "CAT_CLIENTE");
            Cliente = ds.Tables["CAT_CLIENTE"];
            thisConnecion.Close();
            int i = 0;
            foreach (DataRow row in Pedido.Rows)
            {
                int Ped = Convert.ToInt32(row["Canti"]);
                int Sur = 0;
                string Nom = "";
                foreach (DataRow Row in Embarque.Select("pdn_folio = '" + row["pdn_folio"] + "'"))
                    Sur = Convert.ToInt32(Row["Surti"]);
                foreach (DataRow Row in Cliente.Select("cnte_clave = '" + row["cnte_clave"] + "'"))
                    Nom = Row["cnte_nombre"].ToString().Trim();
                string Porce = (Ped == 0) ? "0" : (Convert.ToDecimal(Sur) / Convert.ToDecimal(Ped) * 100).ToString("##0.00");
                DGDetPed.Rows.Add(row["pdn_folio"],row["cnte_clave"],Nom,Ped , Sur, Porce);
                DGDetPed.Rows[i].Cells["CAMPED"].ToolTipText = "Doble Clic para ver el Detalle";
            }
            FormatoSalida();
        }

        private void DGDetPed_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGDetPed.CurrentCell.ColumnIndex == 0)
            {
                Program.MyGlobal.NoPedido = Convert.ToInt32(DGDetPed.CurrentRow.Cells["CAMPED"].Value);
                Program.MyGlobal.TipoPed = "NAL";
                Program.MyGlobal.PubNoTrailer = "";
                Program.MyGlobal.PubFecEmb = Fecha;
                Program.MyGlobal.CveCliente = ""; //DGPedidos.CurrentRow.Cells["NOMCLI"].Value.ToString();
                Program.MyGlobal.CveProv = ""; // DGPedidos.CurrentRow.Cells["NOMPROV"].Value.ToString();
                ConsPedSur ConsPed = new ConsPedSur();
                ConsPed.Opcion = 2;
                ConsPed.Camioneta = Cveauto ;
                ConsPed.Inven = Inventario.Copy();
                ConsPed.PedidoOrigen = DGDetPed.CurrentRow.Cells["CAMPED"].Value.ToString();
                ConsPed.Tipo = "S";
                ConsPed.ShowDialog(this);
            }
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDetPed.Rows.Count; i++)
            {
                if (Convert.ToInt32(DGDetPed.Rows[i].Cells["CAMCAN"].Value) != Convert.ToInt32(DGDetPed.Rows[i].Cells["CAMSUR"].Value))
                    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
            }
        }
    }
}

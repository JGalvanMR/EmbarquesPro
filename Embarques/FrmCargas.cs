using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;

namespace Embarques
{
    public partial class FrmCargas : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        public string Fecha = "", Tot = "";
        DataTable Info = new DataTable();
        Form1 Frm = new Form1();

        public FrmCargas()
        {
            InitializeComponent();
        }

        private void FrmCargas_Load(object sender, EventArgs e)
        {
            CreaTable();
            thisConnecion.Open();
            string Cadena = "SELECT placacaja,PDN_FOLIO,CNTE_CLAVE,PROV_CLAVE,pdn_tipo,PDN_ELABORO, pdn_horent1, pdn_pedorigen" +
                            " FROM tb_mstr_pedidos_nal " +
                            " WHERE pdn_fecha >= '" + Fecha + "' AND pdn_fecha <= '" + Fecha + "' AND pdn_estatus != 'C' AND Prov_clave != 'MRLUCKY' AND (placacaja != '' or PROV_CLAVE != 'PC') " +
                            " UNION " +
                            " SELECT placacaja,PDN_FOLIO,CNTE_CLAVE,PROV_CLAVE,pdn_tipo,PDN_ELABORO, pdn_horent1, pdn_pedorigen" +
                            " FROM tb_mstr_pedidos_exp " +
                            " WHERE pdn_fecha >= '" + Fecha + "' AND pdn_fecha <= '" + Fecha + "' AND pdn_estatus != 'C' AND Prov_clave != 'MRLUCKY' AND (placacaja != '' or PROV_CLAVE != 'PC') " +
                            " GROUP BY placacaja,PDN_FOLIO,CNTE_CLAVE,PROV_CLAVE,pdn_tipo,PDN_ELABORO, pdn_horent1, pdn_pedorigen ";
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "Pedi");
            DataTable Pedidos = ds1.Tables["Pedi"];
            Cadena = "SELECT ' ' AS PLACACAJA, EMB_FOLIO AS PDN_FOLIO, CNTE_CLAVE, PROV_CLAVE, EMB_tipo AS PDN_TIPO " +
                     " FROM tb_mstr_ORDENES_EMB " +
                     " WHERE EMB_fecha >= '" + Fecha + "' AND EMB_fecha <= '" + Fecha + "' ";
            ds1 = new DataSet();
            da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "Maqui");
            DataTable Maquilas = ds1.Tables["Maqui"];
            Cadena = "SELECT turno, destino, chofer, responsable, conse, HoraRegVig, HoraEnt, HoraSal, horaini, horafin, no_trailer" +
                     " FROM tb_mstr_trailer " +
                     " WHERE fecha >= '" + Fecha + "' AND fecha <= '" + Fecha + "' ";
            ds1 = new DataSet();
            da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "Emb");
            DataTable Embar = ds1.Tables["Emb"];
            int mCons = 0;
            string mPlaca = "XX", mTra = "xx";
            int UltConse = 0;
            Info.Columns.Add("Surtible", typeof(string));
            Pedidos.Columns.Add("Placa", typeof(string));
            foreach(DataRow row in Pedidos.Rows)
            {
                if (row["PlacaCaja"].ToString().Trim().Length == 0)
                {
                    if (row["pdn_pedorigen"].ToString().Trim() == "0")
                        row["PlacaCaja"] = row["pdn_folio"];
                    else
                        row["PlacaCaja"] = row["pdn_pedorigen"];
                }
                else
                    row["Placa"] = row["PlacaCaja"];
            }
            Pedidos.DefaultView.Sort = "PlacaCaja";
            Pedidos = Pedidos.DefaultView.ToTable(true);
            foreach (DataRow Row in Pedidos.Rows)
            {
                if (mPlaca == Row["PLACACAJA"].ToString() && mTra == Row["PROV_CLAVE"].ToString())
                    continue;
                if (mPlaca != Row["PLACACAJA"].ToString() || mTra !=  Row["PROV_CLAVE"].ToString())
                {
                    mCons++;
                    mPlaca = Row["PLACACAJA"].ToString();
                    mTra = Row["PROV_CLAVE"].ToString();
                }
                string Hay = "N";
                if (Row["Placa"].ToString().Trim().Length > 0)
                {
                    foreach (DataRow row in Embar.Select("NO_TRAILER = '" + Row["PLACACAJA"].ToString() + "'"))
                    {
                        string HrFin = (row["horafin"].ToString().Trim().Length > 5) ? row["horafin"].ToString().Trim().Substring(11, row["horafin"].ToString().Trim().Length - 11) : "--:--";
                        string HrIni = (row["horaini"].ToString().Trim().Length > 5) ? row["horaIni"].ToString().Trim().Substring(11, row["horaIni"].ToString().Trim().Length - 11) : "--:--";
                        string Var_Sur = (HrFin == "--:--") ? " " : "C";
                        Info.Rows.Add(mCons.ToString(), mPlaca, Row["PDN_FOLIO"].ToString(), Row["PDN_TIPO"].ToString(), Row["CNTE_CLAVE"].ToString(), Row["PROV_CLAVE"].ToString(),
                                      row["conse"].ToString(), Row["pdn_horent1"].ToString(), row["HoraEnt"].ToString(), HrIni, row["destino"].ToString(), row["chofer"].ToString(), HrFin, Row["pdn_Elaboro"].ToString(), Var_Sur);
                        if (Convert.ToInt32(row["conse"]) > UltConse)
                            UltConse = Convert.ToInt32(row["conse"]);
                        Hay = "S";
                    }
                }
                if (Hay == "N")
                    Info.Rows.Add(mCons.ToString(), mPlaca, Row["PDN_FOLIO"].ToString(), Row["PDN_TIPO"].ToString(), Row["CNTE_CLAVE"].ToString(), Row["PROV_CLAVE"].ToString(),"99", Row["pdn_horent1"].ToString(),"","","","","",Row["pdn_elaboro"].ToString(),"--:--");
            }
            foreach (DataRow Row in Maquilas.Rows)
            {
                if (mPlaca != Row["PLACACAJA"].ToString() || mTra != Row["PROV_CLAVE"].ToString())
                {
                    mCons++;
                    mPlaca = Row["PLACACAJA"].ToString();
                    mTra = Row["PROV_CLAVE"].ToString();
                }
                string Hay = "N";
                foreach (DataRow row in Embar.Select("NO_TRAILER = '" + Row["PLACACAJA"].ToString() + "'"))
                {
                    string Var_Sur = (row["horafin"].ToString().Trim() == "--:--") ? " " : "C";
                    Info.Rows.Add(mCons.ToString(), mPlaca, Row["PDN_FOLIO"].ToString(), Row["PDN_TIPO"].ToString(), Row["CNTE_CLAVE"].ToString(), Row["PROV_CLAVE"].ToString(),
                                  row["conse"].ToString(), row["HoraEnt"].ToString(), row["HoraSal"].ToString(), row["destino"].ToString(), row["chofer"].ToString(), row["horafin"].ToString(),Var_Sur);
                    Hay = "S";
                }
                if (Hay == "N")
                    Info.Rows.Add(mCons.ToString(), mPlaca, Row["PDN_FOLIO"].ToString(), Row["PDN_TIPO"].ToString(), Row["CNTE_CLAVE"].ToString(), Row["PROV_CLAVE"].ToString(),"99");
            }
            Frm.LLenaInventario();
            string mfec = Fecha;
            Frm.Pedidosdeldia(mfec);
            Info.DefaultView.Sort = "ConseLLegada";
            Info = Info.DefaultView.ToTable(true); 
            foreach (DataRow row in Info.Rows)
            {
                if (Convert.ToInt32(row["ConseLLegada"]) != 99)
                    row["Conse"] = row["ConseLLegada"].ToString();
                else
                {
                    UltConse++;
                    row["Conse"] = UltConse.ToString();
                }

            }
            foreach (DataRow row in Info.Rows)
            {
                string Sts = row["SURTIBLE"].ToString();
                string HI = row["IniCarga"].ToString().Trim();
                string Mped = row["Pedido"].ToString().Trim();
                if (Sts == "C")
                    Frm.ACTSURTIDO(row["Placa"].ToString());
                else
                    if (HI.Trim() != "--:--" && HI.Trim() != "")
                {
                    Frm.ACTSURTIDO(row["Placa"].ToString());
                    row["SURTIBLE"] = Frm.STSTRANSITO(row["Placa"].ToString());
                    //row.Cells["AVANCE"].Value = PorceAvance(row.Cells["TrailerEmb"].Value.ToString()) + " %";
                }
                else
                    //if (Sts != "C")
                    if (Mped.Trim() != row["PLACA"].ToString().Trim())
                      row["SURTIBLE"] = (Mped.Trim().Length > 0) ? Frm.ACTSURTIBLE(row["Placa"].ToString()) : "";
                    else
                        row["SURTIBLE"] = (Mped.Trim().Length > 0) ? Frm.ACTSURTIBLEPED(Mped) : "";



            }
            DGDatos.DataSource = Info;
            FormatoSalida();
            
            LblFec.Text = "Cargas del día: " + Fecha + "  Total: " + Tot ;
        }



        private void CreaTable()
        {
            Info.Columns.Add("Conse", typeof(string));     //0
            Info.Columns.Add("Placa", typeof(string));     //1
            Info.Columns.Add("Pedido", typeof(string));     //2
            Info.Columns.Add("Tipo", typeof(string));       //3
            Info.Columns.Add("Cliente", typeof(string));    //4
            Info.Columns.Add("Transporte", typeof(string)); //5
            Info.Columns.Add("ConseLLegada", typeof(Int32));   //6
            Info.Columns.Add("Cita", typeof(string));       //7 
            Info.Columns.Add("HrLlego", typeof(string));    //8
            Info.Columns.Add("IniCarga", typeof(string));    //9
            Info.Columns.Add("Destino", typeof(string));    //10
            Info.Columns.Add("Chofer", typeof(string));     //11
            Info.Columns.Add("FinCarga", typeof(string));   //12
            Info.Columns.Add("Elaboro", typeof(string));   //13
        }

        private void FormatoSalida()
        {
            for (int i = 0; i < DGDatos.Rows.Count; i++)
            {
                //if (Convert.ToInt32(DetPed.Rows[i]["PEDIDO"]) > Convert.ToInt32(DetPed.Rows[i]["EXISTENCIA"]))
                //    DGDetPed.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                string StaSur = DGDatos.Rows[i].Cells["SURTIBLE"].Value.ToString() ;
                if (StaSur == "T")
                    DGDatos.Rows[i].Cells["SURTIBLE"].Style.BackColor = System.Drawing.Color.Yellow;
                else
                    if (StaSur == "P" || StaSur == "A")
                    {
                        DGDatos.Rows[i].Cells["SURTIBLE"].Style.BackColor = System.Drawing.Color.Red;
                        DGDatos.Rows[i].Cells["SURTIBLE"].Style.ForeColor = System.Drawing.Color.Yellow;
                    }
                    else if (StaSur == "S")
                        DGDatos.Rows[i].Cells["SURTIBLE"].Style.BackColor = System.Drawing.Color.Green;
            }
            DGDatos.Columns[0].Width = 40;
            DGDatos.Columns[1].Width = 65;
            DGDatos.Columns[2].Width = 70;
            DGDatos.Columns[3].Width = 40;
            DGDatos.Columns[4].Width = 70;
            DGDatos.Columns[5].Width = 80;
            DGDatos.Columns[6].Width = 70;
            DGDatos.Columns[7].Width = 50;
            DGDatos.Columns[8].Width = 50;
            DGDatos.Columns[9].Width = 65;
            DGDatos.Columns[10].Width = 120;
            DGDatos.Columns[11].Width = 75;
            //DGDatos.Columns[12].Visible = false;
            DGDatos.Columns[6].Visible = false;
            DGDatos.Columns[13].Width = 100;
            DGDatos.Columns[14].Width = 50;
            for(int i = 0; i < 14; i++)
              DGDatos.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDatos.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        private void DGDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGDatos.CurrentCell.ColumnIndex == 14) // Consultar Detalle del Pedido Vs lo Surtido CAMPO PEDIDO
            {
                string Sta = DGDatos.CurrentRow.Cells["SURTIBLE"].Value.ToString();
                if ((Sta == "S" || Sta == "P") && DGDatos.CurrentRow.Cells["PEDIDO"].Value.ToString().Trim().Length > 0)
                {
                    Program.MyGlobal.NoPedido = Convert.ToInt32(DGDatos.CurrentRow.Cells["PEDIDO"].Value);
                    Program.MyGlobal.TipoPed = DGDatos.CurrentRow.Cells["TIPO"].Value.ToString();
                    Program.MyGlobal.CveCliente = "";  //DGPedidos.CurrentRow.Cells["NOMCLI"].Value.ToString();
                    Program.MyGlobal.CveProv = DGDatos.CurrentRow.Cells["TRANSPORTE"].Value.ToString();
                    Program.MyGlobal.PubNoTrailer = DGDatos.CurrentRow.Cells["Placa"].Value.ToString();
                    Program.MyGlobal.PubFecEmb = Fecha;
                    ConsulPedPen ConsPed = new ConsulPedPen();
                    ConsPed.Inven = Frm.RegreData().Copy();
                    if (Program.MyGlobal.NoPedido.ToString().Trim() == Program.MyGlobal.PubNoTrailer.ToString().Trim())
                        ConsPed.Opcion = "P";
                    ConsPed.ShowDialog(this);
                }

                if ((Sta == "T" || Sta == "A" || Sta == "C") && DGDatos.CurrentRow.Cells["PEDIDO"].Value.ToString().Trim().Length > 0)
                {
                    Program.MyGlobal.NoPedido = Convert.ToInt32(DGDatos.CurrentRow.Cells["PEDIDO"].Value);
                    Program.MyGlobal.TipoPed = DGDatos.CurrentRow.Cells["TIPO"].Value.ToString();
                    Program.MyGlobal.PubNoTrailer = DGDatos.CurrentRow.Cells["PLACA"].Value.ToString();
                    Program.MyGlobal.PubFecEmb = Fecha;
                    Program.MyGlobal.CveCliente = ""; //DGPedidos.CurrentRow.Cells["NOMCLI"].Value.ToString();
                    Program.MyGlobal.CveProv = ""; // DGPedidos.CurrentRow.Cells["NOMPROV"].Value.ToString();
                    ConsPedSur ConsPed = new ConsPedSur();
                    ConsPed.Inven = Frm.RegreData().Copy();
                    ConsPed.Tipo = DGDatos.CurrentRow.Cells["SURTIBLE"].Value.ToString();
                    ConsPed.PedidoOrigen = DGDatos.CurrentRow.Cells["PEDIDO"].Value.ToString();
                    ConsPed.ShowDialog(this);
                }
            }
        }

       
    }
}

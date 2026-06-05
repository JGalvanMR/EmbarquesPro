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
    public partial class ConsFoliosCaducidades : Form
    {
        DataTable Inven = new DataTable();
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        public string mProd = "", mNom = "";

        public ConsFoliosCaducidades()
        {
            InitializeComponent();
        }

        private void ConsFoliosCaducidades_Load(object sender, EventArgs e)
        {
            LblPed.Text = mNom;
            CreaTable();
            Genera();
            Formato();
        }

        private void CreaTable()
        {
            Inven.Columns.Add("Nombre", typeof(string));
            Inven.Columns.Add("FechaEla", typeof(string)); //int
            Inven.Columns.Add("Lote", typeof(string));
            Inven.Columns.Add("FecCad", typeof(string));
            Inven.Columns.Add("FecCadTeo", typeof(string));
            Inven.Columns.Add("Dias", typeof(int));
            Inven.Columns.Add("Existencia", typeof(int));
            Inven.Columns.Add("Cantidad", typeof(int));
            Inven.Columns.Add("Conse", typeof(int));
            Inven.Columns.Add("Prod", typeof(string));
            Inven.Columns.Add("CvePro", typeof(string));
            Inven.Columns.Add("Tipo", typeof(string));
            Inven.Columns.Add("FechaCad", typeof(string));
            Inven.Columns.Add("Ubica", typeof(string));
            Inven.Columns.Add("Tarima", typeof(string));

        }

        private void Genera()
        {
            Inven.Rows.Clear();
            thisConnecion.Open();
            DataSet ds = new DataSet();
            DataTable Info = new DataTable();
            
            String Cadena = "SELECT C.PROD_NOMBRE, A.RECIBO, A.TARIMA, A.PTI_FECHA, A.LOTE, A.FECHA_CAD, A.ETIQUETA, A.SURTIDO, A.PROD_CLAVE, A.UBICACION " +
                            " FROM TB_DET_TRAZABILIDAD A, tb_mstr_recepcion_pt B, tb_cat_producto C " +
                            " WHERE A.PTI_ESTATUS_SUR =  ' ' AND A.TIPO = 'PTC'  AND A.recibo = B.rpt_recibo AND A.PROD_CLAVE = C.PROD_CLAVE AND B.rpt_estatus = ' ' AND (B.rpt_tipo != 'TR' OR (B.rpt_tipo = 'TR' AND B.RPT_INVENTARIO = 'S'))" +
                            " and A.PROD_CLAVE = '"+ mProd +"' ORDER BY PROD_NOMBRE,RECIBO,PTI_CLAVE ";
            ds = new DataSet();
            Info = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "Info");
            Info = ds.Tables["Info"];
            string Mnom = "", Nprod = "";
            int INI = 1, totp = 0, totg = 0, tott = 0;
            Int32 Teo = 0, Fisi = 0, Surti = 0;
            foreach (DataRow row in Info.Rows)
            {
                if (INI == 1)
                {
                    Mnom = row["PROD_NOMBRE"].ToString();
                    Nprod = row["prod_clave"].ToString();
                    //Inven.Rows.Add(Mnom, "", "", "", "", 0, 0, 0, 1, Mnom, Nprod, ""); se bloqueo el 8 de abril 2017 original
                    Inven.Rows.Add(Mnom, "", "", Mnom, "", 0, 0, 0, 1, Mnom, Nprod, "");
                    INI = 0;
                }

                //if (Mnom == "TOMATE ORGANICO MR.LUCKY CHERRY 12/10 OZ          ")
                //{ 
                //}

                string fol = row["RECIBO"].ToString();
                if (Mnom != row["PROD_NOMBRE"].ToString())
                {
                    Teo = 0; Fisi = 0; Surti = 0;
                    //Inven.Rows.Add("TOTAL " + Mnom, "", "Teorico:", (Teo + tott).ToString(), "", Fisi + tott, 0, totp, 3, Mnom, Nprod, "", "99991231"); se bloqueo el 8 de abril 2017 original
                    Inven.Rows.Add("TOTAL " + Mnom, "", "", "TOTAL " + Nprod + " " + Mnom, "", (Teo + tott - Surti).ToString(), Fisi + tott - Surti, totp, 3, Mnom, Nprod, "", "99991231");
                    Nprod = row["prod_clave"].ToString();
                    Mnom = row["PROD_NOMBRE"].ToString();
                    //Inven.Rows.Add(Mnom, "", "", Mnom, "", 0, 0, 0, 1, Mnom, Nprod, ""); se bloqueo el 8 de abril 2017 original
                    Inven.Rows.Add(Mnom, "", "", "", "", 0, 0, 0, 1, Mnom, Nprod, "");
                    totp = 0; tott = 0;
                }

                totp = totp + (Convert.ToInt32(row["ETIQUETA"]) - Convert.ToInt32(row["SURTIDO"]));
                totg = totg + (Convert.ToInt32(row["ETIQUETA"]) - Convert.ToInt32(row["SURTIDO"]));
                //if (Convert.ToDateTime(row["PTI_FECHA"]).ToShortDateString() == System.DateTime.Now.ToShortDateString())
                //    tott = tott + (Convert.ToInt32(row["ETIQUETA"])); // - Convert.ToInt32(row["SURTIDO"]));
                TimeSpan Mdias = TimeSpan.Zero;
                DateTime FecCad = Convert.ToDateTime(row["PTI_FECHA"]);
                string Ubica = Convert.ToString(row["ubicacion"]);
                if (row["FECHA_CAD"].ToString().Trim().Length > 0)
                {
                    Mdias = Convert.ToDateTime(row["FECHA_CAD"]) - System.DateTime.Now.AddDays(-1);
                    FecCad = Convert.ToDateTime(row["FECHA_CAD"]);
                }
                else
                {
                    if (Mnom.Contains("BETABEL"))
                        FecCad = FecCad.AddDays(60);
                    else
                        if (Mnom.Contains("AJO"))
                            FecCad = FecCad.AddDays(180);
                        else
                            if (Mnom.Contains("ADEREZO") || Mnom.Contains("VINAGRETA") || Mnom.Contains("QUESO"))
                                FecCad = FecCad.AddDays(90);
                            else
                                FecCad = FecCad.AddDays(14);
                    Mdias = FecCad - System.DateTime.Now.AddDays(-1);
                }
                string MNewFec = (row["FECHA_CAD"].ToString().Trim().Length > 0) ? Convert.ToDateTime(row["FECHA_CAD"]).ToString("yyyyMMdd") : FecCad.ToString("yyyyMMdd");
                Inven.Rows.Add(row["RECIBO"].ToString() + "-" + row["TARIMA"].ToString().Trim(), row["PTI_FECHA"].ToString(), row["LOTE"].ToString(), (row["FECHA_CAD"].ToString().Trim().Length > 0) ? row["FECHA_CAD"] : FecCad.ToShortDateString(), "", Mdias.Days, row["ETIQUETA"], (Convert.ToInt32(row["ETIQUETA"]) - Convert.ToInt32(row["SURTIDO"])), 2, Mnom, row["PROD_CLAVE"], "PTC", MNewFec, Ubica, row["TARIMA"].ToString().Trim());

            }
            Teo = 0; Fisi = 0; Surti = 0;
            //Inven.Rows.Add("TOTAL " + Mnom, "", "", "TOTAL " + Mnom, "", 0, 0, totp, 3, Mnom, Nprod, "", "99991231"); se bloqueo el 8 de abril 2017 original
            Inven.Rows.Add("TOTAL " + Mnom, "", "", "TOTAL " + Nprod + " " + Mnom, "", (Teo + tott - Surti).ToString(), Fisi + tott - Surti, totp, 3, Mnom, Nprod, "", "99991231");
            // RECIBOS DE PRODUCCION
            totp = 0;
            //totg = 0;
            tott = 0;
            INI = 1;//
            Cadena = "SELECT A.PROD_NOMBRE, B.FOLIO, B.TARIMA, B.FECHA, B.NUM_LOTE, B.NUM_CAJAS, B.CAJAS_SUR, B.CVE_PROD, B.UBICACION, b.fechacad FROM TB_DET_ETI_FINAL B, TB_CAT_PRODUCTO A " +
                      "WHERE B.ESTATUS_SUR =  ' ' AND B.CVE_PROD = A.PROD_CLAVE and A.PROD_CLAVE = '" + mProd + "' ORDER BY A.PROD_NOMBRE,B.FOLIO,B.TARIMA ";
            ds = new DataSet();
            DataTable Info2 = new DataTable();
            da = new SqlDataAdapter(Cadena, thisConnecion);
            da.Fill(ds, "Info2");
            Info2 = ds.Tables["Info2"];
            //datos = cmd.ExecuteReader();
            //while (datos.Read())
            //{
            //    Var_LoteSem = datos["semana"].ToString() + "-" + OleFE.Value.ToString("ddd").ToUpper();
            //}
            //Var_LoteSem = (Var_LoteSem.Trim().Length > 0) ? Var_LoteSem.Substring(0, 5) : ""; 

            foreach (DataRow row in Info2.Rows)
            {
                if (INI == 1)
                {
                    Mnom = row["PROD_NOMBRE"].ToString();
                    Nprod = row["cve_prod"].ToString();
                    Inven.Rows.Add(Mnom, "", "", Mnom, "", 0, 0, 0, 1, Mnom, Nprod);
                    INI = 0;
                }

                if (Mnom != row["PROD_NOMBRE"].ToString())
                {
                    Teo = 0; Fisi = 0; Surti = 0;
                    //Inven.Rows.Add("TOTAL " + Mnom, "", "Teorico:", (Teo + tott).ToString(), "", Fisi + tott, 0, totp, 3, Mnom, Nprod, "", "99991231"); se bloqueo el 8 de abril 2017 original
                    Inven.Rows.Add("TOTAL " + Mnom, "", "", "TOTAL " + Nprod + " " + Mnom, "", (Teo + tott - Surti).ToString(), Fisi + tott - Surti, totp, 3, Mnom, Nprod, "", "99991231");
                    //Inven.Rows.Add("TOTAL " + Mnom, "", "Teorico:", (Teo).ToString(), "", Fisi, 0, totp, 3, Mnom, Nprod, "", "99991231");
                    Mnom = row["PROD_NOMBRE"].ToString();
                    Nprod = row["cve_prod"].ToString();
                    //Inven.Rows.Add(Mnom, "", "", "", "", 0, 0, 0, 1, Mnom, Nprod);se bloqueo el 8 de abril 2017 original
                    Inven.Rows.Add(Mnom, "", "", Mnom, "", 0, 0, 0, 1, Mnom, Nprod);
                    totp = 0; tott = 0;
                }
                totp = totp + (Convert.ToInt32(row["NUM_CAJAS"]) - Convert.ToInt32(row["CAJAS_SUR"]));
                totg = totg + (Convert.ToInt32(row["NUM_CAJAS"]) - Convert.ToInt32(row["CAJAS_SUR"]));
                //if (Convert.ToDateTime(row["FECHA"]).ToShortDateString() == System.DateTime.Now.ToShortDateString())
                //   tott = tott + (Convert.ToInt32(row["NUM_CAJAS"])); // - Convert.ToInt32(row["CAJAS_SUR"]));
                TimeSpan Mdias = TimeSpan.Zero;
                DateTime FecCad = Convert.ToDateTime(row["FECHA"]);
                string Ubica = Convert.ToString(row["ubicacion"]);
                string Mlot = "", Mfeca = "";
                if (row["NUM_LOTE"].ToString().Trim().Length > 0)
                {
                    int Mtam = row["NUM_LOTE"].ToString().Trim().Length;
                    if (row["fechacad"].ToString().Trim().Length > 0)
                        Mfeca = row["fechacad"].ToString().Substring(6, 2) + "/" + row["fechacad"].ToString().Substring(4, 2) + "/" + row["fechacad"].ToString().Substring(0, 4);
                    else
                        Mfeca = ""; //ConviertetoFecha(row["NUM_LOTE"].ToString().Substring((Mtam == 12) ? 7 : 6, 5));

                    string Mfol = row["FOLIO"].ToString();
                    Mdias = Convert.ToDateTime(Mfeca) - System.DateTime.Now.AddDays(-1);
                }
                else
                {
                    if (Mnom.Contains("BETABLE"))
                        FecCad = FecCad.AddDays(60);
                    else
                        if (Mnom.Contains("AJO"))
                            FecCad = FecCad.AddDays(180);
                        else
                            if (Mnom.Contains("ADEREZO") || Mnom.Contains("VINAGRETA") || Mnom.Contains("QUESO"))
                                FecCad = FecCad.AddDays(90);
                            else
                                FecCad = FecCad.AddDays(14);
                    Mdias = FecCad - System.DateTime.Now.AddDays(-1);
                    Mfeca = FecCad.ToShortDateString();
                }
                Mlot = ""; // Lote(row["Fecha"].ToString());  //row["NUM_LOTE"].ToString().Substring(0, 4);
                //    Mdias = Convert.ToDateTime(row["FECHA_CAD"]) - System.DateTime.Now;
                string MNewFec = Convert.ToDateTime(Mfeca).ToString("yyyyMMdd");
                Inven.Rows.Add(row["FOLIO"].ToString() + "-" + row["TARIMA"].ToString().Trim(), row["FECHA"].ToString(), Mlot, Mfeca, "", Mdias.Days, row["NUM_CAJAS"], (Convert.ToInt32(row["NUM_CAJAS"]) - Convert.ToInt32(row["CAJAS_SUR"])), 2, Mnom, Nprod, "PTP", MNewFec, Ubica, row["TARIMA"].ToString().Trim());
            }
            Teo = 0; Fisi = 0; Surti = 0;
            Inven.Rows.Add("TOTAL " + Mnom, "", "", "TOTAL " + Nprod + " " + Mnom, "", (Teo + tott - Surti).ToString(), Fisi + tott - Surti, totp, 3, Mnom, Nprod, "", "99991231");
            Inven.Rows.Add("TOTAL GENERAL", "", "", "TOTAL GENERAL", "", 0, 0, totg, 4, "ZZZZZZZZZ", "", "", "99999999");
            Inven.DefaultView.Sort = "Prod, Conse, FechaCad ASC";
            Inven = Inven.DefaultView.ToTable();
            DGDatos.DataSource = Inven;
            //if (user == "PRODUCCION")
            //    FormatoProd();
            //else
            //Formato();
            thisConnecion.Close();
            //Inven2 = Inven.Copy();
            decimal NoPB = 0, NoPT = 0, NoPF = 0, TeoFis = 0;
            foreach (DataRow Row in Inven.Select("FechaCad = '99991231'"))
            {
                ////prod_clave, inv_teorico, inv_fisico
                //foreach(DataRow row in Teorico.Select("Prod_Clave = '"+Row["cvepro"].ToString()+"'"))
                //{
                //    Row["Dias"] = Convert.ToInt32(row["inv_teorico"]);
                //    Row["EXistencia"] = Convert.ToInt32(row["inv_fisico"]);
                //}
                if (Row["Nombre"].ToString().Contains("PROCESO") || Row["Nombre"].ToString().Contains("CANASTILLA"))
                {
                    continue;
                }
                NoPT++;
                //Int32 Teo1 = (Row["FecCad"].ToString().Trim() == "" || Row["FecCad"].ToString().Trim().Length > 5) ? 0 : Convert.ToInt32(Row["FecCad"]); // Teorico
                //Int32 Teo2 = (Row["Cantidad"].ToString().Trim() == "") ? 0 : Convert.ToInt32(Row["Cantidad"]); // Actual 
                //Int32 Teo3 = (Row["Dias"].ToString().Trim() == "") ? 0 : Convert.ToInt32(Row["Dias"]);  // Fisico
                Int32 Teo1 = (Row["Dias"].ToString().Trim() == "") ? 0 : Convert.ToInt32(Row["Dias"]); // Teorico
                Int32 Teo3 = (Row["Existencia"].ToString().Trim() == "") ? 0 : Convert.ToInt32(Row["Existencia"]);  // Fisico
                Int32 Teo2 = (Row["Cantidad"].ToString().Trim() == "") ? 0 : Convert.ToInt32(Row["Cantidad"]); // Actual 

                if (Teo1 == Teo2)
                    NoPB++;
                if (Teo3 == Teo2)
                    NoPF++;
                if (Teo1 == Teo3)
                    TeoFis++;

            }
            //LblProd.Text = NoPB.ToString("###") + "/" + NoPT.ToString("###");
            //LblPorce.Text = ((NoPB / NoPT) * 100).ToString("###") + " %";
            //LblFisi.Text = ((NoPF / NoPT) * 100).ToString("###") + " %";
            //LblTeoFis.Text = TeoFis.ToString("###") + "/" + NoPT.ToString("###") + "  " + ((TeoFis / NoPT) * 100).ToString("###") + " %";

        }

        private void Formato()
        {
            DGDatos.Columns[0].Width = 440;
            DGDatos.Columns[1].Width = 85;
            DGDatos.Columns[2].Width = 50;
            DGDatos.Columns[3].Width = 85;
            //DGDatos.Columns[3].Width = 450;
            DGDatos.Columns[4].Width = 85;
            DGDatos.Columns[5].Width = 50; //40
            DGDatos.Columns[6].Width = 60; //60
            DGDatos.Columns[7].Width = 60; //60
            DGDatos.Columns[13].Width = 70;
            DGDatos.Columns[14].Width = 55;
            DGDatos.Columns[0].HeaderText = "FOLIO";
            DGDatos.Columns[1].HeaderText = "FECHA ELA";
            DGDatos.Columns[2].HeaderText = "LOTE";
            DGDatos.Columns[3].HeaderText = "FECHA CAD";
            DGDatos.Columns[4].HeaderText = "FEC CAT TEO";
            DGDatos.Columns[5].HeaderText = "DIAS / TEORICO";
            DGDatos.Columns[6].HeaderText = "CANTIDAD / FISICO";
            DGDatos.Columns[7].HeaderText = "EXISTENCIA";
            DGDatos.Columns[13].HeaderText = "UBICACION";
            DGDatos.Columns[14].HeaderText = "TARIMA";
            //DGDatos.Columns[0].Visible = false; //se modifico el 8 de abril 2017 original
            //DGDatos.Columns[1].Visible = false; //se modifico el 8 de abril 2017 original
            //DGDatos.Columns[2].Visible = false; //se modifico el 8 de abril 2017 original
            DGDatos.Columns[4].Visible = false;
            DGDatos.Columns[8].Visible = false;
            DGDatos.Columns[9].Visible = false;
            DGDatos.Columns[10].Visible = false;
            DGDatos.Columns[11].Visible = false;
            DGDatos.Columns[12].Visible = false;
            DGDatos.Columns[14].Visible = false;
            //DGDatos.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            for (int i = 0; i < Inven.Rows.Count; i++)
            {
                //MessageBox.Show(Vtas.Rows[i][4].ToString());
                //if ((Convert.ToInt32(Vtas.Rows[i][1].ToString())) + (Convert.ToInt32(Vtas.Rows[i][2].ToString())) - (Convert.ToInt32(Vtas.Rows[i][3].ToString())) - (Convert.ToInt32(Vtas.Rows[i][4].ToString())) < 0)
                //    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                //    GridDatos[5, i].Style.BackColor = System.Drawing.Color.Red;
                //if (Vtas.Rows[i][6].ToString() == "0" )
                //    GridDatos[6, i].Style.BackColor = System.Drawing.Color.Red;
                if (Inven.Rows[i]["Conse"].ToString() == "1")
                    //GridDatos[0,i].Style.BackColor = Color.Yellow;
                    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Cyan;
                if (Convert.ToInt32(Inven.Rows[i]["dias"]) <= 4 && Inven.Rows[i]["Conse"].ToString() == "2")
                    //GridDatos[0,i].Style.BackColor = Color.Yellow;
                    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                if (Convert.ToInt32(Inven.Rows[i]["dias"]) >= 5 && Convert.ToInt32(Inven.Rows[i]["dias"]) <= 9 && Inven.Rows[i]["Conse"].ToString() == "2")
                    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Orange;
                if (Convert.ToInt32(Inven.Rows[i]["dias"]) >= 10 && Convert.ToInt32(Inven.Rows[i]["dias"]) <= 15 && Inven.Rows[i]["Conse"].ToString() == "2")
                    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Yellow;
                if (Convert.ToInt32(Inven.Rows[i]["dias"]) >= 16 && Inven.Rows[i]["Conse"].ToString() == "2")
                    DGDatos.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.ForestGreen;
                //if (Inven.Rows[i]["Conse"] == 1)
                //    DGDatos[0, i].Style.Font = new Font(GridDatos.DefaultCellStyle.Font, FontStyle.Bold);
                //if (Inven.Rows[i][8].ToString() == "          " || Inven.Rows[i][9].ToString() == "          ")
                //    DGDatos[0, i].Style.BackColor = System.Drawing.Color.Aquamarine;
                //if (Inven.Rows[i][1].ToString() != Inven.Rows[i][10].ToString())
                //    DGDatos[0, i].Style.Font = new Font(DGDatos.DefaultCellStyle.Font, FontStyle.Bold);

            }
            //DGDatos.Columns[1].DefaultCellStyle.Format = "N0";
            DGDatos.Columns[5].DefaultCellStyle.Format = "###";
            DGDatos.Columns[6].DefaultCellStyle.Format = "##,###";
            DGDatos.Columns[7].DefaultCellStyle.Format = "##,###";
            DGDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGDatos.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //DGDatos.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //DGDatos.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //DGDatos.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Embarques
{
	public partial class MonitorSplit : Form
	{
		DataTable Datos = new DataTable();
		DataTable Info = new DataTable();
		DataTable Nombre = new DataTable();
		SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
		DateTime Fechaini;
		DateTime Fechafin;
		public MonitorSplit()
		{
			InitializeComponent();
			//CreaTable();
			//LlenaHora();
			//createHeaderDatos();
			Todo();


		}

		private void createHeaderDatos()
		{
			//Datos.Columns.Add("HORA", typeof(string));  //0
			Datos.Columns.Add("TOTAL", typeof(Int32));

		}

		private void CreaTable()
		{
			Datos.Columns.Add("HORA", typeof(string));  //0

			thisConnecion.Open();

			string Cadena = "SELECT * FROM tb_Respon_Split WHERE status = 'A' and nom_capsplit NOT LIKE '%SUPERVISOR%' and nom_capsplit NOT LIKE '%SISTEMAS%' and nom_capsplit NOT LIKE '%JOSE DELGADO%'";
			//string Cadena = "SELECT*FROM tb_Respon_Split WHERE cve_capsplit IN ('36  ', '256 ', '321 ', '561 ', '591 ', '640 ', '837 ', '1107', '1132', '1907', '1950', '2269', '2342', '2639', '2677', '2739', '3367', '3630', '3652', '3701', '3889' ) AND status = 'A'";
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
			da.Fill(ds, "Nombre");
			Nombre = ds.Tables["Nombre"];
			SqlCommand cmd;
			cmd = new SqlCommand(Cadena);
			cmd.Connection = thisConnecion;
			SqlDataReader Infox;
			Infox = cmd.ExecuteReader();
			while (Infox.Read())
			{
				Datos.Columns.Add(Infox["nom_capsplit"].ToString().Trim(), typeof(string));  //0
				
			}
			DGDatos.DataSource = Datos;
			thisConnecion.Close();
		}
		private void LlenaHora()
		{
			//Datos.Columns.Add("HORA", typeof(string));  //0
			Datos.Rows.Add("TOTAL");
			Datos.Rows.Add("CAJAS");
			Datos.Rows.Add("CANCELADAS");
			Datos.Rows.Add("BLANCAS");
			Datos.Rows.Add("VERDES");
			for (int i = 4; i < 24; i++)
				Datos.Rows.Add(i.ToString().Trim() + " - " + (i + 1).ToString().Trim());
			for (int i = 0; i < 4; i++)
				Datos.Rows.Add(i.ToString().Trim() + " - " + (i + 1).ToString().Trim());
			
		}

		private void Todo()
		{
			
			try
			{
				if (DGDatos.Rows.Count > 0)
				{
					((DataTable)DGDatos.DataSource).Rows.Clear();
				}


			}
			catch
			{

			}
			try
			{
				if (DGDatos.Rows.Count > 0)
				{
					((DataTable)DGDatos.DataSource).Columns.Clear();
				}

			}
			catch
			{

			}

			DGDatos.Update();
			lblGenerandoReporte.Visible = true;
			progressBar2.Visible = true;
			lblGenerandoReporte.Update();
			progressBar2.Update();

			
			if (Datos.Columns.Count == 0)
			{
				CreaTable();
			}



			Fechaini = Convert.ToDateTime(DT.Value.ToString("dd/MM/yyyy"));
			Fechafin = Convert.ToDateTime(DT.Value.ToString("dd/MM/yyyy")).AddDays(1);
			LlenaHora();
			thisConnecion.Open();
			//string Cadena = "SELECT A.NUM_CAJAS, A.HORA, B.ORDP_FECHA, B.ORDP_LINEA, B.ORDP_TURNO " +
			//                "FROM TB_DET_ETI_FINAL A, TB_MSTR_ORDENES_PROD B " +
			//                "WHERE B.ORDP_FECHA >= '" + Fecha.ToShortDateString()  + "' AND B.ORDP_FECHA <= '" + Fecha.ToShortDateString()  + "' " +
			//                "AND B.ORDP_FOLIO = A.FOLIO AND B.ORDP_LINEA = '" + Linea  + "' AND B.ORDP_TURNO = '" + Turno  + "' " +
			//                "ORDER BY B.ORDP_LINEA, B.ORDP_TURNO,A.HORA ";
			string Cadena = "SELECT * FROM tb_det_split A JOIN tb_Respon_Split B ON A.nom_capsplit = B.nom_capsplit AND B.nom_capsplit NOT LIKE '%SUPERVISOR%' AND B.nom_capsplit NOT LIKE '%SISTEMAS%' and B.nom_capsplit NOT LIKE '%JOSE DELGADO%' WHERE A.estatus != 'C' AND A.FECHA like '" + Fechaini.ToShortDateString() + "%' OR A.FECHA like '" + Fechafin.ToShortDateString() + "%'";
			DataSet ds = new DataSet();
			SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
			da.Fill(ds, "Info");
			Info = ds.Tables["INFO"];
			thisConnecion.Close();


			DateTime fec1 = Convert.ToDateTime(Fechaini.ToShortDateString() + " 04:00");
			DateTime fec2 = Convert.ToDateTime(Fechaini.AddDays(1).ToShortDateString() + " 04:00");


			int armador = 0;
			int TotCaj = 0;
			progressBar2.Maximum = Info.Rows.Count;
			int filasTotales = DGDatos.Rows.Count;
			foreach (DataRow Row in Nombre.Rows)
			{
				//progressBar2.PerformStep();
				int hora = 4;
				decimal totalarmador = 0;
				decimal totalcajascanceladas = 0;
				decimal totalcajasverde = 0;
				decimal totalcajasblancas = 0;
				int totalcajasarmador = 0;
				armador = armador + 1;
				int fila = 1;
				foreach (DataRow row in Info.Select("NOM_CAPSPLIT = '" + Row["NOM_CAPSPLIT"].ToString().Trim() + "'"))
				{


					progressBar2.PerformStep();
					string fechacomp = row["FECHA"].ToString().Trim();
					DateTime fechacompara = DateTime.Now;

					try
					{
						fechacompara = Convert.ToDateTime(fechacomp);
					}
					catch
					{
						fechacompara = Convert.ToDateTime(fechacomp.Replace("a.m.", "a. m.").Replace("p.m.", "p. m."));
					}
					fechacomp = fechacompara.ToString("dd/MM/yyyy HH:mm");
					if ((fechacompara >= fec1) && (fechacompara <= fec2))
					{

						decimal totalcajas = 0;
						int calfila = Convert.ToInt32(fechacomp.Substring(11, 2));
						int filareal = 0;
						switch (calfila)
						{

							case 1:
								calfila = 0;
								filareal = 26;
								break;
							case 2:
								calfila = 1;
								filareal = 27;
								break;
							case 3:
								calfila = 2;
								filareal = 28;
								break;
							case 4:
								calfila = 3;
								filareal = 5;
								break;
							case 5:
								calfila = 4;
								filareal = 6;
								break;
							case 6:
								calfila = 5;
								filareal = 7;
								break;
							case 7:
								calfila = 6;
								filareal = 8;
								break;
							case 8:
								calfila = 7;
								filareal = 9;
								break;
							case 9:
								calfila = 8;
								filareal = 10;
								break;
							case 10:
								calfila = 9;
								filareal = 11;
								break;
							case 11:
								calfila = 10;
								filareal = 12;
								break;
							case 12:
								calfila = 11;
								filareal = 13;
								break;
							case 13:
								calfila = 12;
								filareal = 14;
								break;
							case 14:
								calfila = 13;
								filareal = 15;
								break;
							case 15:
								calfila = 14;
								filareal = 16;
								break;
							case 16:
								calfila = 15;
								filareal = 17;
								break;
							case 17:
								calfila = 16;
								filareal = 18;
								break;
							case 18:
								calfila = 17;
								filareal = 19;
								break;
							case 19:
								calfila = 18;
								filareal = 20;
								break;
							case 20:
								calfila = 19;
								filareal = 21;
								break;
							case 21:
								calfila = 20;
								filareal = 22;
								break;
							case 22:
								calfila = 21;
								filareal = 23;
								break;
							case 23:
								calfila = 22;
								filareal = 24;
								break;
							default:
								calfila = 23;
								filareal = 25;
								break;
						}
						string cantidad = DGDatos.Rows[filareal].Cells[armador].Value.ToString();
						decimal Valor = (cantidad.Trim() == "") ? 0 : Convert.ToDecimal(cantidad);
						totalcajas = totalcajas + Convert.ToInt32(row["cajas"].ToString().Trim());

						DGDatos.Rows[filareal].Cells[armador].Value = string.Format("{0:n2}", (Math.Truncate((Valor + (totalcajas / 56)) * 100) / 100));
						totalcajasarmador = totalcajasarmador + Convert.ToInt32(totalcajas);
						totalarmador = totalarmador + totalcajas / 56;
						//progressBar2.Value = (rowIndex * 100) / filasTotales;
						
					}
					
				}

				DGDatos.Rows[0].Cells[armador].Value = Math.Round(totalarmador, 0);
				DGDatos.Rows[1].Cells[armador].Value = totalcajasarmador;



				thisConnecion.Open();
				string cadena = "SELECT COUNT(Tb_Det_Etiqueta.Fecha) FROM Tb_Det_Etiqueta INNER JOIN tb_det_split AS B ON Tb_Det_Etiqueta.emb_folio = B.emb_folio  AND Tb_Det_Etiqueta.Eti_Producto = B.prod_clave AND Tb_Det_Etiqueta.Eti_Recibo = no_lote AND Tb_Det_Etiqueta.Eti_TarIni = CAST(TARINI AS INT) AND Tb_Det_Etiqueta.split = tarima  WHERE (Tb_Det_Etiqueta.Estatus = 'C') AND (B.NOM_CAPSPLIT = '" + Row["NOM_CAPSPLIT"].ToString().Trim() + "') AND (Tb_Det_Etiqueta.fecha_cap >= '" + fec1.ToString().Replace("a. m.", "") + "') AND (Tb_Det_Etiqueta.fecha_cap <= '" + fec2.ToString().Replace("a. m.", "") + "')";
				SqlCommand cmd;
				cmd = new SqlCommand(cadena);
				cmd.Connection = thisConnecion;
				int total_cancelado = Convert.ToInt32(cmd.ExecuteScalar());
				thisConnecion.Close();
				DGDatos.Rows[2].Cells[armador].Value = total_cancelado;

				thisConnecion.Open();
				cadena = "SELECT COUNT(Tb_Det_Etiqueta.Fecha) FROM Tb_Det_Etiqueta INNER JOIN tb_det_split AS B ON Tb_Det_Etiqueta.emb_folio = B.emb_folio  AND Tb_Det_Etiqueta.Eti_Producto = B.prod_clave AND Tb_Det_Etiqueta.Eti_Recibo = no_lote AND Tb_Det_Etiqueta.Eti_TarIni = CAST(TARINI AS INT) AND Tb_Det_Etiqueta.split = tarima  WHERE (Tb_Det_Etiqueta.Tipo_Captura = 'B') AND (B.NOM_CAPSPLIT = '" + Row["NOM_CAPSPLIT"].ToString().Trim() + "') AND (Tb_Det_Etiqueta.fecha_cap >= '" + fec1.ToString().Replace("a. m.", "") + "') AND (Tb_Det_Etiqueta.fecha_cap <= '" + fec2.ToString().Replace("a. m.", "") + "')";
				cmd = new SqlCommand(cadena);
				cmd.Connection = thisConnecion;
				int total_verde = Convert.ToInt32(cmd.ExecuteScalar());
				thisConnecion.Close();
				DGDatos.Rows[3].Cells[armador].Value = total_verde;

				thisConnecion.Open();
				cadena = "SELECT COUNT(Tb_Det_Etiqueta.Fecha) FROM Tb_Det_Etiqueta INNER JOIN tb_det_split AS B ON Tb_Det_Etiqueta.emb_folio = B.emb_folio  AND Tb_Det_Etiqueta.Eti_Producto = B.prod_clave AND Tb_Det_Etiqueta.Eti_Recibo = no_lote AND Tb_Det_Etiqueta.Eti_TarIni = CAST(TARINI AS INT) AND Tb_Det_Etiqueta.split = tarima  WHERE (Tb_Det_Etiqueta.Tipo_Captura = 'V') AND (B.NOM_CAPSPLIT = '" + Row["NOM_CAPSPLIT"].ToString().Trim() + "') AND (Tb_Det_Etiqueta.fecha_cap >= '" + fec1.ToString().Replace("a. m.", "") + "') AND (Tb_Det_Etiqueta.fecha_cap <= '" + fec2.ToString().Replace("a. m.", "") + "')";
				cmd = new SqlCommand(cadena);
				cmd.Connection = thisConnecion;
				int total_blanca = Convert.ToInt32(cmd.ExecuteScalar());
				thisConnecion.Close();
				DGDatos.Rows[4].Cells[armador].Value = total_blanca;

				//progressBar2.Value = (rowIndex * 100) / filasTotales;
				

			}



			/*
             
             
             
             
             
             
             
             
             
             */
			//DGDatos.DataSource = Datos;
			//ColumTotal();

			label1.Text = "Total de Cajas del día " + Fechaini.ToLongDateString() + "    " + TotCaj.ToString("###,###");
			progressBar2.PerformStep();
			FormatoSalida();
		}


		private void ColumTotal()
		{

			int fila = 1;
			foreach (DataGridViewRow row in DGDatos.Rows)
			{
				fila = fila + 1;
				decimal total = 0;


				for (int i = 1; i < DGDatos.ColumnCount - 1; i++)
				{
					decimal a = Convert.ToDecimal(row.Cells[i].Value);
					total = total + Convert.ToDecimal(row.Cells[i].Value);
				}
				DGDatos.Rows[fila - 1].Cells[DGDatos.ColumnCount - 1].Value = total;
			}
		}

		private void FormatoSalida()
		{
			//DGDatos.DefaultCellStyle.WrapMode= DataGridViewTriState.True;
			DGDatos.Columns[0].HeaderText = "H O R A ";
			//DGDatos.Columns[0].Width = 74;
			DGDatos.Columns[0].Frozen = true;
			int nCol = Datos.Rows.Count;
			int i = 1;
			DGDatos.DefaultCellStyle.BackColor = Color.Black;
			DGDatos.DefaultCellStyle.ForeColor = Color.White;
			DGDatos.DefaultCellStyle.Font = new Font(DGDatos.DefaultCellStyle.Font, FontStyle.Bold);
			if (System.Environment.MachineName == "MASTER1" || System.Environment.MachineName == "SISTEMAS1")
			{
				DGDatos.DefaultCellStyle.Font = new Font("Tahoma", 11, FontStyle.Bold);
				DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
				columnHeaderStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
				DGDatos.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
			}
			else
			{
				DGDatos.DefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
				DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
				columnHeaderStyle.Font = new Font("Tahoma", 7, FontStyle.Bold);
				DGDatos.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
			}

			foreach (DataGridViewRow row in DGDatos.Rows)
			{
				if (row.Cells[0].Value.ToString() == "TOTAL")
				{
					row.DefaultCellStyle.BackColor = Color.Yellow;
					row.DefaultCellStyle.ForeColor = Color.Black;
				}
				if (row.Cells[0].Value.ToString() == "CAJAS")
				{
					row.DefaultCellStyle.BackColor = System.Drawing.Color.Cyan;
					row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
				}
				if (row.Cells[0].Value.ToString() == "CANCELADAS")
				{
					row.DefaultCellStyle.BackColor = System.Drawing.Color.Red;
					row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
				}
				if (row.Cells[0].Value.ToString() == "BLANCAS")
				{
					row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
					row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
				}
				if (row.Cells[0].Value.ToString() == "VERDES")
				{
					row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
					row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
				}
			}
			if (DGDatos.Rows.Count > 0)
			{
				int pos = Convert.ToInt32(System.DateTime.Now.ToString("HH:mm:ss").Substring(0, 2));
				int index = 0;
				if (pos >= 7)
					index = pos - 7 + 2;
				else
					index = 19 + pos;
				DGDatos.CurrentCell = DGDatos.Rows[index].Cells[1];
				
			}
			
			lblGenerandoReporte.Visible = false;
			progressBar2.Visible = false;
			progressBar2.Value = 0;
			progressBar2.Maximum = 0;
		}

		private void DGDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			try
			{
				if (e.RowIndex == 0)
				{
					e.CellStyle.BackColor = Color.Yellow;
					e.CellStyle.ForeColor = Color.Black;
					e.FormattingApplied = true;
				}
				if (e.RowIndex == 1)
				{
					e.CellStyle.BackColor = Color.Cyan;
					e.CellStyle.ForeColor = Color.Black;
					e.FormattingApplied = true;
				}
				if (e.RowIndex == 2)
				{
					e.CellStyle.BackColor = Color.Red;
					e.CellStyle.ForeColor = Color.Black;
					e.FormattingApplied = true;
				}
				if (e.RowIndex == 3)
				{
					e.CellStyle.BackColor = Color.White;
					e.CellStyle.ForeColor = Color.Black;
					e.FormattingApplied = true;
				}
				if (e.RowIndex == 4)
				{
					e.CellStyle.BackColor = Color.LightGreen;
					e.CellStyle.ForeColor = Color.Black;
					e.FormattingApplied = true;
				}
			}
			catch
			{
			}


		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			label2.Text = System.DateTime.Now.ToString("HH:mm:ss");
		}

		private void BtnAcep_Click(object sender, EventArgs e)
		{
			Todo();
		}

		private void DGDatos_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{

		}

		private void MonitorSplit_Load(object sender, EventArgs e)
		{

		}

		private void DGDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			string horaactual = "";
			horaactual = DGDatos.CurrentRow.Cells["HORA"].Value.ToString();
			string nombrecolumna = DGDatos.Columns[e.ColumnIndex].Name.ToString();
			FrmDetalleSplitxHora form = new FrmDetalleSplitxHora(horaactual, nombrecolumna, Fechaini, Fechafin);
			form.Show();
		}

		private void DGDatos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{

		}
	}

}

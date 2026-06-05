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
    public partial class ConsDetSplit : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable Datos = new DataTable();
        DataTable Surtido = new DataTable();

        public ConsDetSplit()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
        }

        private void ConsDetSplit_Load(object sender, EventArgs e)
        {
            CreaTable();
            string responsable = "ARMADOR: SIN ASIGNAR";
            string responsableimp = "IMPRESO: SIN IMPRIMIR";
            string Cadena = "";
            thisConnecion.Open();
            Cadena = "select isnull(nom_usu, 'SIN ASIGNAR') AS nombre_usuario From tb_det_acceso_celulares Where folio = '" + Program.MyGlobal.NoPedido.ToString() + "' AND estado = 'A'";
            SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
            try
            {
                responsable = "ARMADOR: " + cmd.ExecuteScalar().ToString();
            }
            catch { 
            
            }

            Cadena = "select TOP (1) isnull(nom_usu, 'SIN ASIGNAR') AS nombre_usuario From tb_det_acceso_celulares Where folio = '" + Program.MyGlobal.NoPedido.ToString() + "' AND estado = 'I' ORDER BY fecha ASC";
            cmd = new SqlCommand(Cadena, thisConnecion);
            try
            {
                responsableimp = "IMPRESO: " + cmd.ExecuteScalar().ToString();
            }
            catch
            {

            }
            
            thisConnecion.Close();

            lblresponsable.Text = responsable;

            respimp.Text = responsableimp;

            LblPed.Text = "PEDIDO " + Program.MyGlobal.NoPedido;
            
            thisConnecion.Open(); //DBGAB
            Cadena = "SELECT * FROM TB_DET_SPLIT WHERE EMB_FOLIO =  '" + Program.MyGlobal.NoPedido.ToString() + "' AND emb_tipo = '" + Program.MyGlobal.TipoPed + "' ORDER BY EMB_FOLIO,TARIMA,FECHA,PROD_CLAVE ";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            ds = new DataSet();
            da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
            da.Fill(ds, "PEDSUR");
            Surtido = ds.Tables["PEDSUR"];
            //SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion; //DBGAB
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            Int32 mreg = 0;
            Int32 CJS = 0;
            Int32  NPROD = 1;
            Int32 ini = 1;
            string A="",B="",C="",D="",E="",TI="",MPROD="",CapSplit="";
            while (Info.Read())
            {
                
                if (ini == 1)
                {
                  A =Info["EMB_FOLIO"].ToString();
                  B =Info["FECHA"].ToString(); // FECHA
                  C =Info["TARIMA"].ToString(); // NOSPLIT
                  D =Info["HORA"].ToString(); // TIEMPO
                  E = "";
                  TI= Info["HORA"].ToString(); //TIEMPO
                  MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                  CapSplit = Info["nom_capsplit"].ToString(); 
                }
                mreg++;
                if(A != Info["EMB_FOLIO"].ToString() || C != Info["TARIMA"].ToString())
                {
                    Datos.Rows.Add(C, D, E, Fn_Tiempo(D,E), CJS, NPROD, CapSplit);
                    A =Info["EMB_FOLIO"].ToString();
                    B =Info["FECHA"].ToString(); // FECHA
                    C =Info["TARIMA"].ToString(); // NOSPLIT
                    D =Info["HORA"].ToString(); // TIEMPO
                    CapSplit = Info["nom_capsplit"].ToString(); 
                    mreg= 1;
                    CJS = 0;
                    MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                    NPROD = 1;
                }
                ini++;
                E=Info["HORA"].ToString(); 
                CJS = CJS + Convert.ToInt32(Info["CAJAS"]); //CAJAS
                if (MPROD != Info["PROD_CLAVE"].ToString()) //CVE_PROD
                {
                  MPROD = Info["PROD_CLAVE"].ToString(); //CVE_PROD
                  NPROD++;
                }
            }
            Datos.Rows.Add(C, D, E, (D.Trim().Length > 0 && E.Trim().Length > 0) ? Fn_Tiempo(D, E) : "", CJS, NPROD, CapSplit);
            DGSplit.DataSource = Datos;
            FormatoSalida();
            //DGDet.DataSource = Surtido;
            thisConnecion.Close(); //DBGAB
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
        }

        private void DGSplit_KeyUp(object sender, KeyEventArgs e)
        {
            DGDet.Rows.Clear();
            foreach (DataRow row in Surtido.Select("Tarima = '" + DGSplit.CurrentRow.Cells["NoSplit"].Value.ToString() + "'"))
                DGDet.Rows.Add(row["nom_prod"].ToString(), row["cajas"].ToString(), row["hora"].ToString());
        }

        private void FormatoSalida()
        {
            DGSplit.Columns[0].Width = 50;
            DGSplit.Columns[1].Width = 70;
            DGSplit.Columns[2].Width = 70;
            DGSplit.Columns[3].Width = 70;
            DGSplit.Columns[4].Width = 60;
            DGSplit.Columns[5].Width = 70;
            DGSplit.Columns[6].Width = 190;
            
            DGSplit.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGSplit.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGSplit.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGSplit.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGDet.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void DGSplit_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DGDet.Rows.Clear();
            foreach (DataRow row in Surtido.Select("Tarima = '" + DGSplit.CurrentRow.Cells["NoSplit"].Value.ToString() + "'"))
                DGDet.Rows.Add(row["nom_prod"].ToString(), row["cajas"].ToString(), row["hora"].ToString());
        }

        public string Fn_Tiempo(string TI, string TF)
        {
            TI = TI.Replace("a. m.", "a.m.");
            TI = TI.Replace("p. m.", "p.m.");
            TF = TF.Replace("a. m.", "a.m.");
            TF = TF.Replace("p. m.", "p.m.");
            if (TI.Trim().Length == 9)
            {
                TI = "0" + TI;
            }

            if (TF.Trim().Length == 9)
            {
                TF = "0" + TF;
            }

            string TTI = TI; //Fn_CONVIERTEHR(TI);
            string TTF = TF; //Fn_CONVIERTEHR(TF);
          Int32  H1 = Convert.ToInt32(TTI.Substring(0,2));
          Int32  M1 = Convert.ToInt32(TTI.Substring(3,2));
          Int32  H2 = Convert.ToInt32(TTF.Substring(0,2)); 
          Int32  M2 = Convert.ToInt32(TTF.Substring(3,2));
          Int32 HT=0,MT=0;
            string CAD="";
          if( M2 < M1)
          {
            if (Math.Abs(H2 - H1) > 1)
                if (H2 < H1)
                  HT =24-H1+H2-1;
                else 
                    HT = H2-H1-1;
           else 
               HT = 0;
            if (M2 < M1)
              MT = 60-M1+M2;
            else
               MT=M2-M1;
          }
          else
            if (H2 < H1)
              HT=24-H1+H2;
            else 
                HT=H2-H1;
            if (M2 < M1)
               MT=60-M1+M2;
            else 
                MT=M2-M1;
          if (HT<10)
            CAD="0"+HT.ToString().Substring(0,1);
          else
              CAD = HT.ToString().Substring(0,2);
          CAD=CAD+':';
          if(MT<10)
            CAD=CAD+'0'+MT.ToString().Substring(0,1);
            else
               CAD=CAD+MT.ToString().Substring(0,2);
          return CAD;
        }

        public string Fn_CONVIERTEHR(string HORA)
        {
            string cad= "";
            string HI = "";
            HORA = HORA.Replace("a. m. ", "a.m.");
            HORA = HORA.Replace("p. m. ", "p.m.");
            if (HORA.Trim().Length < 11)
              HI = HORA.Substring(6,4);
            else
                HI = HORA.Substring(9);
            Int32 H = 0;
            //if (HI.ToUpper() == "P.M.") 
            try
            {
                H = Convert.ToInt32(HORA.Substring(0, 2));
            }
            catch (Exception e) {
                H = Convert.ToInt32(HORA.Substring(0, 1));
            }
            
            if (H != 12)
            {
                if (HI.ToUpper() == "P.M.")
                {
                    H = Convert.ToInt32(HORA.Substring(0, 2)) + 12;
                    cad = H.ToString().Substring(0, 2) + HORA.Substring(2, 3); // +":00";
                }
                else
                    cad = H.ToString().Trim().PadLeft(2, '0') + HORA.Substring(2, 3); // +":00";
            }
            else
            {
                //string H1 = HORA.Substring(0, 2);
                if (H == 12)
                    if (HI.ToUpper() == "P.M.")
                        cad = H + HORA.Substring(2, 3); // + ":00";
                    else
                        cad = "00" + HORA.Substring(2, 3); // +":00";
                
            }
            return cad;
        }

    }
}

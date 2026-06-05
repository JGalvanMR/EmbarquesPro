using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
//using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using Font = System.Drawing.Font;
using Rectangle = System.Drawing.Rectangle;

namespace Embarques
{
    public partial class ConsDistEmb : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        SqlConnection ThisConnecUERP = new SqlConnection(Utilerias.Class1.ConnectionStringUERP);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        DataTable DETALLE = new DataTable();
        DataTable Normal = new DataTable();
        DataTable Split = new DataTable();
        DataTable DetSplit = new DataTable();
        DataTable Trailer = new DataTable();
        DataTable SinSurtir = new DataTable();
        DataTable CorreosEmb = new DataTable();
        public DataTable Embarque = new DataTable();
        public DataTable DetEmb = new DataTable();
        DataTable Ped = new DataTable();
        DataTable Peddet = new DataTable();
        DataTable CodigosMty = new DataTable();
        Form1 Fr1 = new Form1();
        string ConFormat = "";
        string PedidosEmbarque = "";
        int NReg = 0, TotReg = 0;


        public ConsDistEmb()
        {
            InitializeComponent();
        }

        private void ConsDistEmb_Load(object sender, EventArgs e)
        {

            //Fr1.Fn_TraeNomTra();
            LblPlaca.Text = Program.MyGlobal.PubNoTrailer;
            LblCli.Text = Program.MyGlobal.CveCliente;
            LblProv.Text = Fr1.Fn_TraeNomTra(Program.MyGlobal.CveProv);
            LblFact.Text = Program.MyGlobal.PubFact;
            LblPed.Text = Program.MyGlobal.PubPedido;
            thisConnecion.Open();
            string Cadena = "SELECT A.*,C.PROD_NOMBRE,B.HORA_TRAILER, B.emb_obs FROM TB_DET_EMBARQUE A, TB_MSTR_EMBARQUE B, TB_CAT_PRODUCTO C WHERE SUBSTRING(B.HORA_TRAILER,1,10) = '" + Program.MyGlobal.PubFecEmb + "' AND B.NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer +
                            "'AND B.EMB_FOLIO = A.EMB_FOLIO AND A.PROD_CLAVE = C.PROD_CLAVE AND A.ESTATUS != 'C' ORDER BY A.SECCION,C.PROD_NOMBRE";
            DataSet ds1 = new DataSet();
            SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "DETALLE");
            DETALLE = ds1.Tables["DETALLE"];
            Cadena = "SELECT * FROM TB_MSTR_EMBARQUE WHERE SUBSTRING(HORA_TRAILER,1,10) = '" + Program.MyGlobal.PubFecEmb + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
            ds1 = new DataSet();
            da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "EMBARQUE");
            DetEmb = ds1.Tables["EMBARQUE"];
            //DetEmb = ds1.Tables["DETALLE"];
            //DGLotes.DataSourc= ds1.Tables["DETALLE"];e  = DETALLE ;
            Cadena = "SELECT * FROM TB_MSTR_TRAILER WHERE FECHA = '" + Program.MyGlobal.PubFecEmb + "' AND NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
            ds1 = new DataSet();
            da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(ds1, "TRAILER");
            Trailer = ds1.Tables["TRAILER"];
            //DGDatos.DataSource = Trailer;
            Cadena = "Select CNTE_CLAVE,EMAIL_DEST from TB_MSTR_EMAIL where   EMAIL_MOV = 'EMB'";
            //Cadena = "select 'MEX' as CNTE_CLAVE, 'jgalvan@mrlucky.com.mx' as EMAIL_DEST";
            da1 = new SqlDataAdapter(Cadena, thisConnecion);
            da1.Fill(CorreosEmb);
            thisConnecion.Close();
            InfoAnt();
            CreaTablas();
            SeparaInfo();
            //Embarque = Fr1.Embarques.Copy();
            //DetEmb = Fr1.DetEmb.Copy();
            //Form1.Trailer 
            //DgSplit.DataSource = Split;
            //DGInfo.DataSource = Normal;
        }

        private void CreaTablas()
        {
            Normal.Columns.Add("PRODUCTO", typeof(string)); //0
            Normal.Columns.Add("LOTE", typeof(string));     //1
            Normal.Columns.Add("FECHACAD", typeof(string)); //2
            Normal.Columns.Add("TEMP", typeof(string));     //3
            Normal.Columns.Add("CAJAS", typeof(string));    //4
            Normal.Columns.Add("TIPOTAR", typeof(string));  //5
            Normal.Columns.Add("POSICION", typeof(string)); //6
            Split.Columns.Add("POSICION", typeof(string)); //6
            DetSplit.Columns.Add("PRODUCTO", typeof(string)); //0
            DetSplit.Columns.Add("LOTE", typeof(string));     //1
            DetSplit.Columns.Add("FECHACAD", typeof(string)); //2
            DetSplit.Columns.Add("TEMP", typeof(string));     //3
            DetSplit.Columns.Add("CAJAS", typeof(string));    //4
            DetSplit.Columns.Add("POSICION", typeof(string)); //5
            SinSurtir.Columns.Add("Pedido", typeof(string));
            SinSurtir.Columns.Add("Cliente", typeof(string));
            SinSurtir.Columns.Add("Producto", typeof(string));
            SinSurtir.Columns.Add("CantPed", typeof(string));
            SinSurtir.Columns.Add("CantSur", typeof(string));
            SinSurtir.Columns.Add("Faltante", typeof(string));
            SinSurtir.Columns.Add("Responsable", typeof(string));
            SinSurtir.Columns.Add("Causa", typeof(string));
        }

        private void SeparaInfo()
        {
            int j = 0, cont = 1, k = 1, ultimaposicionsplit = 0;
            string Pro = "", Lote = "", FecCad = "", Temp = "", Cjs = "", NomTar = "", Pos = "";
            int Pos1 = 0;
            int sumcajas = 0;
            foreach (DataRow Row in DETALLE.Rows)
            {


                if (j == 0)
                {
                    Pos1 = Convert.ToInt32(Row["SECCION"]);
                    j++;
                    if (Pos1 > 0) // no hubo tarimas al inicio
                    {
                        for (int l = 1; l < Pos1; l++)
                        {
                            Normal.Rows.Add("********************", "", "", "", "", "", l);
                            k++;
                        }
                    }
                }
                else
                {
                    if (Pos1 != k)
                    {
                        sumcajas = 0;
                        Normal.Rows.Add("********************", "", "", "", "", "", k);
                        k++;
                    }
                    if (Pos1 > k) // no hubo tarimas al inicio
                    {
                        sumcajas = 0;
                        for (int l = k; l < Pos1; l++)
                        {
                            Normal.Rows.Add("********************", "", "", "", "", "", l);
                            k++;
                        }
                    }
                    if (Pos1 == Convert.ToInt32(Row["SECCION"]))
                    {
                        if (sumcajas == 0)
                        {
                            //SELECT CONCAT('S P L I T # ', A.seccion) AS prod_nombre, A.temp, SUM(A.cajas) As Cajas, A.emb_folio FROM TB_DET_EMBARQUE A, TB_MSTR_EMBARQUE B, TB_CAT_PRODUCTO C WHERE SUBSTRING(B.HORA_TRAILER,1,10) = '18/11/2020' AND B.NO_TRAILER = '56UF5K     'AND B.EMB_FOLIO = A.EMB_FOLIO AND A.seccion = '9' AND A.PROD_CLAVE = C.PROD_CLAVE AND A.ESTATUS != 'C' Group by A.seccion, A.temp, A.emb_folio
                            ultimaposicionsplit = Pos1;
                            thisConnecion.Open();
                            string query = "SELECT CONCAT('S P L I T # ', A.seccion) AS prod_nombre, SUM(A.cajas) As Cajas FROM TB_DET_EMBARQUE A, TB_MSTR_EMBARQUE B, TB_CAT_PRODUCTO C WHERE SUBSTRING(B.HORA_TRAILER,1,10) = '" + Program.MyGlobal.PubFecEmb + "' AND B.NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer +
                                           "' AND B.EMB_FOLIO = A.EMB_FOLIO AND A.seccion = '" + Pos.Trim() + "' AND A.PROD_CLAVE = C.PROD_CLAVE AND A.ESTATUS != 'C' Group by A.seccion";
                            SqlCommand cmd = new SqlCommand(query);
                            cmd.Connection = thisConnecion;
                            SqlDataReader Info;
                            Info = cmd.ExecuteReader();
                            while (Info.Read())
                            {
                                Normal.Rows.Add(Info["prod_nombre"].ToString(), "", "", Temp, Info["Cajas"].ToString(), NomTar, Pos);
                            }

                            thisConnecion.Close();


                        }
                        if (cont == 1)
                        {
                            Split.Rows.Add(Pos1.ToString());
                            //Normal.Rows.Add("S P L I T # " + Pos.Trim(), "", "", Temp, Cjs, NomTar, Pos);
                        }
                        cont++;
                        sumcajas++;
                    }
                    else
                    {
                        sumcajas = 0;
                        Pos1 = Convert.ToInt32(Row["SECCION"]);
                        if (cont == 1)
                            Normal.Rows.Add(Pro, Lote, FecCad, Temp, Cjs, NomTar, Pos);
                        cont = 1;
                        k++;
                    }

                }
                Pro = Row["prod_nombre"].ToString();
                Lote = Row["NO_LOTE"].ToString();
                FecCad = Row["FEC_CAD"].ToString();
                Temp = Row["TEMP"].ToString();
                Cjs = Row["CAJAS"].ToString();
                Pos = Row["SECCION"].ToString();
                NomTar = DetTarimas(Row["Emb_Folio"].ToString(), Pos);
            }
            if (k == Convert.ToInt32(Pos))
            {
                if (ultimaposicionsplit != Convert.ToInt32(Pos))
                {
                    Normal.Rows.Add(Pro, Lote, FecCad, Temp, Cjs, NomTar, Pos);
                }
            }
            if (k < 30)
            {
                for (j = k + 1; j <= 30; j++)
                    Normal.Rows.Add("********************", "", "", "", "", "", j);
            }
        }

        private void Borrar()
        {
            Rtbox1.Text = "";
            Rtbox2.Text = "";
            Rtbox3.Text = "";
            Rtbox4.Text = "";
            Rtbox5.Text = "";
            Rtbox6.Text = "";
            Rtbox7.Text = "";
            Rtbox8.Text = "";
            Rtbox9.Text = "";
            Rtbox10.Text = "";
            Rtbox11.Text = "";
            Rtbox12.Text = "";
            Rtbox13.Text = "";
            Rtbox14.Text = "";
        }

        private void BtnSig_Click(object sender, EventArgs e)
        {
            Borrar();
            int i = 1;
            for (i = 17; i <= 30; i++)
            {
                switch (i)
                {
                    case 17:
                        Rtbox1.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 18:
                        Rtbox2.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 19:
                        Rtbox3.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 20:
                        Rtbox4.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 21:
                        Rtbox5.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 22:
                        Rtbox6.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 23:
                        Rtbox7.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 24:
                        Rtbox8.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 25:
                        Rtbox9.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 26:
                        Rtbox10.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 27:
                        Rtbox11.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 28:
                        Rtbox12.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 29:
                        Rtbox13.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 30:
                        Rtbox14.Text = i.ToString() + System.Environment.NewLine;
                        break;
                }
            }
            string Info = "", Cjs = "", Lote = "";
            Rtbox15.Visible = false; Rtbox16.Visible = false;
            foreach (DataRow Row in DETALLE.Rows)
            {
                i = Convert.ToInt32(Row["Seccion"]);
                Info = Row["PROD_NOMBRE"].ToString();
                Cjs = Row["Cajas"].ToString();
                Lote = Row["No_lote"].ToString();
                switch (i)
                {
                    case 17:
                        Rtbox1.Text = Rtbox1.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 18:
                        Rtbox2.Text = Rtbox2.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 19:
                        Rtbox3.Text = Rtbox3.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 20:
                        Rtbox4.Text = Rtbox4.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 21:
                        Rtbox5.Text = Rtbox5.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 22:
                        Rtbox6.Text = Rtbox6.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 23:
                        Rtbox7.Text = Rtbox7.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 24:
                        Rtbox8.Text = Rtbox8.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 25:
                        Rtbox9.Text = Rtbox9.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 26:
                        Rtbox10.Text = Rtbox10.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 27:
                        Rtbox11.Text = Rtbox11.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 28:
                        Rtbox12.Text = Rtbox12.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 29:
                        Rtbox13.Text = Rtbox13.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 30:
                        Rtbox14.Text = Rtbox14.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                }
            }
        }

        private void BtnAnt_Click(object sender, EventArgs e)
        {
            InfoAnt();
        }

        private void InfoAnt()
        {
            Borrar();
            int i = 1;
            for (i = 1; i <= 16; i++)
            {
                switch (i)
                {
                    case 1:
                        Rtbox1.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 2:
                        Rtbox2.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 3:
                        Rtbox3.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 4:
                        Rtbox4.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 5:
                        Rtbox5.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 6:
                        Rtbox6.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 7:
                        Rtbox7.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 8:
                        Rtbox8.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 9:
                        Rtbox9.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 10:
                        Rtbox10.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 11:
                        Rtbox11.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 12:
                        Rtbox12.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 13:
                        Rtbox13.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 14:
                        Rtbox14.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 15:
                        Rtbox15.Text = i.ToString() + System.Environment.NewLine;
                        break;
                    case 16:
                        Rtbox16.Text = i.ToString() + System.Environment.NewLine;
                        break;
                }
            }
            string Info = "", Cjs = "", Lote = "";
            Rtbox15.Visible = true; Rtbox16.Visible = true;
            foreach (DataRow Row in DETALLE.Rows)
            {
                i = Convert.ToInt32(Row["Seccion"]);
                Info = Row["PROD_NOMBRE"].ToString();
                Cjs = Row["Cajas"].ToString();
                Lote = Row["No_lote"].ToString();
                switch (i)
                {
                    case 1:
                        Rtbox1.Text = Rtbox1.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 2:
                        Rtbox2.Text = Rtbox2.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 3:
                        Rtbox3.Text = Rtbox3.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 4:
                        Rtbox4.Text = Rtbox4.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 5:
                        Rtbox5.Text = Rtbox5.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 6:
                        Rtbox6.Text = Rtbox6.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 7:
                        Rtbox7.Text = Rtbox7.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 8:
                        Rtbox8.Text = Rtbox8.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 9:
                        Rtbox9.Text = Rtbox9.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 10:
                        Rtbox10.Text = Rtbox10.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 11:
                        Rtbox11.Text = Rtbox11.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 12:
                        Rtbox12.Text = Rtbox12.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 13:
                        Rtbox13.Text = Rtbox13.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 14:
                        Rtbox14.Text = Rtbox14.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 15:
                        Rtbox15.Text = Rtbox15.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                    case 16:
                        Rtbox16.Text = Rtbox16.Text + Info + " Cjs : " + Cjs + " Lote: " + Lote + System.Environment.NewLine;
                        break;
                }
            }
        }

        private void PrintCForm_PrintPage(object sender, PrintPageEventArgs e)
        {
            int Y = 98; //ANTES ERA 9O SE CAMBIO EL FORMATO 18 ABR 2016 RCC
            if (ConFormat == "S")
            {
                if (!File.Exists(@"C:\SisGabWeb\CondEmb.jpg"))
                    File.Copy(@"\\gabira1\SisGabWeb\CondEmb.jpg", @"C:\SisGabWeb\CondEmb.jpg");
                Image newImage = Image.FromFile(@"C:\SisGabWeb\CondEmb.jpg"); //codigo para mandar llamar la imagen
                e.Graphics.DrawImage(newImage, 0, 0); //codigo para imprimir la imagen (objeto (imagen), coordenada en X, coordenada en Y)
                Y = 120;
            }

            Font drawFont = new Font("Courier New", 7);// PROD,CAJAS
            Font drawFont2 = new Font("Courier New", 6);// LOTE Y FECHA CAD
            Font drawFont3 = new Font("Courier New", 10);// TEMP, DATOS GENERALES
            Font drawFont4 = new Font("Times New Roman", 4);// TEMP, DATOS GENERALES
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            string A = "", AA = "";
            string Tar29 = "N", Tar30 = "";
            foreach (DataRow Row in Normal.Rows)
            {
                int pos = Convert.ToInt32(Row["POSICION"]);
                if (pos == 29)
                {
                    if (!Row["Producto"].ToString().Contains("*****"))
                    {
                        Tar29 = "S";
                        continue;
                    }
                    else
                    {
                        break;
                    }

                }
                if (pos == 30)
                {
                    if (!Row["Producto"].ToString().Contains("*****"))
                    {
                        Tar30 = "S";
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                int tam = Row["PRODUCTO"].ToString().Trim().Length;
                if (tam > 17)
                {
                    A = Row["PRODUCTO"].ToString().Trim().Substring(0, 17);
                    AA = Row["PRODUCTO"].ToString().Trim().Substring(17, tam - 17);
                }
                else
                {
                    A = Row["PRODUCTO"].ToString();
                    AA = "";
                }
                string B = Row["LOTE"].ToString();
                string C = Row["FECHACAD"].ToString();
                string D = Row["TEMP"].ToString();
                string E = Row["CAJAS"].ToString().Trim() + " cjs";
                int tam1 = Row["TIPOTAR"].ToString().Trim().Length;
                string F = Row["TIPOTAR"].ToString();
                string FF = "";
                if (tam1 > 17)
                {
                    F = Row["TIPOTAR"].ToString().Trim().Substring(0, 17);
                    FF = Row["TIPOTAR"].ToString().Trim().Substring(17, tam1 - 17);
                }
                if ((pos % 2) != 0)
                {
                    if (ConFormat == "S")
                    {
                        PointF Pprod = new PointF(65.0F, Y);//Producto
                        PointF Pprod2 = new PointF(65.0F, Y + 10);//Nom producto parte2
                        PointF PLot = new PointF(60.0F, Y + 30);//No de Lote
                        PointF PFecad = new PointF(60.0F, Y + 40);//Fecha Cad
                        PointF PTem = new PointF(211.0F, Y);//temp
                        PointF PCjs = new PointF(175.0F, Y + 20);//Cajas
                        PointF PTar = new PointF(175.0F, Y + 45);//Nom Tarima
                        PointF PTar2 = new PointF(175.0F, Y + 50);//Nom Tarima parte 2
                        e.Graphics.DrawString(A, drawFont, drawBrush, Pprod);
                        e.Graphics.DrawString(AA, drawFont, drawBrush, Pprod2);
                        e.Graphics.DrawString(B, drawFont2, drawBrush, PLot);
                        e.Graphics.DrawString(C, drawFont2, drawBrush, PFecad);
                        e.Graphics.DrawString(D, drawFont3, drawBrush, PTem);
                        e.Graphics.DrawString(E, drawFont, drawBrush, PCjs);
                        e.Graphics.DrawString(F, drawFont4, drawBrush, PTar);
                        e.Graphics.DrawString(FF, drawFont4, drawBrush, PTar2);
                    }
                    else
                    {
                        PointF Pprod3 = new PointF(55.0F, Y);//Producto
                        PointF Pprod4 = new PointF(55.0F, Y + 10);//Nom producto parte2
                        PointF PLot1 = new PointF(50.0F, Y + 30);//No de Lote
                        PointF PFecad1 = new PointF(50.0F, Y + 40);//Fecha Cad
                        PointF PTem1 = new PointF(201.0F, Y);//temp
                        PointF PCjs1 = new PointF(165.0F, Y + 20);//Cajas
                        PointF PTar3 = new PointF(165.0F, Y + 45);//Nom Tarima
                        PointF PTar4 = new PointF(165.0F, Y + 50);//Nom Tarima parte 2
                        e.Graphics.DrawString(A, drawFont, drawBrush, Pprod3);
                        e.Graphics.DrawString(AA, drawFont, drawBrush, Pprod4);
                        e.Graphics.DrawString(B, drawFont2, drawBrush, PLot1);
                        e.Graphics.DrawString(C, drawFont2, drawBrush, PFecad1);
                        e.Graphics.DrawString(D, drawFont3, drawBrush, PTem1);
                        e.Graphics.DrawString(E, drawFont, drawBrush, PCjs1);
                        e.Graphics.DrawString(F, drawFont4, drawBrush, PTar3);
                        e.Graphics.DrawString(FF, drawFont4, drawBrush, PTar4);
                    }

                }
                else
                {
                    PointF Pprod = new PointF((ConFormat == "S") ? 250 : 240, Y);//Producto
                    PointF Pprod2 = new PointF((ConFormat == "S") ? 250 : 240, Y + 10);//Nom Producto Parte 2
                    PointF PLot = new PointF((ConFormat == "S") ? 250 : 240, Y + 30);//No de Lote
                    PointF PFecad = new PointF((ConFormat == "S") ? 250 : 240, Y + 40);//Fecha Cad
                    PointF PTem = new PointF((ConFormat == "S") ? 396 : 386, Y);//Temp
                    PointF PCjs = new PointF((ConFormat == "S") ? 355 : 345, Y + 20);//Cajas
                    PointF PTar = new PointF((ConFormat == "S") ? 355 : 345, Y + 45);//Nom Tarima
                    PointF PTar2 = new PointF((ConFormat == "S") ? 355 : 345, Y + 50);//Nom Tarima Parte 2
                    e.Graphics.DrawString(A, drawFont, drawBrush, Pprod);
                    e.Graphics.DrawString(AA, drawFont, drawBrush, Pprod2);
                    e.Graphics.DrawString(B, drawFont2, drawBrush, PLot);
                    e.Graphics.DrawString(C, drawFont2, drawBrush, PFecad);
                    e.Graphics.DrawString(D, drawFont3, drawBrush, PTem);
                    e.Graphics.DrawString(E, drawFont, drawBrush, PCjs);
                    e.Graphics.DrawString(F, drawFont4, drawBrush, PTar);
                    e.Graphics.DrawString(FF, drawFont4, drawBrush, PTar2);
                    if (pos > 1 && pos < 21)
                        Y = Y + 59;
                    else
                        Y = Y + 70;
                    if (pos == 8 || pos == 10 || pos == 12 || pos == 14 || pos == 16 || pos == 18 || pos == 26)
                        Y = Y - 5;
                }
            }
            if (ConFormat == "S")
                Y = 120;
            else
                Y = 98; //ANTES ERA 9O SE CAMBIO EL FORMATO 18 ABR 2016 RCC
            foreach (DataRow Row1 in Trailer.Rows)
            {
                PointF Dato = new PointF(480, Y - 10);//Fecha
                e.Graphics.DrawString(Program.MyGlobal.PubFecEmb, drawFont3, drawBrush, Dato);
                Dato = new PointF(650, Y - 10);//Factura
                e.Graphics.DrawString(Program.MyGlobal.PubFact, drawFont3, drawBrush, Dato);
                Dato = new PointF(480, Y + 20);//TURNO 140
                string Mtur = (Row1["Turno"].ToString() == "1") ? "VESPERTINO" : "MATUTINO";
                e.Graphics.DrawString(Mtur, drawFont3, drawBrush, Dato);
                Dato = new PointF(650, Y + 20);//PEDIDO 140
                e.Graphics.DrawString(Program.MyGlobal.PubPedido, drawFont3, drawBrush, Dato);
                Dato = new PointF(530, Y + 40);//TRANSPORTISTA
                e.Graphics.DrawString(LblProv.Text, drawFont3, drawBrush, Dato);
                Dato = new PointF(530, Y + 55);//DESTINO
                e.Graphics.DrawString(Row1["DESTINO"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(550, Y + 75);//PLACA
                e.Graphics.DrawString(Program.MyGlobal.PubNoTrailer, drawFont3, drawBrush, Dato);
                Dato = new PointF(700, Y + 95);//TEMP INI
                e.Graphics.DrawString(Row1["TEMPINI"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(700, Y + 113);//TEMP FIN
                e.Graphics.DrawString(Row1["TEMPFIN"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(600, Y + 133);//HORA INI
                e.Graphics.DrawString(Row1["HORAINI"].ToString().Substring(10, 13), drawFont3, drawBrush, Dato);
                Dato = new PointF(600, Y + 150);//HORA FIN
                e.Graphics.DrawString(Row1["HORAFIN"].ToString().Substring(10, 13), drawFont3, drawBrush, Dato);
                // detalle de la inspeccion puntos 1 al 10
                Dato = new PointF(450, Y + 390);//HORA FIN
                e.Graphics.DrawString("Temp. Set Point: " + Row1["TEMPSETPOINT"].ToString().Trim() + "  °F", drawFont3, drawBrush, Dato);
                int j = 15;
                if (ConFormat == "S")
                    j = 0;
                Dato = new PointF(710 - j, Y + 425);//LARGO
                string X = (Row1["CONCEPTO1"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 440);//LARGO
                X = (Row1["CONCEPTO2"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 455);//LARGO
                X = (Row1["CONCEPTO3"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 470);//LARGO
                X = (Row1["CONCEPTO4"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 495);//LARGO
                X = (Row1["CONCEPTO5A"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 510);//LARGO
                X = (Row1["CONCEPTO5B"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 525);//LARGO
                X = (Row1["CONCEPTO5C"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 535);//LARGO
                X = (Row1["CONCEPTO5D"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 550);//LARGO
                X = (Row1["CONCEPTO5E"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 563);//LARGO
                X = (Row1["CONCEPTO5F"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 575);//LARGO
                X = (Row1["CONCEPTO5G"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 590);//LARGO
                X = (Row1["CONCEPTO5H"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 600);//LARGO
                X = (Row1["CONCEPTO5I"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 615);//LARGO
                X = (Row1["CONCEPTO5J"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 630);//LARGO
                X = (Row1["CONCEPTO5K"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 640);//LARGO
                X = (Row1["CONCEPTO5L"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 655);//LARGO
                X = (Row1["CONCEPTO6"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 670);//LARGO
                X = (Row1["CONCEPTO7"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 685);//LARGO
                X = (Row1["CONCEPTO8"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 700);//LARGO
                X = (Row1["CONCEPTO9"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);
                Dato = new PointF(710 - j, Y + 715);//LARGO
                X = (Row1["CONCEPTO10"].ToString().Trim() == "BIEN") ? "X" : "      X";
                e.Graphics.DrawString(X, drawFont3, drawBrush, Dato);

                // TERMINA EL DETALLE DE LA INSPECCION
                Dato = new PointF(550, Y + 735);//LARGO
                e.Graphics.DrawString(Row1["LARGO"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(750, Y + 735);//GATAS
                e.Graphics.DrawString(Row1["GATAS"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y + 755);//RYAN1
                e.Graphics.DrawString(Row1["RYAN1"].ToString().Trim() + "(" + Row1["POSRYAN1"].ToString().Trim() + ")", drawFont3, drawBrush, Dato);
                Dato = new PointF(680, Y + 755);//RYAN2
                e.Graphics.DrawString(Row1["RYAN2"].ToString().Trim() + "(" + Row1["POSRYAN2"].ToString().Trim() + ")", drawFont3, drawBrush, Dato);
                Dato = new PointF(450, Y + 800);//CHOFER
                e.Graphics.DrawString(Row1["CHOFER"].ToString().Trim(), drawFont, drawBrush, Dato);
                Dato = new PointF(630, Y + 800);//SUPERVISOR
                e.Graphics.DrawString(Row1["RESPONSABLE"].ToString().Trim(), drawFont, drawBrush, Dato);
                Pen pen1 = new Pen(Color.Black);
                if (Tar29 == "S")
                {
                    Rectangle rec = new Rectangle(80, Y + 810, 20, 15);
                    e.Graphics.DrawRectangle(pen1, rec);
                    Dato = new PointF(60, Y + 810);//SUPERVISOR
                    e.Graphics.DrawString("29", drawFont3, drawBrush, Dato);
                    Dato = new PointF(82, Y + 810);//SUPERVISOR
                    e.Graphics.DrawString("X", drawFont3, drawBrush, Dato);
                }
                if (Tar30 == "S")
                {
                    Rectangle rec = new Rectangle(270, Y + 810, 20, 15);
                    e.Graphics.DrawRectangle(pen1, rec);
                    Dato = new PointF(250, Y + 810);//SUPERVISOR
                    e.Graphics.DrawString("30", drawFont3, drawBrush, Dato);
                    Dato = new PointF(272, Y + 810);//SUPERVISOR
                    e.Graphics.DrawString("X", drawFont3, drawBrush, Dato);
                }

                //TRAER TODAS LAS ORDENES INVOLUCRADAS
                thisConnecion.Open();
                string query = "SELECT distinct(B.emb_folio) FROM TB_MSTR_EMBARQUE B WHERE SUBSTRING(B.HORA_TRAILER,1,10) = '" + Program.MyGlobal.PubFecEmb + "' AND B.NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Connection = thisConnecion;
                SqlDataReader Info;
                Info = cmd.ExecuteReader();
                string ORDENES = "";
                while (Info.Read())
                {
                    ORDENES = ORDENES + Info["EMB_FOLIO"].ToString() + " - ";
                    query = "SELECT FCN_FOLIO FROM tb_mstr_facturas_nal WHERE (PDN_folio = '" + Convert.ToInt32(Info["EMB_FOLIO"].ToString()) + "')";
                    cmd = new SqlCommand(query, thisConnecion);
                    try
                    {
                        ORDENES = ORDENES + cmd.ExecuteScalar().ToString() + ", ";
                    }
                    catch
                    {
                        ORDENES = ORDENES + "S/F Asig" + ", ";
                    }

                }
                thisConnecion.Close();

                drawFont = new Font("Courier New", 5);// PROD,CAJAS
                Dato = new PointF(60, Y + 900);//Ordenes
                e.Graphics.DrawString("CORRESPONDE A LAS ORDENES Y FACTURAS: " + ORDENES.TrimEnd(','), drawFont, drawBrush, Dato);
            }

        }

        private void BtnImpForm_Click(object sender, EventArgs e)
        {
            ConFormat = "S";
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.PrintCForm_PrintPage);
            //pd.Print();
            // para mandarlo a vista previa el reporte
            PrintPreviewDialog Report = new PrintPreviewDialog();
            Report.Document = pd;
            //DirectCast((Report.Controls(1)), ToolStrip).Items(0).Enabled = False
            ((ToolStripButton)((ToolStrip)Report.Controls[1]).Items[0]).Enabled = false;
            Report.ShowDialog();
            LlenaSplit();
            if (DetSplit.Rows.Count > 0)
            {
                TotReg = DetSplit.Rows.Count;
                NReg = 0;
                PrintDocument pdSplit = new PrintDocument();
                pdSplit.PrintPage += new PrintPageEventHandler(this.PrintSplit_PrintPage);
                //pd.Print();
                // para mandarlo a vista previa el reporte
                PrintPreviewDialog ReportSplit = new PrintPreviewDialog();
                ReportSplit.Document = pdSplit;
                ((ToolStripButton)((ToolStrip)ReportSplit.Controls[1]).Items[0]).Enabled = false;
                ReportSplit.ShowDialog();
                //NReg = 0;
                //PrintDocument pdSplit1 = new PrintDocument();
                //pdSplit1.PrintPage += new PrintPageEventHandler(this.PrintSplit_PrintPage);
                //pdSplit1.Print();
                //pdSplit1.Dispose();
            }
        }

        public string DetTarimas(string MFOL, string Pos)
        {
            thisConnecion.Open(); //DBGAB
            string cad = " SELECT a.seccion, a.id_tarima, a.Emb_Folio, b.nom_tarima " +
                          " FROM Tb_Det_Embarque A, Tb_Cat_Tarima B " +
                          " WHERE A.seccion = '" + Pos + "' AND A.Id_Tarima = B.Id_Tarima and a.emb_FOLIO = '" + MFOL + "'" +
                          " GROUP BY a.Id_tarima, a.seccion, b.Nom_Tarima,a.Emb_Folio ";
            SqlCommand cmd;
            cmd = new SqlCommand(cad);
            cmd.Connection = thisConnecion; //DBGAB
            SqlDataReader Info;
            Info = cmd.ExecuteReader();
            string datos = "";
            while (Info.Read())
            {
                if (datos == "")
                {
                    datos = Info["nom_tarima"].ToString();
                    break;
                }
            }
            thisConnecion.Close(); //DBGAB
            return datos;

        }

        /*public string Fn_TraeNomTra(string var_trans)
        {
            thisConnecion.Open();
            string cad = "";
            string Cadena = "SELECT prov_nombre FROM tb_cat_proveedor WHERE PROV_clave = '" + var_trans + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            cad = Convert.ToString(cmd.ExecuteScalar());
            thisConnecion.Close();
            return cad;
        }
        */

        private void BtnImp_Click(object sender, EventArgs e)
        {
            ConFormat = " ";
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.PrintCForm_PrintPage);
            //pd.Print();
            // para mandarlo a vista previa el reporte
            PrintPreviewDialog Report = new PrintPreviewDialog();
            Report.Document = pd;
            Report.ShowDialog();
            LlenaSplit();
            TotReg = DetSplit.Rows.Count;
            NReg = 0;
            PrintDocument pdSplit = new PrintDocument();
            pdSplit.PrintPage += new PrintPageEventHandler(this.PrintSplit_PrintPage);
            pdSplit.Print();
            pdSplit.Dispose();
            // para mandarlo a vista previa el reporte
            //PrintPreviewDialog ReportSplit = new PrintPreviewDialog();
            //ReportSplit.Document = pdSplit;
            //ReportSplit.ShowDialog();

            NReg = 0;
        }

        private void PrintSplit_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font drawFont = new Font("Courier New", 7);// PROD,CAJAS
            Font drawFont2 = new Font("Courier New", 14);// LOTE Y FECHA CAD
            Font drawFont3 = new Font("Courier New", 9);// TEMP, DATOS GENERALES
            Font drawFont4 = new Font("Times New Roman", 10);// TEMP, DATOS GENERALES
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            PointF Dato = new PointF(300, 20);
            e.Graphics.DrawString("COMERCIALIZADORA GAB, SA. DE C.V.", drawFont3, drawBrush, Dato);
            Dato = new PointF(30, 50);
            e.Graphics.DrawString("RELACION DE LOTES POR SPLIT CAPTURADO", drawFont4, drawBrush, Dato);
            int Y = 60;
            foreach (DataRow Row1 in Trailer.Rows)
            {
                Dato = new PointF(30, Y + 30);//Fecha
                e.Graphics.DrawString("FECHA: " + Program.MyGlobal.PubFecEmb, drawFont3, drawBrush, Dato);
                Dato = new PointF(700, Y - 20);//Factura
                e.Graphics.DrawString("FACTURA", drawFont3, drawBrush, Dato);
                Dato = new PointF(30, Y + 50);//TURNO 140
                string Mtur = (Row1["Turno"].ToString() == "1") ? "VESPERTINO" : "MATUTINO";
                e.Graphics.DrawString("TURNO: " + Mtur, drawFont3, drawBrush, Dato);
                Dato = new PointF(700, Y);//PEDIDO 140
                e.Graphics.DrawString("PEDIDO", drawFont3, drawBrush, Dato);
                Dato = new PointF(30, Y + 70);//TRANSPORTISTA
                e.Graphics.DrawString("TRANSPORTISTA: " + LblProv.Text, drawFont3, drawBrush, Dato);
                Dato = new PointF(30, Y + 90);//DESTINO
                e.Graphics.DrawString("DESTINO: " + Row1["DESTINO"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(30, Y + 110);//PLACA
                e.Graphics.DrawString("PLACAS: " + Program.MyGlobal.PubNoTrailer, drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y + 50);//TEMP INI
                e.Graphics.DrawString("TEMP. INICIAL EN LA CAJA: " + Row1["TEMPINI"].ToString() + " °F", drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y + 70);//TEMP FIN
                e.Graphics.DrawString("  TEMP. FINAL EN LA CAJA: " + Row1["TEMPFIN"].ToString() + " °F", drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y + 90);//HORA INI
                e.Graphics.DrawString("    HORA INICIO DE CARGA: " + Row1["HORAINI"].ToString().Substring(10, 13), drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y + 110);//HORA FIN
                e.Graphics.DrawString("     HORA FINAL DE CARGA: " + Row1["HORAFIN"].ToString().Substring(10, 13), drawFont3, drawBrush, Dato);
            }
            Dato = new PointF(10, Y + 138);
            string CADENA = "     PRODUCTO                                    CANT   TEMP         LOTE                    F.CAD";
            e.Graphics.DrawString(CADENA, drawFont3, drawBrush, Dato);
            Dato = new PointF(10, Y + 140);
            CADENA = "____________________________________________________________________________________________________________________________";
            e.Graphics.DrawString(CADENA, drawFont3, drawBrush, Dato);
            Y = 220;
            int Tot = 0;
            string pos = "";
            //foreach (DataRow Row in Split.Rows)
            for (int i = NReg; i < DetSplit.Rows.Count; i++)
            {
                if (pos != DetSplit.Rows[i]["POSICION"].ToString())
                {
                    if (Tot > 0)
                    {
                        Y = Y + 20;
                        Dato = new PointF(330, Y);
                        e.Graphics.DrawString("Total: " + Tot.ToString("##,###"), drawFont3, drawBrush, Dato);
                        Y = Y + 20;
                        Tot = 0;
                    }
                    Dato = new PointF(30, Y);
                    pos = DetSplit.Rows[i]["POSICION"].ToString();
                    e.Graphics.DrawString("SPLIT # " + pos, drawFont2, drawBrush, Dato);
                    Y = Y + 5;
                }
                Y = Y + 15;
                Dato = new PointF(5, Y);
                e.Graphics.DrawString(DetSplit.Rows[i]["PRODUCTO"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(400, Y);
                e.Graphics.DrawString(DetSplit.Rows[i]["CAJAS"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(450, Y);
                e.Graphics.DrawString(DetSplit.Rows[i]["TEMP"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(500, Y);
                e.Graphics.DrawString(DetSplit.Rows[i]["LOTE"].ToString(), drawFont3, drawBrush, Dato);
                Dato = new PointF(680, Y);
                e.Graphics.DrawString(DetSplit.Rows[i]["FECHACAD"].ToString(), drawFont3, drawBrush, Dato);
                Tot = Tot + Convert.ToInt32(DetSplit.Rows[i]["CAJAS"]);
                if (Y > 1000)
                {
                    e.HasMorePages = true;
                    NReg = i + 1;
                    break;
                }

                if (i == TotReg)
                    e.HasMorePages = false;
            }
            if (Tot > 0)
            {
                Y = Y + 20;
                Dato = new PointF(330, Y);
                e.Graphics.DrawString("Total: " + Tot.ToString("##,###"), drawFont3, drawBrush, Dato);
            }
        }

        public void LlenaSplit()
        {
            DetSplit.Rows.Clear();
            string Tar29 = "N", Tar30 = "N";
            foreach (DataRow Row in Split.Rows)
            {
                string pos = Row["POSICION"].ToString().Trim();
                if (pos == "29")
                    Tar29 = "S";
                if (pos == "30")
                    Tar30 = "S";
                foreach (DataRow row1 in DETALLE.Select("SECCION = '" + pos + "'"))
                {
                    string A = row1["PROD_NOMBRE"].ToString();
                    string B = row1["NO_LOTE"].ToString();
                    string C = row1["FEC_CAD"].ToString();
                    string D = row1["TEMP"].ToString();
                    string E = row1["CAJAS"].ToString();
                    DetSplit.Rows.Add(A, B, C, D, E, pos);
                }
            }
            if (Tar29 == "N")
            {
                foreach (DataRow row1 in DETALLE.Select("SECCION = '29'"))
                {
                    string A = row1["PROD_NOMBRE"].ToString();
                    string B = row1["NO_LOTE"].ToString();
                    string C = row1["FEC_CAD"].ToString();
                    string D = row1["TEMP"].ToString();
                    string E = row1["CAJAS"].ToString();
                    DetSplit.Rows.Add(A, B, C, D, E, 29);
                }
            }
            if (Tar30 == "N")
            {
                foreach (DataRow row1 in DETALLE.Select("SECCION = '30'"))
                {
                    string A = row1["PROD_NOMBRE"].ToString();
                    string B = row1["NO_LOTE"].ToString();
                    string C = row1["FEC_CAD"].ToString();
                    string D = row1["TEMP"].ToString();
                    string E = row1["CAJAS"].ToString();
                    DetSplit.Rows.Add(A, B, C, D, E, 30);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fn_ImpPedVsSur();
        }

        public void Fn_ImpPedVsSur()
        {
            RepPedSur.Rows.Clear();
            SinSurtir.Rows.Clear();
            string Vendedor = "";
            string usuario = "", Elaboro = "";
            //for (i = 0; i < DGDetEmbCap.Rows.Count; i++)
            foreach (DataRow Row in DetEmb.Rows)
            {
                thisConnecion.Open(); //DBGAB
                DataTable Surtido = new DataTable();
                string mPED = "", mTip = "", mObsemb = "";
                mPED = Convert.ToString(Row["emb_folio"]);
                mTip = Convert.ToString(Row["emb_tipo"]);
                mObsemb = Convert.ToString(Row["emb_obs"]);
                string Nomcli = LblCli.Text + " " + Fr1.Fn_TraeNomCli(LblCli.Text);
                string Cadena = "SELECT A.*, B.prod_nomb_ingles, B.PROD_NOMBRE FROM TB_PED_EMBARQUE A, TB_CAT_PRODUCTO B WHERE EMB_FOLIO = '" + mPED + "' AND NALEXP = '" + mTip + "' AND A.PROD_CLAVE = B.PROD_CLAVE";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "PEDSUR");
                Surtido = ds.Tables["PEDSUR"];
                Cadena = "SELECT EMB_FOLIO, prod_clave, SUM(CAJAS) as cajas  FROM tb_det_embarque " +
                         "WHERE emb_folio = '" + mPED + "' AND Estatus = 'A' GROUP BY emb_folio, prod_clave ";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "CARGO");
                DataTable Cargo = ds.Tables["CARGO"];
                string BD = "Tb_MSTR_PEDIDOS_NAL";
                thisConnecion.Close(); //DBGAB
                if (mTip == "EXP")
                    BD = "Tb_MSTR_PEDIDOS_EXP";
                thisConnecion.Open();
                //SqlCommand cmnd2;
                Int32 TotS = 0, TotP = 0;
                if (mTip != "TRA")
                    Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE,B.prod_nomb_ingles,C.PDN_OBSERVACION,C.PDN_SELLO, c.pdn_elaboro, a.pdn_precio_mn, a.pdn_folio FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B, " + BD + " C  WHERE A.PDN_FOLIO = '" + mPED + "' AND A.PDN_TIPO = '" + mTip + "' AND A.PROD_CLAVE = B.PROD_CLAVE AND A.PDN_FOLIO = C.PDN_FOLIO  ORDER BY B.PROD_NOMBRE";
                else
                    Cadena = "SELECT A.PROD_CLAVE,A.EMB_UNIDADES AS PDN_NUM_UNIDADES,B.PROD_NOMBRE,B.prod_nomb_ingles,C.EMB_OBSERVACION AS PDN_OBSERVACION,C.TALON_EMBARQUE AS PDN_SELLO, '' AS pdn_elaboro FROM TB_DET_ORDENES_EMB A, TB_CAT_PRODUCTO B, TB_MSTR_ORDENES_EMB C WHERE A.EMB_FOLIO = '" + mPED + "' AND A.EMB_TIPO = 'MAQ' AND A.PROD_CLAVE = B.PROD_CLAVE AND A.EMB_FOLIO = C.EMB_FOLIO ORDER BY B.PROD_NOMBRE";
                //cmnd2 = thisConnecion.CreateCommand();
                //cmnd2.CommandText = Cadena;
                //SqlDataReader Ped;
                //Ped = cmnd2.ExecuteReader();
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "PED");
                Ped = ds.Tables["PED"];
                if (Peddet.Rows.Count == 0)
                    Peddet = Ped.Clone();
                foreach (DataRow dr in Ped.Rows)
                    Peddet.Rows.Add(dr.ItemArray);

                Cadena = "SELECT a.emb_obs, b.responsable, isnull(b.enviodif,'N') as EnvioDif, b.hora_trailer FROM tb_mstr_embarque a, tb_mstr_trailer b " +
                         " WHERE a.emb_folio = '" + mPED + "' AND a.nalexp = '" + mTip + "' AND a.no_trailer = b.no_trailer AND a.hora_trailer = b.hora_trailer ";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion);
                da.Fill(ds, "Causa");
                DataTable Causa = ds.Tables["Causa"];
                Cadena = "Select usu_login, usu_email, usu_nombre From tb_cat_usuarios";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion);
                da.Fill(ds, "usu");
                DataTable usu = ds.Tables["usu"];

                ///thisConnecion.Open();
                string consultadestino = "SELECT destino FROM tb_mstr_trailer WHERE no_trailer = '" + Program.MyGlobal.PubNoTrailer.Trim() + "' AND hora_trailer = '" + Program.MyGlobal.PubFecEmb.ToString() + "'";
                SqlCommand cmddestino;
                cmddestino = new SqlCommand(consultadestino);
                cmddestino.Connection = thisConnecion;
                string DestinoTrailer = Convert.ToString(cmddestino.ExecuteScalar());
                //thisConnecion.Close();
                if (DestinoTrailer.ToUpper().Contains("MONT"))
                {
                    LblCli.Text = "POM1";
                }

                if (LblCli.Text.Contains("POM1") || LblCli.Text.Contains("POM2"))
                {
                    Cadena = "Select clave_prod, clave_mac from tb_cod_recibos where cliente = 'POM'";
                    da = new SqlDataAdapter(Cadena, thisConnecion);
                    ds = new DataSet();
                    da.Fill(ds, "CodigosMty");
                    CodigosMty = ds.Tables["CodigosMty"];
                }
                //while (Ped.Read())
                //{
                //    int T = 0;
                //    foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                //    {
                //        T = Convert.ToInt32(row["cant_sur"]);
                //        TotS = TotS + Convert.ToInt32(row["cant_sur"]);
                //    }
                //    TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                //    string Nom_Ingles = Ped["prod_nomb_ingles"].ToString().Substring(200, 50).Trim() + " " +Ped["prod_nomb_ingles"].ToString().Substring(100, 50).Trim();
                //    RepPedSur.Rows.Add(mPED, Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]), T, Ped["PDN_OBSERVACION"].ToString(), Ped["PDN_SELLO"].ToString(), mObsemb, Nomcli, Nom_Ingles);
                //}
                usuario = "";
                foreach (DataRow row in Surtido.Rows)
                {
                    int T = 0, xSur = 0; ;
                    string Hay = "N", Obs = "", Sello = "";
                    string Nom_Ingles = row["prod_nomb_ingles"].ToString().Substring(200, 50).Trim() + " " + row["prod_nomb_ingles"].ToString().Substring(100, 50).Trim();
                    foreach (DataRow Row1 in Cargo.Select("prod_clave='" + row["prod_clave"] + "'"))
                    {
                        row["cant_sur"] = Convert.ToInt32(Row1["cajas"]);
                        xSur = Convert.ToInt32(Row1["cajas"]);
                    }
                    foreach (DataRow Row1 in Ped.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
                    {
                        T = Convert.ToInt32(Row1["PDN_NUM_UNIDADES"]);
                        TotS = TotS + Convert.ToInt32(Row1["PDN_NUM_UNIDADES"]);
                        RepPedSur.Rows.Add(mPED, Row1["PROD_CLAVE"].ToString(), Row1["PROD_NOMBRE"].ToString(), T, Convert.ToInt32(row["cant_sur"]), Row1["PDN_OBSERVACION"].ToString(), Row1["PDN_SELLO"].ToString(), mObsemb, Nomcli, Nom_Ingles);
                        Obs = Row1["PDN_OBSERVACION"].ToString().Trim();
                        Sello = Row1["PDN_SELLO"].ToString().Trim();
                        usuario = Row1["pdn_elaboro"].ToString().Trim();
                        Hay = "S";
                    }
                    foreach (DataRow rows in usu.Select("usu_login = '" + usuario + "'"))
                        Elaboro = rows["usu_nombre"].ToString().Trim() + " " + rows["usu_email"].ToString().Trim();
                    string mResp = "", mCau = "";
                    if (xSur != T)
                    {
                        foreach (DataRow rows in Causa.Rows)
                        {
                            mResp = rows["responsable"].ToString().Trim();
                            mCau = rows["emb_obs"].ToString().Trim();
                            LblEnvDif.Text = rows["EnvioDif"].ToString().Trim();
                        }
                        SinSurtir.Rows.Add(mPED, Nomcli, row["PROD_NOMBRE"].ToString(), T.ToString("##0"), xSur.ToString("##0"), (T - xSur).ToString("##0"), mResp, mCau);
                        foreach (DataRow rows in usu.Select("usu_login = '" + usuario + "'"))
                        {
                            if (Vendedor.IndexOf(rows["usu_email"].ToString().Trim()) == -1)
                            {
                                Vendedor += " ;" + rows["usu_email"].ToString().Trim();
                                //Elaboro = rows["usu_email"].ToString().Trim();
                            }
                        }
                    }
                    if (Hay == "N")
                        RepPedSur.Rows.Add(mPED, row["PROD_CLAVE"].ToString(), row["PROD_NOMBRE"].ToString(), 0, Convert.ToInt32(row["cant_sur"]), Obs, Sello, mObsemb, Nomcli, Nom_Ingles);
                }
                thisConnecion.Close();
                // se van a exportar los lotes de trazabilidad a las bases de datos de los cedis
                // 4 Sep 2024 RCC
                if (LblCli.Text.Contains("JAL") || LblCli.Text.Contains("MEX") || LblCli.Text.Contains("MEX1") || LblCli.Text.Contains("QUIN"))
                {
                    string CveEmp = (LblCli.Text.Contains("JAL")) ? "62" : LblCli.Text.Trim() == "MEX" ? "61" : LblCli.Text.Trim() == "MEX1" ? "65" : "63";
                    ExportaLotesUERP(mPED, CveEmp);
                }
            }
            TotReg = RepPedSur.Rows.Count;
            NReg = 0;
            PrintDocument pd = new PrintDocument();
            PrintDocument pdEng = new PrintDocument();
            //Program.MyGlobal.NoPedido = Convert.ToInt32(DGPedidos.CurrentRow.Cells["PEDIDO"].Value);
            //Program.MyGlobal.TipoPed = DGPedidos.CurrentRow.Cells["TIPO"].Value.ToString();
            string cap = "";
            if (SinSurtir.Rows.Count > 0)
            {
                cap = "<table border=\"1\"> " +
                       "<tr> <TH COLSPAN=9>" + "DIFERENCIA DE EMBARQUES </TH></TR> " +
                      " <tr>   " +
                      " <th scope=\"col\">Orden</strong></th> " +
                      " <th scope=\"col\">Cliente</strong></th> " +
                      " <th scope=\"col\">Producto</strong></th> " +
                      " <th scope=\"col\">Cant Ped</strong></th> " +
                      " <th scope=\"col\">Cant Sur</strong></th> " +
                      " <th scope=\"col\">Faltante</strong></th> " +
                      " <th scope=\"col\">Responsable</strong></th> " +
                      " <th scope=\"col\">Causa</strong></th> " +
                      " </tr> ";
                foreach (DataRow Row in SinSurtir.Rows)
                {
                    cap += "<tr> " +
                           "<td ALIGN=LEFT>" + Row["Pedido"].ToString().Trim() + "</td>" +
                           "<td ALIGN=LEFT>" + Row["Cliente"].ToString().Trim() + "</td>" +
                           "<td ALIGN=LEFT>" + Row["Producto"].ToString().Trim() + "</td>" +
                           "<td ALIGN=CENTER>" + Convert.ToInt16(Row["CantPed"]).ToString("##,##0").Trim() + "</td>" +
                           "<td ALIGN=CENTER>" + Convert.ToInt16(Row["CantSur"]).ToString("##,##0") + "</td>" +
                           "<td ALIGN=CENTER>" + Convert.ToInt16(Row["Faltante"]).ToString("##,##0") + "</td>" +
                           "<td>" + Row["Responsable"].ToString().Trim() + "</td>" +
                           "<td ALIGN=LEFT>" + Row["Causa"].ToString().Trim() + "</td>" +
                           "</tr>";
                }
                cap += "</table> ";

            }
            // para mandarlo a vista previa el reporte
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                if (pd.PrinterSettings.IsDefaultPrinter)
                {
                    pd.PrinterSettings.PrinterName = pd.PrinterSettings.PrinterName;
                }
            }
            pd.PrintPage += new PrintPageEventHandler(this.PrintPedSur_PrintPage);
            pd.Print();
            pd.Dispose();
            //PrintPreviewDialog Report = new PrintPreviewDialog();
            //Report.Document = pd;
            //Report.ShowDialog();
            if (LblCli.Text.Contains("MASTR") || LblCli.Text.Contains("GABOP") || LblCli.Text.Contains("MAST2"))
            {
                //pdEng.PrinterSettings.PrinterName = "Foxit Reader PDF Printer";
                //pdEng.PrintPage += new PrintPageEventHandler(this.PrintPedSurEng_PrintPage);
                //pdEng.Print();
                string Cad = string.Format(System.DateTime.Now.ToLongDateString() + "<br><br>" +
                            "Hola Buen día" + "<br><br>" +
                            "Envio Información Del Embarque  " + Program.MyGlobal.PubNoTrailer.Trim() + "<br><br>" +
                            "Atentamente, " + "<br><br>" +
                            "Comercializadora GAB S.A. de C.V." + "<br>" +
                            "www.mrlucky.com.mx" + "<br><br>" +
                            "Elaboro: " + Elaboro + "<br><br>" + "Ventas Exportación");
                //string Arch = @"c:\reportes\document.pdf";
                //string Arch2 = @"c:\reportes\Emb_" + Program.MyGlobal.PubNoTrailer.Trim() + "_del_dia_" + Program.MyGlobal.PubFecEmb.Substring(6, 4) + Program.MyGlobal.PubFecEmb.Substring(3, 2) + Program.MyGlobal.PubFecEmb.Substring(0, 2) + ".pdf";
                ConFormat = "S";
                pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = "Foxit Reader PDF Printer";
                pd.PrintPage += new PrintPageEventHandler(this.PrintCForm_PrintPage);
                pd.Print();
                string Arch = @"c:\reportes\document.pdf";
                string Arch1 = @"c:\reportes\Emb_" + Program.MyGlobal.PubNoTrailer.Trim() + "_del_dia_" + Program.MyGlobal.PubFecEmb.Substring(6, 4) + Program.MyGlobal.PubFecEmb.Substring(3, 2) + Program.MyGlobal.PubFecEmb.Substring(0, 2) + ".pdf";
                if (File.Exists(Arch1))
                    File.Delete(Arch1);
                if (File.Exists(Arch))
                    File.Copy(Arch, Arch1);
                LlenaSplit();
                string Arch2 = "";
                if (DetSplit.Rows.Count > 0)
                {
                    TotReg = DetSplit.Rows.Count;
                    NReg = 0;
                    PrintDocument pdSplit = new PrintDocument();
                    pdSplit.PrinterSettings.PrinterName = "Foxit Reader PDF Printer";
                    pdSplit.PrintPage += new PrintPageEventHandler(this.PrintSplit_PrintPage);
                    pdSplit.Print();
                    Arch = @"c:\reportes\document.pdf";
                    Arch2 = @"c:\reportes\Emb_" + Program.MyGlobal.PubNoTrailer.Trim() + "_del_dia_" + Program.MyGlobal.PubFecEmb.Substring(6, 4) + Program.MyGlobal.PubFecEmb.Substring(3, 2) + Program.MyGlobal.PubFecEmb.Substring(0, 2) + "_2.pdf";
                    if (File.Exists(Arch2))
                        File.Delete(Arch2);
                    if (File.Exists(Arch))
                        File.Copy(Arch, Arch2);
                }
                //if (File.Exists(Arch2))
                //    File.Delete(Arch2); 
                //if (File.Exists(Arch))
                //    File.Copy(Arch, Arch2);
                string Contactos = ""; //"celizarraras@mrlucky.com.mx;lissete@mrlucky.com.mx;cgalarza3@mrlucky.com.mx;karina@mrlucky.com.mx;daniela@mrlucky.com.mx;ana@mrlucky.com.mx;ricardo.cortes@mrlucky.com.mx";
                foreach (DataRow row in CorreosEmb.Select("CNTE_CLAVE = '" + LblCli.Text.Trim() + "'"))
                    Contactos = row["EMAIL_DEST"].ToString().Trim();
                Fr1.SendMail(Contactos, Arch1, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer + " Factura No. " + Program.MyGlobal.PubFact, Arch2);
            }
            string Cuentas = "";



            if (LblCli.Text.Contains("POM1") || LblCli.Text.Contains("POM2") || LblCli.Text.Contains("JAL") || LblCli.Text.Contains("SUGFO") || LblCli.Text.Contains("MEX") || LblCli.Text.Contains("MEX1") || LblCli.Text.Contains("QUIN") || LblCli.Text.Contains("NIETO"))
            {
                pd = new PrintDocument();
                NReg = 0;
                //PrintPedSur.Dispose();
                pd.PrinterSettings.PrinterName = "Foxit Reader PDF Printer";
                pd.PrintPage += new PrintPageEventHandler(this.PrintPedSur_PrintPage);
                pd.Print();
                pd.Dispose();
                string Arch3 = @"c:\reportes\document.pdf";
                string Arch2 = @"c:\reportes\Emb_" + Program.MyGlobal.PubNoTrailer.Trim() + "_del_dia_" + Program.MyGlobal.PubFecEmb.Substring(6, 4) + Program.MyGlobal.PubFecEmb.Substring(3, 2) + Program.MyGlobal.PubFecEmb.Substring(0, 2) + ".pdf";
                if (File.Exists(Arch3))
                    File.Delete(Arch3);
                if (File.Exists(Arch2))
                    File.Delete(Arch2);

                if (File.Exists(Arch3))
                    File.Copy(Arch3, Arch2);
                string mfec = Program.MyGlobal.PubFecEmb.ToString().Substring(6, 4) + Program.MyGlobal.PubFecEmb.ToString().Substring(3, 2) + Program.MyGlobal.PubFecEmb.ToString().Substring(0, 2);
                string Arch = "C:\\REPORTES\\EMB" + LblPlaca.Text.Trim() + "_DIA_" + mfec + ".XLS";
                string Arch1 = "C:\\REPORTES\\" + LblPlaca.Text.Trim() + "_DIA_" + Convert.ToDateTime(Program.MyGlobal.PubFecEmb).ToString("yyyyMMdd") + ".TXT";
                ReporteExcelDetLotes(Arch);
                string Cad = string.Format(
                            "Hola Buen día" + "<br><br>" +
                            "Envio Información Del Detalle de Lotes del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + "<br><br>" +
                            "Del Día " + Convert.ToDateTime(Program.MyGlobal.PubFecEmb.ToString()).ToLongDateString() + "<br><br>" +
                            "Orden de Venta " + PedidosEmbarque.ToString() + "<br><br>" +
                            "Atentamente, " + "<br><br>" +
                            "Comercializadora GAB S.A. de C.V." + "<br>" +
                            "www.mrlucky.com.mx" + "<br><br>" +
                            "Ventas Nacionales");
                foreach (DataRow row in CorreosEmb.Select("CNTE_CLAVE = '" + LblCli.Text.Trim() + "'"))
                    Cuentas = row["EMAIL_DEST"].ToString().Trim();
                if (File.Exists(Arch) && Cuentas.Trim().Length > 0)
                    if (LblCli.Text.Contains("JAL"))
                        //Fr1.SendMail("ricardo.cortes@mrlucky.com.mx;fernando.vargas@mrlucky.com.mx;edgar.rodriguez@mrlucky.com.mx;gcamacho@mrlucky.com.mx;calidadgdl@mrlucky.com.mx;ehernan@mrlucky.com.mx", Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer + "Factura No. " + Program.MyGlobal.PubFact, Arch2);
                        SendMailCedis(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, Arch2, PedidosEmbarque.ToString().Trim());
                    else
                        if (LblCli.Text.Contains("SUGFO"))
                        //Fr1.SendMail("ricardo.cortes@mrlucky.com.mx;mrhernandez@mrlucky.com.mx", Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer + "Factura No. " + Program.MyGlobal.PubFact,"");
                        Fr1.SendMail(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, "");
                    else
                    {
                        if (LblCli.Text.Contains("POM1") || LblCli.Text.Contains("POM2"))
                            //Fr1.SendMail("ricardo.cortes@mrlucky.com.mx;cdelao@deocejo.com.mx;hdeleon@deocejo.com.mx;eocejo@deocejo.com.mx;jvigildeocejo@deocejo.com.mx;rocejo@deocejo.com.mx;nsaucedo@deocejo.com.mx;mtorresg@deocejo.com.mx;esaucedo@deocejo.com.mx;luisf@deocejo.com.mx;mcarranza@deocejo.com.mx", Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer + "Factura No. " + Program.MyGlobal.PubFact, "");
                            Fr1.SendMail(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, "");
                        else
                            if (LblCli.Text.Contains("MEX") || LblCli.Text.Contains("MEX1"))
                            SendMailCedis(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, "", PedidosEmbarque.ToString().Trim());
                        else
                                if (LblCli.Text.Contains("QUIN"))
                            SendMailCedis(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, Arch2, PedidosEmbarque.ToString().Trim());
                        else // NIETO
                            Fr1.SendMail(Cuentas, Arch, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer.Trim() + " Factura No. " + Program.MyGlobal.PubFact, Arch2);
                        //Fr1.SendMail("ricardo.cortes@mrlucky.com.mx;cdelao@deocejo.com.mx;hdeleon@deocejo.com.mx;gpatton@deocejo.com.mx", Arch1, Cad, "Envio Información Del Embarque " + Program.MyGlobal.PubNoTrailer + "Factura No. " + Program.MyGlobal.PubFact);
                    }
            }
            DateTime mfe = Convert.ToDateTime(Program.MyGlobal.PubFecEmb.ToString());
            int prueba = (System.DateTime.Now.Date - mfe).Days;
            if ((System.DateTime.Now.Date - mfe).Days < 3)
                if (SinSurtir.Rows.Count > 0 && LblEnvDif.Text == "N")
                {
                    foreach (DataRow row in CorreosEmb.Select("CNTE_CLAVE = 'FALTANTES'"))
                        Cuentas = row["EMAIL_DEST"].ToString().Trim();
                    //Fr1.SendMail("ricardo.cortes@mrlucky.com.mx;fjcastrejon@mrlucky.com.mx;mdelrio@mrlucky.com.mx;ahernandez@mrlucky.com.mx;embarques@mrlucky.com.mx;jgonzalez@mrlucky.com.mx;supervisorcamfrias@mrlucky.com.mx;ensaladas@mrlucky.com.mx;musabiaga@mrlucky.com.mx;logistica@mrlucky.com.mx;dmunoz@mrlucky.com.mx" + Vendedor, "", cap, "Envio De Faltantes de Embarques " + Program.MyGlobal.PubNoTrailer + "Factura No. " + Program.MyGlobal.PubFact, "");
                    Fr1.SendMail(Cuentas + Vendedor, "", cap, "Envio De Faltantes de Embarques " + Program.MyGlobal.PubNoTrailer.Trim() + "Factura No. " + Program.MyGlobal.PubFact, "");
                    thisConnecion.Open();
                    string Cadena = "Update tb_mstr_trailer set EnvioDif = 'S' WHERE no_trailer = '" + Program.MyGlobal.PubNoTrailer.Trim() + "' and hora_trailer = '" + Program.MyGlobal.PubFecEmb + "'";
                    SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
                    cmd.ExecuteNonQuery();
                    thisConnecion.Close();
                }
            Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "7.1", Program.MyGlobal.PubNoTrailer.Trim(), " Impresion del Reporte PedVsSur " + Program.MyGlobal.PubFact + " " + Program.MyGlobal.PubFecEmb + " Cliente: " + LblCli.Text.Trim(), "SIPGAB");
        }

        private void PrintPedSur_PrintPage(object sender, PrintPageEventArgs e)
        {
            decimal sumasurtido = 0, sumacomp = 0, sumasplit = 0, decimales = 0, enteros = 0;
            Int32 sumapedido = 0, sumtotped = 0, sumtotsur = 0;
            String drawString1 = " ", drawString2 = " ", drawString3 = " ", drawString4 = " ", drawStringDest = " ", drawobs = " ";
            String drawLinea = " ", drawProd = " ", drawPedi = " ", drawcajas = " ", drawtaraprox = " ";
            String drawstring8 = " ", drawstring9 = " ", drawstring10 = " ", drawstring11 = " ", drawstring12 = " ";

            DateTime dt = DateTime.Now;
            //int cont = 148, cont1 = 165;
            int cont = 130, cont1 = 130, cont2 = 130;

            string sub_cli = "";
            string PedObs = "";
            string PedSello = "", PedEmb = "";
            // Create font and brush. 
            Font drawFont = new Font("Courier New", 8);//encabezado
            Font drawFont1 = new Font("PF Barcode 39", 25);//código de barras
            Font drawFont2 = new Font("Arial", 10, FontStyle.Bold);//encabezado
            Font drawFont3 = new Font("Arial", 8);//encabezado
            Font drawFont4 = new Font("Courier New", 7);//observaciones de los embarques por pedido
            Font drawFont5 = new Font("Courier New", 8, FontStyle.Bold);//encabezado
            Font drawFont6 = new Font("Arial", 8, FontStyle.Underline);//encabezado
            Font drawFont7 = new Font("Arial", 6);//encabezado

            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing. 
            PointF drawPoint = new PointF(10.0F, 10.0F);//encabezado
            PointF drawPoint1 = new PointF(00.0F, 40.0F);//codigo de barras            
            PointF drawPoint2 = new PointF(00.0f, 42.0f);//linea1
            PointF drawPoint3 = new PointF(0.0f, 55.0f);//datos1
            PointF drawpoint4 = new PointF(540.0F, 55.0F);
            PointF drawpoint5 = new PointF(0.0F, 68.0F);
            PointF drawpoint6 = new PointF(540.0F, 68.0F);
            PointF drawpoint7 = new PointF(0.0F, 82.0F);
            PointF drawpoint8 = new PointF(540.0F, 82.0F);
            PointF drawpoint9 = new PointF(0.0F, 105.0F);
            PointF drawPoint4 = new PointF(00.0f, 90.0f);//linea2
            PointF drawPoint5 = new PointF(00.0f, 110.0f);//linea3
            drawString2 = "_________________________________________________________________________________________________________________________________________";

            #region
            decimal resulta = 0;
            string mped = "";
            int j;
            for (j = NReg; j < RepPedSur.Rows.Count; j++)
            {
                if (PedObs.Trim().Length == 0)
                    PedObs = RepPedSur.Rows[j].Cells["OBSPED"].Value.ToString();
                if (PedSello.Trim().Length == 0)
                    PedSello = RepPedSur.Rows[j].Cells["PEDSELLO"].Value.ToString();
                //if(PedEmb.Trim().Length ==0) 
                drawString1 = "\tCLIENTE: " + RepPedSur.Rows[j].Cells["NOMCLI"].Value.ToString();
                drawLinea = string.Format("{0}", Convert.ToString(RepPedSur.Rows[j].Cells["PROD"].Value.ToString()));
                drawProd = string.Format("{0}\t{1}", RepPedSur.Rows[j].Cells["PROD"].Value.ToString(), RepPedSur.Rows[j].Cells["NOMBRE"].Value.ToString());
                drawPedi = string.Format("        |");
                drawcajas = (Convert.ToDecimal(RepPedSur.Rows[j].Cells["CANTPED"].Value)).ToString("###,###");
                drawtaraprox = (Convert.ToDecimal(RepPedSur.Rows[j].Cells["CANTSUR"].Value)).ToString("###,###");
                PointF drawPointlin = new PointF(0.0F, cont);

                if (RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString() != mped)
                {

                    if (sumapedido > 0 || sumasurtido > 0)
                    {
                        //cont = cont + 15;
                        drawLinea = string.Format(" {0:##,###}   |    {1:##,###}", sumapedido, sumasurtido);
                        PointF drawPointTotPed = new PointF(490.0F, cont);
                        e.Graphics.DrawString(drawLinea, drawFont2, drawBrush, drawPointTotPed);
                        PointF drawPointObs = new PointF(10.0F, cont);
                        e.Graphics.DrawString(PedEmb, drawFont4, drawBrush, drawPointObs);
                        //Pen p1 = new Pen(Color.Black, 1);
                        //e.Graphics.DrawRectangle(p1, 490, cont, 100, 25); 
                        cont = cont + 15;
                        string Dettar = "DETALLE DE TARIMAS: " + Fr1.DetTarimas(mped);
                        drawPointObs = new PointF(10.0F, cont);
                        e.Graphics.DrawString(Dettar, drawFont5, drawBrush, drawPointObs);
                        cont = cont + 20;
                    }
                    PedEmb = Convert.ToString(RepPedSur.Rows[j].Cells["OBSEMB"].Value);
                    String cadena = "PEDIDO " + RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString();
                    PointF drawPointobs = new PointF(100.0F, cont);
                    e.Graphics.DrawString(cadena, drawFont5, drawBrush, drawPointobs);
                    cadena = RepPedSur.Rows[j].Cells["OBSPED"].Value.ToString();
                    drawPointobs = new PointF(300.0F, cont);
                    mped = RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString();
                    e.Graphics.DrawString(cadena, drawFont, drawBrush, drawPointobs);
                    cont = cont + 20;
                    sumapedido = 0;
                    sumasurtido = 0;
                    PedObs = "";
                }

                PointF drawPointProd = new PointF(30.0f, cont);
                PointF drawPointPedi = new PointF(472.0F, cont);
                PointF drawPointcajas = new PointF(510 - (drawcajas.Trim().Length * 6), cont);
                PointF drawPointtar = new PointF(570 - (drawtaraprox.Trim().Length * 6), cont);
                PointF drawpedi = new PointF(30.0f, cont + 13);
                PointF drawfecpedi = new PointF(100.0F, cont + 13);
                PointF drawadua = new PointF(170.0F, cont + 13);

                //e.Graphics.DrawString(drawLinea, drawFont, drawBrush, drawPointlin);
                e.Graphics.DrawString(drawProd, drawFont, drawBrush, drawPointProd);
                e.Graphics.DrawString(drawPedi, drawFont, drawBrush, drawPointPedi);
                e.Graphics.DrawString(drawcajas, drawFont5, drawBrush, drawPointcajas);
                e.Graphics.DrawString(drawtaraprox, drawFont5, drawBrush, drawPointtar);
                PointF drawPoint7 = new PointF(0.0F, cont + 2);
                //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint7);
                Pen p1 = new Pen(Color.Black, 1);
                e.Graphics.DrawLine(p1, 100, cont + 15, 750, cont + 15);
                cont = cont + 15;
                cont1 = cont1 + 15;
                cont2 = cont2 + 15;
                sumapedido = sumapedido + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTPED"].Value.ToString());
                sumasurtido = sumasurtido + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTSUR"].Value.ToString());
                sumtotped = sumtotped + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTPED"].Value.ToString());
                sumtotsur = sumtotsur + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTSUR"].Value.ToString());
                enteros = Math.Truncate(resulta);
                sumacomp = sumacomp + enteros;
                decimales = enteros - resulta;
                sumasplit = sumasplit + decimales;
                if (cont > 900)
                {
                    e.HasMorePages = true;
                    NReg = j;
                    break;
                }
                if (j == TotReg)
                    e.HasMorePages = false;

            }//for

            if (sumapedido > 0 || sumasurtido > 0)
            {
                //cont = cont + 15;
                drawLinea = string.Format(" {0:##,###}   |    {1:##,###}", sumapedido, sumasurtido);
                PointF drawPointTotPed = new PointF(490.0F, cont);
                e.Graphics.DrawString(drawLinea, drawFont2, drawBrush, drawPointTotPed);
                PointF drawPointObs = new PointF(10.0F, cont);
                e.Graphics.DrawString(PedEmb, drawFont4, drawBrush, drawPointObs);
                cont = cont + 15;
                string Dettar = "DETALLE DE TARIMAS: " + Fr1.DetTarimas(mped);
                drawPointObs = new PointF(10.0F, cont);
                e.Graphics.DrawString(Dettar, drawFont5, drawBrush, drawPointObs);
            }

            //for (i = RegistroEmb ; i <= RegistroEmb; i++)
            //foreach (DataRow row1 in Embarques.Select("NO_TRAILER = '" + RegistroEmb + "'"))
            foreach (DataRow row1 in Embarque.Rows)
            {
                if (row1["NO_TRAILER"].ToString().Trim() == LblPlaca.Text.Trim())
                {
                    String drawString = "\t\t\t\t\t\tREPORTE DE EMBARQUE\r\n ";
                    //drawString3 = "\tTRANSPORTISTA: " + Convert.ToString(DGEmbCap.Rows[i].Cells["TRANS"].Value) + "  " + Fn_TraeNomTra(Convert.ToString(DGEmbCap.Rows[i].Cells["TRANS"].Value));
                    //drawString4 = "FECHA: " + Convert.ToDateTime(DGEmbCap.Rows[i].Cells["Fecha"].Value).ToString("dd/MM/yyyy") + "\r\n";
                    //drawStringDest = "\tDESTINO: " + DGEmbCap.Rows[i].Cells["DESTINO"].Value.ToString() + "\r\n";
                    //drawstring8 = "\t" + sub_cli.Trim() + "";
                    //drawstring9 = "HORA INICIO DE CARGA: " + Convert.ToString(DGEmbCap.CurrentRow.Cells["HRINI"].Value) + "\r\n";
                    //drawstring10 = "\tPLACA: " + Convert.ToString(DGEmbCap.CurrentRow.Cells["TRAILER"].Value);
                    //drawstring11 = "HORA FINAL DE CARGA " + Convert.ToString(DGEmbCap.CurrentRow.Cells["HRFIN"].Value) + " \r\n\r\n";
                    //drawstring12 = "      CODIGO            PRODUCTO \t\t\t\t\t |  PEDIDO  |  CARGADO  |  OBSERVACIONES \r\n";
                    drawString3 = "\tTRANSPORTISTA: " + Convert.ToString(row1["TRANSPORTE"]) + "  " + Fr1.Fn_TraeNomTra(Convert.ToString(row1["TRANSPORTE"]));
                    drawString4 = "FECHA: " + Convert.ToDateTime(row1["Fecha"]).ToString("dd/MM/yyyy") + "\r\n";
                    drawStringDest = "\tDESTINO: " + row1["DESTINO"].ToString() + "\r\n";
                    drawstring8 = "\t" + sub_cli.Trim() + "";
                    drawstring9 = "HORA INICIO DE CARGA: " + Convert.ToString(row1["HORAINI"]) + "\r\n";
                    drawstring10 = "\tPLACA: " + Convert.ToString(row1["NO_TRAILER"]);
                    drawstring11 = "HORA FINAL DE CARGA " + Convert.ToString(row1["HORAFIN"]) + " \r\n\r\n";
                    drawstring12 = "      CODIGO            PRODUCTO \t\t\t\t\t |  PEDIDO  |  CARGADO  |  OBSERVACIONES \r\n";

                    e.Graphics.DrawString(drawString1, drawFont3, drawBrush, drawPoint1);//CLIENTE
                    drawPoint1 = new PointF(680.0F, 30.0F);
                    drawString1 = "Impreso: " + System.DateTime.Now.ToString();
                    e.Graphics.DrawString(drawString1, drawFont7, drawBrush, drawPoint1);//hora de impresion
                    e.Graphics.DrawString(drawString, drawFont2, drawBrush, drawPoint);//encabezado            
                    //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint2);//linea1
                    e.Graphics.DrawString(drawString3, drawFont3, drawBrush, drawPoint3);//datos1
                    e.Graphics.DrawString(drawString4, drawFont3, drawBrush, drawpoint4);//
                    e.Graphics.DrawString(drawStringDest, drawFont3, drawBrush, drawpoint5);//
                    e.Graphics.DrawString(drawstring8, drawFont3, drawBrush, drawpoint5);//
                    e.Graphics.DrawString(drawstring9, drawFont3, drawBrush, drawpoint6);//
                    e.Graphics.DrawString(drawstring10, drawFont3, drawBrush, drawpoint7);//
                    e.Graphics.DrawString(drawstring11, drawFont3, drawBrush, drawpoint8);//
                    e.Graphics.DrawString(drawstring12, drawFont5, drawBrush, drawpoint9);//
                    //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint4);//linea2
                    e.Graphics.DrawString(drawString2, drawFont3, drawBrush, drawPoint5);//linea3                       

                    drawobs = "TOTAL DEL EMBARQUE:    " + string.Format("{0:##,###}  |  {1:##,###}", sumtotped, sumtotsur);
                    PointF drawobser = new PointF(305.00f, 995.0f);//txt observaciones
                    Pen p = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(p, 475, 990, 120, 25);
                    e.Graphics.DrawString(drawobs, drawFont2, drawBrush, drawobser);//txt observaciones

                    String drawString5 = "CARGO : \t\t\t\t\t\t\t  DESTINO:  \t\t\t\t\t\t\t  SELLO: \r\n";
                    String drawString6 = "ME COMPROMETO A MANTENER LA TEMPERARTURA A: 34° F     NOMBRE DEL CHOFER: \t\t\t\t\t  FIRMA CHOFER: _____________\r\n";
                    String drawString7 = "TERMOGRAFO : \t\t\t\t\t\t  EMPEZO A CARGAR CAMION (HRS):  \t\t\t  TERMINO (HRS) : ";
                    PointF drawstring5 = new PointF(10.00f, 1020.0f);//string5
                    PointF drawstring6 = new PointF(10.00f, 1035.0f);//string6
                    PointF drawstring7 = new PointF(10.00f, 1050.0f);//string7            
                    e.Graphics.DrawString(drawString5, drawFont4, drawBrush, drawstring5);//string5
                    e.Graphics.DrawString(drawString6, drawFont4, drawBrush, drawstring6);//string6
                    e.Graphics.DrawString(drawString7, drawFont4, drawBrush, drawstring7);//string7
                    PointF drawryandig = new PointF(553.0f, 1000.0f);//ryan

                    PointF drawchofername = new PointF(505.0f, 1035.0f);//nombre chofer
                    string dat = row1["RESPONSABLE"].ToString();
                    PointF posxy = new PointF(80, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = row1["DESTINO"].ToString();
                    posxy = new PointF(470, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = PedSello.Trim();
                    posxy = new PointF(730, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = row1["CHOFER"].ToString().Trim();
                    posxy = new PointF(480, 1035);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = row1["RYAN1"].ToString() + "(" + row1["POSRYAN1"].ToString().Trim() + ")";
                    posxy = new PointF(100, 1050);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = Convert.ToString(row1["HORAINI"]).Substring(11, 13);
                    posxy = new PointF(530, 1050);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = Convert.ToString(row1["HORAFIN"]).Substring(11, 13);
                    posxy = new PointF(750, 1050);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                }
            }

            //e.Graphics.DrawString(drawchofer, drawFont, drawBrush, drawchofername);//nombre chofer

            //cont1 = 160;
            #endregion
            //sumataraprox = sumacomp = sumasplit = decimales = enteros = 0;
            sumapedido = 0;
            //e.HasMorePages = false;
            thisConnecion.Close();

        }

        public void PrintPedSurEng_PrintPage(object sender, PrintPageEventArgs e)
        {
            decimal sumasurtido = 0, sumacomp = 0, sumasplit = 0, decimales = 0, enteros = 0;
            Int32 sumapedido = 0, sumtotped = 0, sumtotsur = 0;
            String drawString1 = " ", drawString2 = " ", drawString3 = " ", drawString4 = " ", drawStringDest = " ", drawobs = " ";
            String drawLinea = " ", drawProd = " ", drawPedi = " ", drawcajas = " ", drawtaraprox = " ";
            String drawstring8 = " ", drawstring9 = " ", drawstring10 = " ", drawstring11 = " ", drawstring12 = " ";

            DateTime dt = DateTime.Now;
            //int cont = 148, cont1 = 165;
            int cont = 130, cont1 = 130, cont2 = 130;

            string sub_cli = "";
            string PedObs = "";
            string PedSello = "", PedEmb = "";
            // Create font and brush. 
            Font drawFont = new Font("Courier New", 8);//encabezado
            Font drawFont1 = new Font("PF Barcode 39", 25);//código de barras
            Font drawFont2 = new Font("Arial", 10, FontStyle.Bold);//encabezado
            Font drawFont3 = new Font("Arial", 8);//encabezado
            Font drawFont4 = new Font("Courier New", 7);//observaciones de los embarques por pedido
            Font drawFont5 = new Font("Courier New", 8, FontStyle.Bold);//encabezado
            Font drawFont6 = new Font("Arial", 8, FontStyle.Underline);//encabezado

            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing. 
            PointF drawPoint = new PointF(10.0F, 10.0F);//encabezado
            PointF drawPoint1 = new PointF(00.0F, 40.0F);//codigo de barras            
            PointF drawPoint2 = new PointF(00.0f, 42.0f);//linea1
            PointF drawPoint3 = new PointF(0.0f, 55.0f);//datos1
            PointF drawpoint4 = new PointF(540.0F, 55.0F);
            PointF drawpoint5 = new PointF(0.0F, 68.0F);
            PointF drawpoint6 = new PointF(540.0F, 68.0F);
            PointF drawpoint7 = new PointF(0.0F, 82.0F);
            PointF drawpoint8 = new PointF(540.0F, 82.0F);
            PointF drawpoint9 = new PointF(0.0F, 105.0F);
            PointF drawPoint4 = new PointF(00.0f, 90.0f);//linea2
            PointF drawPoint5 = new PointF(00.0f, 110.0f);//linea3
            drawString2 = "_________________________________________________________________________________________________________________________________________";

            #region
            decimal resulta = 0;
            string mped = "";
            int j;
            for (j = NReg; j < RepPedSur.Rows.Count; j++)
            {
                if (PedObs.Trim().Length == 0)
                    PedObs = RepPedSur.Rows[j].Cells["OBSPED"].Value.ToString();
                if (PedSello.Trim().Length == 0)
                    PedSello = RepPedSur.Rows[j].Cells["PEDSELLO"].Value.ToString();
                //if(PedEmb.Trim().Length ==0) 
                drawString1 = "\tCLIENTE: " + RepPedSur.Rows[j].Cells["NOMCLI"].Value.ToString();
                drawLinea = string.Format("{0}", Convert.ToString(RepPedSur.Rows[j].Cells["PROD"].Value.ToString()));
                drawProd = string.Format("{0}\t{1}", RepPedSur.Rows[j].Cells["PROD"].Value.ToString(), RepPedSur.Rows[j].Cells["NOMINGLES"].Value.ToString());
                drawPedi = string.Format("        |");
                drawcajas = (Convert.ToDecimal(RepPedSur.Rows[j].Cells["CANTPED"].Value)).ToString("###,###");
                drawtaraprox = (Convert.ToDecimal(RepPedSur.Rows[j].Cells["CANTSUR"].Value)).ToString("###,###");
                PointF drawPointlin = new PointF(0.0F, cont);

                if (RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString() != mped)
                {

                    if (sumapedido > 0 || sumasurtido > 0)
                    {
                        //cont = cont + 15;
                        drawLinea = string.Format(" {0:##,###}      |      {1:##,###}", sumapedido, sumasurtido);
                        PointF drawPointTotPed = new PointF(615.0F, cont);
                        e.Graphics.DrawString(drawLinea, drawFont2, drawBrush, drawPointTotPed);
                        PointF drawPointObs = new PointF(10.0F, cont);
                        e.Graphics.DrawString(PedEmb, drawFont4, drawBrush, drawPointObs);
                        //Pen p1 = new Pen(Color.Black, 1);
                        //e.Graphics.DrawRectangle(p1, 490, cont, 100, 25); 
                        cont = cont + 15;
                        string Dettar = "PALLETS: " + Fr1.DetTarimas(mped);
                        drawPointObs = new PointF(10.0F, cont);
                        e.Graphics.DrawString(Dettar, drawFont5, drawBrush, drawPointObs);
                        cont = cont + 20;
                    }
                    PedEmb = Convert.ToString(RepPedSur.Rows[j].Cells["OBSEMB"].Value);
                    String cadena = "PEDIDO " + RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString();
                    PointF drawPointobs = new PointF(100.0F, cont);
                    e.Graphics.DrawString(cadena, drawFont5, drawBrush, drawPointobs);
                    cadena = RepPedSur.Rows[j].Cells["OBSPED"].Value.ToString();
                    drawPointobs = new PointF(300.0F, cont);
                    mped = RepPedSur.Rows[j].Cells["NOPEDIDO"].Value.ToString();
                    e.Graphics.DrawString(cadena, drawFont, drawBrush, drawPointobs);
                    cont = cont + 20;
                    sumapedido = 0;
                    sumasurtido = 0;
                    PedObs = "";
                }

                PointF drawPointProd = new PointF(30.0f, cont);
                PointF drawPointPedi = new PointF(610.0F, cont);
                PointF drawPointcajas = new PointF(640 - (drawcajas.Trim().Length * 6), cont);
                PointF drawPointtar = new PointF(710 - (drawtaraprox.Trim().Length * 6), cont);
                PointF drawpedi = new PointF(30.0f, cont + 13);
                PointF drawfecpedi = new PointF(100.0F, cont + 13);
                PointF drawadua = new PointF(170.0F, cont + 13);

                //e.Graphics.DrawString(drawLinea, drawFont, drawBrush, drawPointlin);
                e.Graphics.DrawString(drawProd, drawFont, drawBrush, drawPointProd);
                e.Graphics.DrawString(drawPedi, drawFont, drawBrush, drawPointPedi);
                e.Graphics.DrawString(drawcajas, drawFont5, drawBrush, drawPointcajas);
                e.Graphics.DrawString(drawtaraprox, drawFont5, drawBrush, drawPointtar);
                PointF drawPoint7 = new PointF(0.0F, cont + 2);
                //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint7);
                Pen p1 = new Pen(Color.Black, 1);
                e.Graphics.DrawLine(p1, 100, cont + 15, 750, cont + 15);
                cont = cont + 15;
                cont1 = cont1 + 15;
                cont2 = cont2 + 15;
                sumapedido = sumapedido + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTPED"].Value.ToString());
                sumasurtido = sumasurtido + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTSUR"].Value.ToString());
                sumtotped = sumtotped + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTPED"].Value.ToString());
                sumtotsur = sumtotsur + Convert.ToInt32(RepPedSur.Rows[j].Cells["CANTSUR"].Value.ToString());
                enteros = Math.Truncate(resulta);
                sumacomp = sumacomp + enteros;
                decimales = enteros - resulta;
                sumasplit = sumasplit + decimales;
                if (cont > 900)
                {
                    e.HasMorePages = true;
                    NReg = j;
                    break;
                }
                if (j == TotReg)
                    e.HasMorePages = false;

            }//for

            if (sumapedido > 0 || sumasurtido > 0)
            {
                //cont = cont + 15;
                drawLinea = string.Format(" {0:##,###}      |      {1:##,###}", sumapedido, sumasurtido);
                PointF drawPointTotPed = new PointF(615.0F, cont);
                e.Graphics.DrawString(drawLinea, drawFont2, drawBrush, drawPointTotPed);
                PointF drawPointObs = new PointF(10.0F, cont);
                e.Graphics.DrawString(PedEmb, drawFont4, drawBrush, drawPointObs);
                cont = cont + 15;
                string Dettar = "PALLETS: " + Fr1.DetTarimas(mped);
                drawPointObs = new PointF(10.0F, cont);
                e.Graphics.DrawString(Dettar, drawFont5, drawBrush, drawPointObs);
            }

            //for (i = RegistroEmb ; i <= RegistroEmb; i++)
            //foreach (DataRow row1 in Embarques.Select("NO_TRAILER = '" + RegistroEmb + "'"))
            foreach (DataRow row1 in Embarque.Rows)
            {
                if (Convert.ToString(row1["NO_TRAILER"]) == LblPlaca.Text.Trim())
                {
                    String drawString = "\t\t\t\t\t\tLOADING REPORT\r\n ";
                    //drawString3 = "\tTRANSPORTISTA: " + Convert.ToString(DGEmbCap.Rows[i].Cells["TRANS"].Value) + "  " + Fn_TraeNomTra(Convert.ToString(DGEmbCap.Rows[i].Cells["TRANS"].Value));
                    //drawString4 = "FECHA: " + Convert.ToDateTime(DGEmbCap.Rows[i].Cells["Fecha"].Value).ToString("dd/MM/yyyy") + "\r\n";
                    //drawStringDest = "\tDESTINO: " + DGEmbCap.Rows[i].Cells["DESTINO"].Value.ToString() + "\r\n";
                    //drawstring8 = "\t" + sub_cli.Trim() + "";
                    //drawstring9 = "HORA INICIO DE CARGA: " + Convert.ToString(DGEmbCap.CurrentRow.Cells["HRINI"].Value) + "\r\n";
                    //drawstring10 = "\tPLACA: " + Convert.ToString(DGEmbCap.CurrentRow.Cells["TRAILER"].Value);
                    //drawstring11 = "HORA FINAL DE CARGA " + Convert.ToString(DGEmbCap.CurrentRow.Cells["HRFIN"].Value) + " \r\n\r\n";
                    //drawstring12 = "      CODIGO            PRODUCTO \t\t\t\t\t |  PEDIDO  |  CARGADO  |  OBSERVACIONES \r\n";
                    drawString3 = "\tTRUCK LINE: " + Convert.ToString(row1["TRANSPORTE"]) + "  " + Fr1.Fn_TraeNomTra(Convert.ToString(row1["TRANSPORTE"]));
                    drawString4 = "DATE: " + Convert.ToDateTime(row1["Fecha"]).ToString("dd/MM/yyyy") + "\r\n";
                    drawStringDest = "\tDESTINATION: " + row1["DESTINO"].ToString() + "\r\n";
                    drawstring8 = "\t" + sub_cli.Trim() + "";
                    drawstring9 = "LOADING START: " + Convert.ToString(row1["HORAINI"]).Substring(11, 13) + "\r\n";
                    drawstring10 = "\tPLACA: " + Convert.ToString(row1["NO_TRAILER"]);
                    drawstring11 = "  LOAD FINISH: " + Convert.ToString(row1["HORAFIN"]).Substring(11, 13) + " \r\n\r\n";
                    drawstring12 = "      PRODUCT           CODE     \t\t\t\t\t\t\t\t |  ORDERED  |  LOADED  | \r\n";

                    e.Graphics.DrawString(drawString1, drawFont3, drawBrush, drawPoint1);//código de barras            
                    e.Graphics.DrawString(drawString, drawFont2, drawBrush, drawPoint);//encabezado            
                    //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint2);//linea1
                    e.Graphics.DrawString(drawString3, drawFont3, drawBrush, drawPoint3);//datos1
                    e.Graphics.DrawString(drawString4, drawFont3, drawBrush, drawpoint4);//
                    e.Graphics.DrawString(drawStringDest, drawFont3, drawBrush, drawpoint5);//
                    e.Graphics.DrawString(drawstring8, drawFont3, drawBrush, drawpoint5);//
                    e.Graphics.DrawString(drawstring9, drawFont3, drawBrush, drawpoint6);//
                    e.Graphics.DrawString(drawstring10, drawFont3, drawBrush, drawpoint7);//
                    e.Graphics.DrawString(drawstring11, drawFont3, drawBrush, drawpoint8);//
                    e.Graphics.DrawString(drawstring12, drawFont5, drawBrush, drawpoint9);//
                    //e.Graphics.DrawString(drawString2, drawFont, drawBrush, drawPoint4);//linea2
                    e.Graphics.DrawString(drawString2, drawFont3, drawBrush, drawPoint5);//linea3                       

                    drawobs = "          TOTAL LOADING:          " + string.Format("{0:##,###}  |  {1:##,###}", sumtotped, sumtotsur);
                    PointF drawobser = new PointF(305.00f, 995.0f);//txt observaciones
                    Pen p = new Pen(Color.Black, 1);
                    e.Graphics.DrawRectangle(p, 475, 990, 120, 25);
                    e.Graphics.DrawString(drawobs, drawFont2, drawBrush, drawobser);//txt observaciones

                    String drawString5 = "LOAD : \t\t\t\t\t\t\t  DESTINATION:  \t\t\t\t\t\t\t  SEAL: \r\n";
                    String drawString6 = "                                               DRIVER NAME: \t\t\t\t\t SIGNATURE OF DRIVER: _____________\r\n";
                    String drawString7 = "                                               LOADING START:  \t\t\t\t  LOADING FINISH : ";
                    PointF drawstring5 = new PointF(10.00f, 1020.0f);//string5
                    PointF drawstring6 = new PointF(10.00f, 1035.0f);//string6
                    PointF drawstring7 = new PointF(10.00f, 1050.0f);//string7            
                    e.Graphics.DrawString(drawString5, drawFont4, drawBrush, drawstring5);//string5
                    e.Graphics.DrawString(drawString6, drawFont4, drawBrush, drawstring6);//string6
                    e.Graphics.DrawString(drawString7, drawFont4, drawBrush, drawstring7);//string7
                    PointF drawryandig = new PointF(553.0f, 1000.0f);//ryan

                    PointF drawchofername = new PointF(505.0f, 1035.0f);//nombre chofer
                    string dat = row1["RESPONSABLE"].ToString();
                    PointF posxy = new PointF(80, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = row1["DESTINO"].ToString();
                    posxy = new PointF(470, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = PedSello.Trim();
                    posxy = new PointF(730, 1020);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = row1["CHOFER"].ToString().Trim();
                    posxy = new PointF(385, 1035);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = Convert.ToString(row1["HORAINI"]).Substring(11, 13);
                    posxy = new PointF(400, 1050);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                    dat = Convert.ToString(row1["HORAFIN"]).Substring(11, 13);
                    posxy = new PointF(650, 1050);
                    e.Graphics.DrawString(dat, drawFont6, drawBrush, posxy);
                }
            }

            //e.Graphics.DrawString(drawchofer, drawFont, drawBrush, drawchofername);//nombre chofer

            //cont1 = 160;
            #endregion
            //sumataraprox = sumacomp = sumasplit = decimales = enteros = 0;
            sumapedido = 0;
            //e.HasMorePages = false;
            thisConnecion.Close();

        }

        void ReporteExcelDetLotes(string Archivo)
        {
            string FILE_NAME = @"C:\REPORTES\" + LblPlaca.Text.Trim() + "_DIA_" + Convert.ToDateTime(Program.MyGlobal.PubFecEmb).ToString("yyyyMMdd") + ".TXT";
            System.IO.StreamWriter sw = System.IO.File.CreateText(FILE_NAME);

            Excel.Range r;
            //62164
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add();

            //int count = MiDataGrid.ColumnCount;
            excel.Cells[1, 1] = "Comercializadora GAB S.A. de C.V.";
            excel.Range[excel.Cells[1, 1], excel.Cells[1, 6]].Merge();
            excel.Cells[1, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[2, 1] = "Detalle de lotes del Embarque: " + LblPlaca.Text;
            excel.Range[excel.Cells[2, 1], excel.Cells[2, 6]].Merge();
            excel.Cells[2, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            excel.Cells[3, 1] = "";
            excel.Range[excel.Cells[3, 1], excel.Cells[3, 6]].Merge();
            excel.Cells[3, 1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            r = excel.Range[excel.Cells[3, 1], excel.Cells[3, 7]];
            r.Font.Size = 13;
            //r = excelWorksheet.get_Range("A1", "R1");
            //r.Font.Bold = true;
            excel.Cells[5, 2] = "Del día : " + Program.MyGlobal.PubFecEmb.ToString();
            r = excel.get_Range("A1", "H5");
            r.Font.Bold = true;

            excel.Columns[9].NumberFormat = "@"; // Columna FEC_CAD

            //excel.Cells[6, 4] = "PEDIDO"; excel.Cells[6, 5] = "SURTIDO";
            if (LblCli.Text.Contains("POM1") || LblCli.Text.Contains("POM2"))
            {
                excel.Cells[7, 1] = "ORDEN DE VENTA"; excel.Cells[7, 2] = "CVE. PRODUCTO"; excel.Cells[7, 3] = "CLAVE DEO"; excel.Cells[7, 4] = "NOMBRE"; excel.Cells[7, 5] = "FOLIO"; excel.Cells[7, 6] = "CAJAS";
                excel.Cells[7, 7] = "COSTO"; excel.Cells[7, 8] = "FEC. CADUCIDAD"; excel.Cells[7, 9] = "CAD";
            }
            else
            { excel.Cells[7, 1] = "CVE. PRODUCTO"; excel.Cells[7, 2] = "NOMBRE"; excel.Cells[7, 3] = "FOLIO"; excel.Cells[7, 4] = "LOTE"; excel.Cells[7, 5] = "CAJAS"; excel.Cells[7, 6] = "FEC. CADUCIDAD"; excel.Cells[7, 7] = "CAD"; }
            r = excel.Range[excel.Cells[7, 1], excel.Cells[7, 9]];
            r.Font.Bold = true;
            //for (i = 0; i < dataGridView1.Rows.Count; i++)
            //{
            //    dgc = dataGridView1.Rows[i].Cells[0];
            //    celda = ((String)dgc.Value) + "\r\n";
            //    textBox1.Text += celda.Replace(".", ",");
            //}

            //agrega las filas a excel
            sw.WriteLine("PEDIDO CVEPROD    " + "NOMBRE".PadRight(50) + " FOLIO  " + "LOTE".PadRight(25) + " CAJAS FEC.CAD");
            DataTable DetaLotes = new DataTable();
            DetaLotes = DETALLE.Copy();
            DetaLotes.DefaultView.Sort = "EMB_FOLIO, PROD_NOMBRE ASC";
            DetaLotes = DetaLotes.DefaultView.ToTable(true);

            int i = 8;
            int avance = 0;
            string Mped = "";
            //string mfec = "";  
            AVANCE.Text = avance + " de " + DetaLotes.Rows.Count;
            AVANCE.Visible = true;
            foreach (DataRow row in DetaLotes.Rows)
            {
                if (row["emb_folio"].ToString().Trim() != Mped)
                {
                    thisConnecion.Open();
                    string bd = (row["EMB_TIPO"].ToString().Trim() == "NAL") ? "tb_mstr_pedidos_nal" : "tb_mstr_pedidos_exp";
                    Mped = row["emb_folio"].ToString().Trim();
                    string Cadena = "SELECT cve_subcli FROM " + bd + " WHERE PDN_FOLIO = '" + Mped + "'";
                    SqlCommand cmd;
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    string SubCli = Convert.ToString(cmd.ExecuteScalar());
                    thisConnecion.Close();
                    PedidosEmbarque = PedidosEmbarque + Mped + ", ";
                    excel.Cells[i, 3] = "Cliente: " + SubCli;
                    if (!LblCli.Text.Contains("POM1") && !LblCli.Text.Contains("POM2"))
                    {
                        excel.Cells[i, 2] = "ORDEN DE VENTA : " + Mped;

                        r = excel.Range[excel.Cells[i, 2], excel.Cells[i, 3]];
                        r.Font.Bold = true;
                    }
                    //mfec = row["HORA_TRAILER"].ToString().Substring(6,4)+row["HORA_TRAILER"].ToString().Substring(3,2)+row["HORA_TRAILER"].ToString().Substring(0,2);
                    i++;
                }
                string mLOT = "";
                string CAD = "";
                //emb_folio, prod_clave, emb_tipo, no_lote, cajas, seccion, tempe, tarima, TARIMA_F, Nom_prod, TIPO_REC, transfer, ESTATUS, FEC_CAD, FECHACAD, FECHACAP, OPCAP, ID_TARIMA
                if (!LblCli.Text.Contains("POM1") && !LblCli.Text.Contains("POM2"))
                {
                    r.Font.Bold = true;
                    excel.Cells[i, 1] = row["PROD_CLAVE"].ToString();
                    excel.Cells[i, 2] = row["PROD_NOMBRE"].ToString();
                    excel.Cells[i, 3] = row["RECIBO"].ToString();
                    //string TarFin = (Convert.ToDecimal(row["TARIMA_F"].ToString())>0) ? row["TARIMA_F"].ToString().Trim().PadLeft(2,'0') : "";
                    excel.Cells[i, 4] = row["NO_LOTE"].ToString().Trim(); //+row["PROD_CLAVE"].ToString().Trim()+row["TARIMA"].ToString().Trim().PadLeft(2,'0')+ TarFin ;
                    excel.Cells[i, 5] = row["CAJAS"].ToString().Trim(); //+row["PROD_CLAVE"].ToString().Trim()+row["TARIMA"].ToString().Trim().PadLeft(2,'0')+ TarFin ;
                    excel.Cells[i, 6] = (row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "";
                    mLOT = (row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "";
                    excel.Cells[i, 7] = FormatearFecha((row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "");
                    CAD = FormatearFecha((row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "");
                }
                else   // informacion para pom
                {
                    r.Font.Bold = true;
                    excel.Cells[i, 1] = Mped;
                    excel.Cells[i, 2] = row["PROD_CLAVE"].ToString();
                    foreach (DataRow Row in CodigosMty.Select("clave_prod = '" + row["PROD_CLAVE"].ToString() + "'"))
                    {
                        excel.Cells[i, 3] = Row["clave_mac"].ToString();
                    }
                    excel.Cells[i, 4] = row["PROD_NOMBRE"].ToString();
                    excel.Cells[i, 5] = row["RECIBO"].ToString();
                    //string TarFin = (Convert.ToDecimal(row["TARIMA_F"].ToString())>0) ? row["TARIMA_F"].ToString().Trim().PadLeft(2,'0') : "";
                    excel.Cells[i, 6] = row["CAJAS"].ToString().Trim(); //+row["PROD_CLAVE"].ToString().Trim()+row["TARIMA"].ToString().Trim().PadLeft(2,'0')+ TarFin ;
                    foreach (DataRow row1 in Peddet.Select("prod_clave = '" + row["PROD_CLAVE"].ToString().Trim() + "' and pdn_folio = '" + Mped + "'"))
                        excel.Cells[i, 7] = row1["pdn_precio_mn"].ToString().Trim(); //+row["PROD_CLAVE"].ToString().Trim()+row["TARIMA"].ToString().Trim().PadLeft(2,'0')+ TarFin ;
                    excel.Cells[i, 8] = (row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "";
                    mLOT = (row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "";
                    excel.Cells[i, 9] = FormatearFecha((row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "");
                    CAD = FormatearFecha((row["FEC_CAD"].ToString().Trim().Length > 0) ? row["FEC_CAD"].ToString().Trim() : "");
                }
                sw.WriteLine(row["emb_folio"].ToString().Trim().PadRight(6) + " " + row["PROD_CLAVE"].ToString().PadRight(10) + " " + row["PROD_NOMBRE"].ToString().Trim().PadRight(50) + " " +
                             row["RECIBO"].ToString().Trim().PadRight(6) + " " + row["NO_LOTE"].ToString().Trim().PadRight(25) + " " + row["CAJAS"].ToString().Trim().PadRight(5) + " " + mLOT + " " + CAD);
                i++;
                avance++;
                AVANCE.Text = avance + " de " + DetaLotes.Rows.Count;
            }
            excel.Columns.AutoFit();
            excel.Rows.AutoFit();
            excel.ActiveWorkbook.Saved = true;
            excel.DisplayAlerts = false;
            excel.ActiveWorkbook.SaveAs(Archivo);
            excel.ActiveWorkbook.Close();
            excel.Application.Quit();
            sw.Close();
            //MessageBox.Show("Archivo Generado Con Exito!!!!"+System.Environment.NewLine + System.Environment.NewLine + Arch, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //excel.Visible = true;

        }
        #region METODOS PARA FORMATEAR 
        public static string FormatearFecha(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            input = input.Trim();

            int añoActual = DateTime.Now.Year;

            // Caso 1: cadenas largas -> últimos 5 caracteres
            if (input.Length > 5)
            {
                string ultimos5 = input.Substring(input.Length - 5).ToUpper(); // Ej: OCT04
                string mes = ultimos5.Substring(0, 3);
                string dia = ultimos5.Substring(3, 2);

                return ParsearFecha(dia, mes, añoActual);
            }

            // Caso 2: cadenas cortas -> dd-MMM o ddMMM
            if (Regex.IsMatch(input, @"^\d{2}-[A-Za-z]{3}$"))
            {
                input = input.Replace("-", "").ToUpper(); // 07-oct -> 07OCT
            }

            if (Regex.IsMatch(input, @"^\d{2}[A-Za-z]{3}$"))
            {
                string dia = input.Substring(0, 2);
                string mes = input.Substring(2, 3).ToUpper();

                return ParsearFecha(dia, mes, añoActual);
            }

            return "";
        }
        private static string ParsearFecha(string dia, string mes, int año)
        {
            try
            {
                DateTime dt = DateTime.ParseExact(
                    $"{dia}{mes}{año}",
                    "ddMMMyyyy",
                    CultureInfo.InvariantCulture
                );
                return dt.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }
        #endregion

        private void BtnImpCfor_Click(object sender, EventArgs e)
        {
            // para mandarlo directamente a la Impresora
            ConFormat = "S";
            if (MessageBox.Show("Imprimir Documento de Distribucion de Embarques?", "Embarques", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                PrintDocument pd = new PrintDocument();
                pd.PrintPage += new PrintPageEventHandler(this.PrintCForm_PrintPage);
                PrintDialog printDialog1 = new PrintDialog();
                printDialog1.Document = pd;
                DialogResult result = printDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    pd.Print();
                    pd.Dispose();
                }
            }

            //pd.Print();
            if (MessageBox.Show("Imprimir Documento de Relacion de Splits?", "Embarques", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                LlenaSplit();
                if (DetSplit.Rows.Count > 0)
                {
                    TotReg = DetSplit.Rows.Count;
                    NReg = 0;
                    PrintDocument pdSplit1 = new PrintDocument();
                    pdSplit1.PrintPage += new PrintPageEventHandler(this.PrintSplit_PrintPage);
                    PrintDialog printD = new PrintDialog();
                    printD.Document = pdSplit1;
                    DialogResult result = printD.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        pdSplit1.Print();
                        pdSplit1.Dispose();
                    }
                    //pdSplit1.Print();

                }
            }
        }

        private void BtnDetLotes_Click(object sender, EventArgs e)
        {
            LblGenerar.Visible = true;
            RepPedSur.Rows.Clear();
            SinSurtir.Rows.Clear();
            string Vendedor = "";
            string usuario = "", Elaboro = "";
            //for (i = 0; i < DGDetEmbCap.Rows.Count; i++)
            foreach (DataRow Row in DetEmb.Rows)
            {
                thisConnecion.Open(); //DBGAB
                DataTable Surtido = new DataTable();
                string mPED = "", mTip = "", mObsemb = "";
                mPED = Convert.ToString(Row["emb_folio"]);
                mTip = Convert.ToString(Row["emb_tipo"]);
                mObsemb = Convert.ToString(Row["emb_obs"]);
                string Nomcli = LblCli.Text + " " + Fr1.Fn_TraeNomCli(LblCli.Text);
                string Cadena = "SELECT A.*, B.prod_nomb_ingles, B.PROD_NOMBRE FROM TB_PED_EMBARQUE A, TB_CAT_PRODUCTO B WHERE EMB_FOLIO = '" + mPED + "' AND NALEXP = '" + mTip + "' AND A.PROD_CLAVE = B.PROD_CLAVE";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "PEDSUR");
                Surtido = ds.Tables["PEDSUR"];
                Cadena = "SELECT EMB_FOLIO, prod_clave, SUM(CAJAS) as cajas  FROM tb_det_embarque " +
                         "WHERE emb_folio = '" + mPED + "' AND Estatus = 'A' GROUP BY emb_folio, prod_clave ";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "CARGO");
                DataTable Cargo = ds.Tables["CARGO"];
                string BD = "Tb_MSTR_PEDIDOS_NAL";
                thisConnecion.Close(); //DBGAB
                if (mTip == "EXP")
                    BD = "Tb_MSTR_PEDIDOS_EXP";
                thisConnecion.Open();
                //SqlCommand cmnd2;
                Int32 TotS = 0, TotP = 0;
                if (mTip != "TRA")
                    Cadena = "SELECT A.PROD_CLAVE,A.PDN_NUM_UNIDADES,B.PROD_NOMBRE,B.prod_nomb_ingles,C.PDN_OBSERVACION,C.PDN_SELLO, c.pdn_elaboro, a.pdn_precio_mn, a.pdn_folio FROM TB_DET_PEDIDOS A, TB_CAT_PRODUCTO B, " + BD + " C  WHERE A.PDN_FOLIO = '" + mPED + "' AND A.PDN_TIPO = '" + mTip + "' AND A.PROD_CLAVE = B.PROD_CLAVE AND A.PDN_FOLIO = C.PDN_FOLIO  ORDER BY B.PROD_NOMBRE";
                else
                    Cadena = "SELECT A.PROD_CLAVE,A.EMB_UNIDADES AS PDN_NUM_UNIDADES,B.PROD_NOMBRE,B.prod_nomb_ingles,C.EMB_OBSERVACION AS PDN_OBSERVACION,C.TALON_EMBARQUE AS PDN_SELLO, '' AS pdn_elaboro FROM TB_DET_ORDENES_EMB A, TB_CAT_PRODUCTO B, TB_MSTR_ORDENES_EMB C WHERE A.EMB_FOLIO = '" + mPED + "' AND A.EMB_TIPO = 'MAQ' AND A.PROD_CLAVE = B.PROD_CLAVE AND A.EMB_FOLIO = C.EMB_FOLIO ORDER BY B.PROD_NOMBRE";
                //cmnd2 = thisConnecion.CreateCommand();
                //cmnd2.CommandText = Cadena;
                //SqlDataReader Ped;
                //Ped = cmnd2.ExecuteReader();
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion); //DBGAB
                da.Fill(ds, "PED");
                Ped = ds.Tables["PED"];
                if (Peddet.Rows.Count == 0)
                    Peddet = Ped.Clone();
                foreach (DataRow dr in Ped.Rows)
                    Peddet.Rows.Add(dr.ItemArray);

                Cadena = "SELECT a.emb_obs, b.responsable, isnull(b.enviodif,'N') as EnvioDif, b.hora_trailer FROM tb_mstr_embarque a, tb_mstr_trailer b " +
                         " WHERE a.emb_folio = '" + mPED + "' AND a.nalexp = '" + mTip + "' AND a.no_trailer = b.no_trailer AND a.hora_trailer = b.hora_trailer ";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion);
                da.Fill(ds, "Causa");
                DataTable Causa = ds.Tables["Causa"];
                Cadena = "Select usu_login, usu_email, usu_nombre From tb_cat_usuarios";
                ds = new DataSet();
                da = new SqlDataAdapter(Cadena, thisConnecion);
                da.Fill(ds, "usu");
                DataTable usu = ds.Tables["usu"];
                if (LblCli.Text.Contains("POM1") || LblCli.Text.Contains("POM2"))
                {
                    Cadena = "Select clave_prod, clave_mac from tb_cod_recibos where cliente = 'POM'";
                    da = new SqlDataAdapter(Cadena, thisConnecion);
                    ds = new DataSet();
                    da.Fill(ds, "CodigosMty");
                    CodigosMty = ds.Tables["CodigosMty"];
                }
                //while (Ped.Read())
                //{
                //    int T = 0;
                //    foreach (DataRow row in Surtido.Select("prod_clave = '" + Ped["prod_CLAVE"].ToString() + "'"))
                //    {
                //        T = Convert.ToInt32(row["cant_sur"]);
                //        TotS = TotS + Convert.ToInt32(row["cant_sur"]);
                //    }
                //    TotP = TotP + Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]);
                //    string Nom_Ingles = Ped["prod_nomb_ingles"].ToString().Substring(200, 50).Trim() + " " +Ped["prod_nomb_ingles"].ToString().Substring(100, 50).Trim();
                //    RepPedSur.Rows.Add(mPED, Ped["PROD_CLAVE"].ToString(), Ped["PROD_NOMBRE"].ToString(), Convert.ToInt32(Ped["PDN_NUM_UNIDADES"]), T, Ped["PDN_OBSERVACION"].ToString(), Ped["PDN_SELLO"].ToString(), mObsemb, Nomcli, Nom_Ingles);
                //}
                usuario = "";
                foreach (DataRow row in Surtido.Rows)
                {
                    int T = 0, xSur = 0; ;
                    string Hay = "N", Obs = "", Sello = "";
                    string Nom_Ingles = row["prod_nomb_ingles"].ToString().Substring(200, 50).Trim() + " " + row["prod_nomb_ingles"].ToString().Substring(100, 50).Trim();
                    foreach (DataRow Row1 in Cargo.Select("prod_clave='" + row["prod_clave"] + "'"))
                    {
                        row["cant_sur"] = Convert.ToInt32(Row1["cajas"]);
                        xSur = Convert.ToInt32(Row1["cajas"]);
                    }
                    foreach (DataRow Row1 in Ped.Select("prod_clave = '" + row["prod_CLAVE"].ToString() + "'"))
                    {
                        T = Convert.ToInt32(Row1["PDN_NUM_UNIDADES"]);
                        TotS = TotS + Convert.ToInt32(Row1["PDN_NUM_UNIDADES"]);
                        RepPedSur.Rows.Add(mPED, Row1["PROD_CLAVE"].ToString(), Row1["PROD_NOMBRE"].ToString(), T, Convert.ToInt32(row["cant_sur"]), Row1["PDN_OBSERVACION"].ToString(), Row1["PDN_SELLO"].ToString(), mObsemb, Nomcli, Nom_Ingles);
                        Obs = Row1["PDN_OBSERVACION"].ToString().Trim();
                        Sello = Row1["PDN_SELLO"].ToString().Trim();
                        usuario = Row1["pdn_elaboro"].ToString().Trim();
                        Hay = "S";
                    }
                    foreach (DataRow rows in usu.Select("usu_login = '" + usuario + "'"))
                        Elaboro = rows["usu_nombre"].ToString().Trim() + " " + rows["usu_email"].ToString().Trim();
                    string mResp = "", mCau = "";
                    if (xSur != T)
                    {
                        foreach (DataRow rows in Causa.Rows)
                        {
                            mResp = rows["responsable"].ToString().Trim();
                            mCau = rows["emb_obs"].ToString().Trim();
                            LblEnvDif.Text = rows["EnvioDif"].ToString().Trim();
                        }
                        SinSurtir.Rows.Add(mPED, Nomcli, row["PROD_NOMBRE"].ToString(), T.ToString("##0"), xSur.ToString("##0"), (T - xSur).ToString("##0"), mResp, mCau);
                        foreach (DataRow rows in usu.Select("usu_login = '" + usuario + "'"))
                        {
                            if (Vendedor.IndexOf(rows["usu_email"].ToString().Trim()) == -1)
                            {
                                Vendedor += " ;" + rows["usu_email"].ToString().Trim();
                                //Elaboro = rows["usu_email"].ToString().Trim();
                            }
                        }
                    }
                    if (Hay == "N")
                        RepPedSur.Rows.Add(mPED, row["PROD_CLAVE"].ToString(), row["PROD_NOMBRE"].ToString(), 0, Convert.ToInt32(row["cant_sur"]), Obs, Sello, mObsemb, Nomcli, Nom_Ingles);
                }
                thisConnecion.Close();
            }
            string mfec = Program.MyGlobal.PubFecEmb.ToString().Substring(6, 4) + Program.MyGlobal.PubFecEmb.ToString().Substring(3, 2) + Program.MyGlobal.PubFecEmb.ToString().Substring(0, 2);
            string Arch = "C:\\REPORTES\\EMB" + LblPlaca.Text.Trim() + "_DIA_" + mfec + ".XLS";
            ReporteExcelDetLotes(Arch);
            MessageBox.Show("Archivo Creado en " + Arch, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            System.Diagnostics.Process.Start(@"C:\reportes");
            LblGenerar.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ExportaLotesUERP("642512", "61");
        }

        private void ExportaLotesUERP(string Pedido, string CveEmp)
        {
            Boolean Cerrar = false;
            if (thisConnecion.State == ConnectionState.Closed)
            {
                Cerrar = true;
                thisConnecion.Open();
            }
            ThisConnecUERP.Open();
            string Cadena = "SELECT A.emb_folio, A.no_lote, A.prod_clave, A.cajas, A.tarima, A.tipo_rec, A.recibo, B.prod_nombre, B.fecha_cad, B.pti_fecha, C.fechacad " +
                            "FROM tb_det_embarque A " +
                            "LEFT JOIN tb_det_trazabilidad B ON A.recibo = B.recibo AND A.prod_clave = B.prod_clave AND A.tarima = B.tarima " +
                            "LEFT JOIN tb_det_eti_final C ON A.recibo = C.folio  AND A.prod_clave = C.cve_prod  AND A.tarima = C.tarima " +
                            "WHERE a.emb_folio = '" + Pedido + "' AND A.Estatus = 'A' " +
                            "ORDER BY a.prod_clave, a.recibo, a.tarima ";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, thisConnecion);
            DataSet ds = new DataSet();
            da.Fill(ds, "Lotes");
            DataTable Lotes = ds.Tables["Lotes"];
            Cadena = "SELECT fcn_folio, fcn_fecha, cnte_clave, fcn_tipo FROM tb_mstr_facturas_nal WHERE pdn_folio = '" + Pedido + "' and fcn_estatus != 'C'";
            da = new SqlDataAdapter(Cadena, thisConnecion);
            ds = new DataSet();
            da.Fill(ds, "ConEmb");
            DataTable ConEmb = ds.Tables["ConEmb"];
            string FolFac = "", FecFac = "", CliFac = "", TipFac = "";
            foreach (DataRow row in ConEmb.Rows)
            {
                FolFac = row["fcn_folio"].ToString();
                FecFac = Convert.ToDateTime(row["fcn_fecha"]).ToShortDateString();
                CliFac = row["cnte_clave"].ToString();
                TipFac = row["fcn_tipo"].ToString();
            }
            Cadena = "Select * from IPOLINCO a, IPOLOTES b " +
                     "Where a.ILnIDCom = '" + FolFac + "' and ILnEmp = '" + CveEmp + "' and a.ILnNume = b.ILnNume ";
            SqlCommand cmd1 = new SqlCommand(Cadena, ThisConnecUERP);
            Int32 Regs = Convert.ToInt32(cmd1.ExecuteNonQuery());
            da = new SqlDataAdapter(Cadena, ThisConnecUERP);
            ds = new DataSet();
            da.Fill(ds, "Regs");
            DataTable Regis = ds.Tables["Regs"];

            if (Regs > 0 || Regis.Rows.Count > 0) // YA SE GRABO LA INFORMACION DE LOS LOTES
            {
                if (Cerrar)
                    thisConnecion.Close();
                ThisConnecUERP.Close();
                GeneraIpolote(FolFac, CveEmp, Pedido);
                return;
            }

            Cadena = "Select ILnNume, ILnIDCom, ILnCodArt, ILnCanti, ILnAlma, ILnIVA, ILnPrec, ILnTotal, ILnEmp " +
                     "From IPOLINCO where ILnIDCom = '" + FolFac + "' ";
            da = new SqlDataAdapter(Cadena, ThisConnecUERP);
            ds = new DataSet();
            da.Fill(ds, "IPOLINCO");
            DataTable IPOLINCO = ds.Tables["IPOLINCO"];
            Cadena = "Select ILnNume, ILinLote, ICanLote, IFchLote, IVtoLote, IEmpLote, IRecibo, IArtCod, ITarima " +
                     "From IPOLOTES where ILnIDCom = '" + FolFac + "' ";
            foreach (DataRow row in IPOLINCO.Rows)
            {
                string CvePro = row["ILnCodArt"].ToString();
                string Linea = row["ILnNume"].ToString();
                string Emp = row["ILnEmp"].ToString();
                foreach (DataRow Row in Lotes.Select("prod_clave = '" + CvePro + "'"))
                {
                    string Lot = Row["recibo"].ToString().Trim().PadLeft(6, '0') + CvePro.Trim() + Row["Tarima"].ToString().Trim().PadLeft(3, '0');
                    string FeCad = "", CFec = "S";
                    if (Row["tipo_rec"].ToString().Trim() == "PTP")
                    {
                        FeCad = Row["fechacad"].ToString().Substring(6, 2) + "/" + Row["fechacad"].ToString().Substring(4, 2) + "/" + Row["fechacad"].ToString().Substring(0, 4);
                        if (Convert.ToString(Row["fechacad"]).Trim() == "") // NO tiene Fecha de Caducidad
                        {
                            FeCad = Row["pti_FEcha"].ToString();
                            CFec = "N";
                        }
                    }
                    if (Row["tipo_rec"].ToString().Trim() == "PTC")
                    {
                        FeCad = Row["fecha_cad"].ToString();
                        if (Convert.ToString(Row["fecha_cad"]).Trim() == "") // NO tiene Fecha de Caducidad
                        {
                            FeCad = Row["pti_FEcha"].ToString();
                            CFec = "N";
                        }
                    }
                    if (CFec == "N") // Se le Asigna una Fecha de Caducidad
                    {
                        string Mnom = Row["prod_nombre"].ToString();
                        if (Mnom.Contains("BETABEL"))
                            FeCad = Convert.ToDateTime(FeCad).AddDays(60).ToShortDateString();
                        else
                             if (Mnom.Contains("AJO"))
                            FeCad = Convert.ToDateTime(FeCad).AddDays(180).ToShortDateString();
                        else
                             if (Mnom.Contains("ADEREZO") || Mnom.Contains("VINAGRETA") || Mnom.Contains("QUESO"))
                            FeCad = Convert.ToDateTime(FeCad).AddDays(90).ToShortDateString();
                        else
                            FeCad = Convert.ToDateTime(FeCad).AddDays(14).ToShortDateString();

                    }
                    Cadena = "Select ILnNume From IPOLOTES Where IRecibo = '" + Row["Recibo"].ToString() + "' and IArtCod = '" + CvePro + "' " +
                             "and  ITarima = '" + Row["Tarima"].ToString() + "' and ILnNume = '" + Linea + "'";
                    SqlCommand cmd = new SqlCommand(Cadena, ThisConnecUERP);
                    string Reg = Convert.ToString(cmd.ExecuteScalar());
                    if (Reg == "") // NO Existe el Registro
                        Cadena = "Insert into IPOLOTES(ILnNume, ILinLote, ICanLote, IFchLote, IVtoLote, IEmpLote, IRecibo, IArtCod, ITarima)" +
                               "Values('" + Linea + "','" + Lot + "','" + Row["Cajas"].ToString() + "','" + Convert.ToDateTime(Row["pti_FEcha"]).ToShortDateString() + "','" +
                               FeCad + "','" + Emp + "','" + Row["Recibo"].ToString() + "','" + CvePro + "','" + Row["Tarima"].ToString() + "')";
                    else // Ya Existe Actualizo la Cantidad de Cajas
                        Cadena = "Update IPOLOTES Set ICanLote = ICanLote + '" + Row["Cajas"].ToString() + "' " +
                                 "Where IRecibo = '" + Row["Recibo"].ToString() + "' and IArtCod = '" + CvePro + "' " +
                                 "and  ITarima = '" + Row["Tarima"].ToString() + "' and ILnNume = '" + Linea + "'";
                    cmd = new SqlCommand(Cadena, ThisConnecUERP);
                    cmd.ExecuteNonQuery();
                }
            }
            GeneraIpolote(FolFac, CveEmp, Pedido);
            if (Cerrar)
                thisConnecion.Close();
            ThisConnecUERP.Close();
        }

        private void GeneraIpolote(string Fac, string Emp, string Ped)
        {
            string Cadena = "Select b.* from IPOLINCO a, IPOLOTES b " +
                     "Where a.ILnIDCom = '" + Fac + "' and ILnEmp = '" + Emp + "' and a.ILnNume = b.ILnNume ";
            SqlDataAdapter da = new SqlDataAdapter(Cadena, ThisConnecUERP);
            DataSet ds = new DataSet();
            da.Fill(ds, "Ipolotes");
            string Archivo = @"C:\REPORTES\Ipolotes_Fac_" + Fac  + "_Pedido _" + Ped ;
            Excel.Range r;
            //62164
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Application.Workbooks.Add();
            //ILnNume, ILinLote, ICanLote, IFchLote, IVtoLote, IEmpLote, IRecibo, IArtCod, ITarima
            excel.Cells[1, 1] = "ILnNume"; excel.Cells[1, 2] = "ILinLote"; excel.Cells[1, 3] = "ICanLote"; excel.Cells[1, 4] = "IFchLote"; 
            excel.Cells[1, 5] = "IVtoLote"; 
            excel.Cells[1, 6] = "IEmpLote"; excel.Cells[1, 7] = "IRecibo"; excel.Cells[1, 8] = "IArtCod"; excel.Cells[1, 9] = "ITarima";
            int i = 2;
            foreach (DataRow row in ds.Tables["Ipolotes"].Rows)
            {
                for (int j = 0; j <= 8; j++)
                    excel.Cells[i, j+1] = row[j].ToString();
                i++;
            }
            excel.ActiveWorkbook.Saved = true;
            excel.DisplayAlerts = false;
            excel.ActiveWorkbook.SaveAs(Archivo);
            excel.ActiveWorkbook.Close();
            excel.Application.Quit();
        }

        public void SendMailCedis(string Dest, string Archivo, string mBody, string mAsunto, string ArchivoPDF, string Pedidos)
        {
            MailMessage msg = new MailMessage();
            MailMessage email = new MailMessage();

            string[] destinatarios = Dest.Split(';');
            foreach (string destinos in destinatarios)
            {
                email.To.Add(new MailAddress(destinos));
            }
            //email.To.Add(new MailAddress("gcamacho@mrlucky.com.mx"));

            email.From = new MailAddress("ricardo.cortes@mrlucky.com.mx"); //
            email.Subject = mAsunto; //"Mensaje de Prueba";
            email.Body = mBody;  //"Información de la factura";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;
            //MessageBox.Show("CORREO: PRIORIDAD");
            //string archivo = @"C:\Reportes\factura_informativa.txt";
            if (Archivo.Trim().Length > 0)
                if (File.Exists(Archivo))
                    email.Attachments.Add(new Attachment(Archivo));
            if (ArchivoPDF.Trim().Length > 0)
                if (File.Exists(ArchivoPDF))
                    email.Attachments.Add(new Attachment(ArchivoPDF));
            string rutaDirectorio = @"C:\Reportes\";
            string Facts = Pedidos.Trim().Substring(0, Pedidos.Trim().Length - 1); 
            string[] Archs = Facts.Split(',');
            foreach (string Docs in Archs)
            {
                string patronBusqueda = "*" + Docs.Trim() + ".xlsx"; // O buscar un nombre específico como "reporte*.xlsx"
                                                             // Devuelve un arreglo con todas las rutas que coinciden
                string[] archivos = System.IO.Directory.GetFiles(rutaDirectorio, patronBusqueda, System.IO.SearchOption.TopDirectoryOnly);
                foreach (string archivo in archivos)
                {
                    // Agrega los archivos encontrados a un ListBox (lstArchivos)
                    email.Attachments.Add(new Attachment(archivo));
                }
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail1.mrlucky.com.mx";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("ricardo.cortes", "rcedillo");

            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
    (s, cert, chain, sslPolicyErrors) => true;

            try
            {
                smtp.Send(email);
                email.Dispose();
                MessageBox.Show("correo enviado", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("correo no enviado\r\n" + ex.ToString(), "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

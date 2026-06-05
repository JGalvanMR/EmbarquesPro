using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;
using BarcodeLib.Barcode;

namespace Embarques
{
    public partial class ImprimirSplit : Form
    {

        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //string mTip = "";
        public string usuarioauth = "";
        public string embarque = "";
        public string pdn_origen = "";
        public string etiqueta = "";
        public string nosplit = "";
        string destino = "";

        public ImprimirSplit(string usuario, string emba)
        {
            InitializeComponent();
            usuarioauth = usuario;
            embarque = emba;
            cargarsplit();
            etiqueta = "";
            nosplit = "";
            destino = "";

            string bd = "tb_mstr_pedidos_nal";
            thisConnecion.Open();
            if (Convert.ToInt32(embarque) < 400000) {
                bd = "tb_mstr_pedidos_exp";
            }
            pdn_origen = embarque;
            string Cadena = "SELECT pdn_pedorigen FROM "+bd+" WHERE pdn_folio = '" + embarque.Trim() + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader datos;
            datos = cmd.ExecuteReader();
            while (datos.Read())
            {
                if (datos["pdn_pedorigen"].ToString().Trim() != "0" && datos["pdn_pedorigen"].ToString().Trim() != "")
                    pdn_origen = datos["pdn_pedorigen"].ToString();
            }
            thisConnecion.Close();

            thisConnecion.Open();
            Cadena = "   SELECT destino FROM  tb_mstr_trailer WHERE pdn_folio = '" + Convert.ToInt32(pdn_origen.Trim()) + "'";
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader datos2;
            datos2 = cmd.ExecuteReader();
            destino = "PC/PA";
            while (datos2.Read())
            {
                destino = datos2["destino"].ToString().Trim();
            }
            thisConnecion.Close();


            //


            thisConnecion.Open();
            string CadenaEmergencia = "SELECT Emergencia FROM Tb_Emergencia_Split";
            SqlCommand cmdEmergencia;
            cmdEmergencia = new SqlCommand(CadenaEmergencia);
            cmdEmergencia.Connection = thisConnecion;
            string Info = Convert.ToString(cmdEmergencia.ExecuteScalar());
            thisConnecion.Close();

            if (Info == "0")
            {
                Emergency.Visible = false;
                BtnImp.Visible = true;

            }
            else {
                Emergency.Visible = true;
                BtnImp.Visible = false;
            
            }

        }

        private void cargarsplit()
        {
            thisConnecion.Open();
            DGDatos.Rows.Clear();
            string Cadena = "   SELECT sum(cajas) AS cajas, tarima, emb_folio, estatus, HORAF FROM Tb_Det_Split WHERE NOM_CAPSPLIT = '" + usuarioauth.Trim() + "' and emb_folio = '" + embarque.Trim() + "' GROUP BY emb_folio, tarima, estatus, HORAF";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            SqlDataReader datos;
            datos = cmd.ExecuteReader();
            DGDatos.Rows.Clear();
            string mGTIN = "", mNOMING = "";
            while (datos.Read())
            {
                DGDatos.Rows.Add(datos["emb_folio"].ToString(), datos["tarima"].ToString(), datos["cajas"].ToString(), datos["estatus"].ToString(), datos["HORAF"].ToString());
            }
            thisConnecion.Close();
        }

        private void DGDatos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (this.DGDatos.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            catch
            {

            }
        }

        private void DGDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            etiqueta = "SPLIT*" + embarque + "*" + DGDatos.CurrentRow.Cells["split"].Value.ToString().Trim() + ", " + DGDatos.CurrentRow.Cells["cajas"].Value.ToString().Trim();
            nosplit = DGDatos.CurrentRow.Cells["split"].Value.ToString().Trim();
            MessageBox.Show("Etiqueta Split " + DGDatos.CurrentRow.Cells["split"].Value.ToString() + " Seleccionada para Impresion");
        }

        private void BtnImp_Click(object sender, EventArgs e)
        {
            if (etiqueta != "")
            {
                string estado = "";
                
                string impr_blancas = "";

                if (destino.Length > 9) {
                    destino = destino.Substring(0, 9);
                }

                impr_blancas += "^XA\n";
                impr_blancas += "^LL1200\n";
                impr_blancas += "^LT120\n";
                impr_blancas += "^FX Top section";
                impr_blancas += "^CFB,25\n";
                impr_blancas += "^FO150,103^A0N,70,70,^FD" + destino + "^FS\n";
                impr_blancas += "^FO150,170^A0N,100,100,^FD" + embarque + "^FS\n";
                impr_blancas += "^FO150,258^A0N,80,80,^FDSPLIT: "+ nosplit +"^FS\n";
                impr_blancas += "^FO200,328^BQN,7,7^FDLA,"+etiqueta+"^FS\n";
                impr_blancas += "^FO150,530^BY1,^BCN,80,N,N,N^FD" + etiqueta + "^FS\n";
                impr_blancas += "^FO150,640^GB300,1,3^FS\n";
                impr_blancas += "^XZ\n";


                

                PrintDialog pd = new PrintDialog();
                pd.PrinterSettings = new PrinterSettings();
                if (DialogResult.OK == pd.ShowDialog(this))
                {
                    // Send a printer-specific to the printer.
                    RawPrinterHelper.SendStringToPrinter(pd.PrinterSettings.PrinterName, impr_blancas);
                    MessageBox.Show("SPLIT LIBERADO CORRECTAMENTE", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    thisConnecion.Open();
                    string Cadena = "UPDATE tb_det_split SET HORAF = 'S' WHERE tarima = '" + nosplit.Trim() + "' AND emb_folio = '" + embarque.Trim() + "'";
                    SqlCommand cmd;
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    SqlDataReader datos;
                    datos = cmd.ExecuteReader();
                    thisConnecion.Close();
                    cargarsplit();
                }

            }
            else
            {
                MessageBox.Show("Seleccione una opcion de Split a imprimir", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }


        public class RawPrinterHelper
        {
            // Structure and API declarions:
            [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
            public class DOCINFOA
            {
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDocName;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pOutputFile;
                [MarshalAs(UnmanagedType.LPStr)]
                public string pDataType;
            }
            [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

            [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool ClosePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

            [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndDocPrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool StartPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool EndPagePrinter(IntPtr hPrinter);

            [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
            public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

            // SendBytesToPrinter()
            // When the function is given a printer name and an unmanaged array
            // of bytes, the function sends those bytes to the print queue.
            // Returns true on success, false on failure.
            public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
            {
                Int32 dwError = 0, dwWritten = 0;
                IntPtr hPrinter = new IntPtr(0);
                DOCINFOA di = new DOCINFOA();
                bool bSuccess = false; // Assume failure unless you specifically succeed.

                di.pDocName = "My C#.NET RAW Document";
                di.pDataType = "RAW";

                // Open the printer.
                if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        // Start a page.
                        if (StartPagePrinter(hPrinter))
                        {
                            // Write your bytes.
                            bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                            EndPagePrinter(hPrinter);
                        }
                        EndDocPrinter(hPrinter);
                    }
                    ClosePrinter(hPrinter);
                }
                // If you did not succeed, GetLastError may give more information
                // about why not.
                if (bSuccess == false)
                {
                    dwError = Marshal.GetLastWin32Error();
                }
                return bSuccess;
            }

            public static bool SendFileToPrinter(string szPrinterName, string szFileName)
            {
                // Open the file.
                FileStream fs = new FileStream(szFileName, FileMode.Open);
                // Create a BinaryReader on the file.
                BinaryReader br = new BinaryReader(fs);
                // Dim an array of bytes big enough to hold the file's contents.
                Byte[] bytes = new Byte[fs.Length];
                bool bSuccess = false;
                // Your unmanaged pointer.
                IntPtr pUnmanagedBytes = new IntPtr(0);
                int nLength;

                nLength = Convert.ToInt32(fs.Length);
                // Read the contents of the file into the array.
                bytes = br.ReadBytes(nLength);
                // Allocate some unmanaged memory for those bytes.
                pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                // Send the unmanaged bytes to the printer.
                bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                return bSuccess;
            }
            public static bool SendStringToPrinter(string szPrinterName, string szString)
            {
                IntPtr pBytes;
                Int32 dwCount;
                // How many characters are in the string?
                dwCount = szString.Length;
                // Assume that the printer is expecting ANSI text, and then convert
                // the string to ANSI text.
                pBytes = Marshal.StringToCoTaskMemAnsi(szString);
                // Send the converted ANSI string to the printer.
                SendBytesToPrinter(szPrinterName, pBytes, dwCount);
                Marshal.FreeCoTaskMem(pBytes);
                return true;
            }
        }

        private void Emergency_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            PrintDialog pdi = new PrintDialog();
            pdi.Document = pd;
            if (pdi.ShowDialog() == DialogResult.OK)
            {

                //pd.DefaultPageSettings.PaperSize = new PaperSize("MyPaper", 300, 250);
                pd.PrintPage += new PrintPageEventHandler(this.printDocument1_PrintPage);
                pd.Print();
                MessageBox.Show("SPLIT LIBERADO CORRECTAMENTE", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                thisConnecion.Open();
                string Cadena = "UPDATE tb_det_split SET HORAF = 'S' WHERE tarima = '" + nosplit.Trim() + "' AND emb_folio = '" + embarque.Trim() + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                SqlDataReader datos;
                datos = cmd.ExecuteReader();
                thisConnecion.Close();
                cargarsplit();
            }
            else
            {
                MessageBox.Show("Impresion Cancelada");
            }
        }

        public void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            decimal sumataraprox = 0, sumacomp = 0, sumasplit = 0, decimales = 0, enteros = 0;
            Int32 sumapedido = 0;
            String drawString1 = " ", drawString2 = " ", drawString3 = " ", drawString4 = " ", drawString5 = " ", drawobs = " ", drawryan = " ";
            String drawLinea = " ", drawProd = " ", drawPedi = " ", sumatar = " ", taraprox = " ", sumcomp = " ", sumdeci = " ", drawcajas = " ", drawtaraprox = " ", drawobsprod = " ";
            String drawstring8 = " ", drawstring9 = " ", drawstring10 = " ", drawstring11 = " ", drawstring12 = " ", drawstring13 = " ";
            DateTime dt = DateTime.Now;
            //int cont = 148, cont1 = 165;
            

            /*if (destino.Length > 9)
            {
                destino = destino.Substring(0, 9);
            }*/

            // Create font and brush. 
            //Font drawFont = new Font("Courier New", 15, FontStyle.Bold);//encabezado
            //Font dfCajas = new Font("Courier New", 14, FontStyle.Bold);//encabezado
            //Font drawFontLista = new Font("Courier New", 7, FontStyle.Bold);//encabezado
            //Font drawFontCajas = new Font("Courier New", 8, FontStyle.Bold);//encabezado
            //Font dfEncabezadoLista = new Font("Courier New", 7, FontStyle.Bold);//encabezado
            Font drawFont = new Font("Courier New", 15, FontStyle.Bold);//encabezado
            Font dfCajas = new Font("Courier New", 14, FontStyle.Bold);//encabezado
            Font drawFontLista = new Font("Courier New", 7, FontStyle.Bold);//encabezado
            Font drawFontCajas = new Font("Courier New", 8, FontStyle.Bold);//encabezado
            Font dfEncabezadoLista = new Font("Courier New", 7, FontStyle.Bold);//encabezado
            Font drawFont1 = new Font("Code 128", 50);//código de barras
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // Create point for upper-left corner of drawing. 
            PointF drawPoint = new PointF(65.625F, 10.0F);//encabezado
            PointF drawPoint1 = new PointF(65.625F, 30.0F);//codigo de barras            
            PointF drawPoint3 = new PointF(15.0F, 50.0F);//datos1
            PointF dpCajas = new PointF(145.0F, 50.0F);//datos1
            PointF dpEncabezadoLista = new PointF(00.0F, 300.0F);
            PointF drawpoint8 = new PointF(35.0f, 175.0f);
            PointF drawPointLABEL = new PointF(10.0f, 700.0f);//Codigo de barras
            drawString2 = "_________________________________________________________________________________________________________________________________________";

            #region
            
            BarcodeLib.Barcode.QRCode QR = new BarcodeLib.Barcode.QRCode();
            CreaQr(etiqueta);//Genera Codigo QR
            CREAR128(etiqueta);//Genera Codigo de Barras CODE 128
            String drawString = "" + destino.Trim() + "";
            drawString1 = "" + embarque + "";
            drawString3 = "SPLIT: " + nosplit + "";

            e.Graphics.DrawString(drawString, drawFont, drawBrush, drawPoint);//DESTINO
            e.Graphics.DrawString(drawString1, drawFont, drawBrush, drawPoint1);//EMBARQUE
            e.Graphics.DrawString(drawString3, dfCajas, drawBrush, drawPoint3);//SPLIT
            //e.Graphics.DrawString("\t", drawFont, drawBrush, 00.0f, 85.0f);//linea3

            string dsEtiqueta = "CAJAS: "+DGDatos.CurrentRow.Cells["cajas"].Value.ToString().Trim();
            e.Graphics.DrawString(dsEtiqueta, dfCajas, drawBrush, dpCajas);//CAJAS

            e.Graphics.DrawImage(CodeQR.Image, 37.5F, 90, 200, 200);

            string drawStringLista = "\t" + "PRODUCTO" + "\t\t\t" + "CAJAS";
            List<string> productosSplit = getDetalleSplit(usuarioauth, embarque, DGDatos.CurrentRow.Cells["split"].Value.ToString().Trim());
            float valorY = 315.0F;
            e.Graphics.DrawString(drawStringLista, dfEncabezadoLista, drawBrush, dpEncabezadoLista);
            foreach (string producto in productosSplit)
            {
                PointF drawPoint4 = new PointF(15.0F, valorY);
                e.Graphics.DrawString(producto, drawFontLista, drawBrush, drawPoint4);
                valorY += 23;
            }

            List<string> cajasSplit = getCajasSplit(usuarioauth, embarque, DGDatos.CurrentRow.Cells["split"].Value.ToString().Trim());
            float valorYCajas = 315.0F;
            foreach (string cajas in cajasSplit)
            {
                PointF drawPoint5 = new PointF(203F, valorYCajas);
                e.Graphics.DrawString(cajas, drawFontCajas, drawBrush, drawPoint5);
                valorYCajas += 23;
            }
            //e.Graphics.DrawImage(CodeQR.Image, 30.0f, 90, 200, 200);
            //e.Graphics.DrawImage(Code128.Image, 0.0f, 400);
            //e.Graphics.DrawString("*PRU*\r\n", drawFont1, drawBrush, drawPointLABEL);//código de barras 



            //{
            //    drawchofer = " ";
            //}

            //else
            //{
            //    int limite = txtnomtrans.TextLength;
            //    limite = limite - 20;
            //    if (limite > 0)
            //        drawchofer = drawchofer.Remove(20, limite);
            //}


            #endregion
            sumataraprox = sumacomp = sumasplit = decimales = enteros = 0;
            sumapedido = 0;
            e.HasMorePages = false;
            thisConnecion.Close();

        }

        private void CREAR128(string Folio) { 
        BarcodeLib.Barcode.Linear barcode = new BarcodeLib.Barcode.Linear();
        barcode.Type = BarcodeType.CODE128;
        barcode.Data = Folio;
    
        barcode.UOM = UnitOfMeasure.PIXEL;
        barcode.BarWidth = 1;
        barcode.BarHeight = 80;
        barcode.LeftMargin = 10;
        barcode.RightMargin = 10;
        barcode.TopMargin = 10;
        barcode.BottomMargin = 10;
    
        barcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Png;    
        // more barcode settings here
                            
        // save barcode image into your system
        barcode.drawBarcode("c://barcode.png");
    
        // generate barcode & output to byte array
        byte[] barcodeInBytes = barcode.drawBarcodeAsBytes();
    
        // generate barcode to Graphics object


        MemoryStream ms = new MemoryStream(barcodeInBytes, 0, barcodeInBytes.Length);
        ms.Write(barcodeInBytes, 0, barcodeInBytes.Length);
        Image newImage = Image.FromStream(ms, true);//Exception occurs here
        Code128.Image = newImage;
        }



        private void CreaQr(string Folio)
        {
            BarcodeLib.Barcode.QRCode qrbarcode = new BarcodeLib.Barcode.QRCode();

            // Select QR Code data encoding type: numeric, alphanumeric, byte, and Kanji to select from.
            qrbarcode.Encoding = BarcodeLib.Barcode.QRCodeEncoding.Auto;
            qrbarcode.Data = Folio;

            // Adjusting QR Code barcode module size and quiet zones on four sides.
            qrbarcode.ModuleSize = 1;
            qrbarcode.LeftMargin = 1; //12
            qrbarcode.RightMargin = 1; //12 
            qrbarcode.TopMargin = 1; //12
            qrbarcode.BottomMargin = 1; //12

            // Select QR Code Version (Symbol Size), available from V1 to V40, i.e. 21 x 21 to 177 x 177 modules.
            qrbarcode.Version = BarcodeLib.Barcode.QRCodeVersion.V10; // V1

            // Set QR-Code bar code Reed Solomon Error Correction Level: L(7%), M (15%), Q(25%), H(30%)
            qrbarcode.ECL = BarcodeLib.Barcode.QRCodeErrorCorrectionLevel.H; //L
            qrbarcode.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;

            // More barcode settings here, like ECI, FNC1, Structure Append, etc.

            // save barcode image into your system
            //if (File.Exists(@"c:/reportes/qrcode.png"))
            //    File.Delete(@"c:/reportes/qrcode.png");
            //qrbarcode.drawBarcode(@"c:/reportes/qrcode.png");

            // Generate QR Code barcode & output to byte array
            byte[] barcodeInBytes = qrbarcode.drawBarcodeAsBytes();

            MemoryStream ms = new MemoryStream(barcodeInBytes, 0, barcodeInBytes.Length);
            ms.Write(barcodeInBytes, 0, barcodeInBytes.Length);
            Image newImage = Image.FromStream(ms, true);//Exception occurs here
            CodeQR.Image = newImage;


            // Generate QR Code barcode to Graphics object
            //Graphics graphics =  ... ;
            //barcode.drawBarcode(graphics);

            // Generate QR Code barcode and output to HttpResponse object
            //HttpResponse response = ...;
            //qrbarcode.drawBarcode(response);

            // Generate QR Code barcode and output to Stream object
            //Stream stream = "@c:\reporte\qr";
            //qrbarcode.drawBarcode(stream);
        }

		private void ImprimirSplit_Load(object sender, EventArgs e)
		{

		}

        List<string> listDetalleSplit = new List<string>();
        List<string> getDetalleSplit(string nom_capsplit, string emb_folio, string tarima)
        {
            listDetalleSplit.Clear();
            thisConnecion.Open();
            //string Cadena = "SELECT nom_prod, SUM(cajas) as cajas FROM tb_det_split WHERE NOM_CAPSPLIT='"+ nom_capsplit + "' and emb_folio='" + emb_folio + "' and tarima='" + tarima + "' GROUP BY nom_prod";
            string Cadena = "SELECT CASE WHEN LEN(nom_prod) > 25 THEN LEFT(nom_prod, 25) + CHAR(10) + SUBSTRING(nom_prod, 26, LEN(nom_prod))ELSE nom_prod END AS nom_prod, sum(cajas) as cajas FROM tb_det_split WHERE NOM_CAPSPLIT='" + nom_capsplit + "' and emb_folio='" + emb_folio + "' and tarima='" + tarima + "' GROUP BY nom_prod";
            SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
            SqlDataReader dr = cmd.ExecuteReader();

            int total = 0;

            while (dr.Read())
            {
                //listDetalleSplit.Add(Convert.ToString(dr["nom_prod"]).Trim() + "\t\t\t" + Convert.ToString(dr["cajas"]).Trim());
                listDetalleSplit.Add(Convert.ToString(dr["nom_prod"]).Trim());
            }
            thisConnecion.Close();
            return listDetalleSplit;
        }

        List<string> listCajasSplit = new List<string>();
        List<string> getCajasSplit(string nom_capsplit, string emb_folio, string tarima)
        {
            listCajasSplit.Clear();
            thisConnecion.Open();
            //string Cadena = "SELECT nom_prod, SUM(cajas) as cajas FROM tb_det_split WHERE NOM_CAPSPLIT='"+ nom_capsplit + "' and emb_folio='" + emb_folio + "' and tarima='" + tarima + "' GROUP BY nom_prod";
            string Cadena = "SELECT CASE WHEN LEN(nom_prod) > 25 THEN LEFT(nom_prod, 25) + CHAR(10) + SUBSTRING(nom_prod, 26, LEN(nom_prod))ELSE nom_prod END AS nom_prod, sum(cajas) as cajas FROM tb_det_split WHERE NOM_CAPSPLIT='" + nom_capsplit + "' and emb_folio='" + emb_folio + "' and tarima='" + tarima + "' GROUP BY nom_prod";
            SqlCommand cmd = new SqlCommand(Cadena, thisConnecion);
            SqlDataReader dr = cmd.ExecuteReader();

            int total = 0;

            while (dr.Read())
            {
                listCajasSplit.Add(Convert.ToString(dr["cajas"]).Trim());
            }
            thisConnecion.Close();
            return listCajasSplit;
        }
        
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Utilerias;

namespace Embarques
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Utilerias.Class1.validar_ip();    
            Application.Run(new Form1());
            //Application.Run(new FrmMensaje());
        }

        public class MyGlobal
        {
            public static int NoPedido = 0;
            public static string TipoPed = "";
            public static string CveCliente = "";
            public static string CveProv = "";
            public static string PubFecEmb = "";
            public static string PubNoTrailer = "";
            public static string PubPedido = "";
            public static string PubRecibo = "";
            public static string PubTipoRec = "";
            public static string PubProd = "";
            public static string PubCveProd = "";
            public static string PubLI = "";
            public static string PubLF = "";
            public static string PubPrI = "";
            public static string PubPrF = "";
            public static int PubOpc = 0;
            public static string AltaReg = "A";
            public static string PubFact = "";
            public static string PubResp = "";
            public static string fechadepedido = "";
        }
    }
}

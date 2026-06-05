using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using Office = Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;


namespace Embarques
{
    public partial class FrmAltaTrailer : Form
    {
        SqlConnection thisConnecion = new SqlConnection(Utilerias.Class1.ConnectionString);
        //SqlConnection thisConnecionDBGAB = new SqlConnection(Utilerias.Class1.ConnectionStringDBGAB);
        Form1 Frm1 = new Form1();
        string RegistradoAguilares = "";
        private Timer timerHrLLego;
        private Timer timerHrEnt;

        public FrmAltaTrailer()
        {
            InitializeComponent();
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
            // Inicializa el Timer para ejecutar cada segundo (1000 ms)
            timerHrLLego = new Timer();
            timerHrLLego.Interval = 1000; // 1 segundo
            timerHrLLego.Start();
            timerHrEnt = new Timer();
            timerHrEnt.Interval = 1000; // 1 segundo
            timerHrEnt.Start();
        }

        private void FrmAltaTrailer_Load(object sender, EventArgs e)
        {
            TxtHrLLego.Mask = "00:00";
            TxtHrSal.Mask = "00:00";
            TxtHrEnt.Mask = "00:00";
            TxtLargo.Mask = "00";
            TxtRadio.Mask = "00";
            TxtTemp.Mask = "00";
            //DT1.Format = DateTimePickerFormat.Custom;
            //DT1.CustomFormat = "HH:mm"; 
            LblPLacaAso.Text = "";


            thisConnecion.Open();
            string Cadenax = "Select getdate()";
            SqlCommand cmdx = new SqlCommand(Cadenax, thisConnecion);
            DateTime FecServer = Convert.ToDateTime(cmdx.ExecuteScalar());
            thisConnecion.Close();
            string Hr = FecServer.ToString("HH:mm").Substring(0, 2);
            Int32 Hora = Convert.ToInt32(Hr);
            if (Hora >= 1 && Hora < 4)
            {
                DtFE.Value = DateTime.Now.AddDays(-1);
            }



            thisConnecion.Open();
            if (Program.MyGlobal.AltaReg == "A")
            {
                TxtHrEnt.Enabled = false;
                TxtTemp.Enabled = false;
                TxtHrSal.Enabled = false;
                string Cadena = "SELECT prov_clave, prov_nombre,(prov_nombre + ' ' + prov_clave ) as Nombre FROM tb_cat_proveedor ORDER BY PROV_NOMBRE ";
                DataSet ds1 = new DataSet();
                SqlDataAdapter da1 = new SqlDataAdapter(Cadena, thisConnecion);
                da1.Fill(ds1, "TRANSP");
                System.Data.DataTable TRANSP = new System.Data.DataTable();
                TRANSP = ds1.Tables["TRANSP"];
                CmbTra.DataSource = TRANSP;
                CmbTra.ValueMember = "prov_clave";
                CmbTra.DisplayMember = "Nombre";
                TxtFotoPla.Enabled = false;
                TxtFotoTemp.Enabled = false;
                TxtFotoTra.Enabled = false;
                BtnBuscarImg1.Enabled = false;
                BtnBuscarImg2.Enabled = false;
                BtnBuscarImg3.Enabled = false;
                RtBoxCau.Enabled = false;
                RtBoxFal.Enabled = false;
                RtBoxTra.Enabled = false;
                timerHrLLego.Tick += TimerHrLLego_Tick;
            }
            if (Program.MyGlobal.AltaReg == "M")
            {
                TxtHrLLego.Enabled = false;
                TxtTran.Enabled = false;
                TxtChofer.Enabled = false;
                TxtDest.Enabled = false;
                TxtPLaca.Enabled = false;
                TxtPlaTra.Enabled = false;
                TxtRadio.Enabled = false;
                TxtLargo.Enabled = false;
                TxtNoPed.Enabled = false;
                TxtHrSal.Text = "00:00";
                //TxtHrEnt.Text = "00:00";
                string Cadena = "SELECT HORAREGVIG,TRANSPORTE,CHOFER,DESTINO,NO_TRAILER,PLACA,RADIO,LARGO,HORAENT,TEMP,HORASAL,TIEMPOTOT,CONSE,OBSTRANS,OBSCAUSA,OBSFALTA,PDN_FOLIO  FROM tb_mstr_trailer WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                SqlDataReader Info;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtHrLLego.Text = Info["HORAREGVIG"].ToString();
                    TxtTran.Text = Info["TRANSPORTE"].ToString();
                    TxtChofer.Text = Info["CHOFER"].ToString();
                    TxtDest.Text = Info["DESTINO"].ToString();
                    TxtPLaca.Text = Info["NO_TRAILER"].ToString();
                    TxtPlaTra.Text = Info["PLACA"].ToString();
                    //label5.Text = Info["PLACA"].ToString();
                    TxtRadio.Text = Info["RADIO"].ToString();
                    label6.Text = Info["RADIO"].ToString();
                    TxtLargo.Text = Info["LARGO"].ToString();
                    TxtHrEnt.Text = Info["HORAENT"].ToString();
                    TxtTemp.Text = Info["TEMP"].ToString();
                    TxtHrSal.Text = Info["HORASAL"].ToString();
                    TxtTmpTot.Text = Info["TIEMPOTOT"].ToString();
                    LblConse.Text = Info["CONSE"].ToString();
                    DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
                    RtBoxTra.Text = Info["OBSTRANS"].ToString();
                    RtBoxCau.Text = Info["OBSCAUSA"].ToString();
                    RtBoxFal.Text = Info["OBSFALTA"].ToString();
                    TxtNoPed.Text = Info["PDN_FOLIO"].ToString();
                }
                //BtnGrabar.Enabled = true;
                if (TxtHrEnt.Text.ToString() != "  :" && TxtHrSal.Text.ToString().Trim() == "00:00")
                {
                    TxtFotoPla.Enabled = false;
                    TxtFotoTemp.Enabled = false;
                    TxtFotoTra.Enabled = false;
                    BtnBuscarImg1.Enabled = false;
                    BtnBuscarImg2.Enabled = false;
                    BtnBuscarImg3.Enabled = false;
                    TxtHrEnt.Enabled = false;
                    TxtTemp.Enabled = false;
                    //BtnGrabar.Enabled = false;
                }
                if (TxtHrEnt.Text.ToString() == "  :" && TxtHrSal.Text.ToString().Trim() == "00:00")
                {
                    TxtHrSal.Enabled = false;
                    //BtnGrabar.Enabled = false;
                    timerHrEnt.Tick += TimerHrEnt_Tick;
                }
                if (TxtHrEnt.Text.ToString() != "  :" && TxtHrSal.Text.ToString().Trim() != "00:00")
                {
                    TxtFotoPla.Enabled = false;
                    TxtFotoTemp.Enabled = false;
                    TxtFotoTra.Enabled = false;
                    BtnBuscarImg1.Enabled = false;
                    BtnBuscarImg2.Enabled = false;
                    BtnBuscarImg3.Enabled = false;
                    TxtHrEnt.Enabled = false;
                    TxtTemp.Enabled = false;
                    TxtHrSal.Enabled = false;
                }

                Cadena = "SELECT * FROM TB_FOTOS_TRAILER WHERE FECHA = '" + DtFE.Value.ToShortDateString() + "' AND NO_TRAILER = '" + TxtPLaca.Text.Trim() + "'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtFotoPla.Text = Info["FOTO_PLACA"].ToString().Trim();
                    if (File.Exists(TxtFotoPla.Text))
                        PbxPla.ImageLocation = TxtFotoPla.Text;
                    TxtFotoTemp.Text = Info["FOTO_TEMP"].ToString().Trim();
                    if (File.Exists(TxtFotoTemp.Text))
                        PbxTemp.ImageLocation = TxtFotoTemp.Text;
                    TxtFotoTra.Text = Info["FOTO_TRANS"].ToString().Trim();
                    if (File.Exists(TxtFotoTra.Text))
                        PbxTra.ImageLocation = TxtFotoTra.Text;
                }
            }
            if (Program.MyGlobal.AltaReg == "M")
            {
                TxtHrLLego.Enabled = false;
                TxtTran.Enabled = false;
                TxtChofer.Enabled = false;
                TxtDest.Enabled = false;
                TxtPLaca.Enabled = false;
                TxtPlaTra.Enabled = false;
                TxtRadio.Enabled = false;
                TxtLargo.Enabled = false;
                TxtNoPed.Enabled = false;
                TxtHrSal.Text = "00:00";
                //TxtHrEnt.Text = "00:00";
                string Cadena = "SELECT HORAREGVIG,TRANSPORTE,CHOFER,DESTINO,NO_TRAILER,PLACA,RADIO,LARGO,HORAENT,TEMP,HORASAL,TIEMPOTOT,CONSE,OBSTRANS,OBSCAUSA,OBSFALTA,PDN_FOLIO  FROM tb_mstr_trailer WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                SqlDataReader Info;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtHrLLego.Text = Info["HORAREGVIG"].ToString();
                    TxtTran.Text = Info["TRANSPORTE"].ToString();
                    TxtChofer.Text = Info["CHOFER"].ToString();
                    TxtDest.Text = Info["DESTINO"].ToString();
                    TxtPLaca.Text = Info["NO_TRAILER"].ToString();
                    TxtPlaTra.Text = Info["PLACA"].ToString();
                    //label5.Text = Info["PLACA"].ToString();
                    TxtRadio.Text = Info["RADIO"].ToString();
                    label6.Text = Info["RADIO"].ToString();
                    TxtLargo.Text = Info["LARGO"].ToString();
                    TxtHrEnt.Text = Info["HORAENT"].ToString();
                    TxtTemp.Text = Info["TEMP"].ToString();
                    TxtHrSal.Text = Info["HORASAL"].ToString();
                    TxtTmpTot.Text = Info["TIEMPOTOT"].ToString();
                    LblConse.Text = Info["CONSE"].ToString();
                    DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
                    RtBoxTra.Text = Info["OBSTRANS"].ToString();
                    RtBoxCau.Text = Info["OBSCAUSA"].ToString();
                    RtBoxFal.Text = Info["OBSFALTA"].ToString();
                    TxtNoPed.Text = Info["PDN_FOLIO"].ToString();
                }
                //BtnGrabar.Enabled = true;
                if (TxtHrEnt.Text.ToString() != "  :" && TxtHrSal.Text.ToString().Trim() == "00:00")
                {
                    TxtFotoPla.Enabled = false;
                    TxtFotoTemp.Enabled = false;
                    TxtFotoTra.Enabled = false;
                    BtnBuscarImg1.Enabled = false;
                    BtnBuscarImg2.Enabled = false;
                    BtnBuscarImg3.Enabled = false;
                    TxtHrEnt.Enabled = false;
                    TxtTemp.Enabled = false;
                    //BtnGrabar.Enabled = false;
                }
                if (TxtHrEnt.Text.ToString() == "  :" && TxtHrSal.Text.ToString().Trim() == "00:00")
                {
                    TxtHrSal.Enabled = false;
                    //BtnGrabar.Enabled = false;
                    timerHrEnt.Tick += TimerHrEnt_Tick;
                }
                if (TxtHrEnt.Text.ToString() != "  :" && TxtHrSal.Text.ToString().Trim() != "00:00")
                {
                    TxtFotoPla.Enabled = false;
                    TxtFotoTemp.Enabled = false;
                    TxtFotoTra.Enabled = false;
                    BtnBuscarImg1.Enabled = false;
                    BtnBuscarImg2.Enabled = false;
                    BtnBuscarImg3.Enabled = false;
                    TxtHrEnt.Enabled = false;
                    TxtTemp.Enabled = false;
                    TxtHrSal.Enabled = false;
                }

                Cadena = "SELECT * FROM TB_FOTOS_TRAILER WHERE FECHA = '" + DtFE.Value.ToShortDateString() + "' AND NO_TRAILER = '" + TxtPLaca.Text.Trim() + "'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtFotoPla.Text = Info["FOTO_PLACA"].ToString().Trim();
                    if (File.Exists(TxtFotoPla.Text))
                        PbxPla.ImageLocation = TxtFotoPla.Text;
                    TxtFotoTemp.Text = Info["FOTO_TEMP"].ToString().Trim();
                    if (File.Exists(TxtFotoTemp.Text))
                        PbxTemp.ImageLocation = TxtFotoTemp.Text;
                    TxtFotoTra.Text = Info["FOTO_TRANS"].ToString().Trim();
                    if (File.Exists(TxtFotoTra.Text))
                        PbxTra.ImageLocation = TxtFotoTra.Text;
                }
            }
            if (Program.MyGlobal.AltaReg == "MC")
            {
                TxtHrLLego.Enabled = false;
                TxtTran.Enabled = false;
                TxtChofer.Enabled = false;
                TxtDest.Enabled = false;
                TxtPLaca.Enabled = false;
                TxtPlaTra.Enabled = false;
                TxtRadio.Enabled = false;
                TxtLargo.Enabled = false;
                TxtNoPed.Enabled = false;
                TxtHrSal.Text = "00:00";
                TxtFotoPla.Enabled = false;
                TxtFotoTemp.Enabled = false;
                TxtFotoTra.Enabled = false;
                BtnBuscarImg1.Enabled = false;
                BtnBuscarImg2.Enabled = false;
                BtnBuscarImg3.Enabled = false;
                TxtHrEnt.Enabled = false;
                TxtTemp.Enabled = false;
                TxtHrSal.Enabled = false;
                //TxtHrEnt.Text = "00:00";
                string Cadena = "SELECT HORAREGVIG,TRANSPORTE,CHOFER,DESTINO,NO_TRAILER,PLACA,RADIO,LARGO,HORAENT,TEMP,HORASAL,TIEMPOTOT,CONSE,OBSTRANS,OBSCAUSA,OBSFALTA,PDN_FOLIO  FROM tb_mstr_trailer WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                SqlDataReader Info;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtHrLLego.Text = Info["HORAREGVIG"].ToString();
                    TxtTran.Text = Info["TRANSPORTE"].ToString();
                    TxtChofer.Text = Info["CHOFER"].ToString();
                    TxtDest.Text = Info["DESTINO"].ToString();
                    TxtPLaca.Text = Info["NO_TRAILER"].ToString();
                    TxtPlaTra.Text = Info["PLACA"].ToString();
                    //label5.Text = Info["PLACA"].ToString();
                    TxtRadio.Text = Info["RADIO"].ToString();
                    label6.Text = Info["RADIO"].ToString();
                    TxtLargo.Text = Info["LARGO"].ToString();
                    TxtHrEnt.Text = Info["HORAENT"].ToString();
                    TxtTemp.Text = Info["TEMP"].ToString();
                    TxtHrSal.Text = Info["HORASAL"].ToString();
                    TxtTmpTot.Text = Info["TIEMPOTOT"].ToString();
                    LblConse.Text = Info["CONSE"].ToString();
                    DtFE.Value = Convert.ToDateTime(Program.MyGlobal.PubFecEmb);
                    RtBoxTra.Text = Info["OBSTRANS"].ToString();
                    RtBoxCau.Text = Info["OBSCAUSA"].ToString();
                    RtBoxFal.Text = Info["OBSFALTA"].ToString();
                    TxtNoPed.Text = Info["PDN_FOLIO"].ToString();
                }
                Cadena = "SELECT * FROM TB_FOTOS_TRAILER WHERE FECHA = '" + DtFE.Value.ToShortDateString() + "' AND NO_TRAILER = '" + TxtPLaca.Text.Trim() + "'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                Info = cmd.ExecuteReader();
                while (Info.Read())
                {
                    TxtFotoPla.Text = Info["FOTO_PLACA"].ToString().Trim();
                    if (File.Exists(TxtFotoPla.Text))
                        PbxPla.ImageLocation = TxtFotoPla.Text;
                    TxtFotoTemp.Text = Info["FOTO_TEMP"].ToString().Trim();
                    if (File.Exists(TxtFotoTemp.Text))
                        PbxTemp.ImageLocation = TxtFotoTemp.Text;
                    TxtFotoTra.Text = Info["FOTO_TRANS"].ToString().Trim();
                    if (File.Exists(TxtFotoTra.Text))
                        PbxTra.ImageLocation = TxtFotoTra.Text;
                }
                RtBoxTra.Focus();
            }

            thisConnecion.Close();



            //label1.Text = DateTime.Now.ToString();
        }

        private void BtnGrabar_Click(object sender, EventArgs e)
        {

            if (TxtHrLLego.Text.ToString().Trim().Length < 5)
            {
                MessageBox.Show("Error: no se ha capturado la hora de llegada!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtHrLLego.Focus();
                return;
            }
            if (TxtTran.Text.ToString().Trim().Length < 1)
            {
                MessageBox.Show("Error: no se ha capturado el Transportista!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtTran.Focus();
                return;
            }
            if (TxtChofer.Text.ToString().Trim().Length < 5)
            {
                MessageBox.Show("Error: no se ha capturado el Nombre del Chofer!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtChofer.Focus();
                return;
            }
            //if (TxtDest.Text.ToString().Trim().Length < 5)
            //{
            //    MessageBox.Show("Error: no se ha capturado el Destino!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    TxtDest.Focus();
            //    return;
            //}

            if (TxtPLaca.Text.ToString().Trim().Length < 5)
            {
                MessageBox.Show("Error: no se ha capturado la Placa de la Caja!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtPLaca.Focus();
                return;
            }

            if (ValidaPedidoCancelado(TxtNoPed.Text.Trim()) == "C")
            {
                MessageBox.Show("Error: La orden de venta ingresada como pedido origen esta Cancelada!! Favor de Reportarlo a Ventas", "Aviso **ORDEN CANCELADA**", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtNoPed.Focus();
                return;
            }
            string Existe = validarproveedor();
            if (Existe == "N")
            {
                MessageBox.Show("Error: El proveedor no es correcto, NO SE PUEDE ASIGNAR A UN TRAILER", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtTran.Text = "";
                TxtTran.Focus();
                return;
            }

            if (Program.MyGlobal.AltaReg == "A")
            {
                if (TxtPlaTra.Text.ToString().Trim().Length < 5)
                {
                    MessageBox.Show("Error: no se ha capturado la Placa del Trailer!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtPlaTra.Focus();
                    return;
                }
                if (TxtRadio.Text.ToString().Trim().Length == 0)
                {
                    MessageBox.Show("Error: no se ha capturado el Numero de Radio!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtRadio.Focus();
                    return;
                }
                if (TxtLargo.Text.ToString().Trim().Length < 2)
                {
                    MessageBox.Show("Error: no se ha capturado el Largo del Trailer!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtLargo.Focus();
                    return;
                }
                if (TxtNoPed.Text.ToString().Trim().Length < 2)
                {
                    MessageBox.Show("Error: no se ha capturado el No. de Pedido !!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Focus();
                    return;
                }
            }
            if (Program.MyGlobal.AltaReg == "A")
            {
                string horainicio = "";
                string horafinal = "";
                string fechacaptura = "";

                string ordenventa = TxtNoPed.Text.Trim();
                if (Convert.ToInt32(TxtNoPed.Text.Trim()) < 400000)
                {
                    ordenventa = "0" + ordenventa;
                }
                if (thisConnecion.State == ConnectionState.Closed)
                {
                    thisConnecion.Open();
                }
                string Cadenax = "SELECT * FROM tb_mstr_embarque WHERE emb_folio = '" + ordenventa + "' and no_trailer = '" + TxtPLaca.Text.ToString().Trim() + "'";
                SqlCommand cmdx = new SqlCommand(Cadenax);
                cmdx.Connection = thisConnecion;
                SqlDataReader Info;
                Info = cmdx.ExecuteReader();
                while (Info.Read())
                {
                    horainicio = Info["hora_ini"].ToString().Trim();
                    horafinal = Info["hora_fin"].ToString().Trim();
                    fechacaptura = Info["fecha_cap"].ToString().Trim();
                }
                if (thisConnecion.State == ConnectionState.Open)
                {
                    thisConnecion.Close();
                }

                if (RegistradoAguilares != "1")
                {
                    if (horainicio != "" && fechacaptura != "")
                    {
                        if (horafinal != "--:--")
                        {
                            MessageBox.Show("Error: El pedido ya fue capturado y cerrado el dia " + fechacaptura + " a las " + horafinal + " Favor de Verificarlo!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            //return;
                        }

                        MessageBox.Show("Error: El pedido ya Tiene fecha y hora de inicio " + fechacaptura + " a las " + horainicio + " Favor de Verificarlo!!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //return;
                    }
                }


                string StaAnt = "";
                if (thisConnecion.State == ConnectionState.Closed)
                {
                    thisConnecion.Open();
                    StaAnt = "C";
                }
                //thisConnecion.Open();

                //thisConnecion.Open(); //DBGAB
                // Ahora se va a validar que exista el Pedido RCC 20 JUN 2016
                Int32 Mped = Convert.ToInt32(TxtNoPed.Text);
                string BD = "tb_mstr_pedidos_nal";
                if (Mped < 250000)
                {
                    BD = "tb_mstr_pedidos_exp";
                }

                string Cadena = "SELECT pdn_folio from " + BD + " WHERE pdn_folio = '" + TxtNoPed.Text + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion; //DBGAB
                string mPED = Convert.ToString(cmd.ExecuteScalar()).Trim();
                string NOExiste = "S";
                if (mPED.Trim().Length == 0)
                {
                    NOExiste = "N";
                    if (MessageBox.Show("Error: NO se encuentra el No. de Pedido!! Continuar con el Proceso", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                    {
                        TxtNoPed.Focus();
                        thisConnecion.Close();
                        return;
                    }
                }
                //
                Cadena = "SELECT MAX(CONSE) FROM tb_mstr_trailer WHERE fecha = '" + DtFE.Value.ToShortDateString() + "'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion; //DBGAB
                string mCONSE = Convert.ToString(cmd.ExecuteScalar()).Trim();

                if (mCONSE.Trim() == "")
                    mCONSE = "1";
                else
                    mCONSE = Convert.ToString(Convert.ToInt32(mCONSE) + 1);

                if (RegistradoAguilares != "1")
                {
                    Cadena = "INSERT INTO TB_MSTR_TRAILER(FECHA,hora_trailer,NO_TRAILER,TURNO,DESTINO,TRANSPORTE,TEMPINI,TEMPFIN,HORAINI,HORAFIN,ANDEN,CHOFER,RESPONSABLE,PLACA,HORAENT,PESO,";
                    Cadena = Cadena + "CONCEPTO1,CONCEPTO2,CONCEPTO3,CONCEPTO4,CONCEPTO6,CONCEPTO7,CONCEPTO8,CONCEPTO9,CONCEPTO10,LARGO,GATAS,RYAN1,RYAN2,POSRYAN1,POSRYAN2,HoraRegVig,";
                    Cadena = Cadena + "CONCEPTO5A,CONCEPTO5B,CONCEPTO5C,CONCEPTO5D,CONCEPTO5E,CONCEPTO5F,CONCEPTO5G,CONCEPTO5H,CONCEPTO5I,CONCEPTO5J,CONCEPTO5K,CONCEPTO5L,CONSE,";
                    Cadena = Cadena + "Temp,Surtible,HoraSal,TiempoTot,TiempoCar,PesoBascula,Radio,Guardar,Transfer,OBSTRANS,OBSCAUSA,OBSFALTA,TempSetPoint,PDN_FOLIO) ";
                    Cadena = Cadena + "Values('" + DtFE.Value.ToShortDateString() + "','" + DtFE.Value.ToShortDateString() + "','" + TxtPLaca.Text.Trim() + "','0','" + TxtDest.Text.Trim() + "','" + TxtTran.Text.Trim() + "',' ',' ','--:--','--:--',";
                    Cadena = Cadena + "'0','" + TxtChofer.Text.Trim() + "','','" + TxtPlaTra.Text.Trim() + "','','0.0',' ',' ',' ',' ',' ',' ',' ',' ',' ','" + TxtLargo.Text.Trim() + "','0','0','0','0','0','" + TxtHrLLego.Text.Trim() + "',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','";
                    Cadena = Cadena + mCONSE + "','0',' ','00:00 ',' ',' ','0','" + TxtRadio.Text.Trim() + "','N','N','','','','','" + mPED + "')";
                }
                else
                {
                    Cadena = "UPDATE TB_MSTR_TRAILER SET hora_trailer = '" + DtFE.Value.ToShortDateString() + "', TURNO = '0',DESTINO = '" + TxtDest.Text.Trim() + "', TRANSPORTE = '" + TxtTran.Text.Trim() + "',TEMPINI = ' ',TEMPFIN = ' ', HORAFIN = '--:--',ANDEN = '0',CHOFER = '" + TxtChofer.Text.Trim() + "',RESPONSABLE = '',HORAENT = '',PESO = '0.0',";
                    Cadena = Cadena + "CONCEPTO1 = ' ',CONCEPTO2 = ' ',CONCEPTO3 = ' ',CONCEPTO4 = ' ',CONCEPTO6 = ' ',CONCEPTO7 = ' ',CONCEPTO8 = ' ',CONCEPTO9 = ' ',CONCEPTO10 = ' ',LARGO = '" + TxtLargo.Text.Trim() + "',GATAS = '0',RYAN1 = '0',RYAN2 = '0',POSRYAN1 = '0',POSRYAN2 = '0',HoraRegVig = '" + TxtHrLLego.Text.Trim() + "',";
                    Cadena = Cadena + "CONCEPTO5A = ' ',CONCEPTO5B = ' ',CONCEPTO5C = ' ',CONCEPTO5D = ' ',CONCEPTO5E = ' ',CONCEPTO5F = ' ',CONCEPTO5G = ' ',CONCEPTO5H = ' ',CONCEPTO5I = ' ',CONCEPTO5J = ' ',CONCEPTO5K = ' ',CONCEPTO5L = ' ',CONSE = '" + mCONSE + "',";
                    Cadena = Cadena + "Temp = '0',Surtible = ' ',HoraSal = '00:00 ',TiempoTot = ' ',TiempoCar = ' ',PesoBascula = '0',Radio = '" + TxtRadio.Text.Trim() + "',Guardar = 'N',Transfer = 'N',OBSTRANS = '',OBSCAUSA = '',OBSFALTA = '',TempSetPoint = '',PDN_FOLIO = '" + mPED + "', deaguilares = 'A' WHERE fecha = '" + DtFE.Value.ToShortDateString() + "' AND NO_TRAILER = '" + TxtPLaca.Text.Trim() + "'";
                }
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                cmd.ExecuteNonQuery();

                if (RegistradoAguilares == "1")
                {
                    //Actualizacion en las bases de datos en el maestro de embarque
                    Cadena = "UPDATE tb_mstr_embarque SET hora_trailer = '" + DtFE.Value.ToShortDateString() + "'  WHERE fecha_cap = '" + DtFE.Value.ToShortDateString() + "' AND NO_TRAILER = '" + TxtPLaca.Text.Trim() + "'";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();
                }


                string CADENA1 = "";
                if (NOExiste == "S")
                {
                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "7.1", TxtPLaca.Text, "CONSULTA REGISTRO VIGILANCIA " + Cadena.Replace("'", "*"), "SIPGAB");
                    Cadena = "UPDATE " + BD + " SET PLACACAJA = '" + TxtPLaca.Text.Trim() + "' WHERE PDN_FOLIO = '" + TxtNoPed.Text.Trim() + "' AND PDN_FECHA = '" + DtFE.Value.ToShortDateString() + "'";
                    CADENA1 = Cadena;
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();
                    Cadena = "UPDATE " + BD + " SET PLACACAJA = '" + TxtPLaca.Text.Trim() + "' WHERE PDN_PEDORIGEN = '" + TxtNoPed.Text.Trim() + "'";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();

                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "R", "7.1", TxtPLaca.Text, "CONSULTA REGISTRO VIGILANCIA " + CADENA1.Replace("'", "*"), "SIPGAB");
                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "R", "7.1", TxtPLaca.Text, "CONSULTA REGISTRO VIGILANCIA " + Cadena.Replace("'", "*"), "SIPGAB");
                }

                //thisConnecion.Open();
                string pedidos = "'" + TxtNoPed.Text.Trim() + "',";
                Cadenax = "SELECT pdn_folio FROM tb_mstr_pedidos_nal WHERE pdn_pedorigen = '" + TxtNoPed.Text.Trim() + "' UNION SELECT pdn_folio FROM tb_mstr_pedidos_exp WHERE pdn_pedorigen = '" + TxtNoPed.Text.Trim() + "'";
                cmdx = new SqlCommand(Cadenax);
                cmdx.Connection = thisConnecion;
                Info = cmdx.ExecuteReader();
                while (Info.Read())
                {
                    pedidos = pedidos + " '" + Info["pdn_folio"].ToString().Trim() + "',";
                }
                //thisConnecion.Close();


                int encontrado = 0;

                Cadenax = "SELECT * FROM  tb_det_pend_embarque WHERE pdnorigen IN (" + pedidos.TrimEnd(',') + ") AND estatus NOT IN ('C', 'S')";
                cmdx = new SqlCommand(Cadenax);
                cmdx.Connection = thisConnecion;
                Info = cmdx.ExecuteReader();
                while (Info.Read())
                {
                    encontrado++;
                }


                Cadena = "SELECT EMAIL_DEST from TB_MSTR_EMAIL WHERE CNTE_CLAVE = 'PENDEMB'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                string correo = Convert.ToString(cmd.ExecuteScalar()).Trim();

                if (encontrado > 0)
                {
                    Cadena = "UPDATE tb_det_pend_embarque SET Hora_trailer = '" + DtFE.Value.ToShortDateString() + "', no_trailer = '" + TxtPLaca.Text.Trim() + "', estatus = 'A' WHERE pdnorigen IN (" + pedidos.TrimEnd(',') + ") AND estatus NOT IN ('C', 'S')";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();

                    SendMailPendienteEmbarque(correo, "", pedidos);
                }



                //Cadena = "INSERT INTO TB_MSTR_TRAILER(FECHA,NO_TRAILER,TURNO,DESTINO,TRANSPORTE,TEMPINI,TEMPFIN,HORAINI,HORAFIN,ANDEN,CHOFER,RESPONSABLE,PLACA,HORAENT,PESO,";
                //Cadena = Cadena + "CONCEPTO1,CONCEPTO2,CONCEPTO3,CONCEPTO4,CONCEPTO6,CONCEPTO7,CONCEPTO8,CONCEPTO9,CONCEPTO10,LARGOTRA,GATAS,RYAN1,RYAN2,POSRYAN1,POSRYAN2,HoraRegVig,";
                //Cadena = Cadena + "CONCEPTO5A,CONCEPTO5B,CONCEPTO5C,CONCEPTO5D,CONCEPTO5E,CONCEPTO5F,CONCEPTO5G,CONCEPTO5H,CONCEPTO5I,CONCEPTO5J,CONCEPTO5K,CONCEPTO5L,CONSE,";
                //Cadena = Cadena + "Temp,Surtible,HoraSal,TiempoTot,TiempoCar,PesoBascula,Radio,Guardar,transfer,BANDERA) ";
                //Cadena = Cadena + "Values('" + DtFE.Value.ToShortDateString() + "','" + TxtPLaca.Text.Trim() + "','0','" + TxtDest.Text.Trim() + "','" + TxtTran.Text.Trim() + "',' ',' ','--:--','--:--',";
                //Cadena = Cadena + "'0','" + TxtChofer.Text.Trim() + "','','" + TxtPlaTra.Text.Trim() + "','','0.0',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','0','0','0','0','0','" + TxtHrLLego.Text.Trim() + "',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','";
                //Cadena = Cadena + mCONSE + "','0',' ',' ',' ',' ','0','" + TxtRadio.Text.Trim() + "','N','N',' ')";
                //cmd = new SqlCommand(Cadena);
                //cmd.Connection = thisConnecion; //DBGAB
                //cmd.ExecuteNonQuery();
                if (StaAnt == "C")
                    thisConnecion.Close();
                //thisConnecionDBGAB.Close();
                Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "7.1", TxtPLaca.Text, TxtChofer.Text.Trim() + " " + TxtDest.Text.Trim() + " " + TxtTran.Text.Trim() + " Conse: " + LblConse.Text.Trim(), "SIPGAB");
                if (NOExiste == "S")
                {
                    Utilerias.Class1.registrar_movimiento(DateTime.Now, Environment.MachineName, Utilerias.Class1.Usu_login, "A", "7.1", TxtPLaca.Text, "Encontre el Pedido: " + TxtNoPed.Text.Trim(), "SIPGAB");
                }
                MessageBox.Show("DATOS GRABADOS!!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtHrLLego.Text = "";
                TxtTran.Text = "";
                TxtChofer.Text = "";
                TxtDest.Text = "";
                TxtPLaca.Text = "";
                TxtPlaTra.Text = "";
                TxtRadio.Text = "";
                TxtLargo.Text = "";
                LblPLacaAso.Text = "";
                TxtHrLLego.Focus();
                RegistradoAguilares = "";
            }
            if (Program.MyGlobal.AltaReg == "M")
            {
                thisConnecion.Open();
                //thisConnecionDBGAB.Open();
                string Cadena = "Update TB_MSTR_TRAILER SET HORAENT = '" + TxtHrEnt.Text + "',TEMP = '" + TxtTemp.Text + "',HORASAL = '" + TxtHrSal.Text + "',TIEMPOTOT = '" + TxtTmpTot.Text + "', OBSTRANS = '" + RtBoxTra.Text + "', OBSCAUSA = '" + RtBoxCau.Text + "', OBSFALTA = '" + RtBoxFal.Text + "'";
                Cadena = Cadena + " WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                cmd.ExecuteNonQuery();
                //cmd = new SqlCommand(Cadena);
                //cmd.Connection = thisConnecion;  //DBGAB
                //cmd.ExecuteNonQuery();
                if (TxtHrSal.Text.ToString().Trim() == "00:00" && TxtHrEnt.Text.ToString().Trim() != "00:00")
                {
                    string fotoplaca = subir_foto(TxtFotoPla.Text.Trim());
                    string fototemp = subir_foto(TxtFotoTemp.Text.Trim());
                    string fototrailer = subir_foto(TxtFotoTra.Text.Trim());


                    Cadena = "INSERT INTO TB_FOTOS_TRAILER(FECHA,NO_TRAILER,FOTO_PLACA,CONSE,FOTO_TEMP,FOTO_TRANS)" +
                            " VALUES('" + DtFE.Value.ToShortDateString() + "','" + TxtPLaca.Text.Trim() + "','" + fotoplaca.Trim() + "','" + LblConse.Text + "','" + fototemp.Trim() + "','" + fototrailer.Trim() + "')";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();
                }
                if (TxtHrSal.Text.ToString().Trim() != "00:00" && TxtHrEnt.Text.ToString().Trim() != "00:00")
                {
                    Cadena = "Update TB_MSTR_TRAILER SET OBSTRANS = '" + RtBoxTra.Text + "', OBSCAUSA = '" + RtBoxCau.Text + "', OBSFALTA = '" + RtBoxFal.Text + "'";
                    Cadena = Cadena + " WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("DATOS GRABADOS!!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnGrabar.Enabled = false;
                thisConnecion.Close();
                //thisConnecionDBGAB.Close();
            }
            if (Program.MyGlobal.AltaReg == "MC")
            {
                thisConnecion.Open();
                //thisConnecionDBGAB.Open();
                string Cadena = "Update TB_MSTR_TRAILER SET OBSTRANS = '" + RtBoxTra.Text + "', OBSCAUSA = '" + RtBoxCau.Text + "', OBSFALTA = '" + RtBoxFal.Text + "'";
                Cadena = Cadena + " WHERE fecha = '" + Program.MyGlobal.PubFecEmb + "' and NO_TRAILER = '" + Program.MyGlobal.PubNoTrailer + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;  //DBGAB
                cmd.ExecuteNonQuery();
                MessageBox.Show("COMENTARIOS GRABADOS!!!", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BtnGrabar.Enabled = false;
                thisConnecion.Close();
                //thisConnecionDBGAB.Close();
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtRadio_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt32(TxtRadio.Text.Trim()) > 50)
            //   {
            //       MessageBox.Show("Error: La Cantidad no puede ser mayor a 50", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //       e.Handled = true;
            //       return;
            //   }

            if (TxtRadio.Text.Trim().Length == 2)
                if (Convert.ToInt32(TxtRadio.Text.Trim()) > 50)
                {
                    MessageBox.Show("Error: La Cantidad no puede ser mayor a 50", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                    return;
                }
            if (e.KeyChar == '\r')
                TxtLargo.Focus();

        }

        private void TxtHrLLego_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '\r')
            //{
            //    string Errmsg;
            //    if (ValidaHora(TxtHrLLego.Text, out Errmsg) == true)
            //    {
            //        e.Handled = ValidaHora(TxtHrLLego.Text);
            //        return;
            //    }
            //    TxtTran.Focus();
            //}

        }

        public bool ValidaHora(string Hora, out string errorMessage)
        {
            bool msg = false;
            string msgErr = "";
            if (Hora.Trim().Length < 5)
            {
                msg = true;
                msgErr = "la longitud debe ser de 5 caracteres ejemplo 02:25";
            }
            else
                if (Convert.ToInt32(Hora.Trim().Substring(0, 2)) > 24)
            {
                msgErr = "Error: La Hora no puede ser mayor a 24";
                msg = true;
            }
            else
                    if (Convert.ToInt32(Hora.Trim().Substring(3, 2)) > 59)
            {
                msgErr = "Error: Los Minutos no puede ser mayor a 59";
                msg = true;
            }
            errorMessage = msgErr;
            return msg;

        }

        private void TxtTran_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TxtChofer.Focus();
        }

        private void TxtChofer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TxtDest.Focus();
        }

        private void TxtPLaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter) && !(char.IsLetter(e.KeyChar)))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\r')
            {
                if (TxtPLaca.Text != "")
                {
                    // Región para la validación de placas bloqueadas
                    #region VALIDACIÓN DE PLACAS BLOQUEADAS

                    // Apertura de la conexión a la base de datos (comentada en el código original)
                    // thisConnecion.Open();
                    if (thisConnecion.State == System.Data.ConnectionState.Closed) { thisConnecion.Open(); }

                    // Consulta SQL para buscar la placa en la vista de la lista negra
                    string strLN = "SELECT * FROM vw_lista_negra_actual WHERE placa = @placa"; // Usar parámetros para evitar inyección SQL
                    SqlCommand cmndLN = thisConnecion.CreateCommand();
                    cmndLN.CommandText = strLN;
                    cmndLN.Parameters.AddWithValue("@placa", TxtPLaca.Text.Trim()); // Parámetro seguro

                    // Objeto para leer los datos de la consulta
                    SqlDataReader dtrdLN;
                    dtrdLN = cmndLN.ExecuteReader();

                    // Variables para almacenar los datos obtenidos de la lista negra
                    string placaLN = "";
                    DateTime FechaLN = DateTime.MinValue; // Asignar un valor predeterminado
                    string MotivoLN = "";
                    bool placaEncontrada = false;
                    // Lectura de los resultados de la consulta
                    while (dtrdLN.Read())
                    {
                        placaLN = dtrdLN.GetString(1);          // Obtiene la placa (columna 0)
                        FechaLN = dtrdLN.GetDateTime(2);        // Obtiene la fecha de bloqueo (columna 1)
                        MotivoLN = dtrdLN.GetString(3);         // Obtiene el motivo del bloqueo (columna 2)
                        placaEncontrada = true;
                    }

                    // Cierre del lector y la conexión
                    dtrdLN.Close();
                    thisConnecion.Close();

                    // Verifica si se encontró una placa en la lista negra
                    if (placaEncontrada)
                    {
                        // Muestra un mensaje de advertencia indicando que la placa está bloqueada
                        MessageBox.Show(
                            $"LA PLACA {placaLN} SE ENCUENTRA BLOQUEADA DESDE {FechaLN:dd/MM/yyyy} " +
                            $"POR EL SIGUIENTE MOTIVO: {MotivoLN}. FAVOR DE COMUNICARSE CON EL PERSONAL CORRESPONDIENTE.",
                            "PLACA BLOQUEADA",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        // Coloca el foco en el campo de texto de la placa
                        TxtPLaca.Focus();
                        TxtPLaca.SelectAll();

                        // Finaliza la ejecución del método
                        return;
                    }

                    #endregion

                    if (chexternos.Checked == true)
                    {
                        return;
                    }

                    thisConnecion.Open();
                    string Cadena = "SELECT no_trailer from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' and fecha = '" + DtFE.Value.ToShortDateString() + "'";
                    SqlCommand cmd;
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    string Placa = Convert.ToString(cmd.ExecuteScalar()).Trim();
                    thisConnecion.Close();
                    if (Placa.Trim().Length > 0)
                    {
                        thisConnecion.Open();
                        Cadena = "SELECT responsable from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' and fecha = '" + DtFE.Value.ToShortDateString() + "'";
                        cmd = new SqlCommand(Cadena);
                        cmd.Connection = thisConnecion;
                        string responsable = Convert.ToString(cmd.ExecuteScalar()).Trim();
                        thisConnecion.Close();

                        if (responsable == "J CONCEPCION RAZO PIZANO")
                        {
                            MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA DE HOY EN AGUILARES NO SE PUEDE DUPLICAR, FAVOR DE AVISARLE AL VENDEDOR", "PLACA CAPTURADA EN AGUILARES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RegistradoAguilares = "1";

                        }
                        else
                        {
                            MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA DE HOY NO SE PUEDE DUPLICAR", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TxtPLaca.Text = "";
                            TxtNoPed.Text = "";
                            TxtPLaca.Focus();
                        }
                    }
                    else
                    {
                        //validacion de placa en dia anterior abierta
                        thisConnecion.Open();
                        Cadena = "SELECT hora_trailer from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' AND horafin = '--:--' AND Guardar = 'N'";
                        cmd = new SqlCommand(Cadena);
                        cmd.Connection = thisConnecion;
                        string fechaPlaca = Convert.ToString(cmd.ExecuteScalar()).Trim();
                        thisConnecion.Close();
                        if (fechaPlaca.Trim().Length > 0)
                        {
                            MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA: " + fechaPlaca + " Y No Se Cerro, Favor de Informar a Embarques para cerrar la placa y volver a intenarlo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            TxtPLaca.Text = "";
                            TxtNoPed.Text = "";
                            TxtPLaca.Focus();
                        }
                        else
                        {
                            string pedidoexiste = Valida_Placa_Pedido();
                            if (pedidoexiste == "S")
                            {
                                MessageBox.Show("Error: ESTA PLACA " + TxtPLaca.Text + System.Environment.NewLine + "   " + " YA FUE ASOCIADO A UN PEDIDO " + TxtNoPed.Text +
                                                     System.Environment.NewLine + "SI LA INFORMACION NO ES CORRECTA PUEDE MODIFICARLO POR LA CORRECTA", "PLACA ASOCIADA A ORDEN DE VENTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            TxtPlaTra.Focus();
                        }
                    }
                }
            }
        }

        private bool EstaEnListaNegra(string placa, out string mensaje)
        {
            bool bloqueada = false;
            mensaje = string.Empty;

            try
            {
                if (thisConnecion.State != ConnectionState.Open)
                    thisConnecion.Open();

                using (SqlCommand cmd = new SqlCommand("dbo.sp_ValidarListaNegra", thisConnecion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Placa", placa);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bloqueada = reader.GetInt32(reader.GetOrdinal("EstaEnListaNegra")) == 1;
                            mensaje = reader["Mensaje"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                bloqueada = false;
                mensaje = "Error al validar la placa: " + ex.Message;
            }
            finally
            {
                if (thisConnecion.State == ConnectionState.Open)
                    thisConnecion.Close();
            }

            return bloqueada;
        }

        private void TxtDest_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TxtPLaca.Focus();
        }

        private void TxtPlaTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                string Existe = Valida_Placa(TxtPlaTra.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "S")
                {
                    MessageBox.Show("Error: YA Se Capturo la PLACA" + System.Environment.NewLine + "   " + TxtPlaTra.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtPlaTra.Focus();
                    TxtRadio.Enabled = false;
                    return;
                }
                TxtRadio.Enabled = true;
                TxtRadio.Focus();
            }
        }

        private void TxtLargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                TxtNoPed.Focus();
        }

        private void CmbTra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CmbTra.SelectedValue != null)
                TxtTran.Text = CmbTra.SelectedValue.ToString();
            //TxtTran.Focus();
        }

        private void TxtHrEnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //    if (ValidaHora(TxtHrEnt.Text) == true)
                //        {
                //            e.Handled = ValidaHora(TxtHrLLego.Text);
                //            return;
                //        }
                TxtTemp.Focus();
            }
        }

        private void TxtHrSal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //if (ValidaHora(TxtHrSal.Text) == true)
                //{
                //    e.Handled = ValidaHora(TxtHrSal.Text);
                //    return;
                //}
                TxtTmpTot.Text = Frm1.Fn_Tiempo2(TxtHrLLego.Text, TxtHrSal.Text);
                //TxtTemp.Focus();
            }
        }

        private void BtnBuscarImg1_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            try
            {
                OpenFile.InitialDirectory = @"C:\Users\VIGILANCIA\Documents\Bluetooth\Inbox";
            }
            catch
            {
                OpenFile.InitialDirectory = @"" + System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "";
            }

            OpenFile.Filter = "Jpg File (*.jpg)|*.jpg";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                PbxPla.ImageLocation = OpenFile.FileName.ToString();
                TxtFotoPla.Text = OpenFile.FileName.ToString();
            }
        }

        private void BtnBuscarImg2_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();

            OpenFile.Filter = "Jpg File (*.jpg)|*.jpg";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                PbxTemp.ImageLocation = OpenFile.FileName.ToString();
                TxtFotoTemp.Text = OpenFile.FileName.ToString();
            }
        }

        private void BtnBuscarImg3_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();

            OpenFile.Filter = "Jpg File (*.jpg)|*.jpg";
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                PbxTra.ImageLocation = OpenFile.FileName.ToString();
                TxtFotoTra.Text = OpenFile.FileName.ToString();
            }
        }

        private void TxtNoPed_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Enter))
            {
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\r')
            {
                string Existe = ValidaPedido(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "N")
                {
                    MessageBox.Show("Error: NO EXISTE EL PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " EN EL SISTEMA ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    return;
                }
                Existe = ValidaProveedor(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "S")
                {
                    MessageBox.Show("Error: PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " ES UN PEDIDO DE CAMIONETAS EN EL SISTEMA, NO SE PUEDE ASIGNAR A UN TRAILER", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Text = "";
                    TxtNoPed.Focus();
                    return;
                }
                if (TxtPLaca.Text.Trim() != LblPLacaAso.Text.Trim() && LblPLacaAso.Text.Trim().Length > 0)
                {
                    MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " YA FUE ASOCIADO CON OTRA PLACA : " + LblPLacaAso.Text +
                                    System.Environment.NewLine + " SI CONTINUA LAS PLACAS SE ACTUALIZARAN EN LOS PEDIDOS ", "ORDEN DE VENTA ASOCIADA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " YA FUE ASOCIADO CON OTRA PLACA : "+ LblPLacaAso.Text +
                    //                System.Environment.NewLine + " VERIFICARLO CON VENTAS ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    e.Handled = true;
                }
                string existeorigen = ValidaPedidoOrigen(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (existeorigen == "S")
                {
                    MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " NO ES EL PEDIDO ORIGEN/PRINCIPAL " + LblPLacaAso.Text +
                                    System.Environment.NewLine + " VERIFICARLO CON VENTAS ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    e.Handled = true;
                    TxtNoPed.Text = "";
                    return;
                }
                string existeautorizado = ValidaEstado(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (existeautorizado == "P")
                {
                    MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " SE DIO DE ALTA FUERA DE TIEMPO Y NO ESTA AUTORIZADO A CARGAR, VERIFICARLO CON VENTAS ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Text = "";
                    TxtNoPed.Focus();
                    return;
                }
            }
        }

        private string Valida_Placa(string Mpla, string Mfec)
        {
            string Existe = "N";
            thisConnecion.Open();
            //thisConnecionDBGAB.Open();
            string Cadena = "Select no_trailer from TB_MSTR_TRAILER WHERE fecha = '" + Mfec + "' and NO_TRAILER = '" + Mpla + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string Placa = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (Placa.Trim().Length > 0)
                Existe = "S";
            thisConnecion.Close();
            return Existe;
        }

        private string Valida_Placa_Pedido()
        {
            string pedido_nocoincide = TxtNoPed.Text.Trim();
            string Existe = "N";
            thisConnecion.Open();
            //thisConnecionDBGAB.Open();

            string Cadena = "Select pdn_folio from tb_mstr_pedidos_nal WHERE placacaja = '" + TxtPLaca.Text.Trim() + "' AND pdn_pedorigen IN ('', '0') AND pdn_estatus NOT IN ('C', 'F') AND PDN_surtido != 'S'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string pdn_folio = Convert.ToString(cmd.ExecuteScalar()).Trim();

            #region Obtener Email PDN_Elaboro NACIONAL
            string strCadenaMail = "select usu_email from tb_cat_usuarios where usu_login=(SELECT PDN_ELABORO FROM TB_MSTR_PEDIDOS_NAL WHERE pdn_folio='" + TxtNoPed.Text + "')";
            SqlCommand cmdMail;
            cmdMail = new SqlCommand(strCadenaMail);
            cmdMail.Connection = thisConnecion;
            string email = Convert.ToString(cmdMail.ExecuteScalar()).Trim();
            #endregion

            if (pdn_folio.Trim().Length > 0)
            {
                TxtNoPed.Text = pdn_folio;
                Existe = "S";
            }
            thisConnecion.Close();
            if (Existe == "N")
            {
                thisConnecion.Open();
                Cadena = "Select pdn_folio from tb_mstr_pedidos_exp WHERE placacaja = '" + TxtPLaca.Text.Trim() + "' AND pdn_pedorigen IN ('', '0') AND pdn_estatus NOT IN ('C', 'F') AND PDN_surtido != 'S'";
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                pdn_folio = Convert.ToString(cmd.ExecuteScalar()).Trim();

                #region Obtener Email PDN_Elaboro exportacion
                strCadenaMail = "select usu_email from tb_cat_usuarios where usu_login=(SELECT PDN_ELABORO FROM TB_MSTR_PEDIDOS_EXP WHERE pdn_folio='" + TxtNoPed.Text + "')";
                cmdMail = new SqlCommand(strCadenaMail);
                cmdMail.Connection = thisConnecion;
                email = Convert.ToString(cmdMail.ExecuteScalar()).Trim();
                #endregion

                if (pdn_folio.Trim().Length > 0)
                {
                    TxtNoPed.Text = pdn_folio;
                    Existe = "S";
                }
                thisConnecion.Close();
            }



            if (pedido_nocoincide != TxtNoPed.Text.Trim() && Existe != "N")
            {
                MessageBox.Show("Error: EL PEDIDO " + pedido_nocoincide + " NO COINCIDE CON EL DECLARADO COMO PEDIDO ORIGEN EN VENTAS (" + TxtNoPed.Text + ") FAVOR DE CORROBORAR CON VENTAS, PLACA ASOCIADA AL PEDIDO " + TxtNoPed.Text + " ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                SendMailNocoincide("mdelrio@mrlucky.com.mx;hector.segura@nrlucky.com.mx;ahernandez@mrlucky.com.mx;logistica@mrlucky.com.mx;jgalvan@mrlucky;ricardo.cortes@mrlucky.com.mx;" + email, pedido_nocoincide, TxtNoPed.Text.Trim());
            }

            return Existe;
        }

        public void SendMailNocoincide(string Dest, string pedidoingresado, string pedidoencontrado)
        {
            string correofrom = "vigilancia@mrlucky.com.mx";
            string contraseñacorreo = "viggab";
            string NombreSolicitante = "";
            string mBody;

            //Dest.Replace(correofrom+"@mrlucky.com.mx;", "");
            MailMessage msg = new MailMessage();
            MailMessage email = new MailMessage();


            mBody = "Buen día <br/> Se encontro una diferencia entre los pedidos de la placa " + TxtPLaca.Text.Trim() + " Pedido Ingresado Por vigilancia " + pedidoingresado + " - Pedido Encontrado en las base de datos asignado por ventas = " + pedidoencontrado + "<br/>";




            //string Cadenax = "SELECT * FROM  tb_det_pend_embarque WHERE pdnorigen IN (" + pedidos.TrimEnd(',') + ") AND estatus NOT IN ('C', 'S')";
            string correo = "";
            string resp = "";
            string resparea = "";


            mBody += "GRACIAS!!";

            if (correo != null && correo != "")
            {
                Dest = Dest + ";" + correo;
            }


            string[] destinatarios = Dest.Split(';');
            foreach (string destinos in destinatarios)
            {
                email.To.Add(new MailAddress(destinos));
            }

            email.From = new MailAddress(correofrom); //
            MailAddress bcc = new MailAddress("jgalvan@mrlucky.com.mx");
            email.Bcc.Add(bcc);
            email.Subject = "DIFERENCIA DE PEDIDO GENERADO EN VIGILANCIA"; //"Mensaje de Prueba";
            email.Body = mBody;  //"Información de la factura";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail1.mrlucky.com.mx";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new NetworkCredential("dmunoz", "GuIraSis003$1234");
            smtp.Credentials = new NetworkCredential(correofrom, contraseñacorreo);

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;

            try
            {
                smtp.Send(email);
                email.Dispose();
                MessageBox.Show("correo enviado Correctamente");

            }
            catch (System.Exception ex)
            {

                MessageBox.Show("correo no enviado");
            }
        }

        private string ValidaPedido(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 250000)
                BD = "tb_mstr_pedidos_exp";
            string Cadena = "SELECT pdn_folio from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string Placa = Convert.ToString(cmd.ExecuteScalar()).Trim();
            Cadena = "SELECT prov_clave from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            TxtTran.Text = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (TxtTran.Text == "PC" || TxtTran.Text == "PA" || TxtTran.Text == "")
            {
                string message = "El proveedor de la orden de venta no es valido, debera seleccionar el proveedor del menu de opciones ¿Esta de acuerdo?";
                string title = "Proveedor No valido";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    TxtTran.Text = "";
                    CmbTra.Focus();
                    TxtTran.Text = "";
                }
                else
                {
                    // Do something  
                }

            }
            else
            {
                CmbTra.SelectedValue = TxtTran.Text;
                TxtTran.ReadOnly = true;
            }
            if (Placa.Trim().Length > 0)
                Existe = "S";


            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            Cadena = "Select placacaja  from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            LblPLacaAso.Text = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (StaAnt == "C")
                thisConnecion.Close();
            return Existe;
        }

        private string ValidaPedidoOrigen(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 300000)
                BD = "tb_mstr_pedidos_exp";
            string Cadena = "SELECT pdn_pedorigen from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string pedidoorigen = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (pedidoorigen.Trim() != "0" && pedidoorigen.Trim() != "")
                Existe = "S";
            if (StaAnt == "C")
                thisConnecion.Close();
            return Existe;
        }

        private string ValidaEstado(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 300000)
                BD = "tb_mstr_pedidos_exp";
            string Cadena = "SELECT pdn_autorizado from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            Existe = Convert.ToString(cmd.ExecuteScalar()).Trim();
            thisConnecion.Close();
            return Existe;
        }

        private string validarproveedor()
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            string Cadena = "SELECT prov_clave FROM tb_cat_proveedor Where prov_clave = '" + TxtTran.Text.Trim() + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string proveedor = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (proveedor.Trim() != "")
                Existe = "S";
            if (StaAnt == "C")
                thisConnecion.Close();
            return Existe;
        }

        private string ValidaProveedor(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 300000)
                return Existe;
            string Cadena = "SELECT prov_clave from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string proveedor = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (proveedor.Trim() == "MRLUCKY")
                Existe = "S";
            if (StaAnt == "C")
                thisConnecion.Close();
            return Existe;
        }

        private string Validasurtido(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 300000)
                return Existe;
            string Cadena = "SELECT pdn_surtido from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string proveedor = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (proveedor.Trim() == "S")
                Existe = "S";
            if (StaAnt == "C")
                thisConnecion.Close();
            return Existe;
        }

        private string Validadadoalta(string Mped, string Mfec)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string Cadena = "SELECT CONCAT ('EL PEDIDO ', pdn_folio, ' Ya Fue Registrado el dia ', CONVERT(varchar,fecha,5), ' Por el Trailer ', no_trailer) from tb_mstr_trailer WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string pedido = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (pedido.Trim() != "")
                Existe = "S";
            if (StaAnt == "C")
                thisConnecion.Close();
            return pedido;
        }

        private string traemensajealta(string Mped, string Mfec, string no_trailer)
        {
            string Existe = "N";
            LblPLacaAso.Text = "";
            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string Cadena = "SELECT CONCAT ('EL PEDIDO ', emb_folio, ' Ya Fue Cargado el dia ', CONVERT(varchar,fecha_cap,5), ' Por el Trailer ', no_trailer) from tb_mstr_embarque WHERE emb_folio = '" + Mped + "' AND no_trailer = '" + no_trailer + "'";
            if (MFol < 500000)
            {
                Cadena = "SELECT CONCAT ('EL PEDIDO ', emb_folio, ' Ya Fue Cargado el dia ', CONVERT(varchar,fecha_cap,5), ' Por el Trailer ', no_trailer) from tb_mstr_embarque WHERE emb_folio = '0" + Mped + "' AND no_trailer = '" + no_trailer + "'";
            }
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string pedido = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (pedido.Trim() != "")
                Existe = pedido;
            if (StaAnt == "C")
                thisConnecion.Close();
            return pedido;
        }

        private void TxtPLaca_Leave(object sender, EventArgs e)
        {
            if (TxtPLaca.Text != "")
            {

                // Región para la validación de placas bloqueadas
                #region VALIDACIÓN DE PLACAS BLOQUEADAS

                // Apertura de la conexión a la base de datos (comentada en el código original)
                if (thisConnecion.State == System.Data.ConnectionState.Closed) { thisConnecion.Open(); }

                // Consulta SQL para buscar la placa en la vista de la lista negra
                string strLN = "SELECT * FROM vw_lista_negra_actual WHERE placa = @placa"; // Usar parámetros para evitar inyección SQL
                SqlCommand cmndLN = thisConnecion.CreateCommand();
                cmndLN.CommandText = strLN;
                cmndLN.Parameters.AddWithValue("@placa", TxtPLaca.Text.Trim()); // Parámetro seguro

                // Objeto para leer los datos de la consulta
                SqlDataReader dtrdLN;
                dtrdLN = cmndLN.ExecuteReader();

                // Variables para almacenar los datos obtenidos de la lista negra
                string placaLN = "";
                DateTime FechaLN = DateTime.MinValue; // Asignar un valor predeterminado
                string MotivoLN = "";
                bool placaEncontrada = false;
                // Lectura de los resultados de la consulta
                while (dtrdLN.Read())
                {
                    placaLN = dtrdLN.GetString(1);          // Obtiene la placa (columna 0)
                    FechaLN = dtrdLN.GetDateTime(2);        // Obtiene la fecha de bloqueo (columna 1)
                    MotivoLN = dtrdLN.GetString(3);         // Obtiene el motivo del bloqueo (columna 2)
                    placaEncontrada = true;
                }

                // Cierre del lector y la conexión
                dtrdLN.Close();
                thisConnecion.Close();

                // Verifica si se encontró una placa en la lista negra
                if (placaEncontrada)
                {
                    // Muestra un mensaje de advertencia indicando que la placa está bloqueada
                    MessageBox.Show(
                        $"LA PLACA {placaLN} SE ENCUENTRA BLOQUEADA DESDE {FechaLN:dd/MM/yyyy} " +
                        $"POR EL SIGUIENTE MOTIVO: {MotivoLN}. FAVOR DE COMUNICARSE CON EL PERSONAL CORRESPONDIENTE.",
                        "PLACA BLOQUEADA",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    // Coloca el foco en el campo de texto de la placa
                    TxtPLaca.Focus();
                    TxtPLaca.SelectAll();

                    // Finaliza la ejecución del método
                    return;
                }

                #endregion


                if (chexternos.Checked == true)
                {
                    return;
                }

                if (thisConnecion.State == System.Data.ConnectionState.Closed) { thisConnecion.Open(); }
                string Cadena = "SELECT no_trailer from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' and fecha = '" + DtFE.Value.ToShortDateString() + "'";
                SqlCommand cmd;
                cmd = new SqlCommand(Cadena);
                cmd.Connection = thisConnecion;
                string Placa = Convert.ToString(cmd.ExecuteScalar()).Trim();
                thisConnecion.Close();
                if (Placa.Trim().Length > 0)
                {
                    thisConnecion.Open();
                    Cadena = "SELECT responsable from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' and fecha = '" + DtFE.Value.ToShortDateString() + "'";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    string responsable = Convert.ToString(cmd.ExecuteScalar()).Trim();
                    thisConnecion.Close();

                    if (responsable == "J CONCEPCION RAZO PIZANO")
                    {
                        MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA DE HOY EN AGUILARES, SE PROCEDE A ACTUALIZACION DE INFORMACION", "PLACA CAPTURADA EN AGUILARES", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RegistradoAguilares = "1";
                    }
                    else
                    {
                        MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA DE HOY NO SE PUEDE DUPLICAR", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtPLaca.Text = "";
                        TxtNoPed.Text = "";
                        TxtPLaca.Focus();
                    }
                }
                else
                {
                    //validacion de placa en dia anterior abierta
                    thisConnecion.Open();
                    Cadena = "SELECT hora_trailer from tb_mstr_trailer WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' AND horafin = '--:--' AND Guardar = 'N'";
                    cmd = new SqlCommand(Cadena);
                    cmd.Connection = thisConnecion;
                    string fechaPlaca = Convert.ToString(cmd.ExecuteScalar()).Trim();
                    thisConnecion.Close();
                    if (fechaPlaca.Trim().Length > 0)
                    {
                        MessageBox.Show("LA PLACA YA FUE CAPTURADA EL DIA: " + fechaPlaca + " Y No Se Cerro, Favor de Informar a Embarques para cerrar la placa y volver a intenarlo", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TxtPLaca.Text = "";
                        TxtNoPed.Text = "";
                        TxtPLaca.Focus();
                    }
                    else
                    {
                        string pedidoexiste = Valida_Placa_Pedido();
                        if (pedidoexiste == "S")
                        {
                            MessageBox.Show("INFORMACION: ESTA PLACA " + TxtPLaca.Text + System.Environment.NewLine + "   " + " YA FUE ASOCIADO A UN PEDIDO " + TxtNoPed.Text +
                                                 System.Environment.NewLine + "SI LA INFORMACION NO ES CORRECTA PUEDE MODIFICARLO POR LA CORRECTA Y CONTINUAR CON EL PROCESO", "PLACA ASOCIADA A ORDEN DE VENTA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        TxtPlaTra.Focus();
                    }
                }
            }
        }

        private void TxtHrEnt_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (ValidaHora(TxtHrEnt.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                //TxtHrEnt.Text.Select(0, TxtHrEnt.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                //this.errorProvider1.SetError(TxtHrEnt.Text, errorMsg);
                MessageBox.Show(errorMsg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
                e.Cancel = false;
        }

        private void TxtHrSal_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (ValidaHora(TxtHrSal.Text, out errorMsg))
            {
                // Cancel the event and select the text to be corrected by the user.
                e.Cancel = true;
                //TxtHrEnt.Text.Select(0, TxtHrEnt.Text.Length);

                // Set the ErrorProvider error with the text to display. 
                //this.errorProvider1.SetError(TxtHrEnt.Text, errorMsg);
                MessageBox.Show(errorMsg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
                e.Cancel = false;
        }

        private void TxtNoPed_Leave(object sender, EventArgs e)
        {

            if (TxtNoPed.Text != "")
            {
                string Existe = ValidaPedido(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "N")
                {
                    MessageBox.Show("Error: NO EXISTE EL PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " EN EL SISTEMA ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Focus();
                    return;
                }
                Existe = ValidaProveedor(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "S")
                {
                    MessageBox.Show("Error: PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " ES UN PEDIDO DE CAMIONETAS EN EL SISTEMA, NO SE PUEDE ASIGNAR A UN TRAILER", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Text = "";
                    TxtNoPed.Focus();
                    return;
                }
                Existe = Validasurtido(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe == "S")
                {
                    MessageBox.Show("Error: PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " YA FUE SURTIDO, NO SE PUEDE ASIGNAR A UN TRAILER", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //TxtNoPed.Text = "";
                    //TxtNoPed.Focus();
                    //return;
                }
                if (Convert.ToInt64(TxtNoPed.Text) < 61000)
                {
                    MessageBox.Show("Error: PEDIDO" + System.Environment.NewLine + "   " + TxtNoPed.Text + " NO ES Valido para el sistema Actual", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //TxtNoPed.Text = "";
                    //TxtNoPed.Focus();
                    //return;
                }
                Existe = Validadadoalta(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (Existe.Trim().Length > 0)
                {
                    string mensajealta = traemensajealta(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString(), TxtPlaTra.Text.Trim());
                    if (mensajealta.Trim().Length > 0)
                    {
                        MessageBox.Show(mensajealta.Trim(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //TxtNoPed.Text = "";
                        //TxtNoPed.Focus();
                        //return;

                    }
                    MessageBox.Show(Existe.Trim(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Text = "";
                    //TxtNoPed.Focus();
                    //return;

                }
                string existeorigen = ValidaPedidoOrigen(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (existeorigen == "S")
                {
                    MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " NO ES EL PEDIDO ORIGEN/PRINCIPAL " + LblPLacaAso.Text +
                                    System.Environment.NewLine + " VERIFICARLO CON VENTAS ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    TxtNoPed.Text = "";
                    TxtNoPed.Focus();
                    return;
                }//Validasurtido
                string existeautorizado = ValidaEstado(TxtNoPed.Text.Trim(), DtFE.Value.ToShortDateString());
                if (existeautorizado == "P")
                {
                    MessageBox.Show("Error: ESTE PEDIDO " + TxtNoPed.Text + System.Environment.NewLine + "   " + " SE DIO DE ALTA FUERA DE TIEMPO Y NO ESTA AUTORIZADO A CARGAR, VERIFICARLO CON VENTAS ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //TxtNoPed.Text = "";
                    TxtNoPed.Focus();
                    //return;
                }
            }

        }

        public string subir_foto(string rutaoriginal)
        {
            string sourceFile = @"" + rutaoriginal + "";
            string[] direccion = sourceFile.Split('\\');
            int posicion = direccion.Length - 1;
            string destinationFile = @"\\Gabira1\fotos_trailer\" + direccion[posicion] + "";

            // To move a file or folder to a new location:
            System.IO.File.Move(sourceFile, destinationFile);
            return destinationFile;

        }

        public void SendMailPendienteEmbarque(string Dest, string mBody, string pedidos)
        {
            string correofrom = "vigilancia@mrlucky.com.mx";
            string contraseñacorreo = "viggab";
            string NombreSolicitante = "";

            //Dest.Replace(correofrom+"@mrlucky.com.mx;", "");
            MailMessage msg = new MailMessage();
            MailMessage email = new MailMessage();


            mBody = "Buen día <br/> Se reporta el Trailer:  " + TxtPLaca.Text.Trim() + "<br/>";

            int cajas = 0;
            int presentacion = 0;
            string complementoasunto = "";


            complementoasunto = "PENDIENTE PARA EL TRAILER " + TxtPLaca.Text.Trim();
            mBody += "<p>FAVOR DE MANDAR PARA EL TRAILER " + TxtPLaca.Text.Trim() + " DE TRANSPORTE " + TxtTran.Text + ", CON EL OPERADOR " + TxtChofer.Text + ", EL CUAL YA ESTA REGISTRADO Y EN PLANTA<br/></p>";



            //string Cadenax = "SELECT * FROM  tb_det_pend_embarque WHERE pdnorigen IN (" + pedidos.TrimEnd(',') + ") AND estatus NOT IN ('C', 'S')";
            string correo = "";
            string resp = "";
            string resparea = "";

            string Cadenax = "SELECT A.*, B.usu_nombre, B.usu_email FROM tb_det_pend_embarque A INNER JOIN tb_cat_usuarios B ON A.solicitante = B.usu_login WHERE (A.estatus NOT IN ('C', 'S')) AND (A.pdnorigen IN (" + pedidos.TrimEnd(',') + "))";
            SqlCommand cmdx = new SqlCommand(Cadenax);
            cmdx.Connection = thisConnecion;
            SqlDataReader Info = cmdx.ExecuteReader();
            while (Info.Read())
            {
                mBody += "<font style='color:red'>PAQUETE CON DESTINO A: " + Info["destinopend"].ToString().Trim() + "</font> Con las observaciones Siguientes:<br/></p>";
                mBody += "<p>" + Info["observaciones"].ToString().Trim() + "<br/></p>";
                mBody += "<p>Area Responsable: " + Info["arearesp"].ToString().Trim() + "<br/> - RESPONSABLE: " + Info["responsable"].ToString().Trim() + " </p>";
                mBody += "Cualquier Duda o Comentario con el solicitante: <strong>" + Info["usu_nombre"].ToString().Trim() + "</strong>, Favor de confirmar de recibido<br/></p>";
                email.To.Add(new MailAddress(Info["usu_email"].ToString().Trim()));
                correo = Info["correoresp"].ToString().Trim();
            }

            mBody += "GRACIAS!!";

            if (correo != null && correo != "")
            {
                Dest = Dest + ";" + correo;
            }


            string[] destinatarios = Dest.Split(';');
            foreach (string destinos in destinatarios)
            {
                email.To.Add(new MailAddress(destinos));
            }

            email.From = new MailAddress(correofrom); //
            MailAddress bcc = new MailAddress("jgalvan@mrlucky.com.mx");
            email.Bcc.Add(bcc);
            email.Subject = complementoasunto; //"Mensaje de Prueba";
            email.Body = mBody;  //"Información de la factura";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;



            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mail1.mrlucky.com.mx";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new NetworkCredential("dmunoz", "GuIraSis003$1234");
            smtp.Credentials = new NetworkCredential(correofrom, contraseñacorreo);

            System.Net.ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;

            try
            {
                smtp.Send(email);
                email.Dispose();
                MessageBox.Show("correo enviado Correctamente");

            }
            catch (System.Exception ex)
            {

                MessageBox.Show("correo no enviado");
            }
        }

        private void DtFE_ValueChanged(object sender, EventArgs e)
        {
            if (Program.MyGlobal.AltaReg == "A")
            {
                if (thisConnecion.State == ConnectionState.Closed)
                {
                    thisConnecion.Open();
                }
                string Cadenax = "Select getdate()";
                SqlCommand cmdx = new SqlCommand(Cadenax, thisConnecion);
                DateTime FecServer = Convert.ToDateTime(cmdx.ExecuteScalar());
                if (thisConnecion.State == ConnectionState.Open)
                {
                    thisConnecion.Close();
                }
                string Hr = FecServer.ToString("HH:mm").Substring(0, 2);
                Int32 Hora = Convert.ToInt32(Hr);
                if (Hora >= 1 && Hora < 4)
                {
                    if (DtFE.Value.Date > FecServer.Date.AddDays(-1))
                    {
                        MessageBox.Show("No se pueden Registrar traler con fecha de hoy, porque aun no son las 4 de la mañana, se registrara con fecha de ayer", "No se puede registrar con fecha mayor");
                        DtFE.Value = DateTime.Now.AddDays(-1);
                    }

                }
            }

        }

        private string ValidaPedidoCancelado(string Mped)
        {
            string Existe = "N";

            string StaAnt = "";
            if (thisConnecion.State == ConnectionState.Closed)
            {
                thisConnecion.Open();
                StaAnt = "C";
            }
            //thisConnecionDBGAB.Open();
            Int32 MFol = Convert.ToInt32(TxtNoPed.Text);
            string BD = "tb_mstr_pedidos_nal";
            if (MFol < 250000)
                BD = "tb_mstr_pedidos_exp";
            string Cadena = "SELECT pdn_estatus from " + BD + " WHERE pdn_folio = '" + Mped + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string estado = Convert.ToString(cmd.ExecuteScalar()).Trim();
            if (estado.Trim() != "C")
            {
                Existe = "S";
            }
            else
            {
                Existe = "C";
            }
            if (thisConnecion.State == ConnectionState.Open)
            {
                thisConnecion.Close();
            }
            return Existe;
        }

        private void TxtTran_Leave(object sender, EventArgs e)
        {
            //SELECT prov_clave, prov_nombre,(prov_nombre + ' ' + prov_clave ) as Nombre FROM tb_cat_proveedor ORDER BY PROV_NOMBRE
            string Existe = validarproveedor();
            if (Existe == "N")
            {
                MessageBox.Show("Error: El proveedor no es correcto, NO SE PUEDE ASIGNAR A UN TRAILER", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                TxtTran.Text = "";
                TxtTran.Focus();
                return;
            }
        }

        private void TxtNoPed_TextChanged(object sender, EventArgs e)
        {

        }

        private void TimerHrLLego_Tick(object sender, EventArgs e)
        {
            // Obtener la hora del servidor de forma sincrónica
            DateTime horaServidor = ObtenerHoraDelServidor();

            // Actualizar el TextBox en el hilo principal de la interfaz gráfica
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(() =>
                {
                    TxtHrLLego.Text = horaServidor.ToString("HH:mm");
                }));
            }
            else
            {
                TxtHrLLego.Text = horaServidor.ToString("HH:mm");
            }
        }

        private void TimerHrEnt_Tick(object sender, EventArgs e)
        {
            // Obtener la hora del servidor de forma sincrónica
            DateTime horaServidor = ObtenerHoraDelServidor();

            // Actualizar el TextBox en el hilo principal de la interfaz gráfica
            if (this.InvokeRequired)
            {
                this.Invoke(new System.Action(() =>
                {
                    TxtHrEnt.Text = horaServidor.ToString("HH:mm");
                }));
            }
            else
            {
                TxtHrEnt.Text = horaServidor.ToString("HH:mm");
            }
        }

        public DateTime ObtenerHoraDelServidor()
        {
            DateTime horaServidor = DateTime.MinValue;

            // Asegúrate de que la conexión esté abierta antes de ejecutar el comando.
            if (thisConnecion.State != System.Data.ConnectionState.Open)
            {
                thisConnecion.Open();  // Abrir conexión si está cerrada.
            }

            using (SqlCommand comando = new SqlCommand("SELECT GETDATE()", thisConnecion))
            {
                object resultado = comando.ExecuteScalar();
                if (resultado != null)
                {
                    horaServidor = (DateTime)resultado;
                }
            }

            return horaServidor;
        }

        private void TxtPlaTra_TextChanged(object sender, EventArgs e)
        {

        }

        private void ActualizaEmbarqueAguilare(string placacaja, string pdn_fecha)
        {
            if (thisConnecion.State == System.Data.ConnectionState.Closed) { thisConnecion.Open(); }
            string Cadena = "SELECT pdn_folio, placa,  from tb_mstr_pedidos_nal WHERE no_trailer = '" + TxtPLaca.Text.Trim() + "' and fecha = '" + DtFE.Value.ToShortDateString() + "'";
            SqlCommand cmd;
            cmd = new SqlCommand(Cadena);
            cmd.Connection = thisConnecion;
            string Placa = Convert.ToString(cmd.ExecuteScalar()).Trim();
            thisConnecion.Close();
        }
    }
}

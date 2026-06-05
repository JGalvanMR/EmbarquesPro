using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using Utilerias;
using System.Data.SqlClient;
using Microsoft.VisualBasic.Devices;

namespace Embarques
{
    public partial class Geolocalizacion : Form
    {
        SqlConnection thisConnection = new SqlConnection(Utilerias.Class1.ConnectionString);
        SqlCommand cmnd2;
        SqlDataReader reader2;
        SqlCommand cmnd4;
        SqlDataReader reader4;
        SqlCommand cmnd5;
        SqlDataReader reader5;
        SqlCommand cmnd6;
        SqlDataReader reader6;
        int si = 0;

        int total_entrega = 0;
        int total_pedido = 0;
        int total_pedidos = 0;
        string fecha = "";
        string camioneta = "";
        double latitud = 20.6586337;
        double longitud = -101.2942987;

        GMarkerGoogle marker;
        GMapOverlay markerOverlay;
        DataTable dt;
        DataTable RecorridoDetalle = new DataTable();
        DataTable ReporteDetalle = new DataTable();

        internal readonly GMapOverlay routes = new GMapOverlay("routes");


        public Geolocalizacion()
        {
            InitializeComponent();
        }

        private void Geolocalizacion_Load(object sender, EventArgs e)
        {
            string ruta = @"C:\SisGabWeb\fondo_formularios.jpg";
            this.BackgroundImage = System.Drawing.Bitmap.FromFile(ruta);
            Computer mycomputer = new Computer();

            //Gmaps configuracion e inicializacion
            gMap.DragButton = MouseButtons.Left;
            gMap.CanDragMap = true;
            gMap.MapProvider = GMapProviders.GoogleHybridMap;
            gMap.Position = new PointLatLng(latitud, longitud);
            gMap.MinZoom = 0;
            gMap.MaxZoom = 24;
            gMap.Zoom = 17;
            gMap.AutoScroll = true;
            gMap.ShowCenter = false;

            

            RecorridoDetalle.Columns.Add("Hora");
            RecorridoDetalle.Columns.Add("Ubicacion");

            ReporteDetalle.Columns.Add("Id");
            ReporteDetalle.Columns.Add("Hora");
            ReporteDetalle.Columns.Add("Ubicacion");

            string Folio = (Program.MyGlobal.TipoPed == "EXP") ? "0" + Program.MyGlobal.NoPedido.ToString() : Program.MyGlobal.NoPedido.ToString();



            thisConnection.Open();
            string Cadena = "SELECT * FROM tb_det_embarque WHERE (latitud IS NOT NULL) AND emb_folio = '" + Folio + "'";

            SqlCommand cmX = new SqlCommand(Cadena, thisConnection);
            SqlDataReader drX = cmX.ExecuteReader();


            int ubicaciones = 0;
            double lat_ant = 0;
            double long_ant = 0;

            while (drX.Read())
            {
                if (drX["latitud"].ToString().Trim().Length > 0)
                {
                    latitud = Convert.ToDouble(drX["latitud"].ToString().Trim().Replace(",", "."));
                    longitud = Convert.ToDouble(drX["longitud"].ToString().Trim().Replace(",", "."));

                    PuntosYRutaReportes(latitud, longitud, drX["prod_clave"].ToString().Trim());

                    lat_ant = latitud;
                    long_ant = longitud;
                    ubicaciones++;
                }
            }

            thisConnection.Close();
            gMap.Zoom = 16;
            gMap.Zoom = 17;

        }

        public void PuntosYRutaReportes(Double Latitud, Double longitud, string ubicacionactual)
        {

            GMapOverlay markersOverlay = new GMapOverlay("markers");
            GMapMarker marker = new GMarkerGoogle(new PointLatLng(latitud, longitud), GMarkerGoogleType.red_pushpin);
            //GMarkerGoogleType.red_pushpin);
            markersOverlay.Markers.Add(marker);
            gMap.Overlays.Add(markersOverlay);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = "producto: " + ubicacionactual;
            //Trazado de la ruta de la camioneta
            /*if (ubicaciones > 0)
            {
                PointLatLng start = new PointLatLng(lat_ant, long_ant);
                PointLatLng end = new PointLatLng(latitud, longitud);

                MapRoute route = BingMapProvider.Instance.GetRoute(start, end, false, false, 15);
                GMapRoute r = new GMapRoute(route.Points, "Myroutes");
                GMapOverlay routesOverlay = new GMapOverlay("Myroutes");
                routesOverlay.Routes.Add(r);
                gMap.Overlays.Add(routesOverlay);
                r.Stroke.Width = 2;
                r.Stroke.Color = Color.SeaGreen;


                /*
                GDirections ss = new GDirections();
                //GDirections ss;
                bool direccion = false;
                int intentos = 0;

                while (direccion == false) {
                    var xx = GMapProviders.BingMap.GetRoute(start, end, true, false, 15);

                    if (xx.ToString() == "OK") {
                        direccion = true;
                    }
                    else if (xx.ToString() == "NOT_FOUND") {
                        MessageBox.Show("Una ruta no puede ser encontrada");
                        direccion = true;
                    }
                    else if (xx.ToString() == "REQUEST_DENIED")
                    {
                        MessageBox.Show("El servicio ha sido negado para Camionetas");
                        direccion = true;
                    }
                    else if (xx.ToString() == "ZERO_RESULTS")
                    {
                        MessageBox.Show("No se encontraron resultados");
                        direccion = true;
                    }
                    else if (intentos == 5) {
                        direccion = true;
                    }
                }

                if (ss != null ){
                    GMapRoute r = new GMapRoute(ss.Route, "My route");
                    r.Stroke = new Pen(randomColor(), 3);
                    r.Stroke.Width = 4;
                    GMapOverlay routesOverlay = new GMapOverlay("routes");
                    routesOverlay.Routes.Add(r);
                    gmap.Overlays.Add(routesOverlay);
                }*/
            //}
        }
    }
}

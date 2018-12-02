using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System.Windows;
using System.Windows.Media;


namespace Hack.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Create a point geometry
            var point = new MapPoint(-0.497443, 53.256998, SpatialReferences.Wgs84);

            System.Drawing.Color redColor = System.Drawing.Color.FromName("Red");
            System.Drawing.Color blueColor = System.Drawing.Color.FromName("Blue");

            //Create point symbol with outline
            var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, redColor, 10);
            pointSymbol.Outline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, blueColor, 2);

            //Create point graphic with geometry & symbol
            var pointGraphic = new Graphic(point, pointSymbol);

            //Add point graphic to graphic overlay
            MapGraphics.Graphics.Add(pointGraphic);

            //Create polygon geometry
            var polygonPoints = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);
            polygonPoints.Add(new MapPoint(-0.600128, 53.328413));
            polygonPoints.Add(new MapPoint(-0.549316, 53.342763));
            polygonPoints.Add(new MapPoint(-0.469666, 53.304621));
            polygonPoints.Add(new MapPoint(-0.523911, 53.231523));
            var polygon = new Polygon(polygonPoints);

            System.Drawing.Color blueColorTrans = System.Drawing.Color.FromArgb(128, 0, 0, 255);


            //Create symbol for polygon with outline
            var polygonSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, blueColorTrans,
             new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, redColor, 2));

            //Create polygon graphic with geometry and symbol
            Graphic polygonGraphic = new Graphic(polygon, polygonSymbol);

            //Add polygon graphic to graphics overlay
            MapGraphics.Graphics.Add(polygonGraphic);

            MapGraphics.Graphics.Clear();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}

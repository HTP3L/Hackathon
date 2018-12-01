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
        }
    }
}

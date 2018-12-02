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

        PersonOfInterest personOfInterest = new PersonOfInterest();

        public MainWindow()
        {
            InitializeComponent();

            InterestDatagrid.ItemsSource = personOfInterest.Incidents;

            personOfInterest.Incidents.Add(new Incident
            {
                XCoord = -0.600128,
                YCoord = 53.328413
            });


            personOfInterest.Incidents.Add(new Incident
            {
                XCoord = -0.549316,
                YCoord = 53.342763
            });

            personOfInterest.Incidents.Add(new Incident
            {
                XCoord = -0.469666,
                YCoord = 53.304621
            });

            personOfInterest.Incidents.Add(new Incident
            {
                XCoord = -0.523911,
                YCoord = 53.231523
            });


            

            
        }

        private void Clear()
        {
            MapGraphics.Graphics.Clear();
        }
        private void Draw(bool points, bool area)
        {
            System.Drawing.Color redColor = System.Drawing.Color.FromName("Red");
            System.Drawing.Color blueColor = System.Drawing.Color.FromName("Blue");

            if (points)
            {
                foreach (Incident incident in personOfInterest.Incidents)
                {
                    //Create a point geometry
                    var point = new MapPoint(incident.XCoord, incident.YCoord, SpatialReferences.Wgs84);



                    //Create point symbol with outline
                    var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, redColor, 10);
                    pointSymbol.Outline = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, blueColor, 2);

                    //Create point graphic with geometry & symbol
                    var pointGraphic = new Graphic(point, pointSymbol);

                    //Add point graphic to graphic overlay
                    MapGraphics.Graphics.Add(pointGraphic);
                }
            }





            if (area)
            {
                //Create polygon geometry
                var polygonPoints = new Esri.ArcGISRuntime.Geometry.PointCollection(SpatialReferences.Wgs84);

                foreach (Incident incident in personOfInterest.Incidents)
                {
                    polygonPoints.Add(new MapPoint(incident.XCoord, incident.YCoord));
                }

                var polygon = new Polygon(polygonPoints);

                System.Drawing.Color blueColorTrans = System.Drawing.Color.FromArgb(128, 0, 0, 255);


                //Create symbol for polygon with outline
                var polygonSymbol = new SimpleFillSymbol(SimpleFillSymbolStyle.Solid, blueColorTrans,
                 new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, redColor, 2));

                //Create polygon graphic with geometry and symbol
                Graphic polygonGraphic = new Graphic(polygon, polygonSymbol);

                //Add polygon graphic to graphics overlay
                MapGraphics.Graphics.Add(polygonGraphic);
            }
            
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
        }

        private void PointBox_Checked(object sender, RoutedEventArgs e)
        {
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
        }

        private void General_Checked(object sender, RoutedEventArgs e)
        {
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
        }

        private void AreaBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
        }

        private void PointBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
        }
    }
}

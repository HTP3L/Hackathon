using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;
using System.Windows;
using System.Windows.Media;
using Hack.Database;
using System.Collections.ObjectModel;

namespace Hack.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PersonOfInterest personOfInterest;
        Database.DatabaseWrapper databaseWrapper;
        WebScraper webScraper;
        ObservableCollection<Article> articles = new ObservableCollection<Article>();

        PersonOfInterest Person1 = new PersonOfInterest();
        PersonOfInterest Person2 = new PersonOfInterest();
        PersonOfInterest Person3 = new PersonOfInterest();

        public MainWindow()
        {
            InitializeComponent();

            databaseWrapper = new DatabaseWrapper("Data Source=C:/Users/Matthew Rimmer/Documents/GitHub/Hackathon/src/FinalDatabase.db;Version=3;new=False;");
            var queryResult1 = databaseWrapper.ReaderQuery("SELECT * FROM PersonOfInterest");

            PersonOfInterestDatagrid.ItemsSource = queryResult1[0];

            var queryResult2 = databaseWrapper.ReaderQuery("SELECT * FROM Address");

            AddressDatagrid.ItemsSource = queryResult2[0];

            var queryResult3 = databaseWrapper.ReaderQuery("SELECT * FROM PoliceCase");

            PoliceCaseDatagrid.ItemsSource = queryResult3[0];

            var queryResult4 = databaseWrapper.ReaderQuery("SELECT * FROM Contact");

            ContactDatagrid.ItemsSource = queryResult4[0];

            //var queryResult5 = databaseWrapper.ReaderQuery("SELECT * FROM ContactPoIRelationship");

            //ContactPoIRelDatagrid.ItemsSource = queryResult5[0];

            var queryResult6 = databaseWrapper.ReaderQuery("SELECT * FROM Report");

            ReportDatagrid.ItemsSource = queryResult6[0];

            //var queryResult7 = databaseWrapper.ReaderQuery("SELECT * FROM NewsArticles");

            //NewsArticlesDatagrid.ItemsSource = queryResult7[0];

            //var queryResult8 = databaseWrapper.ReaderQuery("SELECT * FROM SocialMediaPosts");

            //SocialMediaPostsDatagrid.ItemsSource = personOfInterest.Incidents;


            personOfInterest = Person1;


            PopulatePersons();  

            
        }

        private void PopulatePersons()
        {
            //Person 1
            Person1.Incidents.Add(new Incident
            {
                XCoord = -0.600128,
                YCoord = 53.328413
            });


            Person1.Incidents.Add(new Incident
            {
                XCoord = -0.549316,
                YCoord = 53.342763
            });

            Person1.Incidents.Add(new Incident
            {
                XCoord = -0.469666,
                YCoord = 53.304621
            });

            Person1.Incidents.Add(new Incident
            {
                XCoord = -0.523911,
                YCoord = 53.231523
            });

            //Person 2 

            Person2.Incidents.Add(new Incident
            {
                XCoord = -0.412448,
                YCoord = 53.391794
            });
            Person2.Incidents.Add(new Incident
            {
                XCoord = -0.933838,
                YCoord = 53.279995
            });
            Person2.Incidents.Add(new Incident
            {
                XCoord = -1.101379,
                YCoord = 52.915527
            });
            Person2.Incidents.Add(new Incident
            {
                XCoord = -0.675659,
                YCoord = 52.737955
            });
            Person2.Incidents.Add(new Incident
            {
                XCoord = -0.362549,
                YCoord = 52.835958
            });
            Person2.Incidents.Add(new Incident
            {
                XCoord = -0.214233,
                YCoord = 53.057723
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
            PointBox.IsChecked = true;
            AreaBox.IsChecked = true;






            //ScraperDatagrid.ItemsSource = WebScraper.Webscraper

            
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

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Person1_Checked(object sender, RoutedEventArgs e)
        {
            personOfInterest = Person1;
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
            InterestDatagrid.ItemsSource = personOfInterest.Incidents;
        }

        private void Person2_Checked(object sender, RoutedEventArgs e)
        {
            personOfInterest = Person2;
            Clear();
            Draw((bool)PointBox.IsChecked, (bool)AreaBox.IsChecked);
            InterestDatagrid.ItemsSource = personOfInterest.Incidents;
        }

        private void PoI1(object sender, RoutedEventArgs e)
        {
            var queryResult1 = databaseWrapper.ReaderQuery("SELECT * FROM PersonOfInterest");
            PersonOfInterestDatagrid.ItemsSource = queryResult1[0];
        }

        private void PoI2(object sender, RoutedEventArgs e)
        {
            var queryResult1 = databaseWrapper.ReaderQuery("SELECT * FROM PersonOfInterest");
            PersonOfInterestDatagrid.ItemsSource = queryResult1[1];
        }

        private void PoI3(object sender, RoutedEventArgs e)
        {
            var queryResult1 = databaseWrapper.ReaderQuery("SELECT * FROM PersonOfInterest");
            PersonOfInterestDatagrid.ItemsSource = queryResult1[2];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Article article in webScraper.Articles)
            {
                articles.Add(article);

            }

            ScraperDatagrid.ItemsSource = articles;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            webScraper = new WebScraper();

            webScraper.GetNews(ArticleTextTextbox.Text, int.Parse(ArticleCountTextbox.Text));


        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            webScraper.GetArticle(ArticleTextTextbox.Text, int.Parse(ArticleNumberSelectTextbox.Text));

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MainTextBox.Text = webScraper.ArticleText;
        }
    }
}

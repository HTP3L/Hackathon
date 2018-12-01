using Esri.ArcGISRuntime.Mapping;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Hack.Frontend
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            // When MapViewModel is created, call a function to open a web map from ArcGIS Online
            LoadWebMap();
        }

        /// <summary>
        /// Opens a web map stored in ArcGIS Online and uses it to set the MapViewModel.Map property
        /// </summary>
        private async void LoadWebMap()
        {
            // Unique ID for an ArcGIS Online portal item (web map)
            var itemId = "036bc85606d642b0a8b8f730e6f4d51e";

            // URL that points to the item
            var webMapUrl = string.Format("http://www.arcgis.com/sharing/rest/content/items/{0}/data", itemId);
           
            // Create a new Map from the portal item
            Esri.ArcGISRuntime.Mapping.Map webMap = new Map(new System.Uri(webMapUrl));

            // Assign the map to the MapViewModel.Map property 
            // (MapView.Map is bound to this property, so this will display the map in your app)
            Map = webMap;
        }

        private Map _map;

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack.Frontend
{
    class Incident
    {
        public int ID { get; set; }
        public string TypeOfIncident { get; set; }
        public double XCoord { get; set; }
        public double YCoord { get; set; }
        public string Description { get; set; }

       public Incident()
        {
            Random random = new Random();
            ID = random.Next(0, 9999);
            TypeOfIncident = "Sighting";
            Description = "N/A";
        }
    }
}

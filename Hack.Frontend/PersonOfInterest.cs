using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hack.Frontend
{
    class PersonOfInterest
    {
        public string Name { get; set; }

        public ObservableCollection<Incident> Incidents = new ObservableCollection<Incident>();
    }
}

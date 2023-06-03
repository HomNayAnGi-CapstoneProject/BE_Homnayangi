using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class GoongDistanceResponse
    {
        public List<double> GeocodedWaypoints { get; set; }
        public List<GoongRoute> Routes { get; set; }
    }
}

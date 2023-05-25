using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class GoongRoute
    {
        public List<GoongLeg> Legs { get; set; }
        public GoongOverviewPolyline OverviewPolyline { get; set; }
        public List<string> Warnings { get; set; }
        public List<int> WaypointOrder { get; set; }
    }
}

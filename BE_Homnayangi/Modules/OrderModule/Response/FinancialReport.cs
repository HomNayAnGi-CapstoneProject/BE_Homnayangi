using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class FinancialReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Revenue { get; set; }
        public decimal ShipCost { get; set; }

        public int TotalOrders { get; set; }
        public int TotalPackages { get; set; }
        public int DeliveredOrders { get; set; }
        public int DeniedOrders { get; set; }
        public int CanceledOrders { get; set; }
    }

    public class TrendingPackage
    {
        public Guid PackageId { get; set; }
        public string PackageTitle { get; set; }
        public int Count { get; set; }
    }

}


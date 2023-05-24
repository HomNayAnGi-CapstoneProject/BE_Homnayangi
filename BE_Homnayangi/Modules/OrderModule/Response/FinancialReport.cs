using System;
using System.Collections.Generic;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class FinancialReport
    {
        public DateTime StartDate;
        public DateTime EndDate;
        public decimal Revenue;
        //public decimal Expenses;
        //public decimal Gains;
        //public decimal Losses;
        //public decimal NetIncome;
        public decimal TotalShipCost;

        public int TotalOrder;
        public int TotalPaypalOrder;
        public int DeliverdOrderCount;
        public int DeliverdPaypalOrderCount;
        public int DeniedOrderCount;
        public int DeniedPaypalOrderCount;
        public int CanceledOrderCount;
        public int CanceledPaypalOrderCount;

        public List<TrendingPackage> TrendingPackages;
    }

    public class TrendingPackage
    {
        public Guid PackageId;
        public string PackageTitle;
        public int Count;
    }
}


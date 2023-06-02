using BE_Homnayangi.Modules.OrderModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule.Interface
{
    public interface IOrderService
    {
        public Task<string> AddNewOrder(Order newOrder);

        public Task UpdateOrder(Order OrderUpdate);

        public Task<ICollection<Order>> GetAll();

        public Task<ICollection<OrderResponse>> GetOrderResponse(DateTime? fromDate, DateTime? toDate, int status = -1);

        public Task<ICollection<OrderResponse>> GetOrderByCustomer(DateTime? fromDate, DateTime? toDate, Guid customerId, int status = -1);

        public Task<ICollection<Order>> GetByCustomer(Guid id);

        public Task<ICollection<Order>> GetOrdersBy(
            Expression<Func<Order, bool>> filter = null,
            Func<IQueryable<Order>, ICollection<Order>> options = null,
            string includeProperties = null);

        public Task<ICollection<Order>> GetRandomOrdersBy(Expression<Func<Order, bool>> filter = null,
            Func<IQueryable<Order>, ICollection<Order>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Order GetOrderByID(Guid? id);

        public Task DeleteOrder(Guid id);

        public Task PaidOrder(Guid id);

        public Task AcceptOrder(Guid id);

        public Task DenyOrder(Guid id);

        public Task RefundOrder(Guid id);

        public Task CancelOrder(Guid id);

        public Task Shipping(Guid id);

        public Task Delivered(Guid id);

        public Task DeliveredFail(Guid id);

        public Task<string> PaymentWithPaypal(
            Guid orderId,
            string Cancel = null,
            string blogId = "",
            string PayerID = "",
            string guid = "");

        public List<string> GetLocalDistrict();
        public List<string> GetLocalWard(string district);
        public Task<decimal> CalculateShippingCost(double lat2, double lon2);

        public Task<FinancialReport> GetYearlyFinancialReport(int year);
        public Task<ICollection<FinancialReport>> ExportYearlyFinancialReport(int year);
        public Task<FinancialReport> GetMonthlyFinancialReport(int month, int year);
        public Task<ICollection<FinancialReport>> ExportMonthlyFinancialReport(int month, int year);
        public Task<FinancialReport> GetFinancialReport(DateTime startDate, DateTime endDate);

        public Task<ICollection<TrendingPackage>> GetTrendingPackages(DateTime startDate, DateTime endDate);

    }
}

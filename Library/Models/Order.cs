using System;
using System.Collections.Generic;

#nullable disable

namespace Library.Models
{
    public partial class Order
    {
        public Order()
        {
            Notifications = new HashSet<Notification>();
            OrderCookedDetails = new HashSet<OrderCookedDetail>();
            OrderIngredientDetails = new HashSet<OrderIngredientDetail>();
            OrderPackageDetails = new HashSet<OrderPackageDetail>();
        }

        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShippedAddress { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OrderStatus { get; set; }
        public Guid? UserId { get; set; }

        public virtual Transaction OrderNavigation { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<OrderCookedDetail> OrderCookedDetails { get; set; }
        public virtual ICollection<OrderIngredientDetail> OrderIngredientDetails { get; set; }
        public virtual ICollection<OrderPackageDetail> OrderPackageDetails { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BE_Homnayangi.Models2
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShippedAddress { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OrderStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public bool? IsCooked { get; set; }
        public int? TransactionStatus { get; set; }
        public Guid? VoucherId { get; set; }
        public int? PaymentMethod { get; set; }
        public decimal? ShippingCost { get; set; }
        public string PaypalUrl { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Voucher Voucher { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

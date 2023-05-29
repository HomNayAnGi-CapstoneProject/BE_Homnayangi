using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Library.Models;

namespace BE_Homnayangi.Modules.OrderModule.Request
{
    public class CreateOrderRequest
    {
        [MaxLength(255, ErrorMessage = "Max length is 255")]
        public string ShippedAddress { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public Guid? VoucherId { get; set; }
        public bool? IsCooked { get; set; }
        public int? PaymentMethod { get; set; }
        public decimal? ShippingCost { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}


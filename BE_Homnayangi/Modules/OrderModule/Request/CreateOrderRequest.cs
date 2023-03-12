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
        //[Range(0, 100, ErrorMessage = "Discount from 0 to 100")]
        public decimal? Discount { get; set; }
        //[MinLength(1, ErrorMessage = "Total price must higher than 0")]
        public decimal? TotalPrice { get; set; }
        public Guid? VoucherId { get; set; }
        public int? PaymentMethod { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}


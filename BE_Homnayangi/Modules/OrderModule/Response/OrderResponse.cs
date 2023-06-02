using System;
using System.Collections.Generic;
using Library.Models;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class OrderResponse
    {
        public class IngredientResponse
        {
            public Guid IngredientId { get; set; }
            public int? Quantity { get; set; }
            public decimal? Price { get; set; }
            public decimal? ShippingCost { get; set; }
            public string IngredientImage { get; set; }
            public string IngredientName { get; set; }

        }
        public class OrderDetailResponse
        {
            public OrderDetailResponse()
            {
                PackageDetails = new HashSet<IngredientResponse>();
            }

            public Guid OrderId { get; set; }

            public Guid? PackageId { get; set; }
            public string PackageImage { get; set; }
            public string PackageName { get; set; }
            public int PackageQuantity { get; set; }
            public decimal? PackagePrice { get; set; }
            public decimal? ShippingCost { get; set; }

            public ICollection<IngredientResponse> PackageDetails { get; set; }
        }

        public OrderResponse()
        {
            OrderDetailRecipes = new HashSet<OrderDetailResponse>();
        }

        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShippedAddress { get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? ShippingCost { get; set; }
        public int? OrderStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public bool? IsCooked { get; set; }
        public Guid? VoucherId { get; set; }
        public int? PaymentMethod { get; set; }
        public string PaypalUrl { get; set; }

        public ICollection<OrderDetailResponse> OrderDetailRecipes { get; set; }
        public ICollection<IngredientResponse> OrderDetailIngredients { get; set; }
    }
}


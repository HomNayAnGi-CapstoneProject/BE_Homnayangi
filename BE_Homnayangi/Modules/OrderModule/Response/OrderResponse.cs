using System;
using System.Collections.Generic;
using Library.Models;

namespace BE_Homnayangi.Modules.OrderModule.Response
{
    public class OrderResponse
    {
        public class RecipeDetailResponse
        {
            public Guid OrderDetailId { get; set; }
            public Guid IngredientId { get; set; }
            public int? IngredientQuantity { get; set; }
            public decimal? Price { get; set; }

            public string IngredientImage { get; set; }
            public string IngredientName { get; set; }

        }
        public class OrderDetailResponse
        {
            public OrderDetailResponse()
            {
                RecipeDetails = new HashSet<RecipeDetailResponse>();
            }

            public Guid OrderId { get; set; }

            public Guid? RecipeId { get; set; }
            public string RecipeImage { get; set; }
            public string RecipeName { get; set; }
            public int? RecipeQuantity { get; set; }
            public decimal? PackagePrice { get; set; }
            public decimal? CookedPrice { get; set; }

            public ICollection<RecipeDetailResponse> RecipeDetails { get; set; }
        }

        public OrderResponse()
        {
            OrderDetails = new HashSet<OrderDetailResponse>();
        }

        public Guid OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShippedAddress { get; set; }
        public decimal? Discount { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? OrderStatus { get; set; }
        public Guid? CustomerId { get; set; }
        public bool? IsCooked { get; set; }
        public Guid? VoucherId { get; set; }
        public int? PaymentMethod { get; set; }

        public ICollection<OrderDetailResponse> OrderDetails { get; set; }
    }
}


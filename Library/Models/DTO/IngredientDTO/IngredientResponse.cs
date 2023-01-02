using System;
namespace Library.Models.DTO.IngredientDTO
{
	public class IngredientResponse
	{
        public Guid IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantitative { get; set; }
        public string Picture { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? Status { get; set; }
        public decimal? Price { get; set; }
        public Guid? TypeId { get; set; }

        public virtual Type Type { get; set; }
    }
}


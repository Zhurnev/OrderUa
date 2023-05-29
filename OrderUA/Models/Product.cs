using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderUA.Models
{
	public class Product
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string ShortDesc { get; set; }
		public string Description { get; set; }
		[Range(1, int.MaxValue)]
		public double Price { get; set; }
		public string Image { get; set; }
		[Display(Name = "Category Type")]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }

		[Display(Name = "Product Type")]
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public virtual ProductType ProductType { get; set; }
	}
}

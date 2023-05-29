using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderUA.Models
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

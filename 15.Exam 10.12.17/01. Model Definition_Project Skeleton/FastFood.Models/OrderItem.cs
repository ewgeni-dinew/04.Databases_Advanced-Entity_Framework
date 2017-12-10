using System.ComponentModel.DataAnnotations;

namespace FastFood.Models
{
    public class OrderItem
    {
        public Order Order { get; set; }
        public int OrderId { get; set; }
        
        public Item Item { get; set; }
        public int ItemId { get; set; }
        

        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
}
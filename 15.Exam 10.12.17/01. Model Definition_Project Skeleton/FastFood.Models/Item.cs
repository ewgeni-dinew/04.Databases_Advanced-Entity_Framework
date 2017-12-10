using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FastFood.Models
{
    public class Item
    {
        public Item()
        {
            this.OrderItems = new List<OrderItem>();
        }
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [Required]
        [Range(0.01,79228162514264337593543950335.00)]
        public decimal Price { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}

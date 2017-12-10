using System;
using FastFood.Data;
using System.Linq;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            var result = "";
            if (!context.Items.Any(i => i.Name == itemName))
            {
                result = $"Item {itemName} not found!";
            }
            else
            {
                var item = context.Items
                    .Where(i => i.Name == itemName)
                    .SingleOrDefault();

                var oldPrice = item.Price;

                item.Price = newPrice;
                context.SaveChanges();

                result = $"{itemName} Price updated from ${oldPrice:F2} to ${newPrice:F2}";
            }
            return result;
        }
    }
}

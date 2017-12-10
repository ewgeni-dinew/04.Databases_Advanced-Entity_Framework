using System;
using System.IO;
using FastFood.Data;
using System.Linq;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using FastFood.Models.Enums;
using Newtonsoft.Json;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
		public static string ExportOrdersByEmployee(FastFoodDbContext context,
            string employeeName, string orderType)
		{
            throw new NotImplementedException();

            //var orderParsed = Enum.Parse<OrderType>(orderType);
            
            //var orders = context.Orders
            //    .Where(o => o.Employee.Name == employeeName && o.Type == orderParsed)
            //    .Select(e => new
            //    {
            //        Name = e.Employee.Name,
            //        Orders = e.Employee.Orders
            //        .Select(eo => new
            //        {
            //            Customer = eo.Customer,
            //            Items = eo.OrderItems
            //            .Select(oi => new
            //            {
            //                Name = oi.Item.Name,
            //                Price = oi.Item.Price,
            //                Quantity = oi.Quantity
            //            })
            //            .ToList(),
            //            TotalPrice = eo.OrderItems.Select(oi=>oi.Item.Price*oi.Quantity),
            //        })
            //        .OrderByDescending(ob => ob.TotalPrice)
            //        .ThenByDescending(eo => eo.Items.Count)
            //        .ToList(),
            //        TotalMade = e.Employee.Orders.Sum(m => m.TotalPrice)
            //    })
            //    .ToList();

            //var jsonConv = JsonConvert.SerializeObject(orders);
            //return jsonConv;
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            throw new NotImplementedException();
        }
	}
}
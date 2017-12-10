using System;
using FastFood.Data;
using Newtonsoft.Json;
using FastFood.DataProcessor.Dto.Import;
using System.Text;
using System.Linq;
using FastFood.Models;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            //"Name": "Too Young",
            //"Age": 14,
            //"Position": "Invalid"

            var employees = JsonConvert.DeserializeObject<EmployeeImportDto[]>(jsonString);
            var sb = new StringBuilder();

            foreach (var employee in employees)
            {
                if (employee.Name.Length<3||employee.Name.Length>30||
                    employee.Age<15||employee.Age>80||
                    employee.Position.Length<3||employee.Position.Length>30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentEmp = new Employee()
                {
                    Name = employee.Name,
                    Age = employee.Age,
                };

                if (!context.Positions.Any(p=>p.Name==employee.Position))
                {
                    currentEmp.Position = new Position() { Name = employee.Position };
                }
                else
                {
                    currentEmp.Position = context.Positions
                        .Where(p => p.Name == employee.Position)
                        .FirstOrDefault();
                }

                context.Employees.Add(currentEmp);
                context.SaveChanges();
                sb.AppendLine($"Record {employee.Name} successfully imported.");
            }

            var result = sb.ToString().TrimEnd();
            return result;
        }

		public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            var items = JsonConvert.DeserializeObject<ItemImportDto[]>(jsonString);
            var sb = new StringBuilder();

            foreach (var item in items)
            {
                if (item.Name.Length<3||item.Name.Length>30||
                    item.Category.Length<3||item.Category.Length>30||
                    item.Price<=0||context.Items.Any(i=>i.Name==item.Name)||
                    item.Category.Length<3||item.Category.Length>30)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var currentItem = new Item()
                {
                    Name = item.Name,
                    Price = item.Price
                };

                if (!context.Categories.Any(c=>c.Name==item.Category))
                {
                    currentItem.Category = new Category() { Name = item.Category };
                }
                else
                {
                    var serachCat= context.Categories
                        .Where(c => c.Name == item.Category)
                        .FirstOrDefault();

                    currentItem.Category = serachCat;
                }

                context.Items.Add(currentItem);
                context.SaveChanges();
                sb.AppendLine($"Record {item.Name} successfully imported.");
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            var orders = XDocument.Parse(xmlString);
            var sb = new StringBuilder();
            var skip = false;

            foreach (var order in orders.Root.Elements())
            {
                var customer = order.Element("Customer")?.Value;
                var employee = order.Element("Employee")?.Value;
                var dateTime = order.Element("DateTime")?.Value;
                var type = order.Element("Type")?.Value;
                var items = order.Elements("Items");

                var dateParsed = DateTime.ParseExact(dateTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var orderItems = new List<OrderItem>();

                if (customer == null || employee == null || dateTime == null ||
                    items == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (!context.Employees.Any(e => e.Name == employee))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }


                foreach (var item in items.Elements("Item"))
                {
                    var itemName = item.Element("Name")?.Value;
                    var itemQuantity = item.Element("Quantity")?.Value;
                    if (itemName == null || itemQuantity == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    if (itemName.Length < 3 || itemName.Length > 30 ||
                        int.Parse(itemQuantity) < 1)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    if (!context.Items.Any(i => i.Name == itemName))
                    {
                        sb.AppendLine(FailureMessage);
                        skip = true;
                        break;
                    }

                    var searchItem = context.Items
                        .Where(i => i.Name == itemName)
                        .FirstOrDefault();

                    var currentOrderItems = new OrderItem()
                    {
                        Item = searchItem,
                        Quantity = int.Parse(itemQuantity)
                    };
                    orderItems.Add(currentOrderItems);
                }

                if (skip == true)
                {
                    continue;
                }
                var searchEmp = context.Employees
                    .Where(e => e.Name == employee)
                    .FirstOrDefault();

                var currentOrder = new Order()
                {
                    Customer = customer,
                    DateTime = dateParsed,
                    Employee = searchEmp,
                    OrderItems = orderItems,
                };
                switch (type)
                {
                    case "ForHere": currentOrder.Type = Models.Enums.OrderType.ForHere; break;
                    case "ToGo": currentOrder.Type = Models.Enums.OrderType.ToGo; break;
                    default:
                        currentOrder.Type = Models.Enums.OrderType.ForHere;
                        break;
                }
                context.Orders.Add(currentOrder);
                context.SaveChanges();
                sb.AppendLine($"Order for {customer} on {dateTime} added");
                skip = false;
            }
            var result = sb.ToString().TrimEnd();
            return result;
        }
	}
}
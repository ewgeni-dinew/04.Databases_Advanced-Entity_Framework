using ProductShop.Data;
using System;
using Newtonsoft.Json;
using System.IO;
using ProductShop.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ProductShop.App
{
    class StartUp
    {
        static void Main()
        {
            //ImportCategoriesFromJson();
            //ImportUsersFromJson();
            //ImportProductsFromJson();
            //SetCategories();

            //GetProductsInRange();
            //GetSoldProducts();
            //GetCategories();
            //GetUsersAndProducts();

            //GetProductsInRangeXml();
            //GetSoldProductsXml();
            //GetCategoriesByProdCountXml();
            //GetUsersAndProductsXml();
        }

        static T[] ImportJson<T>(string path)
        {
            var jsonString = File.ReadAllText(path);
            var objects = JsonConvert.DeserializeObject<T[]>(jsonString);
            return objects;
        }

        //01.Import Data From .json-->
        static string ImportCategoriesFromJson()
        {
            var categories = ImportJson<Category>("Files/categories.json");
            using (var db=new ProductShopContext())
            {
                db.Categories.AddRange(categories);
                db.SaveChanges();
            }
            return $"{categories.Length} categories were added to the database.";
        }
        static string ImportUsersFromJson()
        {
            var users = ImportJson<User>("Files/users.json");

            using (var db=new ProductShopContext())
            {
                db.Users.AddRange(users);
                db.SaveChanges();
            }

            return $"{users.Length} users were added to tha database.";
        }
        static string ImportProductsFromJson()
        {
            var random = new Random();

            var products = ImportJson<Product>("Files/products.json");
             
            using (var context=new ProductShopContext())
            {
                var userIds = context.Users.Select(u => u.Id).ToList();

                foreach (var p in products)
                {
                    var index = random.Next(0, userIds.Count);
                    var sellerId = userIds[index];

                    int? buyerId = sellerId;
                    while (buyerId == sellerId)
                    {
                        var buyerIndex = random.Next(0, userIds.Count);
                        buyerId = userIds[buyerIndex];
                    }

                    if (buyerId-sellerId<5 &&buyerId-sellerId>0)
                    {
                        buyerId = null;
                    }

                    p.SellerId = sellerId;
                    p.BuyerId = buyerId;
                }

                context.AddRange(products);
                context.SaveChanges();
            }
            return $"{products.Length} products were added successfully.";
        }
        static string SetCategories()
        {
            var random = new Random();

            using (var context=new ProductShopContext())
            {
                var categoryIds = context.Categories.Select(c => c.Id).ToList();
                var productIds = context.Products.Select(p=>p.Id).ToList();

                var categoryProducts = new List<CategoryProduct>();

                foreach (var p in productIds)
                {
                    var indexes = new List<int>();

                    for (int i = 0; i < 3; i++)
                    {
                        int index;

                        do
                        {   
                            index = random.Next(0, categoryIds.Count);
                        } while (indexes.Contains(index));

                        indexes.Add(index);

                        var catPr = new CategoryProduct()
                        {
                            ProductId = p,
                            CategoryId = categoryIds[index]
                        };
                        categoryProducts.Add(catPr);
                    }
                }
                context.CategoryProducts.AddRange(categoryProducts);
                context.SaveChanges();
            }

            return $"";
        }
        //<--

        //02.Query and Export Data To .json-->
        static void GetProductsInRange()
        {
            using (var context=new ProductShopContext())
            {
                var products = context
                    .Products
                    .Where(p => p.Price >= 500 && p.Price <= 1000)
                    .OrderBy(p => p.Price)
                    .Select(p => new
                    {
                        name=p.Name,
                        price=p.Price,
                        seller = p.Seller.FirstName + ' ' + p.Seller.LastName,
                    })
                    .ToList();

                var jsonString = JsonConvert.SerializeObject(products,Formatting.Indented);

                File.WriteAllText("PricesInRange.json", jsonString);
            }
        }
        static void GetSoldProducts()
        {
            using (var context=new ProductShopContext())
            {
                var users = context
                    .Users
                    .Where(u => u.ProductsSold.Any(ps => ps.BuyerId != null))
                    .OrderBy(u => u.LastName)
                    .ThenBy(u => u.FirstName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        SoldProducts=u.ProductsSold
                            .Select(p=>new
                            {
                                p.Name,
                                p.Price,
                                p.Buyer.FirstName,
                                p.Buyer.LastName
                            })
                    })
                    .ToList();

                var jsonString = JsonConvert.SerializeObject(users, Formatting.Indented,
                    new JsonSerializerSettings {DefaultValueHandling=DefaultValueHandling.Ignore });
                File.WriteAllText("SoldProducts.json", jsonString);
            }
        }
        static void GetCategories()
        {
            using (var context=new ProductShopContext())
            {
                var categories = context
                    .Categories
                    .OrderBy(c => c.Name)
                    .Select(c => new
                    {
                        c.Name,
                        productCount = c.CategoryProducts.Count,
                        averagePrice = c.CategoryProducts.Average(p => p.Product.Price),
                        totalRevenue = c.CategoryProducts.Sum(p => p.Product.Price)
                    })
                    .ToList();

                var jsonString = JsonConvert.SerializeObject(categories, Formatting.Indented);
                File.WriteAllText("Categories.json", jsonString);
            }
        }
        static void GetUsersAndProducts()
        {
            using (var context = new ProductShopContext())
            {
                var sellers = context
                     .Products
                     .Where(p => p.BuyerId != null)
                     .ToList();

                var sellerCount = sellers
                    .Select(s => s.Seller)
                    .Count();

                var result = new
                {
                    usersCount = sellerCount,
                    users = sellers
                    .Select(u => new
                    {
                        u.Seller.FirstName,
                        u.Seller.LastName,
                        u.Seller.Age,
                        soldProducts = new
                        {
                            productCount = u.Seller.ProductsSold.Count(),
                            products = new
                            {
                                product = u.Seller
                                .ProductsSold
                                .Select(ps => new
                                {
                                    ps.Name,
                                    ps.Price
                                })
                                .ToList()
                            }
                        }
                    })
                    .ToList()
                };

                var jsonString = JsonConvert.SerializeObject(result, Formatting.Indented);
                File.WriteAllText("UsersAndProducts.json", jsonString);
            }
        }
        //<--

        //03.Import Data From .xml-->
        static string ImportUsersFromXml()
        {
            var xmlString = File.ReadAllText("Files/users.xml");

            var xmlDoc = XDocument.Parse(xmlString);

            var elements = xmlDoc.Root.Elements();

            var users = new List<User>();

            foreach (var e in elements)
            {
                var firstName = e.Attribute("firstName")?.Value;
                var lastName = e.Attribute("lastName").Value;
                int? age = null;

                if (e.Attribute("age")!=null)
                {
                    age = int.Parse(e.Attribute("age").Value);
                }

                var currentUser = new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };

                users.Add(currentUser);
            }

            using (var context = new ProductShopContext())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            return $"{users.Count} users were imported from .xml!";
        }
        static string ImportCategoriesFromXml()
        {
            var xmlString = File.ReadAllText("categories.xml");
            var xmlDoc = XDocument.Parse(xmlString);

            var categories = xmlDoc.Root.Elements();
            var categoryList = new List<Category>();

            foreach (var c in categories)
            {
                var catName = c.Attribute("name").Value;

                var currentCat = new Category()
                {
                    Name = catName,
                };
                categoryList.Add(currentCat);
            }

            using (var context= new ProductShopContext())
            {
                context.Categories.AddRange(categoryList);
                context.SaveChanges();
            }

            return $"{categoryList.Count} categories imported from .xml!";
        }
        static string ImportProductsFromXml()
        {
            var xmlString = File.ReadAllText("products.xml");
            var xmlDoc = XDocument.Parse(xmlString);

            var catProducts = new List<CategoryProduct>();

            var elements = xmlDoc.Root.Elements();

            using (var context=new ProductShopContext())
            {
                var random = new Random();

                var userIds = context.Users
                    .Select(u => u.Id)
                    .ToList();

                var categoryIds = context.Categories
                    .Select(c => c.Id)
                    .ToList();

                foreach (var e in elements)
                {
                    var indexS = random.Next(0, userIds.Count);
                    var indexC = random.Next(0, categoryIds.Count);

                    var name = e.Attribute("name").Value;
                    var price = decimal.Parse(e.Attribute("price").Value);
                    var sellerId = userIds[indexS];
                    var categoryId = categoryIds[indexC];

                    var currentProduct = new Product()
                    {
                        Name = name,
                        Price = price,
                        SellerId = sellerId,
                    };

                    var currentCatProd = new CategoryProduct()
                    {
                        Product = currentProduct,
                        CategoryId = categoryId,
                    };

                    catProducts.Add(currentCatProd);
                }
                context.CategoryProducts.AddRange(catProducts);
                context.SaveChanges();
            }
            return $"{catProducts.Count} products were imported from .xml!";
        }
        //<--

        //04.Query and Export Data to .xml-->
        static void GetProductsInRangeXml()
        {
            using (var context=new ProductShopContext())
            {
                var products = context.Products
                    .Where(p => p.Price >= 1000 && p.Price <= 2000 && p.BuyerId != null)
                    .OrderBy(p=>p.Price)
                    .Select(p => new
                    {
                        name = p.Name,
                        price = p.Price,
                        buyer = p.Buyer.FirstName + ' ' + p.Buyer.LastName
                    })
                    .ToList();

                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("products"));

                foreach (var p in products)
                {
                    xmlDoc.Root.Add(new XElement("product",
                                    new XAttribute("name", p.name),
                                    new XAttribute("price", p.price),
                                    new XAttribute("buyer", p.buyer)));
                }

                var xmlString = xmlDoc.ToString();
                File.WriteAllText("products.xml",xmlString);
            }
        }
        static void GetSoldProductsXml()
        {
            using (var context=new ProductShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count >= 1)
                    .OrderBy(u => u.FirstName)
                    .ThenBy(u => u.LastName)
                    .Select(u=>new
                    {
                        u.FirstName,
                        u.LastName,
                        productsSold = u.ProductsSold.Select(ps => new
                        {
                            ps.Name,
                            ps.Price
                        })
                        .ToList()
                    })
                    .ToList();

                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("users"));

                foreach (var u in users)
                {
                    var user = new XElement("user");

                    if (u.FirstName!=null)
                    {
                        user.Add(new XAttribute("first-name", u.FirstName));
                    }
                    user.Add(new XAttribute("last-name", u.LastName));

                    var soldProducts = new List<XElement>();
                    
                    foreach (var sp in u.productsSold)
                    {
                        var childOfSoldProducts = new XElement("product",
                               new XElement("name", sp.Name),
                               new XElement("price", sp.Price));

                        soldProducts.Add(childOfSoldProducts);
                    }

                    user.Add(new XElement("sold-products", soldProducts));
                    xmlDoc.Root.Add(user);
                }
                var xmlString = xmlDoc.ToString();
                File.WriteAllText("users.xml", xmlString);
            }
        }
        static void GetCategoriesByProdCountXml()
        {
            using (var context=new ProductShopContext())
            {
                var categories = context.Categories.
                    Include(c => c.CategoryProducts)
                    .Select(c => new
                    {
                        c.Name,
                        catProductCount = c.CategoryProducts.Count(),
                        catProductAvgPrice = c.CategoryProducts.Average(p => p.Product.Price),
                        catProductTotalRev = c.CategoryProducts.Sum(p => p.Product.Price)
                    })
                    .OrderByDescending(c => c.catProductCount)
                    .ToList();

                var xDoc = new XDocument();
                xDoc.Add(new XElement("categories"));

                foreach (var c in categories)
                {
                    xDoc.Root.Add(new XElement("category",
                        new XAttribute("name", c.Name),
                        new XElement("product-count", c.catProductCount),
                        new XElement("average-price", c.catProductAvgPrice),
                        new XElement("total-revenue", c.catProductTotalRev)));
                }
                File.WriteAllText("catProp", xDoc.ToString());
            }

        }
        static void GetUsersAndProductsXml()
        {
            using (var context=new ProductShopContext())
            {
                var users = context.Users
                    .Where(u => u.ProductsSold.Count >= 1)
                    .OrderByDescending(u => u.ProductsSold.Count)
                    .ThenBy(u => u.LastName)
                    .Select(u => new
                    {
                        u.FirstName,
                        u.LastName,
                        u.Age,
                        productsSold = u.ProductsSold
                            .Select(ps => new
                            {
                                ps.Name,
                                ps.Price
                            }).ToList(),
                    })
                    .ToList();

                var xmlDoc = new XDocument();
                xmlDoc.Add(new XElement("users",new XAttribute("count",users.Count)));

                foreach (var u in users)
                {
                    var userChild = new XElement("user");

                    if (u.FirstName != null)
                    {
                        userChild.Add(new XAttribute("first-name", u.FirstName));
                    }
                    userChild.Add(new XAttribute("last-name", u.LastName));

                    if (u.Age != null)
                    {
                        userChild.Add(new XAttribute("age", u.Age));
                    }

                    var soldProducts = new XElement("sold-products",
                        new XAttribute("count",u.productsSold.Count));

                    foreach (var p in u.productsSold)
                    {
                        soldProducts.Add(new XElement("product",
                            new XAttribute("name",p.Name),
                            new XAttribute("price",p.Price)));
                    }

                    userChild.Add(soldProducts);
                    xmlDoc.Root.Add(userChild);
                }

                File.WriteAllText("userProd.xml", xmlDoc.ToString());
            }
        }
        //<--
    }
}

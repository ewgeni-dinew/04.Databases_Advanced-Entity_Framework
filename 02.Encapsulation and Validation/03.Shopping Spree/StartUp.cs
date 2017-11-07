using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class StartUp
{
    static void Main()
    {
        var listOfPersons = new List<Person>();
        var listOfProducts = new List<Product>();

        var filterPersons = Console.ReadLine()
            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < filterPersons.Length; i++)
        {
            var inputPerson = filterPersons[i]
                .Split('=');

            var currentBagOfProducts = new List<string>();

            var personName = inputPerson[0];
            var personMoney = double.Parse(inputPerson[1]);

            try
            {
                var currentPerson = new Person(personName, personMoney, currentBagOfProducts);
                listOfPersons.Add(currentPerson);
            }
            catch (Exception e )
            {

                Console.WriteLine(e.Message);
                return;
            } 
        }

        var filterProducts = Console.ReadLine()
            .Split(new[] { ';' },StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < filterProducts.Length; i++)
        {
            var inputProduct = filterProducts[i]
                .Split('=');

            var productName = inputProduct[0];
            var productPrice = double.Parse(inputProduct[1]);

            try
            {
                var currentProduct = new Product(productName, productPrice);
                listOfProducts.Add(currentProduct);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        var line = Console.ReadLine();
        while (line!="END")
        {
            var currentLine = line
                .Split(' ');

            var person = currentLine[0];
            var product = currentLine[1];

            var specificPerson = listOfPersons.Find(p => p.Name == person);
            var specificProduct = listOfProducts.Find(pr => pr.Name == product);

            if (specificPerson.Money>=specificProduct.Price)
            {
                Console.WriteLine($"{person} bought {product}");

                specificPerson.Money -= specificProduct.Price;

                specificPerson.BagOfProducts.Add(product);
            }
            else
            {
                Console.WriteLine($"{person} can't afford {product}");
            }

            line = Console.ReadLine();
        }

        foreach (var prs in listOfPersons)
        {
            if (prs.BagOfProducts.Count==0)
            {
                Console.WriteLine($"{prs.Name} - Nothing bought");
            }
            else
            {
                Console.WriteLine($"{prs.Name} - {string.Join(", ",prs.BagOfProducts)}");
            }
        }
    }
}
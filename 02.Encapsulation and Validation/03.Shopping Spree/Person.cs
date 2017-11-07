using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Person
{
    private string name;
    private double money;
    private List<string> bagOfProducts;

    public string Name
    {
        get { return this.name; }
        set
        {
            if (value == "")
            {
                throw new Exception("Name cannot be empty");
                
            }
            else { this.name = value; }
        }
    }

    public double Money
    {
        get { return this.money; }
        set
        {
            if (value < 0)
            {
                throw new Exception("Money cannot be negative");
            }
            else { this.money = value; }
        }
    }

    public List<string> BagOfProducts
    {
        get { return this.bagOfProducts; }
        set { this.bagOfProducts = value; }
    }

    public Person(string name, double money, List<string> bagOfProducts)
    {
        this.Name = name;
        this.Money = money;
        this.BagOfProducts = bagOfProducts;
    }

   
}

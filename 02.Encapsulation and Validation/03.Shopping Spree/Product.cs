using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Product
{
    private string name;
    private double price;

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

    public double Price
    {
        get { return this.price; }
        set
        {
            if (value<=0)
            {
                throw new Exception("Price cannot be zero or negative");
            }
            else { this.price = value; }
        }
    }

    public Product(string name,double price)
    {
        this.Name = name;
        this.Price = price;
    }
}


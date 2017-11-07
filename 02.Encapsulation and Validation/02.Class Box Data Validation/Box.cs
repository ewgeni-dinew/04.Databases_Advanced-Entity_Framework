using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Box
{
    private double length;

    private double width;

    private double height;


    public double Length
    {
        get { return this.length; }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Length cannot be zero or negative.");
            }
            else this.length = value;
        }
    }

    public double Width
    {
        get { return this.width; }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Width cannot be zero or negative.");
            }
            else this.width = value;
        }
    }

    public double Height
    {
        get { return this.height; }
        set
        {
            if (value <= 0)
            {
                throw new Exception("Height cannot be zero or negative.");
            }
            else this.height = value;
        }
    }

    public Box(double len, double wdt, double hgh)
    {
        this.Length = len;
        this.Width = wdt;
        this.Height = hgh;
    }

    public double Volume()
    {
        return this.length * this.width * this.height;
    }

    public double SurfaceArea()
    {
        return 2 * this.length * this.width
             + 2 * this.length * this.height
             + 2 * this.width * this.height;
    }

    public double LateralSurfaceArea()
    {
        return 2 * this.length * this.height + 2 * this.width * this.height;
    }
}

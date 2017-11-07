using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class StartUp
{
    static void Main()
    {
        Type boxType = typeof(Box);
        FieldInfo[] fields = boxType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine(fields.Count());

        var inputLength = double.Parse(Console.ReadLine());
        var inputWidth = double.Parse(Console.ReadLine());
        var inputHeight = double.Parse(Console.ReadLine());

        var newBox = new Box(inputLength, inputWidth, inputHeight);

        var surfaceArea = newBox.SurfaceArea();
        var volume = newBox.Volume();
        var lateralSurfaceArea = newBox.LateralSurfaceArea();

        Console.WriteLine($"Surface Area - {surfaceArea:f2}");
        Console.WriteLine($"Lateral Surface Area - {lateralSurfaceArea:f2}");
        Console.WriteLine($"Volume - {volume:f2}");
    }
}


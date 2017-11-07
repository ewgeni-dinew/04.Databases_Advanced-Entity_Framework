using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main()
    {
        var studentInfo = Console.ReadLine().Split();
        var studentFirstName = studentInfo[0];
        var studentLastName = studentInfo[1];
        var facNumber = studentInfo[2];

        var workerInfo = Console.ReadLine().Split();
        var workerFirstName = workerInfo[0];
        var workerLastName = workerInfo[1];
        var weeklySalary = decimal.Parse(workerInfo[2]);
        var weeklyWorkHours = double.Parse(workerInfo[3]);

        try
        {
            var currentStudent = new Student(studentFirstName, studentLastName, facNumber);
            var currentWorker = new Worker(workerFirstName, workerLastName, weeklySalary, weeklyWorkHours);

            Console.WriteLine(currentStudent);
            Console.WriteLine();
            Console.WriteLine(currentWorker);
        }
        catch (ArgumentException ae)
        {
            Console.WriteLine(ae.Message);
        }
    }
}


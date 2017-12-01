using System;
using System.Collections.Generic;
using System.Text;

namespace Mapper.App.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Goodbye!");
            Environment.Exit(0);
            return null;
        }
    }
}

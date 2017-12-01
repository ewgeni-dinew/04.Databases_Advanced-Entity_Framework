using System;
using System.Linq;

namespace Mapper.App
{
    internal class Engine
    {
        private readonly IServiceProvider serviceProvider;

        public Engine(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        internal void Run()
        {
            while (true)
            {
                var input = Console.ReadLine();

                var commandTokens = input.Split(' ');

                var commandName = commandTokens[0];

                string[] commandArgs = commandTokens.Skip(1).ToArray();

                var command = CommandParser.Parse(serviceProvider, commandName);

                var result = command.Execute(commandArgs);
                Console.WriteLine(result);
            }
        }
    }
}
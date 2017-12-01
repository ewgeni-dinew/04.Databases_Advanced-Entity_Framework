namespace Mapper.App
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Mapper.App.Commands;

    internal class CommandParser
    {
        public static ICommand Parse (IServiceProvider serviceProvider,string commandName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces()
                            .Contains(typeof(ICommand)));

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name.ToLower() ==$"{commandName.ToLower()}command");

            if (commandType==null)
            {
                throw new InvalidOperationException("Invalid command!");
            }

            var constructor = commandType
                .GetConstructors()
                .FirstOrDefault();

            var constructorParams = constructor
                .GetParameters()
                .Select(p=>p.ParameterType)
                .ToArray();

            var constructorArgs = constructorParams
                .Select(p => serviceProvider.GetService(p))
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorArgs);

            return command;
        }
    }
}

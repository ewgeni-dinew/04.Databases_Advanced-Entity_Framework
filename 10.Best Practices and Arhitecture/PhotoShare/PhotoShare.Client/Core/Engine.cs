namespace PhotoShare.Client.Core
{
    using PhotoShare.Models;
    using System;

    public class Engine
    {
        private readonly CommandDispatcher commandDispatcher;
        internal Session session;

        public Engine(CommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            session = new Session();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine().Trim();
                    string[] data = input.Split(' ');
                    string result = this.commandDispatcher.DispatchCommand(data, session);
                    Console.WriteLine(result);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}

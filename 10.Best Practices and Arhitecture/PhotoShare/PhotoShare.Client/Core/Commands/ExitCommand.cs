namespace PhotoShare.Client.Core.Commands
{
    using System;

    public class ExitCommand
    {
        public static string Execute()
        {
            Console.WriteLine("Good Bye!");
            Environment.Exit(0);
            return "";
        }
    }
}

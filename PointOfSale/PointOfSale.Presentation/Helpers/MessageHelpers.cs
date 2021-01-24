using System;
using System.Threading;

namespace PointOfSale.Presentation.Helpers
{
    public static class MessageHelpers
    {
        public static void ColorText(string message, ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
        {
            Console.BackgroundColor = background;
            Console.ForegroundColor = foreground;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void Error(string message)
        {
            SoundHelper.Play(Sounds.Error);
            ColorText(message, ConsoleColor.Yellow);
        }

        public static void Confirm(string message)
        {
            SoundHelper.Play(Sounds.Confirm);
            ColorText(message, ConsoleColor.Red);
        }

        public static void Success(string message)
        {
            ColorText(message, ConsoleColor.Green);
        }

        public static void NotAvailable(string message)
        {
            Error(message);
            Thread.Sleep(1500);
        }

    }
}

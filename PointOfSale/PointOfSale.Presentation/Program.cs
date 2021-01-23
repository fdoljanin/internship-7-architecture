using System;
using PointOfSale.Presentation.Extensions;
using PointOfSale.Presentation.Factories;
using System.Threading;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            SoundHelper.Play(Sounds.Startup);

            var mainMenuActions = MainMenuFactory.GetMainMenuActions();
            mainMenuActions.PrintActionsAndCall();

            Console.Clear();
            Console.WriteLine("Logging off...");
            SoundHelper.Play(Sounds.Shutdown);
            Thread.Sleep(1500);
        }
    }
}

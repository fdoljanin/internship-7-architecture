using PointOfSale.Presentation.Extensions;
using PointOfSale.Presentation.Factories;

namespace PointOfSale.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainMenuActions = MainMenuFactory.GetMainMenuActions();
            mainMenuActions.PrintActionsAndCall();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Extensions
{
    public static class ActionExtensions
    {
        public static void PrintActionsAndCall(this IList<IAction> actions)
        {
            var doesContinue = true;
            while (true)
            {
                Console.WriteLine("Press enter in any point in program to return. \n");

                foreach (var action in actions)
                {
                    Console.WriteLine($"{action.MenuIndex}. {action.Label}");
                }

                var actionCalled = ReadHelpers.TryGetListMember(actions, ref doesContinue);
                if (!doesContinue) return;

                Console.Clear();
                actionCalled.Call();
                Console.Clear();
                if (actionCalled is ExitMenuAction) return;
            }
        }

        public static void SetActionIndexes(this IList<IAction> actions)
        {
            for (var index = 0; index < actions.Count; index++)
            {
                actions[index].MenuIndex = index + 1;
            }
        }
    }
}

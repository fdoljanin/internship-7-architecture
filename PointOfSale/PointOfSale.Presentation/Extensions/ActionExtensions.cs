﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Actions;

namespace PointOfSale.Presentation.Extensions
{
    public static class ActionExtensions
    {
        public static void PrintActionsAndCall(this IList<IAction> actions)
        {
            var exitActionSelected = false;
            do
            {
                foreach (var action in actions)
                {
                    Console.WriteLine($"{action.MenuIndex}. {action.Label}");
                }

                Console.WriteLine("Press enter in any point in program to return.");

                var input = Console.ReadLine().Trim();
                if (input == "")
                {
                    exitActionSelected = true;
                    continue;
                }

                var isInputInteger = int.TryParse(input, out var actionIndex);
                if (isInputInteger)
                {
                    var action = actions.FirstOrDefault(a => a.MenuIndex == actionIndex);
                    if (action == null)
                    {
                        Console.WriteLine("Please select available action");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                    else
                    {
                        Console.Clear();
                        exitActionSelected = action is ExitMenuAction;
                        action.Call();
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Please type in number.");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            } while (!exitActionSelected);
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

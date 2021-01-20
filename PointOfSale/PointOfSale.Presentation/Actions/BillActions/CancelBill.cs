using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class CancelBill:IAction
    {
        private readonly BillRepository _billRepository;
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Cancel bill";

        public CancelBill(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public void Call()
        {
            var doesContinue = true;
            var bills = _billRepository.GetBills();

            Console.WriteLine("Choose bill index to delete");
            PrintHelpers.PrintBills(bills);

            var chosenIndex = ReadHelpers.TryIntParse(ref doesContinue, 1, bills.Count);
            if (!doesContinue) return;

            if (!ReadHelpers.Confirm($"Are you sure you want to delete bill number {chosenIndex}")) return;

            _billRepository.CancelBill(bills.ElementAt(chosenIndex-1).Id);
            Console.WriteLine("Cancelled!");
        }
    }
}

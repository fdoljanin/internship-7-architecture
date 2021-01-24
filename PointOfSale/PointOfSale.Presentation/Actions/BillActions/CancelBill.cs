using System;
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
            var billList = _billRepository.GetBills();
            Console.WriteLine("Choose bill index to delete");
            PrintHelpers.PrintBills(billList);
            if (billList.Count == 0) return;


            var chosenBill = ReadHelpers.TryGetListMember(billList, ref doesContinue);
            if (!doesContinue) return;

            if (!ReadHelpers.Confirm("Are you sure you want to delete bill? (yes/no)"))
            {
                MessageHelpers.Success("Action stopped.");
                Console.ReadLine();
                return;
            }

            _billRepository.CancelBill(chosenBill.Id);

            MessageHelpers.Success("Cancelled!");
            Console.ReadLine();
        }
    }
}

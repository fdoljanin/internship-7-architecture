using System;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportYearProfit:IAction
    {
        private readonly BillRepository _billRepository;

        public ReportYearProfit(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get profit by year";

        public void Call()
        {
            var doesContinue = true;

            Console.WriteLine("Enter year (yyyy) to see profit:");
            var year = ReadHelpers.TryIntParse(ref doesContinue);
            if (!doesContinue) return;

            var profit = _billRepository.GetYearProfit(year);
            Console.WriteLine($"Profit of year {year}: {profit}");

            Console.ReadLine();
        }
    }
}

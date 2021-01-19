using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportByOfferTypeAction:IAction
    {
        private readonly BillRepository _billRepository;
        public ReportByOfferTypeAction(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public string Label { get; set; } = "Get bills by offer type";
        public void Call()
        {
            var doesContinue = true;

            Console.WriteLine("Enter bill type (Traditional, Subscription, Service):");
            var offerType = ReadHelpers.TryEnumParse<OfferType>(ref doesContinue);
            if (!doesContinue) return;

            var dateRange = ReadHelpers.GetDateRange(ref doesContinue);
            if (!doesContinue) return;

            var report = _billRepository.GetOfferTypeReport(offerType, dateRange);
            PrintHelpers.PrintBills(report);
        }
    }
}

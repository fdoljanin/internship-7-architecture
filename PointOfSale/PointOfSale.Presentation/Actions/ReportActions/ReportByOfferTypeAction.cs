﻿using System;
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
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get bills by offer type";
        public void Call()
        {
            var doesContinue = true;

            Console.WriteLine("Enter bill type (Item, Service, Rent):");
            var offerType = ReadHelpers.TryEnumParse<OfferType>(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter start (and end, sep by space) date:");
            var dateRange = ReadHelpers.GetDateRange(ref doesContinue);
            if (!doesContinue) return;

            var report = _billRepository.GetOfferTypeReport(offerType, dateRange);
            PrintHelpers.PrintBills(report);

            Console.ReadLine();
        }
    }
}

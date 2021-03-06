﻿using System;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.ReportActions
{
    public class ReportByCategoryAction:IAction
    {
        private readonly BillRepository _billRepository;

        public ReportByCategoryAction(BillRepository billRepository)
        {
            _billRepository = billRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Get bills by category";

        public void Call()
        {
            var doesContinue = true;

            Console.WriteLine("Enter bill type (Traditional, Subscription, Service):");
            var billType = ReadHelpers.TryEnumParse<BillType>(ref doesContinue);
            if (!doesContinue) return;

            Console.WriteLine("Enter start (and end, sep by space) date:");
            var dateRange = ReadHelpers.GetDateRange(ref doesContinue);
            if (!doesContinue) return;

            var report = _billRepository.GetCategoryReport(billType, dateRange);
            PrintHelpers.PrintBills(report);

            Console.ReadLine();
        }
    }
}

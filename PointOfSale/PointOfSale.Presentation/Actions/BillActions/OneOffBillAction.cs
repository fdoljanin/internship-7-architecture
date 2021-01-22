using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class OneOffBillAction:IAction
    {
        public string Label { get; set; } = "One-off bill";
        public int MenuIndex { get; set; }

        private readonly BillRepository _billRepository;
        private readonly ArticleBillRepository _articleBillRepository;
        private readonly ServiceBillRepository _serviceBillRepository;

        private readonly ArticleBillHelpers _articleBillHelper;
        private readonly ServiceBillHelpers _serviceBillHelper;

        public OneOffBillAction(BillRepository billRepository, ArticleBillRepository articleBillRepository,
            ServiceBillRepository serviceBillRepository, EmployeeRepository employeeRepository)
        {
            _billRepository = billRepository;
            _articleBillRepository = articleBillRepository;
            _serviceBillRepository = serviceBillRepository;

            _articleBillHelper = new ArticleBillHelpers();
            _serviceBillHelper = new ServiceBillHelpers(employeeRepository);
        }

        public void Call()
        {
            var doesContinue = true;
            var newBill = _billRepository.GetNewBill(BillType.Traditional);

            var articleList = _articleBillRepository.GetAllAvailable();
            Console.WriteLine("Add articles:");
            PrintHelpers.PrintOfferList(articleList);

            while (true)
            {
                var articleBill = _articleBillHelper.TryGetArticleBill(ref doesContinue, articleList);
                if (!doesContinue) break;

                if (_articleBillRepository.CheckIsArticleThere(newBill.Id, articleBill))
                {
                    Console.WriteLine("Article already in! Quantity added...");
                    continue;
                }

                articleBill.BillId = newBill.Id;
                _articleBillRepository.Add(articleBill);

                Console.WriteLine("Article added!");
            }

            var serviceList = _serviceBillRepository.GetAll();
            Console.WriteLine("Add services:");

            while (true)
            {
                var serviceBill = new ServiceBill();
                PrintHelpers.PrintOfferList(serviceList);

                Console.WriteLine("Enter service index:");

                serviceBill.OfferId = ReadHelpers.TryGetListMember(serviceList, ref doesContinue).Id;

                var serviceBillInfo = _serviceBillHelper.TryGetServiceInfo(ref doesContinue);
                if (!doesContinue) break;

                serviceBill.StartTime = serviceBillInfo.StartTime;
                serviceBill.Duration = serviceBillInfo.Duration;
                serviceBill.EmployeeId = serviceBillInfo.EmployeeId;
                serviceBill.BillId = newBill.Id;
                _serviceBillRepository.Add(serviceBill);

                Console.WriteLine("Service added!");
            }

            var billCost = _billRepository.FinishBillAndGetCost(newBill.Id);

            Console.WriteLine($"Cost: {billCost}");
            Console.ReadLine();
        }
    }
}

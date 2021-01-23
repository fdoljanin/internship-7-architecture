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
        private readonly EmployeeRepository _employeeRepository;

        public OneOffBillAction(BillRepository billRepository, ArticleBillRepository articleBillRepository,
            ServiceBillRepository serviceBillRepository, EmployeeRepository employeeRepository)
        {
            _billRepository = billRepository;
            _articleBillRepository = articleBillRepository;
            _serviceBillRepository = serviceBillRepository;
            _employeeRepository = employeeRepository;
        }

        public void Call()
        {
            var doesContinue = true;
            var newBill = _billRepository.GetNewBill(BillType.Traditional);

            var articleList = _articleBillRepository.GetAllAvailable();
            Console.WriteLine("Add articles:");
            PrintHelpers.PrintOfferList(articleList);
            if (articleList.Count == 0) goto Service;

            while (true)
            {
                var articleBill = ArticleBillHelpers.TryGetArticleBill(articleList, ref doesContinue);
                if (!doesContinue) break;

                if (_articleBillRepository.CheckIsArticleThere(newBill.Id, articleBill))
                {
                    MessageHelpers.ColorText("Article already in! Quantity added...", ConsoleColor.Yellow);
                    continue;
                }

                articleBill.BillId = newBill.Id;
                _articleBillRepository.Add(articleBill);

                Console.WriteLine("Article added!\n");
            }

            Service:
            var serviceList = _serviceBillRepository.GetAll();
            Console.WriteLine("Add services:");

            while (true)
            {
                var serviceBill = new ServiceBill();
                PrintHelpers.PrintOfferList(serviceList);
                if (serviceList.Count == 0) goto Bill;
                if (serviceList.Count == 0)
                {
                    MessageHelpers.NotAvailable("No customers found!");
                    return;
                }

                Console.WriteLine("Enter service index:");

                var chosenService = ReadHelpers.TryGetListMember(serviceList, ref doesContinue);
                if (!doesContinue) break;

                var serviceBillInfo = ServiceBillHelpers.TryGetServiceInfo(_employeeRepository, ref doesContinue);
                if (!doesContinue) break;

                serviceBill.OfferId = chosenService.Id;
                serviceBill.StartTime = serviceBillInfo.StartTime;
                serviceBill.Duration = serviceBillInfo.Duration;
                serviceBill.EmployeeId = serviceBillInfo.EmployeeId;
                serviceBill.BillId = newBill.Id;
                _serviceBillRepository.Add(serviceBill);

                Console.WriteLine("Service added!\n");
            }
            
            Bill:
            var billCost = _billRepository.FinishBillAndGetCost(newBill.Id);

            if (billCost == 0)
            {
                MessageHelpers.Error("Bill is empty and won't be created!");
                Console.ReadLine();
                return;
            }

            MessageHelpers.Success("Bill created!");
            Console.WriteLine($"Cost: {billCost}");
            Console.ReadLine();
        }
    }
}

using System;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
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

            _articleBillHelper = new ArticleBillHelpers(articleBillRepository);
            _serviceBillHelper = new ServiceBillHelpers(serviceBillRepository, employeeRepository);
        }

        public void Call()
        {
            var newBill = _billRepository.GetNewBill(BillType.Traditional);

            var doesContinue = true;

            //PrintHelpers.PrintOfferList(_articleBillRepository.GetAllAvailable());
            Console.WriteLine("Add articles:");
            while (true)
            {
                var articleBill = _articleBillHelper.TryGetArticleBill(ref doesContinue);
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

            //PrintHelpers.PrintOfferList(_serviceBillRepository.GetAll());
            Console.WriteLine("Add services:");
            while (true)
            {
                var serviceBill = _serviceBillHelper.TryGetService(ref doesContinue);
                if (!doesContinue) break;

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

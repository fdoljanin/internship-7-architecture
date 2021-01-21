using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
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

            _articleBillHelper = new ArticleBillHelpers(articleBillRepository);
            _serviceBillHelper = new ServiceBillHelpers(serviceBillRepository, employeeRepository);
        }

        public void Call()
        {
            var bill = new Bill();
            _billRepository.Add(bill);

            var doesContinue = true;
            decimal totalCost = 0; //move to domain later

            //PrintHelpers.PrintOfferList(_articleBillRepository.GetAllAvailable());
            Console.WriteLine("Add articles:");
            while (true)
            {
                var articleBill = _articleBillHelper.TryGetArticleBill(ref doesContinue);
                if (!doesContinue) break;

                totalCost += _articleBillRepository.GetPrice(articleBill.OfferId) * articleBill.Quantity;

                if (_billRepository.CheckIsArticleThere(bill.Id, articleBill))
                {
                    Console.WriteLine("Article already in! Quantity added...");
                    continue;
                }
                articleBill.BillId = bill.Id;
                _articleBillRepository.Add(articleBill);

                Console.WriteLine("Article added!");
            }

            //PrintHelpers.PrintOfferList(_serviceBillRepository.GetAll());
            Console.WriteLine("Add services:");
            while (true)
            {
                var serviceBill = _serviceBillHelper.TryGetService(ref doesContinue);
                if (!doesContinue) break;

                totalCost += _serviceBillRepository.GetPrice(serviceBill.OfferId) * serviceBill.Duration;
                serviceBill.BillId = bill.Id;
                _serviceBillRepository.Add(serviceBill);

                Console.WriteLine("Service added!");
            }

            _billRepository.FinishBill(bill.Id, totalCost, DateTime.Now);

            Console.ReadLine();
        }
    }
}

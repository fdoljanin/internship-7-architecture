using System;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class ServiceBillAction:IAction
    {
        private readonly ServiceBillRepository _serviceBillRepository;
        private readonly BillRepository _billRepository;
        private readonly ServiceBillHelpers _serviceBillHelper;
        public ServiceBillAction(ServiceBillRepository serviceBillRepository, 
            BillRepository billRepository, EmployeeRepository employeeRepository)
        {
            _serviceBillRepository = serviceBillRepository;
            _serviceBillHelper = new ServiceBillHelpers(serviceBillRepository, employeeRepository);
            _billRepository = billRepository;
        }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add service bill";
        public void Call()
        {
            var doesContinue = true;
            var newBill = _billRepository.GetNewBill(BillType.Service);
            //PrintHelpers.PrintOfferList(_serviceBillRepository.GetAll());

            var service = _serviceBillHelper.TryGetService(ref doesContinue);
            service.BillId = newBill.Id;

            _serviceBillRepository.Add(service);

            var billCost = _billRepository.FinishBillAndGetCost(newBill.Id);
            Console.WriteLine($"Cost: {billCost}");
            Console.ReadLine();

        }
    }
}

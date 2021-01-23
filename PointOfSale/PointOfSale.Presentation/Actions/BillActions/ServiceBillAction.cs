using System;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;
using PointOfSale.Presentation.Helpers.EntityReadHelpers;

namespace PointOfSale.Presentation.Actions.BillActions
{
    public class ServiceBillAction:IAction
    {
        private readonly ServiceBillRepository _serviceBillRepository;
        private readonly BillRepository _billRepository;
        private readonly EmployeeRepository _employeeRepository;
        public ServiceBillAction(ServiceBillRepository serviceBillRepository, 
            BillRepository billRepository, EmployeeRepository employeeRepository)
        {
            _serviceBillRepository = serviceBillRepository;
            _billRepository = billRepository;
            _employeeRepository = employeeRepository;
        }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add service bill";
        public void Call()
        {
            var doesContinue = true;
            var serviceList = _serviceBillRepository.GetAll();

            var serviceBill = new ServiceBill();
            PrintHelpers.PrintOfferList(serviceList);
            if (serviceList.Count == 0) return;

            Console.WriteLine("Enter service index:");
            var chosenService = ReadHelpers.TryGetListMember(serviceList, ref doesContinue);
            if (!doesContinue) return;

            var serviceBillInfo = ServiceBillHelpers.TryGetServiceInfo(_employeeRepository, ref doesContinue);
            if (!doesContinue) return;

            var newBill = _billRepository.GetNewBill(BillType.Service);
            serviceBill.OfferId = chosenService.Id;
            serviceBill.StartTime = serviceBillInfo.StartTime;
            serviceBill.Duration = serviceBillInfo.Duration;
            serviceBill.EmployeeId = serviceBillInfo.EmployeeId;
            serviceBill.BillId = newBill.Id;
            _serviceBillRepository.Add(serviceBill);
            Console.WriteLine("Service added!\n");

            var billCost = _billRepository.FinishBillAndGetCost(newBill.Id);

            MessageHelpers.Success("Bill created!");
            Console.WriteLine($"Cost: {billCost}");
            Console.ReadLine();

        }
    }
}

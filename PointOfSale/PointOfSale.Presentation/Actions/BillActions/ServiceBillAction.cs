﻿using System;
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
        private readonly ServiceBillHelpers _serviceBillHelper;
        public ServiceBillAction(ServiceBillRepository serviceBillRepository, 
            BillRepository billRepository, EmployeeRepository employeeRepository)
        {
            _serviceBillRepository = serviceBillRepository;
            _serviceBillHelper = new ServiceBillHelpers(employeeRepository);
            _billRepository = billRepository;
        }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add service bill";
        public void Call()
        {
            var doesContinue = true;
            var newBill = _billRepository.GetNewBill(BillType.Service);
            var serviceList = _serviceBillRepository.GetAll();

            var serviceBill = new ServiceBill();
            PrintHelpers.PrintOfferList(serviceList);

            Console.WriteLine("Enter service index:");

            serviceBill.OfferId = ReadHelpers.TryGetListMember(serviceList, ref doesContinue).Id;
            if (!doesContinue) return;
            var serviceBillInfo = _serviceBillHelper.TryGetServiceInfo(ref doesContinue);
            if (!doesContinue) return;

            serviceBill.StartTime = serviceBillInfo.StartTime;
            serviceBill.Duration = serviceBillInfo.Duration;
            serviceBill.EmployeeId = serviceBillInfo.EmployeeId;
            serviceBill.BillId = newBill.Id;
            _serviceBillRepository.Add(serviceBill);
            Console.WriteLine("Service added!");

            var billCost = _billRepository.FinishBillAndGetCost(newBill.Id);
            Console.WriteLine($"Cost: {billCost}");
            Console.ReadLine();

        }
    }
}

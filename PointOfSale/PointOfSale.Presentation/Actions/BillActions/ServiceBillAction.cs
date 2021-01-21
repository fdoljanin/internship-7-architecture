using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
            _serviceBillHelper = new ServiceBillHelpers(serviceBillRepository, employeeRepository);
            _billRepository = billRepository;
        }

        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Add service bill";
        public void Call()
        {
            var doesContinue = true;
            //PrintHelpers.PrintOfferList(_serviceBillRepository.GetAll());

            var service = _serviceBillHelper.TryGetService(ref doesContinue);
            if (!doesContinue) return;



            var bill = new Bill()
            {
                Type = BillType.Service,
                TransactionDate = DateTime.Now,
                Cost = _serviceBillRepository.GetPrice(service.OfferId) * service.Duration,
            };
            _billRepository.Add(bill);

            service.BillId = bill.Id;
            _serviceBillRepository.Add(service);

        }
    }
}

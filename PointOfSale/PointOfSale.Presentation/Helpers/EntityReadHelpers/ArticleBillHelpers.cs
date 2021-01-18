using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class ArticleBillHelpers
    {
        private readonly ArticleBillRepository _articleBillRepository;
        public ArticleBillHelpers(ArticleBillRepository articleBillRepository)
        {
            _articleBillRepository = articleBillRepository;
        }

        private (string name, int quantity) TryGetNameAndQuantity(ref bool doesContinue)
        {
            while (true)
            {
                doesContinue = ReadHelpers.DoesContinue(out var input);
                if (!doesContinue) return (default, default);
                if (!input.Contains('x'))
                {
                    Console.WriteLine("Quantity is missing!");
                    continue;
                }

                var quantityInput = input.Substring(input.LastIndexOf('x') + 1);
                var doesParse = int.TryParse(quantityInput, out var quantity);
                if (!doesParse || quantity <= 0)
                {
                    Console.WriteLine("Please enter valid quantity!");
                    continue;
                }

                var name = input.Substring(0, input.LastIndexOf('x')).Trim();
                return (name, quantity);
            }
        }
    

        public ArticleBill TryGetArticleBill(ref bool doesContinue)
        {
            while (true)
            {
                var (name, quantity) = TryGetNameAndQuantity(ref doesContinue);
                if (!doesContinue) return default;

                if (!_articleBillRepository.CheckDoesExist(name))
                {
                    Console.WriteLine($"Article {name} does not exist!");
                    continue;
                }

                if (!_articleBillRepository.CheckIsAvailable(name, quantity))
                {
                    Console.WriteLine("Article not available in that quantity!");
                    continue;
                }

                return new ArticleBill()
                {
                    OfferId = _articleBillRepository.FindByName(name).Id,
                    Quantity = quantity
                };
            }
        }

    }
}

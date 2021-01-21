using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public class ArticleBillHelpers
    {
        private ArticleBillRepository _articleBillRepository;

        public ArticleBillHelpers(ArticleBillRepository articleBillRepository)
        {
            _articleBillRepository = articleBillRepository;
        }
        private (int index, int quantity) TryGetIndexAndQuantity(ref bool doesContinue, int maxIndex)
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

                var indexInput = input.Substring(0, input.LastIndexOf('x'));
                doesParse &= int.TryParse(indexInput, out var index);

                if (doesParse && quantity > 0 && index > 0 && index <= maxIndex) return (index-1, quantity);
                Console.WriteLine("Please enter valid numbers!");
            }
        }


        public ArticleBill TryGetArticleBill(ref bool doesContinue)
        {
            while (true)
            {
                var articleList = _articleBillRepository.GetAllAvailable();
                PrintHelpers.PrintOfferList(articleList);

                var (articleIndex, quantity) = TryGetIndexAndQuantity(ref doesContinue, articleList.Count);
                if (!doesContinue) return default;

                var article = articleList.ElementAt(articleIndex);

                if (quantity <= article.Quantity)
                    return new ArticleBill()
                    {
                        OfferId = article.Id,
                        Quantity = quantity
                    };
                Console.WriteLine("Article not available in that quantity!");
            }
        }
    }
}

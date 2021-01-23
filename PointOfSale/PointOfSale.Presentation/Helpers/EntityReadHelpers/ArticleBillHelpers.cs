using System;
using System.Collections.Generic;
using System.Linq;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Presentation.Helpers.EntityReadHelpers
{
    public static class ArticleBillHelpers
    {
        private static (int index, int quantity) TryGetIndexAndQuantity(ref bool doesContinue, int maxIndex)
        {
            while (true)
            {
                var input = ReadHelpers.TryGetInput(ref doesContinue);

                if (!doesContinue) return (default, default);
                if (!input.Contains('x'))
                {
                    MessageHelpers.Error("Quantity is missing!");
                    continue;
                }

                var quantityInput = input.Substring(input.LastIndexOf('x') + 1);
                var doesParse = int.TryParse(quantityInput, out var quantity);

                var indexInput = input.Substring(0, input.LastIndexOf('x'));
                doesParse &= int.TryParse(indexInput, out var index);

                if (doesParse && quantity > 0 && index > 0 && index <= maxIndex) return (index-1, quantity);
                MessageHelpers.Error("Please enter valid numbers!");
            }
        }


        public static ArticleBill TryGetArticleBill(ICollection<Offer> articleList, ref bool doesContinue)
        {
            var articleBill = new ArticleBill();

            while (true)
            {
                var (articleIndex, quantity) = TryGetIndexAndQuantity(ref doesContinue, articleList.Count);
                if (!doesContinue) return default;
                articleBill.Quantity = quantity;

                var article = articleList.ElementAt(articleIndex);
                articleBill.OfferId = article.Id;

                if (quantity <= article.Quantity)
                {
                    return articleBill;
                }

                MessageHelpers.Error("Article not available in that quantity!");
            }
        }
    }
}

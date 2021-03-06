﻿using System;
using System.Linq;
using PointOfSale.Data.Enums;
using PointOfSale.Domain.Repositories;
using PointOfSale.Presentation.Abstractions;
using PointOfSale.Presentation.Helpers;

namespace PointOfSale.Presentation.Actions.InventoryActions
{
    public class ArticleStatusAction:IAction
    {
        private readonly OfferRepository _offerRepository;

        public ArticleStatusAction(OfferRepository offerRepository)
        {
            _offerRepository = offerRepository;
        }
        public int MenuIndex { get; set; }
        public string Label { get; set; } = "Article inventory status";
        public void Call()
        {
            var articles = _offerRepository.GetAll()
                .Where(o => o.Type == OfferType.Item).ToList();

            PrintHelpers.PrintOfferList(articles);

            Console.ReadLine();
        }
    }
}

﻿using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Data.Enums;

namespace PointOfSale.Domain.Repositories
{
    public class ArticleBillRepository:BaseRepository
    {
        public ArticleBillRepository(PointOfSaleDbContext dbContext):base(dbContext)
        {
        }


        public bool CheckDoesExist(string name)
        {
            return DbContext.Offers.Any(o => o.Type == OfferType.Item && o.Name.ToLower() == name.ToLower() && o.IsActive);
        }

        public Offer FindByName(string name)
        {
            return DbContext.Offers.First(o => o.Type == OfferType.Item && o.Name == name && o.IsActive);
        }

        public bool CheckIsAvailable(string name, int quantity)
        {
            var article = DbContext.Offers.First(o => o.Type == OfferType.Item && o.Name == name && o.IsActive);
            return article.Quantity >= quantity;
        }



    }
}

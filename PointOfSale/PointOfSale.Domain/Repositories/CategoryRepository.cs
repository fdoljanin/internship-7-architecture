using System.Collections.Generic;
using System.Collections.Specialized;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;
using PointOfSale.Domain.Repositories.Abstractions;

namespace PointOfSale.Domain.Repositories
{
    public class CategoryRepository : BaseRepository, IUniqueString
    {
        public CategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckUnique(string name)
        {
            return !DbContext.Categories.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public bool IsStringUnique(string name)
        {
            return !DbContext.Categories.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public void Add(Category category)
        {
            DbContext.Categories.Add(category);
            SaveChanges();
        }

        public void Edit(int categoryId, Category editedCategory)
        {
            var categoryDb = DbContext.Categories.Find(categoryId);
            categoryDb.Name = editedCategory.Name;
            SaveChanges();
        }

        public void Delete(int categoryId)
        {
            var categoryDb = DbContext.Categories.Find(categoryId);
            DbContext.Categories.Remove(categoryDb);
            SaveChanges();
        }

        public ICollection<Category> GetAll()
        {
            return DbContext.Categories.ToList();
        }

        public Category Find(int offerId)
        {
            return DbContext.Categories.Find(offerId);
        }

        public Category FindFullByName(string name)
        {
            return DbContext.Categories.Include(c=>c.OfferCategories).First(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
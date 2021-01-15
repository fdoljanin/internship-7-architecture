using System.Collections.Generic;
using System.Collections.Specialized;
using PointOfSale.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PointOfSale.Data.Entities.Models;

namespace PointOfSale.Domain.Repositories
{
    public class CategoryRepository : BaseRepository
    {
        public CategoryRepository(PointOfSaleDbContext dbContext) : base(dbContext)
        {
        }

        public bool CheckUnique(string name)
        {
            return !DbContext.Categories.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public void Add(Category category)
        {
            DbContext.Categories.Add(category);
            SaveChanges();
        }

        public void Edit(string name, Category editedCategory)
        {
            var categoryDb = DbContext.Categories.First(c => c.Name.ToLower() == name.ToLower());
            categoryDb.Name = editedCategory.Name;
            SaveChanges();
        }

        public void Delete(string name)
        {
            var categoryDb = DbContext.Categories.First(c => c.Name.ToLower() == name.ToLower());
            DbContext.Categories.Remove(categoryDb);
            SaveChanges();
        }

        public ICollection<Category> GetAll()
        {
            return DbContext.Categories.ToList();
        }

        public Category FindByName(string name)
        {
            return DbContext.Categories.First(c => c.Name.ToLower() == name.ToLower());
        }

        public Category FindFullByName(string name)
        {
            return DbContext.Categories.Include(c=>c.OfferCategories).First(c => c.Name.ToLower() == name.ToLower());
        }
    }
}
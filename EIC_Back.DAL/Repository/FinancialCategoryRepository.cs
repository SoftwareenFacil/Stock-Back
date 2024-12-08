using Microsoft.EntityFrameworkCore;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using AutoMapper;

namespace EIC_Back.DAL.Repository
{
    public class FinancialCategoryRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public FinancialCategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FinancialCategory>> GetAllFinancialCategory(int? id, int? pageNumber = null, int? pageSize = null)
        {
            pageNumber = pageNumber ?? 1;
            pageSize = pageSize ?? 100;

            IQueryable<FinancialCategory> query = _dbContext.FinancialCategory
            .Include(q => q.FinancialSubCategory)
            .ThenInclude(q => q.FinancialMovements);


            if (id.HasValue && id.Value > 0)
            {
                query = query.Where(q => q.Id == id.Value);
            }

            var financialcategory = await query
            .Skip((pageNumber.Value - 1) * pageSize.Value)
            .Take(pageSize.Value)
            .ToListAsync();


            return financialcategory;
        }
        public async Task<bool> DeleteFinancialCategory(int id)
        {
            var FinancialCategory = await _dbContext.FinancialCategory.FindAsync(id);

            if (FinancialCategory is not null) _dbContext.FinancialCategory.Remove(FinancialCategory);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<FinancialCategory?> GetFinancialCategoryById(int id)
        {
            return await _dbContext.FinancialCategory.FindAsync(id);
        }
        public async Task<int> InsertFinancialCategory(FinancialCategory FinancialCategory)
        {
            await _dbContext.FinancialCategory.AddAsync(FinancialCategory);
            await _dbContext.SaveChangesAsync();
            return FinancialCategory.Id;
        }
        public async Task<bool> UpdateFinancialCategory(FinancialCategory FinancialCategory)
        {
            var dbFinancialCategory = await _dbContext.FinancialCategory.FindAsync(FinancialCategory.Id);

            if (dbFinancialCategory is not null)
            {
                var updatedFinancialCategory = _mapper.Map(FinancialCategory, dbFinancialCategory);
                _dbContext.FinancialCategory.Update(updatedFinancialCategory);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

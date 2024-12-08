using AutoMapper;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace EIC_Back.DAL.Repository
{
    public class FinancialSubCategoryRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public FinancialSubCategoryRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> UpdateFinancialSubCategory(FinancialSubCategory FinancialSubCategory)
        {
            var dbFinancialSubCategory = await _dbContext.FinancialSubCategory.FindAsync(FinancialSubCategory.Id);

            if (dbFinancialSubCategory is not null)
            {
                var updatedFinancialSubCategory = _mapper.Map(FinancialSubCategory, dbFinancialSubCategory);
                _dbContext.FinancialSubCategory.Update(updatedFinancialSubCategory);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteFinancialSubCategory(int id)
        {
            var FinancialSubCategory = await _dbContext.FinancialSubCategory.FindAsync(id);

            if (FinancialSubCategory is not null) _dbContext.FinancialSubCategory.Remove(FinancialSubCategory);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<int> InsertFinancialSubCategory(FinancialSubCategory FinancialSubCategory)
        {
            await _dbContext.FinancialSubCategory.AddAsync(FinancialSubCategory);
            await _dbContext.SaveChangesAsync();
            return FinancialSubCategory.Id;
        }
        public async Task<FinancialSubCategory?> GetFinancialSubCategoryById(int id)
        {
            return await _dbContext.FinancialSubCategory.FindAsync(id);
        }

        public async Task<IEnumerable<FinancialSubCategory>> GetFinancialSubCategoryByCategoryId(int categoryid)
        {
            return await _dbContext.FinancialSubCategory.Where(x => x.FinancialCategoryId == categoryid).ToListAsync();
        }
        public async Task<IEnumerable<FinancialSubCategory>> GetAllFinancialSubCategory(int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbContext.FinancialSubCategory.AsQueryable();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }
            else
            {
                query = query.Take(100);
            }

            return await query.ToListAsync();
        }
    }
}

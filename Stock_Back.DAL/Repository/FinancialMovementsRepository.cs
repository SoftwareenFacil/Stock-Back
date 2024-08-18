using Microsoft.EntityFrameworkCore;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using AutoMapper;

namespace Stock_Back.DAL.Repository
{
    public class FinancialMovementsRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public FinancialMovementsRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FinancialMovements>> GetAllFinancialMovements(int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbContext.FinancialMovements.AsQueryable();

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
        public async Task<bool> DeleteFinancialMovements(int id)
        {
            var FinancialMovements = await _dbContext.FinancialMovements.FindAsync(id);

            if (FinancialMovements is not null) _dbContext.FinancialMovements.Remove(FinancialMovements);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<FinancialMovements?> GetFinancialMovementsById(int id)
        {
            return await _dbContext.FinancialMovements.FindAsync(id);
        }
        public async Task<int> InsertFinancialMovements(FinancialMovements FinancialMovements)
        {
            await _dbContext.FinancialMovements.AddAsync(FinancialMovements);
            await _dbContext.SaveChangesAsync();
            return FinancialMovements.Id;
        }
        public async Task<bool> UpdateFinancialMovements(FinancialMovements FinancialMovements)
        {
            var dbFinancialMovements = await _dbContext.FinancialMovements.FindAsync(FinancialMovements.Id);

            if (dbFinancialMovements is not null)
            {
                var updatedFinancialMovements = _mapper.Map(FinancialMovements, dbFinancialMovements);
                _dbContext.FinancialMovements.Update(updatedFinancialMovements);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

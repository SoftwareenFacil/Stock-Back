using AutoMapper;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Stock_Back.DAL.Repository
{
    public class MaterialRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public MaterialRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> UpdateMaterial(MaterialType MaterialType)
        {
            var dbMaterial = await _dbContext.Materials.FindAsync(MaterialType.Id);

            if (dbMaterial is not null)
            {
                var updatedMaterial = _mapper.Map(MaterialType, dbMaterial);
                _dbContext.Materials.Update(updatedMaterial);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteMaterial(int id)
        {
            var MaterialType = await _dbContext.Materials.FindAsync(id);

            if (MaterialType is not null) _dbContext.Materials.Remove(MaterialType);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<int> InsertMaterial(MaterialType MaterialType)
        {
            await _dbContext.Materials.AddAsync(MaterialType);
            await _dbContext.SaveChangesAsync();
            return MaterialType.Id;
        }
        public async Task<MaterialType?> GetMaterialById(int id)
        {
            return await _dbContext.Materials.FindAsync(id);
        }
        public async Task<IEnumerable<MaterialType>> GetAllMaterials(int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbContext.Materials.AsQueryable();

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

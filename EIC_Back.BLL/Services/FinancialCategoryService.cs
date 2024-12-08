using AutoMapper;
using EIC_Back.BLL.Models.FinancialCategoryModelDTO;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using EIC_Back.DAL.Repository;

namespace EIC_Back.BLL.Services
{
    public class FinancialCategoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FinancialCategoryRepository _financialCategoryService;

        public FinancialCategoryService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _financialCategoryService = new FinancialCategoryRepository(dbContext, mapper);
        }

        public async Task<int> AddFinancialCategory(FinancialCategoryInsertDTO FinancialCategoryInsertDTO)
        {
            var FinancialCategoryCreate = _mapper.Map<FinancialCategoryInsertDTO, FinancialCategory>(FinancialCategoryInsertDTO);

            return await _financialCategoryService.InsertFinancialCategory(FinancialCategoryCreate);
        }
        public async Task<bool> DeleteFinancialCategoryById(int id)
        {
            return await _financialCategoryService.DeleteFinancialCategory(id);
        }
        public async Task<dynamic?> GetFinancialCategory(int id, int? pageNumber, int? pageSize)
        {
            if (id == 0)
                return await GetAllFinancialCategory(null, pageNumber, pageSize);
            return await GetFinancialCategoryById(id);
        }

        public async Task<FinancialCategoryDTO?> GetFinancialCategoryById(int id)
        {
            var FinancialCategory = await _financialCategoryService.GetFinancialCategoryById(id);

            return _mapper.Map<FinancialCategory?, FinancialCategoryDTO?>(FinancialCategory);
        }

        public async Task<IEnumerable<FinancialCategoryFullDTO>?> GetAllFinancialCategory(int? id, int? pageNumber, int? pageSize)
        {
            var FinancialCategory = await _financialCategoryService.GetAllFinancialCategory(id, pageNumber, pageSize);

            return _mapper.Map<IEnumerable<FinancialCategory>, IEnumerable<FinancialCategoryFullDTO>>(FinancialCategory);
        }
        public async Task<(bool, bool)> UpdateFinancialCategory(FinancialCategoryEditDTO FinancialCategoryEdited)
        {
            bool isUpdated = false;
            bool isFinancialCategory = false;

            var FinancialCategory = await _financialCategoryService.GetFinancialCategoryById(FinancialCategoryEdited.Id);

            if (FinancialCategory != null)
            {
                isFinancialCategory = true;
                var editedFinancialCategory = _mapper.Map(FinancialCategoryEdited, FinancialCategory);
                isUpdated = await _financialCategoryService.UpdateFinancialCategory(editedFinancialCategory);
            }

            return (isUpdated, isFinancialCategory);
        }
    }
}

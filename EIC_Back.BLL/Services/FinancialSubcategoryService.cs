using AutoMapper;
using EIC_Back.BLL.Models.FinancialSubCategoryModelDTO;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using EIC_Back.DAL.Repository;

namespace EIC_Back.BLL.Services
{
    public class FinancialSubcategoryService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FinancialSubCategoryRepository _repository;

        public FinancialSubcategoryService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = new FinancialSubCategoryRepository(dbContext, mapper);
        }

        public async Task<int> AddFinancialSubCategory(FinancialSubCategoryInsertDTO FinancialSubCategoryInsertDTO)
        {
            var FinancialSubCategoryCreate = _mapper.Map<FinancialSubCategoryInsertDTO, FinancialSubCategory>(FinancialSubCategoryInsertDTO);

            return await _repository.InsertFinancialSubCategory(FinancialSubCategoryCreate);
        }
        public async Task<bool> DeleteFinancialSubCategoryById(int id)
        {
            return await _repository.DeleteFinancialSubCategory(id);
        }
        public async Task<dynamic?> GetFinancialSubCategory(int id, int? categoryId)
        {
            if (categoryId != null)
                return await GetFinancialSubCategoryByCategoryId((int)categoryId);
            if (id == 0)
                return await GetAllFinancialSubCategory(1, 10);      
            return await GetFinancialSubCategoryById(id);
        }

        public async Task<FinancialSubCategoryDTO?> GetFinancialSubCategoryById(int id)
        {
            var FinancialSubCategory = await _repository.GetFinancialSubCategoryById(id);

            return _mapper.Map<FinancialSubCategory?, FinancialSubCategoryDTO?>(FinancialSubCategory);
        }
        public async Task<IEnumerable<FinancialSubCategoryDTO?>> GetFinancialSubCategoryByCategoryId(int subcategoryid)
        {
            var FinancialSubCategory = await _repository.GetFinancialSubCategoryByCategoryId(subcategoryid);

            return _mapper.Map<IEnumerable<FinancialSubCategory>, IEnumerable<FinancialSubCategoryDTO>>(FinancialSubCategory);
        }


        public async Task<IEnumerable<FinancialSubCategoryDTO>?> GetAllFinancialSubCategory(int? pageNumber, int? pageSize)
        {
            var FinancialSubCategory = await _repository.GetAllFinancialSubCategory(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<FinancialSubCategory>, IEnumerable<FinancialSubCategoryDTO>>(FinancialSubCategory);
        }
        public async Task<(bool, bool)> UpdateFinancialSubCategory(FinancialSubCategoryEditDTO FinancialSubCategoryEdited)
        {
            bool isUpdated = false;
            bool isFinancialSubCategory = false;

            var FinancialSubCategoryUpdater = new FinancialSubCategoryRepository(_dbContext, _mapper);
            var FinancialSubCategory = await _repository.GetFinancialSubCategoryById(FinancialSubCategoryEdited.Id);

            if (FinancialSubCategory != null)
            {
                isFinancialSubCategory = true;
                var editedFinancialSubCategory = _mapper.Map(FinancialSubCategoryEdited, FinancialSubCategory);
                isUpdated = await FinancialSubCategoryUpdater.UpdateFinancialSubCategory(editedFinancialSubCategory);
            }

            return (isUpdated, isFinancialSubCategory);
        }
    }
}

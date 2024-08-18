using AutoMapper;
using Stock_Back.BLL.Models.FinancialSubCategoryModelDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using Stock_Back.DAL.Repository;

namespace Stock_Back.BLL.Services
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
        public async Task<dynamic?> GetFinancialSubCategory(int id, int? pageNumber, int? pageSize)
        {
            if (id == 0)
                return await GetAllFinancialSubCategory(pageNumber, pageSize);
            return await GetFinancialSubCategoryById(id);
        }

        public async Task<FinancialSubCategoryDTO?> GetFinancialSubCategoryById(int id)
        {
            var FinancialSubCategory = await _repository.GetFinancialSubCategoryById(id);

            return _mapper.Map<FinancialSubCategory?, FinancialSubCategoryDTO?>(FinancialSubCategory);
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

using AutoMapper;
using Stock_Back.BLL.Models.FinancialMovementsModelDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using Stock_Back.DAL.Repository;

namespace Stock_Back.BLL.Services
{
    public class FinancialMovementService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FinancialMovementsRepository _repository;

        public FinancialMovementService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = new FinancialMovementsRepository(dbContext, mapper);
        }

        public async Task<int> AddFinancialMovements(FinancialMovementsInsertDTO FinancialMovementsInsertDTO)
        {
            var FinancialMovementsCreate = _mapper.Map<FinancialMovementsInsertDTO, FinancialMovements>(FinancialMovementsInsertDTO);

            return await _repository.InsertFinancialMovements(FinancialMovementsCreate);
        }
        public async Task<bool> DeleteFinancialMovementsById(int id)
        {
            return await _repository.DeleteFinancialMovements(id);
        }
        public async Task<(bool, bool)> UpdateFinancialMovements(FinancialMovementsEditDTO FinancialMovementsEdited)
        {
            bool isUpdated = false;
            bool isFinancialMovements = false;

            var FinancialMovements = await _repository.GetFinancialMovementsById(FinancialMovementsEdited.Id);

            if (FinancialMovements != null)
            {
                isFinancialMovements = true;
                var editedFinancialMovements = _mapper.Map(FinancialMovementsEdited, FinancialMovements);
                isUpdated = await _repository.UpdateFinancialMovements(editedFinancialMovements);
            }

            return (isUpdated, isFinancialMovements);
        }
        public async Task<dynamic?> GetFinancialMovements(int id, int? pageNumber, int? pageSize)
        {
            if (id == 0)
                return await GetAllFinancialMovements(pageNumber, pageSize);
            return await GetFinancialMovementsById(id);
        }

        public async Task<FinancialMovementsDTO?> GetFinancialMovementsById(int id)
        {
            var FinancialMovements = await _repository.GetFinancialMovementsById(id);

            return _mapper.Map<FinancialMovements?, FinancialMovementsDTO?>(FinancialMovements);
        }

        public async Task<IEnumerable<FinancialMovementsDTO>?> GetAllFinancialMovements(int? pageNumber, int? pageSize)
        {
            var FinancialMovements = await _repository.GetAllFinancialMovements(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<FinancialMovements>, IEnumerable<FinancialMovementsDTO>>(FinancialMovements);
        }
    }
}

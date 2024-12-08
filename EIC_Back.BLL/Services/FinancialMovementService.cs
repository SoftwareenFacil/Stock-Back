using AutoMapper;
using EIC_Back.BLL.Models.FinancialMovementsModelDTO;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using EIC_Back.DAL.Repository;

namespace EIC_Back.BLL.Services
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

        public async Task<IEnumerable<FinancialMovementsDTO>> getFinancialMovementsFromReference(int referenceId)
        {
            var FinancialMovements = await _repository.GetAllFinancialMovementsFromReference(referenceId);

            return _mapper.Map<IEnumerable<FinancialMovements>, IEnumerable<FinancialMovementsDTO>>(FinancialMovements);
        }

        public async Task<IEnumerable<FinancialMovementsDTO>?> GetAllFinancialMovements(int? pageNumber, int? pageSize)
        {
            var FinancialMovements = await _repository.GetAllFinancialMovements(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<FinancialMovements>, IEnumerable<FinancialMovementsDTO>>(FinancialMovements);
        }

        private async Task<IEnumerable<FinancialMovementsDTO>?> getFinancialMovementsByDateRange(DateTime start, DateTime end)
        {
            var FinancialMovements = await _repository.getFinancialMovementsByStartAndEndDate(start, end);

            return _mapper.Map<IEnumerable<FinancialMovements>, IEnumerable<FinancialMovementsDTO>>(FinancialMovements);
        }

        private List<int> getIdsfromFinancialMovements(IEnumerable<FinancialMovementsDTO?> movements)
        {
            var ids = new List<int>
            {
                0
            };
            foreach (var movement in movements)
            {
                if(movement.ReferenceId != null)
                    if(!ids.Contains((int)movement.ReferenceId))
                        ids.Add((int)movement.ReferenceId);
            }
            return ids;
        }

    }
}

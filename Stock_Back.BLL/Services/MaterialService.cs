using AutoMapper;
using Stock_Back.BLL.Models.MaterialModelDTO;
using Stock_Back.DAL.Context;
using Stock_Back.DAL.Models;
using Stock_Back.DAL.Repository;

namespace Stock_Back.BLL.Services
{
    public class MaterialService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly MaterialRepository _repository;

        public MaterialService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _repository = new MaterialRepository(dbContext, mapper);
        }

        public async Task<dynamic?> GetMaterials(int id, int? pageNumber, int? pageSize)
        {
            if (id == 0)
                return await GetAllMaterial(pageNumber, pageSize);
            return await GetMaterialById(id);
        }

        public async Task<MaterialDTO?> GetMaterialById(int id)
        {
            var MaterialType = await _repository.GetMaterialById(id);
            if (MaterialType == null)
            {
                return null;
            }
            else
            {
                return _mapper.Map<MaterialType, MaterialDTO>(MaterialType);
            }
        }

        public async Task<IEnumerable<MaterialDTO>?> GetAllMaterial(int? pageNumber, int? pageSize)
        {
            var materials = await _repository.GetAllMaterials(pageNumber, pageSize);

            if (materials.Any())
            {
                return _mapper.Map<IEnumerable<MaterialType>, IEnumerable<MaterialDTO>>(materials);
            }
            else
            {
                return null;
            }
        }
        public async Task<int> AddMaterial(MaterialInsertDTO materialInsertDTO)
        {
            var materialCreate = _mapper.Map<MaterialInsertDTO, MaterialType>(materialInsertDTO);

            return await _repository.InsertMaterial(materialCreate);
        }
        public async Task<bool> DeleteMaterialById(int id)
        {
            return await _repository.DeleteMaterial(id);
        }
        public async Task<(bool, bool)> UpdateMaterial(MaterialEditDTO materialEdited)
        {
            bool isUpdated = false;
            bool isMaterial = false;

            var MaterialType = await _repository.GetMaterialById(materialEdited.Id);

            if (MaterialType != null)
            {
                isMaterial = true;
                var editedMaterial = _mapper.Map(materialEdited, MaterialType);
                isUpdated = await _repository.UpdateMaterial(editedMaterial);
            }

            return (isUpdated, isMaterial);
        }
    }
}

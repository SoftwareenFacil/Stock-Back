using AutoMapper;
using EIC_Back.BLL.EnumExtensions;
using EIC_Back.BLL.Models.PersonnelModelDTO;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Repository;
using EIC_Back.DAL.Models;

namespace EIC_Back.BLL.Services
{
    public class PersonnelService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PersonnelRepository _personnelRepository;

        public PersonnelService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _personnelRepository = new PersonnelRepository(dbContext, _mapper);
        }
        public async Task<(bool, bool)> UpdatePersonnel(PersonnelEditDTO personnelEdited)
        {
            bool isUpdated = false;
            bool isPersonnel = false;

            var personnel = await _personnelRepository.GetPersonnelById(personnelEdited.Id);

            if (personnel != null)
            {
                isPersonnel = true;
                var personnelExists = false;
                var personnelTaxExistsOnEnterprise = false;
                if(personnelEdited.Email != personnel.Email)
                    personnelExists = await _personnelRepository.GetPersonnelByEmail(personnelEdited.Email) != null ? true : false;
                if(personnelEdited.TaxId != personnel.TaxId)
                    personnelTaxExistsOnEnterprise = (await _personnelRepository.GetPersonnelByTaxId(personnelEdited.TaxId) != null);

                if (personnelExists || personnelTaxExistsOnEnterprise) return (isUpdated, isPersonnel);
            
                var editedPersonnel = _mapper.Map(personnelEdited, personnel);
                isUpdated = await _personnelRepository.UpdatePersonnel(editedPersonnel);
                if (isUpdated && personnelEdited.projectId != null)
                    await AssignPersonnel(personnelEdited.Id, (int)personnelEdited.projectId);
            }

            return (isUpdated, isPersonnel);
        }
        public async Task<int> AddPersonnel(PersonnelInsertDTO personnelInsertDTO)
        {
            var personnelExists = await _personnelRepository.GetPersonnelByEmail(personnelInsertDTO.Email);
            var personnelTaxExistsOnEnterprise = await _personnelRepository.GetPersonnelByTaxId(personnelInsertDTO.TaxId);

            if (personnelExists != null || personnelTaxExistsOnEnterprise != null) return -1;

            var personnelCreate = _mapper.Map<PersonnelInsertDTO, Personnel>(personnelInsertDTO);           

            var inserted =  await _personnelRepository.InsertPersonnel(personnelCreate);

            return inserted;
        }
        public async Task<bool> DeletePersonnelById(int id)
        {
            return await _personnelRepository.DeletePersonnel(id);
        }
        public async Task<dynamic?> GetPersonnel(string value, Identifier identifier, int? pageNumber, int? pageSize)
        {
            return identifier switch
            {
                Identifier.Unknown or Identifier.All => await GetAllPersonnel(pageNumber, pageSize),
                Identifier.Id => await GetById(value),
                Identifier.CreatedDate => await GetByCreatedDate(value),
                Identifier.Email => await GetByEmail(value),
                Identifier.Phone => await GetByPhone(value),
                Identifier.Specialty => await GetBySpeciality(value),
                _ => null
            };
        }

        public async Task<IEnumerable<PersonnelDTO>> GetBySpeciality(string value)
        {
            var personnel = await _personnelRepository.GetPersonnelBySpeciality(value);

            return _mapper.Map<IEnumerable<PersonnelDTO>>(personnel);
        }

        public async Task<PersonnelDTO?> GetByPhone(string value)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrEmpty(value))
                return null;

            var personnel = await _personnelRepository.GetClientByPhone(value);

            return _mapper.Map<Personnel?, PersonnelDTO?>(personnel);
        }

        public async Task<PersonnelDTO?> GetByEmail(string value)
        {
            var personnel = await _personnelRepository.GetPersonnelByEmail(value);

            return _mapper.Map<Personnel?, PersonnelDTO?>(personnel);
        }

        public async Task<IEnumerable<PersonnelDTO>?> GetByCreatedDate(string value)
        {
            var success = DateTime.TryParse(value, out DateTime date);
            if (!success) return null;

            var personnel = await _personnelRepository.GetPersonnelByCreatedDate(date);

            return _mapper.Map<IEnumerable<PersonnelDTO>>(personnel);
        }

        public async Task<PersonnelDTO?> GetById(string value)
        {
            var success = int.TryParse(value, out int id);
            if (!success) return null;

            var personnel = await _personnelRepository.GetPersonnelById(id);

            return _mapper.Map<Personnel?, PersonnelDTO?>(personnel);
        }

        public async Task<IEnumerable<PersonnelDTO>> GetAllPersonnel(int? pageNumber, int? pageSize)
        {
            var personnel = await _personnelRepository.GetAllPersonnel(pageNumber, pageSize);

            return _mapper.Map<IEnumerable<Personnel>, IEnumerable<PersonnelDTO>>(personnel);
        }

        private async Task<int> AssignPersonnel(int inserted, int projectid)
        {
            //var service = new ProjectPersonnelService(_dbContext, _mapper);
            //var projectdesignate = new ProjectPersonnelInsertDTO
            //{
            //    HoursWorked = 0,
            //    LastTimeWorked = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
            //    PersonnelId = inserted,
            //    ProjectId = projectid,
            //    WorkDescription = "work"
            //};
            //return await service.AddProjectPersonnel(projectdesignate);
            return 0;
        }
    }
}

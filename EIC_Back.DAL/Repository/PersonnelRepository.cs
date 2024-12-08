using AutoMapper;
using EIC_Back.DAL.Context;
using EIC_Back.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace EIC_Back.DAL.Repository
{
    public class PersonnelRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public PersonnelRepository(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> UpdatePersonnel(Personnel personnel)
        {
            var dbPersonnel = await _dbContext.Personnel.FindAsync(personnel.Id);

            if (dbPersonnel is not null)
            {
                var updatedPersonnel = _mapper.Map(personnel, dbPersonnel);
                _dbContext.Personnel.Update(updatedPersonnel);
            }

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeletePersonnel(int id)
        {
            var personnel = await _dbContext.Personnel.FindAsync(id);

            if (personnel is not null) _dbContext.Personnel.Remove(personnel);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<int> InsertPersonnel(Personnel personnel)
        {
            await _dbContext.Personnel.AddAsync(personnel);
            await _dbContext.SaveChangesAsync();
            return personnel.Id;
        }
        public async Task<IEnumerable<Personnel>> GetPersonnelBySpeciality(string? speciality)
        {
            return await _dbContext.Personnel
                .Where(x => x.Specialty == speciality)
                .Take(100)
                .ToListAsync();
        }
        public async Task<Personnel?> GetClientByPhone(string phone)
        {
            return await _dbContext.Personnel.FirstOrDefaultAsync(x => x.Phone == phone);
        }
        public async Task<Personnel?> GetPersonnelById(int id)
        {
            return await _dbContext.Personnel.FindAsync(id);
        }
        public async Task<Personnel?> GetPersonnelByEmail(string? email)
        {
            return await _dbContext.Personnel.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Personnel?> GetPersonnelByTaxId(int taxid)
        {
            return await _dbContext.Personnel.FirstOrDefaultAsync(x => x.TaxId == taxid);
        }
        public async Task<IEnumerable<Personnel>> GetPersonnelByCreatedDate(DateTime date)
        {
            var utcDate = date.Date.ToUniversalTime();
            return await _dbContext.Personnel
                .Where(x => x.Created.Date == utcDate.Date)
                .Take(100)
                .ToListAsync();
        }
        public async Task<IEnumerable<Personnel>> GetAllPersonnel(int? pageNumber = null, int? pageSize = null)
        {
            var query = _dbContext.Personnel
                    .Select(person => new Personnel
                    {
                        Id = person.Id,
                        Name = person.Name,
                        Specialty = person.Specialty,
                        PricePerWorkDay = person.PricePerWorkDay,
                        TaxId = person.TaxId,
                        Phone = person.Phone,
                        Email = person.Email,
                        Created = person.Created,
                        Updated = person.Updated,

                    })
                    .AsQueryable();

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

using AutoMapper;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repository.Interfaces;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class ProfessionService : IProfessionService
    {
        public readonly IProfessionRepository _professionRepository;
        public readonly IMapper _mapper;
        public ProfessionService(IProfessionRepository professionRepository,IMapper mapper)
        {
            _professionRepository = professionRepository;
            _mapper = mapper;
            
        }
        public async Task CreateAsync([FromForm] ProfessionCreateDto professionCreateDto)
        {
            var profession = _mapper.Map<Profession>(professionCreateDto);
            //profession.CreatedDate = DateTime.UtcNow.AddHours(4);
            //profession.UpdatedDate = DateTime.UtcNow.AddHours(4);
            await _professionRepository.CreateAsync(profession);
            await _professionRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {         
            var profession = await _professionRepository.GetByIdAsync(x => x.Id == id);
            if (profession == null) throw new NullReferenceException();
            profession.IsDeleted = !profession.IsDeleted;
            //profession.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _professionRepository.CommitAsync();
        }

        public async Task<List<ProfessionGetDto>> GetAllAsync()
        {
            var profession = await _professionRepository.GetAllAsync(x => x.IsDeleted==false);
            IEnumerable<ProfessionGetDto> professionGetDtos =profession.Select(x => new ProfessionGetDto
            {
                Name = x.Name,

            });
            return professionGetDtos.ToList();
        }

        public async Task<ProfessionGetDto> GetByIdAsync(int id)
        {
            var profession =await _professionRepository.GetByIdAsync(x => x.Id == id);
            if (profession == null) throw new NullReferenceException();

            var professionGetDto = _mapper.Map<ProfessionGetDto>(profession);
            return professionGetDto;
        }

        public async Task UpdateAsync([FromForm] ProfesionUpdateDto profesionUpdateDto)
        {

            var profession =await _professionRepository.GetByIdAsync(x => x.Id == profesionUpdateDto.Id);
            if (profession == null) throw new NullReferenceException();

            profession = _mapper.Map(profesionUpdateDto, profession);
            //profession.UpdatedDate= DateTime.UtcNow.AddHours(4);

            await _professionRepository.CommitAsync();
        }
    }
}

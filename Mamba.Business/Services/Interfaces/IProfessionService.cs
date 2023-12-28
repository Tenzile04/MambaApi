using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Interfaces
{
    public interface IProfessionService
    {
        Task CreateAsync([FromForm]ProfessionCreateDto professionCreateDto);
        Task Delete(int id);
        Task<ProfessionGetDto> GetByIdAsync(int id);
        Task<List<ProfessionGetDto>> GetAllAsync();
        Task UpdateAsync([FromForm]ProfesionUpdateDto profesionUpdateDto);
    }
}

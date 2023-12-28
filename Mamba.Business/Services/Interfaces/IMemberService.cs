using MambaManyToManyCrud.DTOs.MemberDtos;
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
    public interface IMemberService
    {
        Task CreateAsync([FromForm] MemberCreateDto memberCreateDto);
        Task Delete(int id);
        Task<MemberGetDto> GetByIdAsync(int id);
        Task<List<MemberGetDto>> GetAllAsync(string? search,int? profrssionId,int? order);
        Task UpdateAsync([FromForm] MemberUpdateDto memberUpdateDto);
    }
}

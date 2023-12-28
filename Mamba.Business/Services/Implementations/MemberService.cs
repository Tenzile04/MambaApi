using AutoMapper;
using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using Mamba.Core.Repository.Interfaces;
using Mamba.Data.Repository.Implementations;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.DTOs.MemberDtos;
using MambaManyToManyCrud.Entities;
using MambaManyToManyCrud.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Business.Services.Implementations
{
    public class MemberService : IMemberService
    {

        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IMemberProfessionRepository _memberProfessionRepository;
        private readonly AppDbContext _context;
        public MemberService(IMemberRepository memberRepository,IMapper mapper,IWebHostEnvironment env,IMemberProfessionRepository memberProfessionRepository,AppDbContext context)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _env = env;
            _memberProfessionRepository = memberProfessionRepository;
            _context = context;
        }
        public async Task CreateAsync([FromForm] MemberCreateDto memberCreateDto)
        {
            var member = _mapper.Map<Member>(memberCreateDto);
            //member.CreatedDate = DateTime.UtcNow.AddHours(4);
            //member.UpdatedDate = DateTime.UtcNow.AddHours(4);
            bool check = true;
            if (memberCreateDto.ProfessionIds != null)
            {
                foreach (var professionId in memberCreateDto.ProfessionIds)
                {
                    if (_context.Professions.Any(x => x.Id == professionId))
                    {
                        check = false;
                        break;
                    }
                }
            }
            if (!check)
            {
                if (memberCreateDto.ProfessionIds != null)
                {
                    foreach (var professionId in memberCreateDto.ProfessionIds)
                    {
                        MemberProfession memberProfession = new MemberProfession()
                        {
                            Member = member,
                            ProfessionId = professionId
                        };
                        _context.MembersProfessions.Add(memberProfession);
                    }
                }
            }
            else
            {
                throw new InvalidNotFoundException();

            }


            if (memberCreateDto.ImageFile != null)
            {

                if (memberCreateDto.ImageFile.ContentType != "image/jpeg" && memberCreateDto.ImageFile.ContentType != "image/png")
                {
                    throw new InvalidImageFileException("ImageFile","ImageFile ContentType must be jpeg or png" );
                }

                if (memberCreateDto.ImageFile.Length > 2097152)
                {
                    throw new InvalidImageFileException("ImageFile", "ImageFile File size must be lower than 2mb");
                }

                member.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/members", memberCreateDto.ImageFile);
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile Required!");

            }


            await _memberRepository.CreateAsync(member);
            await _memberRepository.CommitAsync();
        }

        public async Task Delete(int id)
        {
            var member =await _memberRepository.GetByIdAsync(x => x.Id == id);
            if (member == null) throw new  InvalidNotFoundException();
            member.IsDeleted = !member.IsDeleted;

            //member.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _memberRepository.CommitAsync();
        }

        public async Task<List<MemberGetDto>> GetAllAsync(string? search, int? profrssionId, int? order)
        {
            var member =  _memberRepository.GetQueryable();

            var profession = await _memberRepository.GetAllAsync(x => x.IsDeleted == false);
            if (search != null)
            {
                member = member.Where(m => m.FullName.Contains(search.Trim().ToLower()));

            }
            if (profrssionId != null)
            {
                member = member.Where(x => x.MemberProfessions.Any(x => x.ProfessionId == profrssionId && x.MemberId == x.Id));
            }
            if (order != null)
            {
                switch (order)
                {
                    case 1:
                        member = member.OrderByDescending(x => x.CreatedDate); break;
                    case 2:
                        member = member.OrderBy(x => x.FullName); break;
                    default:
                        throw new Exception();
                };
            }

            IEnumerable<MemberGetDto> memberGetDtos = new List<MemberGetDto>();
            memberGetDtos = member.Select(x => new MemberGetDto
            {
                FullName = x.FullName

            });

            return memberGetDtos.ToList();
        }

        public async Task<MemberGetDto> GetByIdAsync(int id)
        {

            var member = await _memberRepository.GetByIdAsync(x => x.Id == id );

            if (member == null) throw new InvalidNotFoundException();

            MemberGetDto memberGetDto = _mapper.Map<MemberGetDto>(member);

            return memberGetDto;
        }

        public async Task UpdateAsync([FromForm] MemberUpdateDto memberUpdateDto)
        {
            var member = await _memberRepository.GetByIdAsync(x => x.Id == memberUpdateDto.Id, "MemberProfessions");
            if (member == null) throw new InvalidNotFoundException();

            member.MemberProfessions.RemoveAll(x => !memberUpdateDto.ProfessionIds.Contains(x.ProfessionId));

            foreach (var professionId in memberUpdateDto.ProfessionIds.Where(profId => !member.MemberProfessions.Any(mp => mp.ProfessionId == profId)))
            {
                MemberProfession memberProfession = new MemberProfession()
                {
                    Member=member,
                    ProfessionId = professionId
                };
                await _memberProfessionRepository.CreateAsync(memberProfession);
            }


            if (memberUpdateDto.ImageFile != null)
            {

                if (memberUpdateDto.ImageFile.ContentType != "image/jpeg" && memberUpdateDto.ImageFile.ContentType != "image/png")
                {
                    throw new InvalidImageFileException("ImageFile", "ImageFile ContentType must be jpeg or png");
                }

                if (memberUpdateDto.ImageFile.Length > 2097152)
                {
                    throw new InvalidImageFileException("ImageFile", "ImageFile File size must be lower than 2mb");
                }
                string deletepath = Path.Combine(_env.WebRootPath, "uploads/members", member.ImageUrl);
                if (System.IO.File.Exists(deletepath))
                {
                    System.IO.File.Delete(deletepath);
                }
                member.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/members", memberUpdateDto.ImageFile);
            }
            else
            {
                throw new InvalidImageFileException("ImageFile", "ImageFile Required!");

            }
            

            member = _mapper.Map(memberUpdateDto, member);
            //member.UpdatedDate = DateTime.UtcNow.AddHours(4);

            await _memberRepository.CommitAsync();
        }
    }
}

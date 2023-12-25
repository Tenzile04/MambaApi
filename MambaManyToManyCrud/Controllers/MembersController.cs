using AutoMapper;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.DTOs.MemberDtos;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;
using MambaManyToManyCrud.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MambaManyToManyCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MembersController(AppDbContext context, IMapper mapper, IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var member = _context.Members.ToList();
            IEnumerable<MemberGetDto> memberGetDtos = new List<MemberGetDto>();
            memberGetDtos = member.Select(x => new MemberGetDto
            {
                FullName = x.FullName

            });

            return Ok(memberGetDtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            if (id == null && id <= 0) return NotFound();

            var member = _context.Members.FirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound();

            MemberGetDto memberGetDto = _mapper.Map<MemberGetDto>(member);

            return Ok(memberGetDto);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Create([FromForm] MemberCreateDto memberCreateDto)
        {
            var member = _mapper.Map<Member>(memberCreateDto);
            member.CreatedDate = DateTime.UtcNow.AddHours(4);
            member.UpdatedDate = DateTime.UtcNow.AddHours(4);
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
                return NotFound();

            }


            if (memberCreateDto.ImageFile != null)
            {

                if (memberCreateDto.ImageFile.ContentType != "image/jpeg" && memberCreateDto.ImageFile.ContentType != "image/png")
                {
                    return BadRequest(new { message = "ImageFile ContentType must be jpeg or png" });
                }

                if (memberCreateDto.ImageFile.Length > 2097152)
                {
                    return BadRequest(new { message = "ImageFile File size must be lower than 2mb" });
                }

                member.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/members", memberCreateDto.ImageFile);
            }
            else
            {
                return BadRequest(new { message = "ImageFile Required!" });

            }
            _context.Members.Add(member);
            _context.SaveChanges();
            return StatusCode(201, new { message = "Member Created" });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromForm] MemberUpdateDto memberUpdateDto, int id)
        {

            var member = _context.Members.Include(member => member.MemberProfessions).FirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound();

            member.MemberProfessions.RemoveAll(x => !memberUpdateDto.ProfessionIds.Contains(x.ProfessionId));

            foreach (var professionId in memberUpdateDto.ProfessionIds.Where(profId => !member.MemberProfessions.Any(mp => mp.ProfessionId == profId)))
            {
                MemberProfession memberProfession = new MemberProfession()
                {
                    ProfessionId = professionId
                };
                member.MemberProfessions.Add(memberProfession);
            }


            if (memberUpdateDto.ImageFile != null)
            {

                if (memberUpdateDto.ImageFile.ContentType != "image/jpeg" && memberUpdateDto.ImageFile.ContentType != "image/png")
                {
                    return BadRequest(new { message = "ImageFile ContentType must be jpeg or png" });
                }

                if (memberUpdateDto.ImageFile.Length > 2097152)
                {

                    return BadRequest(new { message = "ImageFile File size must be lower than 2mb" });
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
                return BadRequest(new { message = "ImageFile Required!" });

            }

            member = _mapper.Map(memberUpdateDto, member);
            member.UpdatedDate = DateTime.UtcNow.AddHours(4);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.FirstOrDefault(x => x.Id == id);
            if (member == null) return NotFound();
            member.IsDeleted = !member.IsDeleted;

            member.UpdatedDate = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();
            return StatusCode(204);
        }
    }

}

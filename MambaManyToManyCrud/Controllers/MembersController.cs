using AutoMapper;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.DTOs.MemberDtos;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;
using MambaManyToManyCrud.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("")]
        public IActionResult Create([FromForm] MemberCreateDto memberCreateDto)
        {
            var member = _mapper.Map<Member>(memberCreateDto);
            member.CreatedDate = DateTime.UtcNow.AddHours(4);
            member.UpdatedDate = DateTime.UtcNow.AddHours(4);

            
         

            if (memberCreateDto.ImageFile != null)
            {

                string fileName = memberCreateDto.ImageFile.FileName;
                if (memberCreateDto.ImageFile.ContentType != "image/jpeg" && memberCreateDto.ImageFile.ContentType != "image/png")
                {
                    throw new Exception();
                }

                if (memberCreateDto.ImageFile.Length > 2097152)
                {

                    throw new Exception();
                }


                member.ImageUrl = Helper.SaveFile(_env.WebRootPath, "uploads/members", memberCreateDto.ImageFile);
            }

            return StatusCode(201, new { message = "Member Created" });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromForm] MemberUpdateDto memberUpdateDto,int id)
        {
            return NoContent();
        }


        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var member = _context.Professions.FirstOrDefault(x => x.Id == id);
        //    if (member == null) return NotFound();
        //    //member.IsDeleted=!member.IsDeleted;

        //    _context.Members.Remove(member);
        //    _context.SaveChanges();
        //    return NoContent();
        }
    }
}

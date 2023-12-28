using AutoMapper;
using Mamba.Business.Exceptions;
using Mamba.Business.Services.Interfaces;
using MambaManyToManyCrud.DAL;
using MambaManyToManyCrud.DTOs.ProfessionDtos;
using MambaManyToManyCrud.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MambaManyToManyCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionService _professionService;
        public ProfessionsController(IProfessionService professionService)
        {
            _professionService = professionService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<ProfessionGetDto> professionGetDtos = await _professionService.GetAllAsync();

            return Ok(professionGetDtos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public async Task<IActionResult> Create([FromForm] ProfessionCreateDto professionCreateDto)
        {
            await _professionService.CreateAsync(professionCreateDto);
            return StatusCode(201, new { message = "Created Profession" });
        }

        [HttpGet("{id}")]

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
    
            var professionGetDto = await _professionService.GetByIdAsync(id);
            return Ok(professionGetDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(ProfesionUpdateDto profesionUpdateDto)
        {
            await _professionService.UpdateAsync(profesionUpdateDto);
            return NoContent();

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async void Delete(int id)
        {
            if (id == null) throw new InvalidNotFoundException();

            await _professionService.Delete(id);

            

        }




        //private readonly IMapper _mapper;
        //private readonly AppDbContext _context;
        //public ProfessionsController(AppDbContext context, IMapper mapper)
        //{
        //    _context = context;
        //    _mapper = mapper;
        //}
        //[HttpGet("")]
        //public IActionResult GetAll()
        //{
        //    var profession = _context.Professions.ToList();
        //    IEnumerable<ProfessionGetDto> professionGetDtos = new List<ProfessionGetDto>();
        //    professionGetDtos = profession.Select(x => new ProfessionGetDto
        //    {
        //        Name = x.Name,

        //    });
        //    return Ok(professionGetDtos);

        //}
        //[HttpPost]
        //public IActionResult Create(ProfessionCreateDto dto)
        //{
        //    var profession = _mapper.Map<Profession>(dto);
        //    //profession.CreatedDate = DateTime.UtcNow.AddHours(4);
        //    //profession.UpdatedDate = DateTime.UtcNow.AddHours(4);
        //    _context.Professions.Add(profession);
        //    _context.SaveChanges();
        //    return StatusCode(201, new { message = "Created Profession" });
        //}
        //[HttpGet("{id}")]

        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Get(int id)
        //{
        //    var profession=_context.Professions.FirstOrDefault(x => x.Id == id);
        //    if(profession == null)return NotFound();

        //    var professionGetDto=_mapper.Map<ProfessionGetDto>(profession);
        //    return Ok(profession);
        //}
        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Update(int id,ProfesionUpdateDto dto)
        //{
        //    var profession = _context.Professions.FirstOrDefault(x => x.Id == id);
        //    if (profession == null) return NotFound();

        //    profession=_mapper.Map(dto, profession);
        //    //profession.UpdatedDate= DateTime.UtcNow.AddHours(4);
        //    _context.SaveChanges();
        //    return NoContent();

        //}
        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public IActionResult Delete(int id)
        //{
        //    var profession = _context.Professions.FirstOrDefault(x => x.Id == id);
        //    if (profession == null) return NotFound();
        //    profession.IsDeleted = !profession.IsDeleted;
        //    //_context.Professions.Remove(profession);
        //    //profession.UpdatedDate = DateTime.UtcNow.AddHours(4);
        //    _context.SaveChanges();
        //    return StatusCode(204);

        //}      


    }
}

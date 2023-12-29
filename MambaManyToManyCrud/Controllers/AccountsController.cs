using AutoMapper;
using Mamba.Business.DTOs.AppUserDtos;
using Mamba.Core.Entities;
using MambaManyToManyCrud.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MambaManyToManyCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public AccountsController(AppDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            AppUser user = null;
            user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user is not null)
            {
                return StatusCode(400, "Email already exist!");

            }
            AppUser appUser = _mapper.Map<AppUser>(registerDto);
            //AppUser appuser = new AppUser()
            //{
            //    FullName = registerDto.Fullname,
            //    Email = registerDto.Email,
            //    UserName = registerDto.UserName

            //};

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    return BadRequest(error.Description);
                }
            }

            return StatusCode(201, "Created");

        }
    }
}

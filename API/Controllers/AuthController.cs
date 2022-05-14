using API.ApplicationDbContext;
using Domain.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ITokenService _tokenService;
        public AuthController(AppDbContext context, ITokenService tokenService)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
            var user = _context.LoginModels.FirstOrDefault(u =>
                (u.UserName == loginModel.UserName) && (u.Password == loginModel.Password));
            if (user is null)
                return Unauthorized();
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, loginModel.UserName),
            new Claim(ClaimTypes.Role, "Manager")
        };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _context.SaveChanges();
            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel loginModel)
        {
            if (loginModel is null)
            {
                return BadRequest("Invalid client request");
            }
           
            if(loginModel.UserType == Domain.Enums.UserType.Student)
            {
                var student = await _context.Students.Where(x => x.Id == loginModel.UserDetails).FirstOrDefaultAsync();
                if(student == null)
                {
                    return NotFound();
                }
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.UserName),
                new Claim(ClaimTypes.Role, loginModel.UserType.ToString())
            };
            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();
            loginModel.RefreshToken = refreshToken;
            loginModel.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _context.LoginModels.Add(loginModel);
            _context.SaveChanges();
            return Ok(new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}

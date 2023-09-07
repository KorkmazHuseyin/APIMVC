using APIMVC.MyApis.DAL.AuthDAL.Interfaces;
using APIMVC.MyApis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APIMVC.MyApis.Controllers
{
    /// <summary>
    /// stateless nedir ?
    /// </summary>
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //DI
        IAuthDAL _authDAL;
        IConfiguration _conf;
        public AuthController(IAuthDAL authDAL,IConfiguration conf)
        {
            _authDAL = authDAL;
            _conf = conf;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (await _authDAL.UserExists(dto.UserName))
            {
                ModelState.AddModelError("not valid", "zaten varsın");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var kisi = await _authDAL.Register(new DAL.Entities.User() { UserName = dto.UserName }, dto.Password);
            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var bulunanUser = await _authDAL.Login(dto.UserName, dto.Password);
            if (bulunanUser == null)
            {
                //return null;
                return BadRequest();
            }
            else
            {   ////////////  Token ı kişiye özel hale getirmeya çalışıyıoruz.  CreateToken metodunu istediği
                var desc = new SecurityTokenDescriptor()
                {
                    Expires = DateTime.Now.AddDays(1),                    
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier,bulunanUser.UserID.ToString()),
                        new Claim(ClaimTypes.Name,bulunanUser.UserName)
                        //new Claim("tel","05065656323"),
                    }),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_conf.GetSection("AppSettings:Token").Value)),SecurityAlgorithms.HmacSha512Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(desc);
                var donulecekTokenDegeri = tokenHandler.WriteToken(token);
                return Ok(donulecekTokenDegeri);

            }

        }

    }
}

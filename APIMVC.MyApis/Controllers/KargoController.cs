using APIMVC.MyApis.DAL.Context;
using APIMVC.MyApis.DAL.Entities;
using APIMVC.MyApis.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIMVC.MyApis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class KargoController : ControllerBase
    {
        MyContext _context;
        IMapper _mapper;
        public KargoController(MyContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            var hede = _context.Shippers.ToList();

            GeneralApiType<List<ShipperDTO>> donulecekDEger = new GeneralApiType<List<ShipperDTO>>();
            donulecekDEger.ApiStatusCode = 200;
            donulecekDEger.ReturnObject = hede.Select(a => new ShipperDTO()
            {
                CompanyName = a.CompanyName,
                ShipperID = a.ShipperID,
                Phone = a.Phone
            }).ToList();

            return Ok(donulecekDEger);

            return Ok(hede.Select(a=>new ShipperDTO() { 
            CompanyName=a.CompanyName,
            ShipperID=a.ShipperID,
            Phone=a.Phone
            }).ToList());
        }
        [HttpPost]
        [Route("~/api/addShip")]
        public IActionResult Post([FromBody]ShipperDTO dto)
        {
            Shippers donusturulenDTOBilgisi = _mapper.Map<Shippers>(dto);
            _context.Shippers.Add(donusturulenDTOBilgisi);
            _context.SaveChanges();
            return Ok("kayıt başarılı");
        }
    }
}

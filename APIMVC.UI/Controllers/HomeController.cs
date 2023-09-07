using APIMVC.UI.ApiServices;
using APIMVC.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace APIMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KargoApiService _service;
        private readonly TokenApiService _tokenservice;
        public HomeController(ILogger<HomeController> logger, KargoApiService service, TokenApiService tokenservice)
        {
            _logger = logger;
            _service = service;
            _tokenservice = tokenservice;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Index02()
        {
            var deger = _service.KargolariListele();
            return View();
        }




       // 1. Adım


        ///Kullanıcının kayıt olmask istediğnde kullanılacak metodlar.

        public IActionResult Index03KullaniciKayit()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index03KullaniciKayit(RegisterDTO dto)
        {
            // apiye balan register metodunu çalıştır.
            var deger = await _tokenservice.KullaniciKaydet(dto);
            return View();
        }












        //2. Adım


        // Kullanıcı bu metodlarlar login olucak. Login olmak istediğinde çalışcak metodlar bunlar


        public IActionResult IndeLogin()
        {
            return View( new LoginDTO());
        }
        [HttpPost]
        public async Task<IActionResult> IndeLogin(LoginDTO dto)
        {
            // tokenal methodunu çalıştır.

            string uretilenTokenDegeri = await _tokenservice.TokenAl(dto.UserName, dto.Password);
            //cookie de tut.
            return View();
        }










        //~/api/addShip
        public IActionResult Index03Post()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index03Post([Bind("CompanyName,Phone")] ShipperDTO dto)
        {
            if (ModelState.IsValid)
            {
                //apiye bu dtoyu gonder
                string apidenGelenEnSonCevap = await _service.ViewToAddShip(dto);
            }
            return View("Index03Post");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

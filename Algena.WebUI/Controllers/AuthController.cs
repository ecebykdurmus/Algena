using Algena.Business.Abstract;
using Algena.Entities.Dtos.LoginDtos;
using Algena.Entities.Dtos.SellerDtos;
using Microsoft.AspNetCore.Mvc;

namespace Algena.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Account(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            Microsoft.AspNetCore.Identity.SignInResult result = await _authService.LoginAsync(loginDto);
            if (result.Succeeded)
            {
                var user = await _authService.GetUserAsync(loginDto.UserName);
                var roles = await _authService.GetRolesAsync(user);
                switch (roles.Contains("seller"))
                {
                    case true : return RedirectToAction("Index", "Home");
;                              
                    case false : return RedirectToAction("Index", "Product");                   
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(SellerAddDto sellerAddDto)
        {
            bool response = (await _authService.SellerRegisterAsync(sellerAddDto)).Succeeded;
            return RedirectToAction("Account", new {message = response ? "Kayıt işlemi başarılı." : "Bir hata oluştu."});
        }
    }
}

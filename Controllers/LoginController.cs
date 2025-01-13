using Emprestimos.Dto;
using Emprestimos.Services.LoginServices;
using Emprestimos.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace Emprestimos.Controllers
{
    public class LoginController : Controller
    {
        private readonly iLoginInterface _loginInterface;
        private readonly iSessaoInterface _sessaoInterface;
        public LoginController(iLoginInterface loginInterface, iSessaoInterface sessaoInterface) 
        {
            _loginInterface = loginInterface;
            _sessaoInterface = sessaoInterface; 
        }
        public IActionResult Login()
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public IActionResult Logout()
        {
            _sessaoInterface.RemoverSessao();
            return RedirectToAction("Login");
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioRegisterBto usuarioRegisterBto)
        {
            if(ModelState.IsValid) 
            { 
                var usuario = await _loginInterface.RegistrarUsuario(usuarioRegisterBto);
                if (usuario.Status)
                {
                    TempData["MessagemSucesso"] = usuario.Mensagem;
                }
                else
                {
                    TempData["MessagemError"] = usuario.Mensagem;
                    return View(usuarioRegisterBto);
                }

                return RedirectToAction("Login");
            }
            else
            {
                return View(usuarioRegisterBto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(UsuarioLoginDto usuarioLoginDto)
        {
            if (ModelState.IsValid)
            {
               var usuario = await _loginInterface.Login(usuarioLoginDto);
                if (usuario.Status)
                {
                    TempData["MessagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Emprestimo");
                }else
                {
                    TempData["MessagemError"] = usuario.Mensagem;
                    return View(usuarioLoginDto);
                }
            }
            else
            {
                return View(usuarioLoginDto);
            }
        }
    }   
}

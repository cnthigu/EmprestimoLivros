using Emprestimos.Data;
using Emprestimos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Emprestimos.Controllers
{
    public class EmprestimoController : Controller
    {

        readonly private ApplicationDbContext _db;


        public EmprestimoController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {

            IEnumerable<EmprestimosModel> Emprestimos = _db.Emprestimos;


            return View(Emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);

            if(emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);

        }
        [HttpGet]
        public IActionResult Excluir(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            EmprestimosModel emprestimo = _db.Emprestimos.FirstOrDefault(x => x.Id == id);
            
            if(emprestimo == null)
            {
                return NotFound();
            }

            return View(emprestimo);
        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Update(emprestimo);
                _db.SaveChanges();
                TempData["MessagemSucesso"] = "Edição realizada com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(emprestimo);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.Emprestimos.Add(emprestimo);
                _db.SaveChanges();

                TempData["MessagemSucesso"] = "Emprestimo cadastrado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                return View(emprestimo);
            }
        }

        [HttpPost]
        public IActionResult Excluir(EmprestimosModel emprestimo)
        {
            if (emprestimo == null)
            {
                return NotFound();
            }

            _db.Emprestimos.Remove(emprestimo);
            _db.SaveChanges();
            TempData["MessagemSucesso"] = "Remoção realizada com sucesso!";
            return RedirectToAction("Index");

        }
    }
}

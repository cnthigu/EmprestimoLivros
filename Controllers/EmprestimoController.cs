using System.Data;
using ClosedXML.Excel;
using Emprestimos.Data;
using Emprestimos.Models;
using Emprestimos.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;

namespace Emprestimos.Controllers
{
    public class EmprestimoController : Controller
    {

        readonly private ApplicationDbContext _db;
        readonly private iSessaoInterface _sessaoInterface;    


        public EmprestimoController(ApplicationDbContext db, iSessaoInterface sessaoInterface)
        {
            _db = db;
            _sessaoInterface = sessaoInterface;
        }


        public IActionResult Index()
        {

            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }


            IEnumerable<EmprestimosModel> Emprestimos = _db.Emprestimos;
            return View(Emprestimos);
        }
        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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

            var usuario = _sessaoInterface.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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

        public IActionResult Exportar()
        {
            var dados = GetDados();

            using(XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados Emprestimos");

                using(MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(),"application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }

            return Ok();
        }

        private DataTable GetDados()
        {
            DataTable dataTable = new DataTable();

            dataTable.TableName = "Dados Emprestimo";
            dataTable.Columns.Add("Recebedor", typeof(string));
            dataTable.Columns.Add("Fornecedor", typeof(string));
            dataTable.Columns.Add("Livro", typeof(string));
            dataTable.Columns.Add("Datá emprestimo", typeof(DateTime));

            var dados = _db.Emprestimos.ToList();

            if (dados.Count > 0)
            {
                dados.ForEach(emprestimo =>
                {
                    dataTable.Rows.Add(emprestimo.Recebedor, emprestimo.Fornecedor, emprestimo.LivroEmprestimo, emprestimo.DataDevolucao);
                });               
            }

            return dataTable;

        }

        [HttpPost]
        public IActionResult Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoDB = _db.Emprestimos.Find(emprestimo.Id);

                emprestimoDB.Fornecedor = emprestimo.Fornecedor;
                emprestimoDB.Recebedor = emprestimoDB.Recebedor;
                emprestimoDB.LivroEmprestimo = emprestimoDB.LivroEmprestimo;

                _db.Emprestimos.Update(emprestimoDB);
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
                emprestimo.DataDevolucao =  DateTime.Now;

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

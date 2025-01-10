using System.ComponentModel.DataAnnotations;

namespace Emprestimos.Models
{
    public class EmprestimosModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "Digite o nome do R eceberdor.")]
        public string Recebedor { get; set; }
        [Required(ErrorMessage = "Digite o nome Fornecedor.")]
        public string Fornecedor { get; set; }  
        [Required(ErrorMessage = "Digite o nome do Livro.")]
        public string LivroEmprestimo { get; set; }
        public DateTime DataDevolucao { get; set; } = DateTime.Now;
    }
}





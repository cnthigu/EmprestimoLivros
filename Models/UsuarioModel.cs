using System.Security.Cryptography.X509Certificates;

namespace Emprestimos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Name { get; set; }    
        public string Sobrenome { get; set; }
        public string Email { get; set; }

        public byte[] SenhaHash { get; set; }
        public byte[] SenhaSalt { get; set; }

    }
}

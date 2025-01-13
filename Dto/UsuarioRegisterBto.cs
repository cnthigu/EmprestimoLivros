using System.ComponentModel.DataAnnotations;

namespace Emprestimos.Dto
{
    public class UsuarioRegisterBto
    {

        [Required(ErrorMessage = "Digite o Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Digite o SobreNome")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a Senha")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Confirme a Confirma de Senha"),
            Compare("Senha", ErrorMessage = "As senhas não estão iguais.")]

        public string ConfirmaSenha { get; set; }


    }
}
    
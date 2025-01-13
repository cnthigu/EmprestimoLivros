using Emprestimos.Dto;
using Emprestimos.Models;

namespace Emprestimos.Services.LoginServices
{
    public interface iLoginInterface 
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterBto usuarioRegisterBto);
        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);

    }
}

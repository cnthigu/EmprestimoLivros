using Emprestimos.Models;

namespace Emprestimos.Services.SessaoService
{
    public interface iSessaoInterface
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoverSessao();
    }
}

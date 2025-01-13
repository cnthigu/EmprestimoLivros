namespace Emprestimos.Services.SenhaServices
{
    public interface iSenhaInterface
    {
        void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt);
    }
}

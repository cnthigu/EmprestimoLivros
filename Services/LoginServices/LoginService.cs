using Azure;
using Emprestimos.Data;
using Emprestimos.Dto;
using Emprestimos.Models;
using Emprestimos.Services.SenhaServices;
using Emprestimos.Services.SessaoService;

namespace Emprestimos.Services.LoginServices
{
    public class LoginService : iLoginInterface
    {
        private readonly ApplicationDbContext _context;
        private readonly iSenhaInterface _senhaInterface;
        private readonly iSessaoInterface _sessaoInterface;
        public LoginService(ApplicationDbContext context, iSenhaInterface senhaInterface, iSessaoInterface sessaoInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
            _sessaoInterface = sessaoInterface;
                
        }

        public async Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto)
        {
            ResponseModel<UsuarioModel>response = new ResponseModel< UsuarioModel>();

            try
            {

                var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioLoginDto.Email);
                if (usuario == null)
                {
                    response.Mensagem = "Credenciais invalidas";
                    response.Status = false;
                    return response;
                }

                if(!_senhaInterface.VerificaSenha(usuarioLoginDto.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Mensagem = "Credenciais invalidas";
                    response.Status = false;
                    return response;
                }
                ///criarsessao
                ///


                _sessaoInterface.CriarSessao(usuario);
                response.Mensagem = "Uusuario LOGADO";
                return response;




            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterBto usuarioRegisterBto)
        {

            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                if (VerificarSeEmailExiste(usuarioRegisterBto))
                {
                    response.Mensagem = "Email já cadastrado";
                    response.Status = false;
                    return response;
                }

                _senhaInterface.CriarSenhaHash(usuarioRegisterBto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel()
                {
                    Name = usuarioRegisterBto.Name,
                    Sobrenome = usuarioRegisterBto.Sobrenome,
                    Email = usuarioRegisterBto.Email,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt

                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuario Cadastrado.";
                    return response;


            }
            catch (Exception ex)
            {
            
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        private bool VerificarSeEmailExiste(UsuarioRegisterBto usuarioRegisterBto)
        { 
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == usuarioRegisterBto.Email);

            if (usuario == null)
            {
                return false;
            }
            return true;
        }
    }


}

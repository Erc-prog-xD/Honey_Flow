using Microsoft.EntityFrameworkCore;
using BackendApi.Dto;
using BackendApi.Models;
using BackendApi.Services.SenhaService;
using BackendApi.Data;

namespace BackendApi.Services.AuthService
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _context;
        private readonly ISenhaInterface _senhaInterface;
        public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
        {
            _context = context;
            _senhaInterface = senhaInterface;
        }
        

        public async Task<Response<UserCriacaoDTO>> Registrar(UserCriacaoDTO userRegister)
        {
            Response<UserCriacaoDTO> response = new Response<UserCriacaoDTO>();
            try
            {
                if (!VerificaCpfJaExiste(userRegister))
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensage = "Cpf ou Email ja cadastrado!";
                    return response;
                }

                _senhaInterface.CriarSenhaHash(userRegister.Password, out byte[] senhaHash, out byte[] senhaSalt);
                User User = new User
                {
                    Nome = userRegister.Nome,
                    Cpf = userRegister.Cpf,
                    Celular = userRegister.Celular,
                    Email = userRegister.Email,
                    PasswordHash = senhaHash,
                    PasswordSalt = senhaSalt
                };
                _context.Add(User);
                await _context.SaveChangesAsync();

                response.Mensage = "Usuário Cadastrado!";
               
            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Status = false;
                response.Mensage = ex.Message;
            }
            return response;

        }

        public async Task<Response<string>> Login(UserLoginDTO userLogin)
        {
            Response<string> response = new Response<string> { Mensage = "" };

            try
                {
                    var User = await _context.User.FirstOrDefaultAsync(User => User.Email == userLogin.Email && User.DeletionDate == null);
                    if (User == null)
                    {
                        response.Status = false;
                        response.Mensage = "User não encontrado!";
                        return response;
                    }
                    if (!_senhaInterface.VerificaSenhaHash(userLogin.Password, User.PasswordHash, User.PasswordSalt))
                    {
                        response.Status = false;
                        response.Mensage = "Credenciais invalidas!";
                        return response;
                    }

                    var token = _senhaInterface.CriarToken(User);
                    response.Mensage = "Login realizado com sucesso!";
                    response.Dados = token;
                    response.Status = true;

                }
            catch(Exception ex)
                {
                    response.Dados = null;
                    response.Status = false;
                    response.Mensage = ex.Message;
                }

            return response;
        }


          public bool VerificaCpfJaExiste(UserCriacaoDTO userRegister)
        {
            var user = _context.User.FirstOrDefault(User => User.Cpf == userRegister.Cpf || User.Email == userRegister.Email);
            return user == null ? true : false;
        }
    }
}

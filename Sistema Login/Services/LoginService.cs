using FluentResults;
using Microsoft.AspNetCore.Identity;
using Sistema_Login.Data.Requests;
using Sistema_Login.Models;
using System;
using System.Linq;

namespace Sistema_Login.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;
        private TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var resultadoIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(usuario => usuario.NormalizedUserName == request.Username.ToUpper());
                Token token = _tokenService.CreateToken(identityUser);
                return Result.Ok().WithSuccess(token.Value);
            }
            else
            {
                return Result.Fail("Login falhou!");
            }
        }

        public Result SolicitaResetSenhaUsuario(SolicitaResetRequest request)
        {
            IdentityUser<int> identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => usuario.NormalizedEmail == request.Email.ToUpper());
            if(identityUser != null )
            {
                string codigoDeRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;
                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }
            else
            {
                return Result.Fail("Falha ao solicitar redefinição");
            }

        }

        public Result ResetSenhaUsuario(EfetuaResetRequest request)
        {
            IdentityUser<int> identityUser = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(usuario => usuario.NormalizedEmail == request.Email.ToUpper());
            IdentityResult resultadoIdentity = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;
            if(resultadoIdentity.Succeeded)
            {
                return Result.Ok().WithSuccess("Senha redefinida com sucesso!");
            }
            else
            {
                return Result.Fail("Houve um erro na operação");
            }
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Sistema_Login.Data.Dtos;
using Sistema_Login.Models;

namespace Sistema_Login.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
        }
    }
}

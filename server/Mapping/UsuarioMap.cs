using AutoMapper;
using SmartAuth.Entities;
using SmartAuth.Models;

namespace SmartAuth.Mapping
{
    public class UsuarioMap : Profile
    {
        public UsuarioMap()
        {
            CreateMap<Usuario, UsuarioModel>();
            CreateMap<UsuarioModel, Usuario>();
        }
    }
}

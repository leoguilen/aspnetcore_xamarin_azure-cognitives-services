using SmartAuth.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAuth.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioModel>> GetUsuariosAsync();
        Task<UsuarioModel> GetUsuarioAsync(Guid id);
        Task<bool> AddUsuarioAsync(UsuarioModel usuario);
        Task<bool> UpdateUsuarioAsync(UsuarioModel usuario);
        Task<bool> DeleteUsuarioAsync(Guid id);
    }
}

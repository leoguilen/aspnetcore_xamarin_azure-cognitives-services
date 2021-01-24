using Refit;
using SmartAuth.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAuth.Services
{
    public interface ISmartAuthApi
    {
        [Get("/users")]
        Task<ApiResponse<List<Usuario>>> GetUsuariosAsync();

        [Get("/users/{id}")]
        Task<ApiResponse<Usuario>> GetUsuarioAsync(Guid id);

        [Post("/users")]
        Task AddUsuariosAsync([Body] Usuario usuario);

        [Put("/users")]
        Task UpdateUsuariosAsync([Body] Usuario usuario);

        [Delete("/{id}")]
        Task DeleteUsuariosAsync(Guid id);

        [Multipart]
        [Post("/documents")]
        Task<ApiResponse<Usuario>> ExtractUsuarioInfo([AliasAs("file")] StreamPart stream);
    }
}

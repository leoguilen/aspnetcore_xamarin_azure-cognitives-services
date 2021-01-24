using Microsoft.AspNetCore.Http;
using SmartAuth.Models;
using System.Threading.Tasks;

namespace SmartAuth.Interfaces
{
    public interface IComputerVisionService
    {
        Task<UsuarioModel> ExtractUsuarioInfoByDocumentImage(IFormFile docFile);
    }
}

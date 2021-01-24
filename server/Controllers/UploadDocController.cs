using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartAuth.Interfaces;
using System.Threading.Tasks;

namespace SmartAuth.Controllers
{
    /// <summary>
    /// Controller responsavel por receber upload de documentos dos usuários e extrair os dados
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class UploadDocController : ControllerBase
    {
        /// <summary>
        /// Processar documento importado
        /// </summary>
        /// <response code="200">Documento processado e dados extraidos com sucesso</response>
        [HttpPost("api/v1.0/documents")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file,
            [FromServices] IComputerVisionService computerVisionService)
        {
            if (file is null)
                return BadRequest(new { Error = "Documento não selecionado" });

            var usuario = await computerVisionService
                .ExtractUsuarioInfoByDocumentImage(file);

            return Ok(usuario);
        }
    }
}

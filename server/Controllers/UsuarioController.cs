using Microsoft.AspNetCore.Mvc;
using SmartAuth.Interfaces;
using SmartAuth.Models;
using System;
using System.Threading.Tasks;

namespace SmartAuth.Controllers
{
    /// <summary>
    /// Controller responsavel pela gestão dos usuários
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService ??
                throw new ArgumentNullException(nameof(usuarioService));
        }

        /// <summary>
        /// Obtem todos usuários cadastrados
        /// </summary>
        /// <response code="200">Retornado lista com todos usuários</response>
        [HttpGet("api/v1.0/users")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService
                .GetUsuariosAsync();

            return Ok(usuarios);
        }

        /// <summary>
        /// Busca usuário cadastrado pelo id
        /// </summary>
        /// <response code="200">Busca feita com sucesso</response>
        /// <response code="404">Nenhum usuário encontrado</response>
        [HttpGet("api/v1.0/users/{id:Guid}")]
        public async Task<IActionResult> GetUsuario([FromRoute] Guid id)
        {
            var usuario = await _usuarioService
                .GetUsuarioAsync(id);

            if (usuario is null)
                return NotFound();

            return Ok(usuario);
        }

        /// <summary>
        /// Adicionar usuário ao sistema
        /// </summary>
        /// <response code="200">Usuário adicionado com sucesso</response>
        [HttpPost("api/v1.0/users")]
        public async Task<IActionResult> AddUsuarios([FromBody] UsuarioModel usuarioModel)
        {
            var result = await _usuarioService
                .AddUsuarioAsync(usuarioModel);

            if (result is false)
                return BadRequest(new { error = "Ação de adicionar usuário não foi concluida com sucesso" });

            return Ok();
        }

        /// <summary>
        /// Atualizar usuário existente
        /// </summary>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="404">Nenhum usuário encontrado</response>
        [HttpPut("api/v1.0/users")]
        public async Task<IActionResult> UpdateUsuario([FromBody] UsuarioModel usuarioModel)
        {
            var result = await _usuarioService
                .UpdateUsuarioAsync(usuarioModel);

            if (result is false)
                return NotFound();

            return Ok();
        }

        /// <summary>
        /// Deletar usuário existente
        /// </summary>
        /// <response code="204">Usuário deletado com sucesso</response>
        /// <response code="404">Nenhum usuário encontrado</response>
        [HttpDelete("api/v1.0/users/{id:Guid}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
        {
            var result = await _usuarioService
                .DeleteUsuarioAsync(id);
                
            if (result is false)
                return NotFound();

            return NoContent();
        }
    }
}

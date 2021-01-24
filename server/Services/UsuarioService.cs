using AutoMapper;
using Microsoft.Extensions.Logging;
using SmartAuth.Entities;
using SmartAuth.Interfaces;
using SmartAuth.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartAuth.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IRepository<Usuario> _repository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IMapper _mapper;

        public UsuarioService(IRepository<Usuario> repository,
            IMapper mapper, ILogger<UsuarioService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<bool> AddUsuarioAsync(UsuarioModel usuario)
        {
            try
            {
                var newUsuario = _mapper.Map<Usuario>(usuario);
                newUsuario.Id = Guid.NewGuid();
                newUsuario.DataCadastro = DateTime.Now;

                var inserted = await _repository.AddAsync(newUsuario);
                
                _logger.LogInformation("Novo usuário (Id: {0}) inserido com sucesso no banco de dados", newUsuario.Id);

                return inserted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteUsuarioAsync(Guid id)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id);

                if (deleted is false)
                {
                    _logger.LogWarning("Não foi possível deletar usuário (id: {0})", id);
                    return false;
                }

                _logger.LogInformation("Usuario (id: {0}) foi deletado com sucesso do banco de dados", id);
                return deleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<UsuarioModel> GetUsuarioAsync(Guid id)
        {
            try
            {
                var usuario = await _repository.GetByIdAsync(id);

                if (usuario is null)
                {
                    _logger.LogWarning("Nenhum usuário com id {0} foi encontrado ao executar ação de busca", id);
                    return null;
                }

                _logger.LogInformation("Usuario (id: {0}) retornado com sucesso", id);
                return _mapper.Map<UsuarioModel>(usuario);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<List<UsuarioModel>> GetUsuariosAsync()
        {
            try
            {
                var usuarios = await _repository.ListAsync();

                _logger.LogInformation("Retornados {0} usuário registrados no banco de dados", usuarios.Count);
                return _mapper.Map<List<UsuarioModel>>(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateUsuarioAsync(UsuarioModel usuarioModel)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(usuarioModel);

                var updated = await _repository.UpdateAsync(usuario);

                if (!updated)
                {
                    _logger.LogInformation("Ação de atualizar usuário (id: {0}) não foi executada com sucesso", usuario.Id);
                    return false;
                }

                _logger.LogInformation("Usuário (id: {0}) atualizado com sucesso", usuario.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}

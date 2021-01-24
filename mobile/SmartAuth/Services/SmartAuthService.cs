using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using SmartAuth.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartAuth.Services
{
    public class SmartAuthService
    {
        private readonly ISmartAuthApi _api;

        public SmartAuthService()
        {
            _api = RestService.For<ISmartAuthApi>(GetBaseUrl());
        }

        private string GetBaseUrl()
        {
            var configInStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("SmartAuth.appsettings.json");

            using (var sr = new StreamReader(configInStream))
            {
                var jsonReader = new JsonTextReader(sr);
                var jsonConfig = JToken.ReadFrom(jsonReader);

                var baseUrl = (string)jsonConfig?
                    .SelectToken("ApiConfig")?
                    .SelectToken("BaseUrl");

                return baseUrl;
            }
        }

        public async Task<ApiResponse<List<Usuario>>> GetUsuariosAsync()
        {
            return await _api.GetUsuariosAsync();
        }

        public async Task<ApiResponse<Usuario>> GetUsuarioAsync(Guid id)
        {
            return await _api.GetUsuarioAsync(id);
        }

        public async Task AddUsuariosAsync(Usuario usuario)
        {
            await _api.AddUsuariosAsync(usuario);
        }

        public async Task UpdateUsuariosAsync(Usuario usuario)
        {
            await _api.UpdateUsuariosAsync(usuario);
        }

        public async Task DeleteUsuariosAsync(Guid id)
        {
            await _api.DeleteUsuariosAsync(id);
        }

        public async Task<ApiResponse<Usuario>> ExtractUsuarioInfo(Stream doc, string filename)
        {
            var file = new StreamPart(doc, filename);

            return await _api.ExtractUsuarioInfo(file);
        }
    }
}

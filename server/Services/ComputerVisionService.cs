using Microsoft.AspNetCore.Http;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartAuth.Configurations;
using SmartAuth.Extensions;
using SmartAuth.Interfaces;
using SmartAuth.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartAuth.Services
{
    public class ComputerVisionService : IComputerVisionService
    {
        private readonly ILogger<ComputerVisionService> _logger;
        private readonly IComputerVisionClient _computerVisionlClient;

        public ComputerVisionService(IOptions<ComputerVisionConfiguration> computerVisionConfig, ILogger<ComputerVisionService> logger)
        {
            if (computerVisionConfig is null)
                throw new ArgumentNullException(nameof(computerVisionConfig));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _logger.LogWarning("Inicializando cliente do computer vision");
            _computerVisionlClient = new ComputerVisionClient(new ApiKeyServiceClientCredentials(computerVisionConfig.Value.ApiKey))
            {
                Endpoint = computerVisionConfig.Value.Endpoint
            };
        }

        public async Task<UsuarioModel> ExtractUsuarioInfoByDocumentImage(IFormFile docFile)
        {
            try
            {
                using var ms = docFile.OpenReadStream();

                var textHeaders = await _computerVisionlClient
                    .ReadInStreamAsync(ms, language: "pt");

                var operationLocation = textHeaders.OperationLocation;
                string operationId = operationLocation.Substring(operationLocation.Length - 36);

                ReadOperationResult results;
                do
                {
                    results = await _computerVisionlClient.GetReadResultAsync(Guid.Parse(operationId));
                }
                while ((results.Status == OperationStatusCodes.Running ||
                        results.Status == OperationStatusCodes.NotStarted));

                var content = new StringBuilder(string.Empty);

                foreach (var page in results.AnalyzeResult.ReadResults)
                {
                    foreach (var line in page.Lines)
                    {
                        content.AppendLine(line.Text + "&");
                    }
                }

                _logger.LogInformation("Extração feita com sucesso. Texto extraido: {0}", content.ToString());

                var nomeParts = content.ToString().ExtractNome().Split(" ");
                var cpf = content.ToString().ExtractCpf();
                var rg = content.ToString().ExtractRg();
                var dates = content.ToString().ExtractDates();

                if (string.IsNullOrEmpty(cpf)
                    && string.IsNullOrEmpty(rg))
                {
                    _logger.LogInformation("Documento não foi identificado como uma CNH", content.ToString());
                    return null;
                }

                return new UsuarioModel
                {
                    Nome = nomeParts.First(),
                    Sobrenome = nomeParts.Last(),
                    DataNascimento = DateTime.Parse(dates[0]),
                    Rg = rg,
                    Cpf = cpf,
                };
            }
            catch (ComputerVisionErrorException ex)
            {
                _logger.LogError(ex, "Message: {0} | Body: {1}", ex.Message, JsonSerializer.Serialize(ex.Body));
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
            finally
            {
                _computerVisionlClient.Dispose();
            }
        }
    }
}

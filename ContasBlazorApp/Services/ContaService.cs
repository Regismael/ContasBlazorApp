using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ContasBlazorApp.ViewModels;

namespace ContasBlazorApp.Services
{
    public class ContaService
    {
        private readonly HttpClient _httpClient;

        public ContaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PagarContasViewModel.Conta> GetContasByIdAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Contas/porid/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var conta = await response.Content.ReadFromJsonAsync<PagarContasViewModel.Conta>();
                    return conta;
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar contas por ID: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Conta>> GetContasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/Contas/vencimento?dataInicio={dataInicio:yyyy-MM-dd}&dataFim={dataFim:yyyy-MM-dd}");

                if (response.IsSuccessStatusCode)
                {
                    var contas = await response.Content.ReadFromJsonAsync<List<Conta>>();
                    return contas ?? new List<Conta>();
                }
                else
                {
                    throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar contas por período: {ex.Message}");
                throw;
            }
        }

        public async Task PagarContaAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"/api/Contas/pagar/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Erro na requisição: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao pagar conta: {ex.Message}");
                throw;
            }
        }

        public class Conta
        {
            public Guid Id { get; set; }
            public string Descricao { get; set; }
            public decimal Valor { get; set; }
            public DateTime DataVencimento { get; set; }
        }
    }
}

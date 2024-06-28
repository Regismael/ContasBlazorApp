using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContasBlazorApp.Services;

namespace ContasBlazorApp.ViewModels
{
    public class VisualizacaoContasViewModel
    {
        private readonly ContaService _contaService;
        public List<ContaService.Conta> Contas { get; private set; } = new List<ContaService.Conta>();

        public VisualizacaoContasViewModel(ContaService contaService)
        {
            _contaService = contaService;
        }

        public async Task LoadContasPorPeriodoAsync(DateTime dataInicio, DateTime dataFim)
        {
            Contas = await _contaService.GetContasPorPeriodoAsync(dataInicio, dataFim);
        }
    }
}

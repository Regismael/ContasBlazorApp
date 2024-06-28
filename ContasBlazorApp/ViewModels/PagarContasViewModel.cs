using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContasBlazorApp.Services;

namespace ContasBlazorApp.ViewModels
{
    public class PagarContasViewModel
    {
        private readonly ContaService _contaService;

        public List<Conta> Contas { get; private set; }
        public string MensagemSucesso { get; private set; }

        public PagarContasViewModel(ContaService contaService)
        {
            _contaService = contaService;
        }

        public async Task LoadContasByIdAsync(Guid id)
        {
            var conta = await _contaService.GetContasByIdAsync(id);
            Contas = new List<Conta> { conta };
        }

        public async Task PagarContaAsync(Guid id)
        {
            await _contaService.PagarContaAsync(id);
            Contas.RemoveAll(c => c.Id == id);
            MensagemSucesso = "CONTA PAGA COM SUCESSO!";
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

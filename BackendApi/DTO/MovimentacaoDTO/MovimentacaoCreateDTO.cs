using BackendApi.Enums;

namespace BackendApi.Dto.MovimentacaoDTO
{
    public class MovimentacaoCreateDTO
    {   
        public TipoMovimentoMelEnum Tipo { get; set; }
        public decimal QuantidadeKg { get; set; }
        public double Valor { get; set; }
        public DateOnly Data { get; set; }
        public string? Observacao { get; set; }
    }
}

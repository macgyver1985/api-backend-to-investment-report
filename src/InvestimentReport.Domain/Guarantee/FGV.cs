using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Guarantee
{

    public class FGV : IGuarantee
    {

        public string Name => "Fundo Garantidor de Crédito";

        public string Initials => "FGV";

        public double Value => 250000D;

    }

}
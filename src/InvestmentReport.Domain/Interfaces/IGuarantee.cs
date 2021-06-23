namespace InvestmentReport.Domain.Interfaces
{

    public interface IGuarantee
    {

        string Name { get; }

        string Initials { get; }

        double Value { get; }

    }

}
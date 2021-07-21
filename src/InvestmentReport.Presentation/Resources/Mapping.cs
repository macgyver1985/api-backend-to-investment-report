using System.Linq;
using AutoMapper;
using InvestmentReport.Domain.Investments;
using InvestmentReport.Presentation.ViewModels;

namespace InvestmentReport.Presentation.Resources
{

    public static class Mapping
    {

        public static IMapper Mapper { get; private set; }

        static Mapping()
        {
            var mapperConfiguration = new AutoMapper.MapperConfiguration(config =>
            {
                config.CreateMap<Investment, InvestmentModel>()
                    .ForMember(dest => dest.IrTax, src => src.MapFrom(opt => opt.Taxes.FirstOrDefault(tax => tax.Name == "IR").Value));
            });

            Mapper = mapperConfiguration.CreateMapper();
        }

    }

}
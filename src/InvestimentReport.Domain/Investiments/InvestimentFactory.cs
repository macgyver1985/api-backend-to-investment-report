using System;
using System.Collections.Generic;
using System.Linq;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Investiments
{

    public static class InvestimentFactory
    {

        private static readonly List<Tuple<ETypeInvestiment, Type>> listFactoryByType;
        private static readonly List<Tuple<ETypeInvestiment, ITypeInvestimentFactory>> factories;

        static InvestimentFactory()
        {
            listFactoryByType = new List<Tuple<ETypeInvestiment, Type>>();
            factories = new List<Tuple<ETypeInvestiment, ITypeInvestimentFactory>>();

            listFactoryByType.Add(
                new Tuple<ETypeInvestiment, Type>(
                    ETypeInvestiment.DirectTreasure, typeof(DirectTreasureFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<ETypeInvestiment, Type>(
                    ETypeInvestiment.FixedIncome, typeof(FixedIncomeFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<ETypeInvestiment, Type>(
                    ETypeInvestiment.Funds, typeof(FundsFactory)
                )
            );
        }

        public static Investiment CreateInvestiment(ETypeInvestiment typeInvestiment, InvestimentData data)
        {
            ITypeInvestimentFactory factory = factories.FirstOrDefault(t => t.Item1 == typeInvestiment)?.Item2;

            if (factory == null)
            {
                factory = Activator
                    .CreateInstance(
                        listFactoryByType.FirstOrDefault(t => t.Item1 == typeInvestiment).Item2
                    ) as ITypeInvestimentFactory;

                factories.Add(
                    new Tuple<ETypeInvestiment, ITypeInvestimentFactory>(
                        typeInvestiment,
                        factory
                    )
                );
            }

            return factory.CreateInvestiment(data);
        }

    }

}
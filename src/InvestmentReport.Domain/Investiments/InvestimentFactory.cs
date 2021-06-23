using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Interfaces;

namespace InvestmentReport.Domain.Investments
{

    public static class InvestmentFactory
    {

        private static readonly List<Tuple<ETypeInvestment, Type>> listFactoryByType;
        private static readonly List<Tuple<ETypeInvestment, ITypeInvestmentFactory>> factories;

        static InvestmentFactory()
        {
            listFactoryByType = new List<Tuple<ETypeInvestment, Type>>();
            factories = new List<Tuple<ETypeInvestment, ITypeInvestmentFactory>>();

            listFactoryByType.Add(
                new Tuple<ETypeInvestment, Type>(
                    ETypeInvestment.DirectTreasure, typeof(DirectTreasureFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<ETypeInvestment, Type>(
                    ETypeInvestment.FixedIncome, typeof(FixedIncomeFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<ETypeInvestment, Type>(
                    ETypeInvestment.Funds, typeof(FundsFactory)
                )
            );
        }

        public static Investment CreateInvestment(ETypeInvestment typeInvestment, InvestmentData data)
        {
            ITypeInvestmentFactory factory = factories.FirstOrDefault(t => t.Item1 == typeInvestment)?.Item2;

            if (factory == null)
            {
                factory = Activator
                    .CreateInstance(
                        listFactoryByType.FirstOrDefault(t => t.Item1 == typeInvestment).Item2
                    ) as ITypeInvestmentFactory;

                factories.Add(
                    new Tuple<ETypeInvestment, ITypeInvestmentFactory>(
                        typeInvestment,
                        factory
                    )
                );
            }

            return factory.CreateInvestment(data);
        }

    }

}
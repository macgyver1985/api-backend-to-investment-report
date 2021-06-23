using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentReport.Application.DTOs;
using InvestmentReport.Infrastructure.Interfaces;
using Newtonsoft.Json.Linq;

namespace InvestmentReport.Infrastructure.Services
{

    internal static class ServiceResponseFactory
    {

        private static readonly List<Tuple<string, Type>> listFactoryByType;
        private static readonly List<Tuple<string, ITypeServiceResponseFactory>> factories;

        static ServiceResponseFactory()
        {
            listFactoryByType = new List<Tuple<string, Type>>();
            factories = new List<Tuple<string, ITypeServiceResponseFactory>>();

            listFactoryByType.Add(
                new Tuple<string, Type>(
                    nameof(DirectTreasureDTO), typeof(DirectTreasureResponseFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<string, Type>(
                    nameof(FixedIncomeDTO), typeof(FixedIncomeResponseFactory)
                )
            );
            listFactoryByType.Add(
                new Tuple<string, Type>(
                    nameof(FundsDTO), typeof(FundsResponseFactory)
                )
            );
        }

        public static List<T> ConvertList<T>(JObject json)
        {
            ITypeServiceResponseFactory factory = factories.FirstOrDefault(t => t.Item1 == typeof(T).Name)?.Item2;

            if (factory == null)
            {
                factory = Activator
                    .CreateInstance(
                        listFactoryByType.FirstOrDefault(t => t.Item1 == typeof(T).Name).Item2
                    ) as ITypeServiceResponseFactory;

                factories.Add(
                    new Tuple<string, ITypeServiceResponseFactory>(
                        typeof(T).Name,
                        factory
                    )
                );
            }

            return (List<T>)factory.ConvertList(json);
        }

    }

}
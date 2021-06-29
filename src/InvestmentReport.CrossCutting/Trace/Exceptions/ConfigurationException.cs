using System;

namespace InvestmentReport.CrossCutting.Trace.Exceptions
{

    /// <summary>
    /// Classe de exceção para as configurações do Logger.
    /// </summary>
    public sealed class ConfigurationException : Exception
    {

        /// <summary>
        /// Nome da seção que contém problema.
        /// </summary>
        public string Section { get; private set; }

        /// <summary>
        /// Atributo da seção que contém problema.
        /// </summary>
        public string Attribute { get; private set; }

        /// <summary>
        /// Construtor para relatar problema em toda seção.
        /// </summary>
        /// <param name="section">Nome da seção que contém problema.</param>
        public ConfigurationException(string section) :
            base($"Section {section} is not defined in configuration.")
        {
            this.Section = section;
        }

        /// <summary>
        /// Construtor para relatar problema em um atributo específico da seção.
        /// </summary>
        /// <param name="section">Nome da seção que contém problema.</param>
        /// <param name="attribute">Atributo da seção que contém problema.</param>
        public ConfigurationException(string section, string attribute) :
            base($"The {attribute} attribute is not defined in the {section} section.")
        {
            this.Section = section;
            this.Attribute = attribute;
        }

    }

}
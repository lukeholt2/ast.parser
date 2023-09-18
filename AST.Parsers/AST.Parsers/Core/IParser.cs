using System.IO;
using AST.Parsers.Models;

namespace AST.Parsers.Core
{
    /// <summary>
    /// List of supported languages supported by a parser implementation
    /// </summary>
    public enum SupportedLanguages
    {
        CSHARP,
        VISUALBASIC,
        JAVA,
        PHP,
        PYTHON,
        SQL
    };


    /// <summary>
    /// Interface defining the functionality required by each Parser implementation
    /// </summary>
    public interface IParser
    {
        /// <summary>
        /// Parse the raw source code
        /// </summary>
        /// <param name="source">
        /// The text of the source code to be parsed
        /// </param>
        /// <param name="parserOptions">
        /// Options defining some additonal parsing settings
        /// </param>
        /// <returns>
        /// A <see cref="Hierarchy"/>  representing the AST of the parsed result
        /// </returns>
        Hierarchy Parse(string source, ParserOptions? parserOptions = default);

        /// <summary>
        /// Parse the source code contained in the provided file
        /// </summary>
        /// <param name="source">
        /// The file containing the source code to be parsed
        /// </param>
        /// <param name="parserOptions">
        /// Options defining some additonal parsing settings
        /// </param>
        /// <returns>
        /// A <see cref="Hierarchy"/>  representing the AST of the parsed result
        /// </returns>
        Hierarchy Parse(Stream file, ParserOptions? parseOptions = default);
    }
}

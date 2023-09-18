using System;
using System.IO;
using AST.Parsers.Parsers;
using AST.Parsers.Models;

namespace AST.Parsers.Core
{
    /// <summary>
    /// Base class containing properties / methods required for each Parser
    /// </summary>
    public abstract partial class Parser
    {
        /// <summary>
        /// Create a new Parser implementation that supports the provided language
        /// </summary>
        /// <param name="language">
        /// The language to create the parser for
        /// </param>
        /// <returns>
        /// Newly constructed parser capbable of parsing source code of the given language
        /// </returns>
        public static IParser Create(SupportedLanguages language)
        {
            return language switch
            {
                SupportedLanguages.CSHARP => new CSharpParser(),
                SupportedLanguages.VISUALBASIC => new VBParser(),
                SupportedLanguages.JAVA => new JavaParser(),
                SupportedLanguages.PHP => new PhpParser(),
                SupportedLanguages.PYTHON => new PythonParser(),
                SupportedLanguages.SQL => new SQLParser(),
                _ => throw new NotSupportedException()
            };
        }
    }

    /// <summary>
    /// Base parser implementation
    /// </summary>
    /// <typeparam name="TNode">
    /// The 'node' that representing an AST node of the parsed code
    /// </typeparam>
    internal abstract partial class Parser<TNode> : IParser where TNode : class
    {
        /// <summary>
        /// The language supported by the current parser
        /// </summary>
        /// <value></value>
        internal SupportedLanguages SupportedLanguage { get; init; }

        /// <summary>
        /// Construct a new Parser of the given language
        /// </summary>
        /// <param name="language">
        /// The language to be used by the parser
        /// </param>
        protected Parser(string language) => SupportedLanguage = Enum.Parse<SupportedLanguages>(language, true);

        /// <summary>
        /// Construct a new Parser of the given language
        /// </summary>
        /// <param name="language">
        /// The language to be used by the parser
        /// </param>
        /// <typeparam name="SupportedLanguages"></typeparam>
        protected Parser(SupportedLanguages language) => SupportedLanguage = language;

        /// <summary>
        /// Perform the internal parsing operation to be defined by inherited parsers
        /// </summary>
        /// <param name="source">
        /// The source code to be parsed
        /// </param>
        /// <param name="parserOptions">Additional options defining how to parse the code</param>
        /// <returns></returns>
        protected abstract TNode ParseInternal(string source, ParserOptions? parserOptions = default);

        /// <summary>
        /// Parse the source code into a <see cref="Hierarchy"/>
        /// </summary>
        /// <param name="source">
        /// The source code to be parsed
        /// </param>
        /// <param name="parserOptions">
        /// Additional options defining how to parse the code
        /// </param>
        /// <returns>A Hierarchy representing the parsed AST</returns>
        public Hierarchy Parse(string source, ParserOptions? parserOptions = default) => HierarchyFactory.Create(ParseInternal(source, parserOptions));

        /// <summary>
        /// Parse the source code into a <see cref="Hierarchy"/>
        /// </summary>
        /// <param name="source">
        /// The source file to be parsed
        /// </param>
        /// <param name="parserOptions">
        /// Additional options defining how to parse the code
        /// </param>
        /// <returns>A Hierarchy representing the parsed AST</returns>
        public Hierarchy Parse(Stream file, ParserOptions? parserOptions = default)
        {
            using StreamReader reader = new(file);
            return Parse(reader.ReadToEnd(), parserOptions);
        }
        
    }

    public struct ParserOptions
    {
        // TODO:
    }
}

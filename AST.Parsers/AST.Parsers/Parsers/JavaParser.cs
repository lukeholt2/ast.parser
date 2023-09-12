using com.github.javaparser.ast;
using AST.Parsers.Core;
using JParser = com.github.javaparser.JavaParser;

namespace AST.Parsers.Parsers
{
    /// <summary>
    /// Parser implementation for Java source code
    /// </summary>
    internal class JavaParser : Parser<Node>
    {
        /// <summary>
        /// Construct a new <see cref="JavaParser"/>
        /// </summary>
        internal JavaParser() : base(SupportedLanguages.JAVA) { }

        protected override Node ParseInternal(string source, ParserOptions? parseOptions = default) => JParser.parse(source);
    }
}

using Microsoft.CodeAnalysis.CSharp;
using AST.Parsers.Core;

namespace AST.Parsers.Parsers
{
    /// <summary>
    /// Parser implementation for CSharp source code
    /// </summary>
    internal class CSharpParser : Parser<CSharpSyntaxNode>
    {
        /// <summary>
        /// Construct a new <see cref="CSharpParser"/>
        /// </summary>
        internal CSharpParser() : base(SupportedLanguages.CSHARP) { }

        protected override CSharpSyntaxNode ParseInternal(string source, ParserOptions? parseOptions = default)
            => CSharpSyntaxTree.ParseText(source).GetRoot() as CSharpSyntaxNode;
    }
}

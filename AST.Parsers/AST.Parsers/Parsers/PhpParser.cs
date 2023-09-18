extern alias PeachPieCodeAnalysis;

using System.Text;
using Pchp.CodeAnalysis;
using Devsense.PHP.Syntax.Ast;
using AST.Parsers.Core;
using SourceText = PeachPieCodeAnalysis::Microsoft.CodeAnalysis.Text.SourceText;

namespace AST.Parsers.Parsers
{
    /// <summary>
    /// Parser implementation for PhpParser source code
    /// </summary>
    internal class PhpParser : Parser<GlobalCode>
    {
         /// <summary>
        /// Construct a new <see cref="PhpParser"/>
        /// </summary>
        internal PhpParser() : base(SupportedLanguages.PHP) { }

        protected override GlobalCode ParseInternal(string source, ParserOptions? parserOptions = null)
        {
            return PhpSyntaxTree.ParseCode(SourceText.From(source, Encoding.UTF8),
                                                         PhpParseOptions.Default,
                                                         PhpParseOptions.Default,
                                                         ".").Root;
        }

    }
}

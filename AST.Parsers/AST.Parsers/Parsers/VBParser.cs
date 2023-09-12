using AST.Parsers.Core;
using Microsoft.CodeAnalysis.VisualBasic;

namespace AST.Parsers.Parsers
{
    internal class VBParser : Parser<VisualBasicSyntaxNode>
    {
        internal VBParser() : base(SupportedLanguages.VISUALBASIC) { }

        protected override VisualBasicSyntaxNode ParseInternal(string source, ParserOptions? parserOptions = null)
            => VisualBasicSyntaxTree.ParseText(source).GetRoot() as VisualBasicSyntaxNode;
    }
}

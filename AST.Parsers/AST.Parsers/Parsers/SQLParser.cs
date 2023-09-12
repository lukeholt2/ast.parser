using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using AST.Parsers.Core;
using SParser = Microsoft.SqlServer.Management.SqlParser.Parser.Parser;

namespace AST.Parsers.Parsers
{
    internal class SQLParser : Parser<SqlScript>
    {
        internal SQLParser() : base(SupportedLanguages.SQL) { }

        protected override SqlScript ParseInternal(string source, ParserOptions? parseOptions = default) => SParser.Parse(source).Script;
    }
}

using System.IO;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting.Runtime;
using Microsoft.Scripting.Hosting.Providers;
using IronPython.Hosting;
using AST.Parsers.Core;
using static AST.Parsers.Utilities.ScriptUtilities;
using PyParser = IronPython.Compiler.Parser;
using PyAst = IronPython.Compiler.Ast.PythonAst;

namespace AST.Parsers.Parsers
{
    internal class PythonParser : Parser<string>
    {
        private readonly string _script = Path.Combine("native", "python", "parse.py");

        internal PythonParser() : base(SupportedLanguages.PYTHON) { }

        protected override string ParseInternal(string source, ParserOptions? parserOptions = null)
        {
            return RunScript("python3", $"{_script} \"{source}\"").Trim();
        }

        // TODO: Add as fallback
        private PyAst ParserInternalSharp(string source, ParserOptions? parserOptions = null)
        {
            ScriptEngine engine = Python.CreateEngine();
            ScriptSource src = engine.CreateScriptSourceFromString(source);
            SourceUnit sourceUnit = HostingHelpers.GetSourceUnit(src);
            LanguageContext langContext = HostingHelpers.GetLanguageContext(engine);
            CompilerContext compilerCtxt = new CompilerContext(sourceUnit, langContext.GetCompilerOptions(), ErrorSink.Default);
            PyParser parser = PyParser.CreateParser(compilerCtxt, (IronPython.PythonOptions)langContext.Options);
            return parser.ParseFile(false);
        }
    }
}

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.SqlServer.Management.SqlParser.SqlCodeDom;
using com.github.javaparser.ast;
using Devsense.PHP.Syntax.Ast;
using Devsense.PHP.Text;
using Newtonsoft.Json;
using static AST.Parsers.Utilities.NodeTypeExtensions;
using PyAst = IronPython.Compiler.Ast;

namespace AST.Parsers.Models
{
    /// <summary>
    /// Factory class used to create <see cref="Hierarchy"/> instances from an AST node 
    /// </summary>
    public static class HierarchyFactory
    {
        public static Hierarchy Create<T>(T node) where T : class
        {
            return node switch
            {
                CSharpSyntaxNode cNode => FromCSharp(cNode),
                VisualBasicSyntaxNode vNode => FromVisualBasic(vNode),
                Node jNode => FromJava(jNode),
                GlobalCode php => FromPHP(php),
                SqlCodeObject script => FromSQL(script),
                string pyASt => FromPython(pyASt),
                PyAst.SuiteStatement pyASt => FromPython(pyASt),
                _ => null
            };
        }

        private static Hierarchy FromCSharp(CSharpSyntaxNode cNode) => new Hierarchy(cNode.Kind().ToString(), cNode.GetText().ToString(),  cNode.ChildNodes().Cast<CSharpSyntaxNode>().Select(n => FromCSharp(n)));
        
        private static Hierarchy FromVisualBasic(VisualBasicSyntaxNode vNode) => new Hierarchy(vNode.Kind().ToString(), vNode.GetText().ToString(), vNode.ChildNodes().Cast<VisualBasicSyntaxNode>().Select(n => FromVisualBasic(n)));

        private static Hierarchy FromSQL(SqlCodeObject script) => new Hierarchy(script.GetType().Name, script.Sql, script.Children.Select(s => FromSQL(s)));

        private static Hierarchy FromPython(string source) => JsonConvert.DeserializeObject<Hierarchy>(source);

        private static Hierarchy FromJava(Node jNode)
        {
            string name = jNode.GetType().Name;
            string text = jNode.toString();
            IEnumerable<Hierarchy> children = jNode.getChildrenNodes().toArray().Cast<Node>().Select(n => FromJava(n));
            return new Hierarchy(name, text, children);
        }


        private static Hierarchy FromPHP(GlobalCode node)
        {
            string name = node.GetType().Name;
            string text = node.ContainingSourceUnit.GetSourceCode(new Span(node.Span.Start, node.Statements.Max(s => s.Span.End)));
            IEnumerable<Hierarchy> children = node.Statements.Select(s => FromPHP(s));
            return new Hierarchy(name, text, children);
        }

        private static Hierarchy FromPHP(LangElement node)
        {
            // HACK: it's a bit hacky to use reflection to get child nodes .. but propabably simpler than writing an entire node visitor
            string name = node.GetType().Name;
            string text = node.ContainingSourceUnit.GetSourceCode(node.Span);
            PropertyInfo[] properties = node.ChildrenOfType(new Type [] {typeof(Statement), typeof(Expression)}, new Type []{typeof(TypeDecl), typeof(NamespaceDecl)});
            IEnumerable<Hierarchy> children = properties.Select(p => p.GetValue(node) as LangElement).Where(p => p is not null).Select(n => FromPHP(n));
            return new Hierarchy(name, text, children);
        }

        private static Hierarchy FromPython(PyAst.SuiteStatement node)
        {
            string name = node.NodeName;
            string text = string.Empty;
            IEnumerable<Hierarchy> children = node.Statements.Select(n => FromPython(n));
            return new Hierarchy(name, text, children);
        }   

        private static Hierarchy FromPython(PyAst.Node node)
        {
            string name = node.NodeName;
            string text = string.Empty;
            PropertyInfo[]? properties = node.ChildrenOfType(new Type[]{ typeof(PyAst.Statement), typeof(PyAst.Expression)}, Array.Empty<Type>());
            IEnumerable<Hierarchy>? children = properties?.Where(p => p.Name != "Parent").Select(p => p?.GetValue(node) as PyAst.Node).Where(p => p is not null).Select(n => FromPython(n));
            return new Hierarchy(name, text, children ?? new List<Hierarchy>());
        }   
    }
}

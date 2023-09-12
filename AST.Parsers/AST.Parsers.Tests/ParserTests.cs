using System.IO;
using System.Collections.Generic;
using Xunit;
using AST.Parsers.Models;
using AST.Parsers.Parsers;
using AST.Parsers.Core;
using static AST.Parsers.Utilities.ScriptUtilities;

namespace AST.Parsers.Tests
{
    public class ParserTests
    {
        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void ParseTest(string source, SupportedLanguages language)
        {
            IParser parser = Parser.Create(language);
            Hierarchy hierarchy =  parser.Parse(source);

            Assert.NotNull(hierarchy);
            Assert.NotEmpty(hierarchy.Children);
        }

        [Theory]
        [MemberData(nameof(CreateTestData))]
        public void PrinterTest(string source, SupportedLanguages language)
        {
            IParser parser = Parser.Create(language);
            Hierarchy hierarchy = parser.Parse(source);

            string result = hierarchy.Text;

            Assert.NotNull(result);
            Assert.Equal(source, result);
        }

        [Fact]
        public void PythonNativeTest()
        {
            // Given
            const string source = "\"print('hello world')\"";
            string path = Path.Combine("native", "python", "parse.py");

            // When
            string result = RunScript("python3", $"{path} {source}").Trim();
            Hierarchy converted = Newtonsoft.Json.JsonConvert.DeserializeObject<Hierarchy>(result);

            // Then
            Assert.NotEmpty(converted.Children);
            Assert.Equal(source.Substring(1, source.Length-2), converted.Text);
        }



        public static IEnumerable<object[]> CreateTestData => new List<object[]>
        {
            new object[]
            {
                File.ReadAllText("TestData/testSharp.cs"),
                SupportedLanguages.CSHARP,
            },
            // new object[]
            // {
            //      File.ReadAllText("TestData/testJava.java"),
            //      SupportedLanguages.JAVA
            // },
            new object[]
            {
                File.ReadAllText("TestData/testSQL.sql"),
                SupportedLanguages.SQL
            },
            new object[]
            {
                File.ReadAllText("TestData/testPhp.php"),
                SupportedLanguages.PHP
            },
            // new object[]
            // {
            //    File.ReadAllText("TestData/testPython.py"),
            //    SupportedLanguages.PYTHON
            // }
        };
    }
}

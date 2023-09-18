using System;
using System.Collections.Generic;
using Xunit;

namespace AST.Parsers.Tests
{
    public class HierarchyFactoryTests
    {
        [MemberData(nameof(CreateTestData))]
        public void CreateTest(string source)
        {

        }

        public static IEnumerable<object[]> CreateTestData => new List<object[]>
                        {
                        new object[]
                        {

                        }
                        };
    }
}
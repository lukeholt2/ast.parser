# AST.Parser

![workflow](https://github.com/lukeholt2/ast.parser/actions/workflows/ci.yml/badge.svg)
[![codecov](https://codecov.io/gh/lukeholt2/ast.parser/branch/master/graph/badge.svg)](https://codecov.io/gh/lukeholt2/ast.parser/)
## Description

AST.Parser is a collection of language parsers used to parse and represent source code in a generic hierarchical structure.

There are currently several parser implementations providing support for the following languages:

- C#
- Visual Basic
- Java
- Python
- PHP
- SQL

## Installation

Install the package using nuget:

`dotnet add package AST.Parsers -v <target_version>`

## Usage

To start parsing, simply create an `IParser` instance and call the `Parse` method.

```csharp
using Stream stream = File.Open("source.cs")
IParser parser = Parsers.Create(SupportedLanguages.CSHARP);
parser.Parse(stream);
``````

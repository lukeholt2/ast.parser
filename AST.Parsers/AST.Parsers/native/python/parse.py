import ast
import sys
import re

class Hierarchy:
    def __init__(self, name, text, children, parent) -> None:
       self.Name = re.split('\'', str(name))[1];
       self.Text = text
       self.Children: list[Hierarchy] = children
       self.parent = parent
       
    def convert(self):
        return {
            "Name": self.Name,
            "Text": self.Text,
            "Children": [x.convert() for x in self.Children]
        }

class AstGraphGenerator(object):
    
    def __init__(self):
        self.graph = []

    def __str__(self):
        return str(self.graph)

    def visit(self, node):
        """Visit a node."""
        method = 'visit_' + node.__class__.__name__
        visitor = getattr(self, method, self.generic_visit)
        return visitor(node)

    def generic_visit(self, node):
        """Called if no explicit visitor function exists for a node."""
        self.graph.append(Hierarchy(
            type(node),
            ast.unparse(node),
            [],
            None
        ));
        for value in ast.iter_child_nodes(node):
            if isinstance(value, list):
                for item in value:
                    if isinstance(item, ast.AST):
                        self.visit(item)

            elif isinstance(value, ast.AST):
                self.graph[-1].Children.append(Hierarchy(type(value), ast.unparse(value), [], ast.unparse(node)))
                self.visit(value)




    
def setChildren(node: Hierarchy, graph: list):
    node.Children = list(filter(lambda x: x.parent == node.Text, graph))
    for c in node.Children:
        setChildren(c, list(filter(lambda x: x not in node.Children, graph)));

source = sys.argv[1]

tree = ast.parse(source)

generator = AstGraphGenerator()
generator.visit(tree)
allNodes = generator.graph
for g in generator.graph:
    for c in g.Children:
        allNodes.append(c)
        
root: Hierarchy = list(filter(lambda x: x.parent == None, generator.graph))[0];
setChildren(root, allNodes);


print(str(root.convert()))
    
    
    

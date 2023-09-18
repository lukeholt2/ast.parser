using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AST.Parsers.Models
{
    /// <summary>
    /// Intermediate Representation of AST nodes. 
    /// A 'tree' based structure used to generically represent AST nodes
    /// </summary>
    public class Hierarchy
    {
        /// <summary>
        /// String representing the current node type
        /// </summary>
        [JsonProperty]
        public string Name { get; private set; }


        /// <summary>
        /// String containing the raw / original code from which this node was built
        /// </summary>
        [JsonProperty]
        public string Text { get; private set; }

        /// <summary>
        /// Collection of child nodes
        /// </summary>
        [JsonProperty]
        public List<Hierarchy> Children { get; private set; }

        [JsonConstructor]
        private Hierarchy() { }

        /// <summary>
        /// Construct a new <see cref="Hierarchy"/> object using the provided metadata 
        /// </summary>
        /// <param name="name">Type of node being represented</param>
        /// <param name="text">Original source code of the node</param>
        /// <param name="childNodes">Collection of child nodes</param>
        internal Hierarchy(string name, string text, IEnumerable<Hierarchy> childNodes)
        {
            Name = name;
            Text = text;
            Children = childNodes.ToList();
        }
    }
}

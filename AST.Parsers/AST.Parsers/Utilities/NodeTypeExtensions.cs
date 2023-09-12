using System;
using System.Linq;
using System.Reflection;

namespace AST.Parsers.Utilities
{
    /// <summary>
    /// General utilities for analyzing node types & properties using reflection 
    /// </summary>
    internal static class NodeTypeExtensions
    {
        /// <summary>
        /// Default binding flags to use when reflecting objects
        /// </summary>
        private static BindingFlags _bindings = BindingFlags.Instance | BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.GetProperty;

        /// <summary>
        /// Verify if the given type is included in the provided collection
        /// </summary>
        /// <param name="toCheck"> The type to be checked</param>
        /// <param name="types">Collection of types to check against</param>
        /// <param name="exclude">Collection of excluded types</param>
        /// <returns>boolean value indicating whether the type is included</returns>
        private static bool IsAnyTypes(Type toCheck, Type[] types, Type[] exclude) => types.Any(t => toCheck.Equals(t)) && !exclude.Any(t => toCheck.Equals(t));

        internal static PropertyInfo[] ChildrenOfType<TNode>(this TNode node, Type[] baseTypes, Type[] excluded) 
            => node.GetType().GetProperties(_bindings).Where(p => IsAnyTypes(p.DeclaringType.BaseType, baseTypes, excluded)).ToArray();
        
    }
}
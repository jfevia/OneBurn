using System;
using System.Reflection;

namespace OneBurn.Core
{
    public static class AssemblyExtensions
    {
        /// <summary>
        ///     Gets the version.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>The version.</returns>
        public static Version GetVersion(this Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            return assemblyName.Version;
        }
    }
}
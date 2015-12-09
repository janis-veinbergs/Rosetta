﻿/// <summary>
/// SourceGenerator.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Tests.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Acts like a factory.
    /// </summary>
    public static class SourceGenerator
    {
        /// <summary>
        /// The class name.
        /// </summary>
        public static string Name { get; set; }

        /// <summary>
        /// The namespace name.
        /// </summary>
        public static string NamespaceName { get; set; }

        /// <summary>
        /// The base class name.
        /// </summary>
        public static string BaseName { get; set; }

        /// <summary>
        /// The interface #1 name.
        /// </summary>
        public static string Interface1Name { get; set; }

        /// <summary>
        /// The interface #2 name.
        /// </summary>
        public static string Interface2Name { get; set; }

        /// <summary>
        /// The interface #3 name.
        /// </summary>
        public static string Interface3Name { get; set; }

        /// <summary>
        /// Defines default values.
        /// </summary>
        static SourceGenerator()
        {
            Name = "MyClass";
            BaseName = "MyBaseClass";
            NamespaceName = "MyNamespace";
            Interface1Name = "MyInterface1";
            Interface2Name = "MyInterface2";
            Interface3Name = "MyInterface3";
        }

        /// <summary>
        /// Generates the appropriate class given some options.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="classOptions">The options for classes.</param>
        /// <param name="functionOptions">The options for functions.</param>
        /// <returns>
        /// A <see cref="KeyValuePair"/> where the first element is the source 
        /// string and the second element is the set of attributes.
        /// </returns>
        /// 
        /// TODO: Abstract with a wrapper type wrapping around KeyValuePair.
        public static KeyValuePair<string, IReadOnlyDictionary<string, string>> Generate(
            SourceOptions options = SourceOptions.None, 
            ClassOptions classOptions = ClassOptions.None,
            FunctionOptions functionOptions = FunctionOptions.None)
        {
            ClassGenerator classes = new ClassGenerator()
            {
                Name = Name
            };

            // Class with many interfaces
            if (options.HasFlag(SourceOptions.ImplementsInterfaces) && 
                options.HasFlag(SourceOptions.BaseListMany))
            {
                classes.Interface1Name = Interface1Name;
                classes.Interface2Name = Interface2Name;
                classes.Interface3Name = Interface3Name;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.ClassWithManyInterfaces, classes.ClassWithManyInterfacesAttributes);
            }

            // Class with interface
            if (options.HasFlag(SourceOptions.ImplementsInterfaces))
            {
                classes.Interface1Name = Interface1Name;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.ClassWith1Interface, classes.ClassWith1InterfaceAttributes);
            }

            // Class with base class
            if (options.HasFlag(SourceOptions.HasInheritance))
            {
                classes.BaseClassName = BaseName;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.ClassWithBaseClass, classes.ClassWithBaseClassAttributes);
            }

            // Empty elements
            if (options.HasFlag(SourceOptions.EmptyElements))
            {
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.VerySimpleClassWithEmptyMethods, classes.VerySimpleClassWithEmptyMethodsAttributes);
            }

            // Namespace
            if (options.HasFlag(SourceOptions.HasNamespace))
            {
                classes.NamespaceName = NamespaceName;
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                    classes.VerySimpleClassInNamespace, classes.VerySimpleClassInNamespaceAttributes);
            }

            // Class with expressions
            if (classOptions.HasFlag(ClassOptions.HasContent) && functionOptions.HasFlag(FunctionOptions.HasExpressions))
            {
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                classes.ClassWithMethodArithmeticExpressions, classes.ClassWithMethodArithmeticExpressionsAttributes);
            }

            // Simple class
            if (classOptions.HasFlag(ClassOptions.HasContent))
            {
                return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                classes.SimpleClass, classes.SimpleClassAttributes);
            }
            
            return new KeyValuePair<string, IReadOnlyDictionary<string, string>>(
                classes.VerySimpleClass, classes.VerySimpleClassAttributes);
        }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AlgebraLibrary.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AlgebraLibrary.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nothing To Evaluate.
        /// </summary>
        internal static string EmptyExpression {
            get {
                return ResourceManager.GetString("EmptyExpression", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression Contains Empty Parenthesis.
        /// </summary>
        internal static string EmptyParenthesis {
            get {
                return ResourceManager.GetString("EmptyParenthesis", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number too large or negative.
        /// </summary>
        internal static string FactorialError {
            get {
                return ResourceManager.GetString("FactorialError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Can&apos;t square root a negative number.
        /// </summary>
        internal static string NegativeRoot {
            get {
                return ResourceManager.GetString("NegativeRoot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The Expression Contains no operands.
        /// </summary>
        internal static string OperatorOnly {
            get {
                return ResourceManager.GetString("OperatorOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Seems Like Some Brackets Are Missing.
        /// </summary>
        internal static string ParenthesisMismatch {
            get {
                return ResourceManager.GetString("ParenthesisMismatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression can not start with a Binary Operator.
        /// </summary>
        internal static string StartingWithOperator {
            get {
                return ResourceManager.GetString("StartingWithOperator", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Syntax Error.
        /// </summary>
        internal static string WrongSyntax {
            get {
                return ResourceManager.GetString("WrongSyntax", resourceCulture);
            }
        }
    }
}

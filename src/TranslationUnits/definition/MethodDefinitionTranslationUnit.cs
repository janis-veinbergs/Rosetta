﻿/// <summary>
/// MethodDefinitionTranslationUnit.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation
{
    using System;

    /// <summary>
    /// Class describing a method signature for definitions.
    /// 
    /// TODO: Move to a separate project, this is specific to ScriptSharp.
    /// </summary>
    public class MethodDefinitionTranslationUnit : MethodSignatureDeclarationTranslationUnit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        protected MethodDefinitionTranslationUnit() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="visibility"></param>
        protected MethodDefinitionTranslationUnit(ITranslationUnit name, VisibilityToken visibility) 
            : base(name, visibility)
        {
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="MethodDefinitionTranslationUnit"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public MethodDefinitionTranslationUnit(MethodDefinitionTranslationUnit other)
            : base(other)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="visibility"></param>
        /// <param name="returnType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MethodDefinitionTranslationUnit Create(VisibilityToken visibility, ITranslationUnit returnType, ITranslationUnit name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return new MethodDefinitionTranslationUnit()
            {
                Visibility = visibility,
                Name = name,
                ReturnType = returnType
            };
        }

        protected override string RenderedVisibilityModifier
        {
            get { return string.Empty; }
        }
    }
}

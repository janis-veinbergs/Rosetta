﻿/// <summary>
/// Renderer.Class.cs
/// Andrea Tino - 2015
/// </summary>

namespace Rosetta.Translation.Renderings
{
    using System;

    using Rosetta.Renderings.Utils;

    /// <summary>
    /// 
    /// </summary>
    internal partial class Renderer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [RenderingResource("SimpleClass.ts")]
        public string RenderSimpleClass()
        {
            ITranslationUnit translationUnit = TranslationUnitBuilder.BuildClassTranslationUnit(
                VisibilityToken.Public, "SampleClass", null);

            return translationUnit.Translate();
        }
    }
}
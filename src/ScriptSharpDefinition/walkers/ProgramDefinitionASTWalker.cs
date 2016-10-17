﻿/// <summary>
/// ProgramASTWalker.cs
/// Andrea Tino - 2016
/// </summary>

namespace Rosetta.ScriptSharp.Definition.AST
{
    using System;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    using Rosetta.AST;
    using Rosetta.Translation;

    /// <summary>
    /// Walks a program AST node.
    /// </summary>
    public class ProgramDefinitionASTWalker : CSharpSyntaxWalker, IASTWalker
    {
        // Protected for testability
        protected CSharpSyntaxNode node;

        // Protected for testability
        protected ProgramTranslationUnit program;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        protected ProgramDefinitionASTWalker(CSharpSyntaxNode node, ProgramTranslationUnit program)
        {
            var programSyntaxNode = node as CompilationUnitSyntax;
            if (programSyntaxNode == null)
            {
                throw new ArgumentException(
                    string.Format("Specified node is not of type {0}",
                    typeof(CompilationUnitSyntax).Name));
            }

            if (program == null)
            {
                throw new ArgumentNullException(nameof(program));
            }

            this.node = node;
            this.program = program;
        }

        /// <summary>
        /// Copy initializes a new instance of the <see cref="ProgramDefinitionASTWalker"/> class.
        /// </summary>
        /// <param name="other"></param>
        /// <remarks>
        /// For testability.
        /// </remarks>
        public ProgramDefinitionASTWalker(ProgramDefinitionASTWalker other)
        {
            if (other == null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            this.node = other.node;
            this.program = other.program;
        }

        /// <summary>
        /// Factory method for class <see cref="ProgramASTWalker"/>.
        /// </summary>
        /// <param name="node"><see cref="ProgramDefinitionASTWalker"/> Used to initialize the walker.</param>
        /// <returns></returns>
        public static ProgramDefinitionASTWalker Create(CSharpSyntaxNode node)
        {
            // No helper needed for this walker
            var program = ProgramTranslationUnit.Create();

            return new ProgramDefinitionASTWalker(node, program);
        }

        /// <summary>
        /// Walk the whole tree starting from specified <see cref="CSharpSyntaxNode"/> and 
        /// build the translation unit tree necessary for generating TypeScript output.
        /// </summary>
        /// <returns>The root of the translation unit tree.</returns>
        public ITranslationUnit Walk()
        {
            // Visiting
            this.Visit(node);

            // Returning root
            return this.program;
        }

        #region CSharpSyntaxWalker overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var classDefinitionWalker = ClassDefinitionASTWalker.Create(node);
            var translationUnit = classDefinitionWalker.Walk();
            this.program.AddContent(translationUnit);

            this.InvokeClassDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <remarks>
        /// This will cause an AST walker to be created, thus we don't need to go further deeper in the
        /// tree by visiting the node.
        /// </remarks>
        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            var namespaceWalker = NamespaceDefinitionASTWalker.Create(node);
            var translationUnit = namespaceWalker.Walk();
            this.program.AddContent(translationUnit);

            this.InvokeNamespaceDeclarationVisited(this, new WalkerEventArgs());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // We do not output interfaces for definitions
        }

        #endregion

        #region Walk events

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent ClassDeclarationVisited;

        /// <summary>
        /// 
        /// </summary>
        public event WalkerEvent NamespaceDeclarationVisited;

        #endregion

        private void InvokeClassDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.ClassDeclarationVisited != null)
            {
                this.ClassDeclarationVisited(sender, e);
            }
        }

        private void InvokeNamespaceDeclarationVisited(object sender, WalkerEventArgs e)
        {
            if (this.NamespaceDeclarationVisited != null)
            {
                this.NamespaceDeclarationVisited(sender, e);
            }
        }
    }
}
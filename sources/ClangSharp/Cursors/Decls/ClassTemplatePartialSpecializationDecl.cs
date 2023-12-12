// Copyright (c) .NET Foundation and Contributors. All Rights Reserved. Licensed under the MIT License (MIT). See License.md in the repository root for more information.

using ClangSharp.Interop;
using static ClangSharp.Interop.CXCursorKind;
using static ClangSharp.Interop.CX_DeclKind;
using System;
using System.Collections.Generic;

namespace ClangSharp;

public sealed class ClassTemplatePartialSpecializationDecl : ClassTemplateSpecializationDecl
{
    private readonly Lazy<IReadOnlyList<Expr>> _associatedConstraints;
    private readonly Lazy<Type> _injectedSpecializationType;
    private readonly Lazy<ClassTemplatePartialSpecializationDecl> _instantiatedFromMember;
    private readonly Lazy<IReadOnlyList<NamedDecl>> _templateParameters;

    internal ClassTemplatePartialSpecializationDecl(CXCursor handle) : base(handle, CXCursor_ClassTemplatePartialSpecialization, CX_DeclKind_ClassTemplatePartialSpecialization)
    {
        _associatedConstraints = new Lazy<IReadOnlyList<Expr>>(() => {
            var associatedConstraintCount = Handle.NumAssociatedConstraints;
            var associatedConstraints = new List<Expr>(associatedConstraintCount);

            for (var i = 0; i < associatedConstraintCount; i++)
            {
                var parameter = TranslationUnit.GetOrCreate<Expr>(Handle.GetAssociatedConstraint(unchecked((uint)i)));
                associatedConstraints.Add(parameter);
            }

            return associatedConstraints;
        });

        _injectedSpecializationType = new Lazy<Type>(() => TranslationUnit.GetOrCreate<Type>(Handle.InjectedSpecializationType));
        _instantiatedFromMember = new Lazy<ClassTemplatePartialSpecializationDecl>(() => TranslationUnit.GetOrCreate<ClassTemplatePartialSpecializationDecl>(Handle.InstantiatedFromMember));

        _templateParameters = new Lazy<IReadOnlyList<NamedDecl>>(() => {
            var parameterCount = Handle.GetNumTemplateParameters(0);
            var parameters = new List<NamedDecl>(parameterCount);

            for (var i = 0; i < parameterCount; i++)
            {
                var parameter = TranslationUnit.GetOrCreate<NamedDecl>(Handle.GetTemplateParameter(0, unchecked((uint)i)));
                parameters.Add(parameter);
            }

            return parameters;
        });
    }

    public IReadOnlyList<Expr> AssociatedConstraints => _associatedConstraints.Value;

    public bool HasAssociatedConstraints => Handle.NumAssociatedConstraints != 0;

    public Type InjectedSpecializationType => _injectedSpecializationType.Value;

    public ClassTemplatePartialSpecializationDecl InstantiatedFromMember => _instantiatedFromMember.Value;

    public ClassTemplatePartialSpecializationDecl InstantiatedFromMemberTemplate => InstantiatedFromMember;

    public bool IsMemberSpecialization => Handle.IsMemberSpecialization;

    public new ClassTemplatePartialSpecializationDecl MostRecentDecl => (ClassTemplatePartialSpecializationDecl)base.MostRecentDecl;

    public IReadOnlyList<NamedDecl> TemplateParameters => _templateParameters.Value;
}

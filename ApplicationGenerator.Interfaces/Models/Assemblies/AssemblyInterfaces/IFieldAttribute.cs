﻿using System;
using AbstraX.ServerInterfaces;
using System.Collections.Generic;
using AbstraX.XPathBuilder;
using System.Linq.Expressions;
using System.Linq;
using CodeInterfaces.XPathBuilder;

namespace AbstraX.AssemblyInterfaces
{
    public interface IFieldAttribute
    {
        ScalarType DataType { get; }
        IEnumerable<IElement> ChildElements { get; }
        float ChildOrdinal { get; }
        void ClearPredicates();
        string DebugInfo { get; }
        string DesignComments { get; }
        bool HasDocumentation { get; }
        string DocumentationSummary { get; }
        string Documentation { get; }
        IQueryable ExecuteWhere(XPathAxisElement element);
        IQueryable ExecuteWhere(Expression expression);
        IQueryable ExecuteWhere(string property, object value);
        Facet[] Facets { get; }
        string FolderKeyPair { get; set; }
        bool HasChildren { get; }
        string ID { get; }
        string ImageURL { get; }
        DefinitionKind Kind { get; }
        string Name { get; }
        IBase Parent { get; }
        string ParentID { get; }
        Modifiers Modifiers { get; }
    }
}

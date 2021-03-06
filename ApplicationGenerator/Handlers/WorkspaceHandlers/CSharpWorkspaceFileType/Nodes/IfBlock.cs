﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstraX.Handlers.WorkspaceHandlers.CSharpWorkspaceFileType.Nodes
{
    public class IfBlock : IfDirectiveNodeBase
    {
        public string Condition { get; }

        public IfBlock(IfDirectiveTriviaSyntax node) : base(node)
        {
            this.Condition = node.Condition.ToFullString();
        }

        public override string Code
        {
            get
            {
                return this.Node.ToFullString();
            }
        }
    }
}

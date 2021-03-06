﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AbstraX.ServerInterfaces;

namespace AbstraX.AssemblyInterfaces
{
    public interface IGetSetProperty
    {
        MethodInfo Method { get; }
        string PropertyName { get; }
        Modifiers Modifiers { get; }
    }
}

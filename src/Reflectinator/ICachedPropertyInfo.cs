﻿using System;
using System.Reflection;

namespace Reflectinator
{
    public interface ICachedPropertyInfo
    {
        PropertyInfo PropertyInfo { get; }
        string Name { get; }
        bool IsPublic { get; }
        bool IsStatic { get; }
        bool CanRead { get; }
        bool CanWrite { get; }
        ICachedType PropertyType { get; }
        ICachedType DeclaringType { get; }
        Func<object, object> Get { get; } 
        Action<object, object> Set { get; }
    }
}
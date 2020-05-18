using System;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public interface IMapping
    {
        Dictionary<string, Func<string, ModelObject, bool>> Functions { get; set; }
    }
}
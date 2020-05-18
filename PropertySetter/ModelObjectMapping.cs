using System;
using System.Collections.Generic;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class ModelObjectMapping : IMapping
    {
        public Dictionary<string, Func<string, ModelObject, bool>> Functions { get; set; }

        public ModelObjectMapping()
        {
            Functions = new Dictionary<string, Func<string, ModelObject, bool>>()
            {
                { "PHASE", new Func<string, ModelObject, bool>(SetPhase) }
            };
        }

        private bool SetPhase(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                result = modelObject.SetPhase(new Phase(parsed));
            }
            return result;
        }
    }
}

using System;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class SingleRebarMapping : ReinforcementMapping
    {
        public SingleRebarMapping()
        {
            Functions.Add("SIZE", new Func<string, ModelObject, bool>(SetSize));
        }

        private bool SetSize(string value, ModelObject modelObject)
        {
            (modelObject as SingleRebar).Size = value;
            return modelObject.Modify();
        }
    }
}

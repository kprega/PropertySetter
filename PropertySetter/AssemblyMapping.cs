using System;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class AssemblyMapping : ModelObjectMapping
    {
        public AssemblyMapping()
        {
            Functions.Add("ASSEMBLY_START_NUMBER", new Func<string, ModelObject, bool>(SetAssemblyStartNumber));
            Functions.Add("ASSEMBLY_PREFIX",       new Func<string, ModelObject, bool>(SetAssemblyPrefix)     );
            Functions.Add("NAME",                  new Func<string, ModelObject, bool>(SetAssemblyName)       );
        }

        private bool SetAssemblyStartNumber(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Assembly).AssemblyNumber.StartNumber = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetAssemblyPrefix(string value, ModelObject modelObject)
        {
            (modelObject as Assembly).AssemblyNumber.Prefix = value;
            return modelObject.Modify();
        }

        private bool SetAssemblyName(string value, ModelObject modelObject)
        {
            (modelObject as Assembly).Name = value;
            return modelObject.Modify();
        }
    }
}

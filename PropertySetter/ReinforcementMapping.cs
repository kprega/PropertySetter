using System;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class ReinforcementMapping : ModelObjectMapping
    {
        public ReinforcementMapping()
        {
            Functions.Add("CLASS",        new Func<string, ModelObject, bool>(SetClass));
            Functions.Add("GRADE",        new Func<string, ModelObject, bool>(SetGrade));
            Functions.Add("NAME",         new Func<string, ModelObject, bool>(SetName));
            Functions.Add("PREFIX",       new Func<string, ModelObject, bool>(SetPrefix));
            Functions.Add("START_NUMBER", new Func<string, ModelObject, bool>(SetStartNumber));
        }

        private bool SetClass(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Reinforcement).Class = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetGrade(string value, ModelObject modelObject)
        {
            (modelObject as Reinforcement).Grade = value;
            return modelObject.Modify();
        }

        private bool SetName(string value, ModelObject modelObject)
        {
            (modelObject as Reinforcement).Name = value;
            return modelObject.Modify();
        }

        private bool SetPrefix(string value, ModelObject modelObject)
        {
            (modelObject as Reinforcement).NumberingSeries.Prefix = value;
            return modelObject.Modify();
        }

        private bool SetStartNumber(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Reinforcement).NumberingSeries.StartNumber = parsed;
                result = modelObject.Modify();
            }
            return result;
        }
    }
}

using System;
using System.Globalization;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class PartMapping : ModelObjectMapping
    {
        public PartMapping()
        {
            Functions.Add("ASSEMBLY_START_NUMBER", new Func<string, ModelObject, bool>(SetAssemblyStartNumber));
            Functions.Add("ASSEMBLY_PREFIX",       new Func<string, ModelObject, bool>(SetAssemblyPrefix));
            Functions.Add("CAST_UNIT_TYPE",        new Func<string, ModelObject, bool>(SetCastUnitType));
            Functions.Add("CLASS_ATTR",            new Func<string, ModelObject, bool>(SetClass));
            Functions.Add("FINISH",                new Func<string, ModelObject, bool>(SetFinish));
            Functions.Add("MATERIAL",              new Func<string, ModelObject, bool>(SetMaterial));
            Functions.Add("NAME",                  new Func<string, ModelObject, bool>(SetName));
            Functions.Add("PART_START_NUMBER",     new Func<string, ModelObject, bool>(SetPartStartNumber));
            Functions.Add("PART_PREFIX",           new Func<string, ModelObject, bool>(SetPartPrefix));
            Functions.Add("PREFIX",                new Func<string, ModelObject, bool>(SetPartPrefix));
            Functions.Add("PartCambering",         new Func<string, ModelObject, bool>(SetCambering));
            Functions.Add("PartShortening",        new Func<string, ModelObject, bool>(SetShortening));
            Functions.Add("Warping1",              new Func<string, ModelObject, bool>(SetWarpingStart));
            Functions.Add("Warping2",              new Func<string, ModelObject, bool>(SetWarpingEnd));
            Functions.Add("POUR_PHASE",            new Func<string, ModelObject, bool>(SetPouringPhase));
            Functions.Add("PROFILE",               new Func<string, ModelObject, bool>(SetProfile));
        }

        private bool SetProfile(string value, ModelObject modelObject)
        {
            (modelObject as Part).Profile.ProfileString = value;
            return modelObject.Modify();
        }

        private bool SetPouringPhase(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Part).PourPhase = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetWarpingEnd(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as Part).DeformingData.Angle2 = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetWarpingStart(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as Part).DeformingData.Angle = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetShortening(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as Part).DeformingData.Shortening = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetCambering(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as Part).DeformingData.Cambering = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetPartPrefix(string value, ModelObject modelObject)
        {
            (modelObject as Part).PartNumber.Prefix = value;
            return modelObject.Modify();
        }

        private bool SetPartStartNumber(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Part).PartNumber.StartNumber = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetMaterial(string value, ModelObject modelObject)
        {
            (modelObject as Part).Material.MaterialString = value;
            return modelObject.Modify();
        }

        private bool SetFinish(string value, ModelObject modelObject)
        {
            (modelObject as Part).Finish = value;
            return modelObject.Modify();
        }

        private bool SetName(string value, ModelObject modelObject)
        {
            (modelObject as Part).Name = value;
            return modelObject.Modify();
        }

        private bool SetAssemblyStartNumber(string value, ModelObject modelObject)
        {
            var parsed = -1;
            var result = false;
            if (int.TryParse(value, out parsed))
            {
                (modelObject as Part).AssemblyNumber.StartNumber = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetAssemblyPrefix(string value, ModelObject modelObject)
        {
            (modelObject as Part).AssemblyNumber.Prefix = value;
            return modelObject.Modify();
        }

        private bool SetCastUnitType(string value, ModelObject modelObject)
        {
            var parsed = (Part.CastUnitTypeEnum)(-1);
            var result = false;
            if (Enum.TryParse(value, out parsed))
            {
                (modelObject as Part).CastUnitType = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetClass(string value, ModelObject modelObject)
        {
            (modelObject as Part).Class = value;
            return modelObject.Modify();
        }
    }
}

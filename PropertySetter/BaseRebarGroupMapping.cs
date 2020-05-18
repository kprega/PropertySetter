using System;
using System.Collections;
using System.Globalization;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class BaseRebarGroupMapping : ReinforcementMapping
    {
        public BaseRebarGroupMapping()
        {
            Functions.Add("END_X",     new Func<string, ModelObject, bool>(SetEndPointX));
            Functions.Add("END_Y",     new Func<string, ModelObject, bool>(SetEndPointY));
            Functions.Add("END_Z",     new Func<string, ModelObject, bool>(SetEndPointZ));
            Functions.Add("START_X",   new Func<string, ModelObject, bool>(SetStartPointX));
            Functions.Add("START_Y",   new Func<string, ModelObject, bool>(SetStartPointY));
            Functions.Add("START_Z",   new Func<string, ModelObject, bool>(SetStartPointZ));
            Functions.Add("SIZE",      new Func<string, ModelObject, bool>(SetSize));
            Functions.Add("CC_EXACT",  new Func<string, ModelObject, bool>(SetExactSpacings));
            Functions.Add("CC_TARGET", new Func<string, ModelObject, bool>(SetTargetSpacings));
            Functions.Add("NUMBER",    new Func<string, ModelObject, bool>(SetBarsNumber));
        }

        private bool SetEndPointX(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).EndPoint.X = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetEndPointY(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).EndPoint.Y = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetEndPointZ(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).EndPoint.Z = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetStartPointX(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).StartPoint.X = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetStartPointY(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).StartPoint.Y = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetStartPointZ(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as BaseRebarGroup).StartPoint.Z = parsed;
                result = modelObject.Modify();
            }
            return result;
        }

        private bool SetSize(string value, ModelObject modelObject)
        {
            (modelObject as BaseRebarGroup).Size = value;
            return modelObject.Modify();
        }

        private bool SetExactSpacings(string value, ModelObject modelObject)
        {
            var distances = Tekla.Structures.Datatype.DistanceList.Parse(value);
            var spacings = new ArrayList();
            foreach (var dist in distances)
            {
                spacings.Add(dist.Millimeters);
            }
            (modelObject as BaseRebarGroup).SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACINGS;
            (modelObject as BaseRebarGroup).Spacings = spacings;
            return modelObject.Modify();
        }

        private bool SetTargetSpacings(string value, ModelObject modelObject)
        {
            // Return false if spacing type is not set to target value spacing
            if ((modelObject as BaseRebarGroup).SpacingType < (BaseRebarGroup.RebarGroupSpacingTypeEnum)2) return false;
            var distances = Tekla.Structures.Datatype.DistanceList.Parse(value);
            var spacings = new ArrayList();
            foreach (var dist in distances)
            {
                spacings.Add(dist.Millimeters);
            }
            (modelObject as BaseRebarGroup).Spacings = spacings;

            return modelObject.Modify();
        }

        private bool SetBarsNumber(string value, ModelObject modelObject)
        {
            var distances = Tekla.Structures.Datatype.DistanceList.Parse(value);
            var spacings = new ArrayList();
            foreach (var dist in distances)
            {
                spacings.Add(dist.Millimeters);
            }
            (modelObject as BaseRebarGroup).SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_NUMBER;
            (modelObject as BaseRebarGroup).Spacings = spacings;

            return modelObject.Modify();
        }
    }
}

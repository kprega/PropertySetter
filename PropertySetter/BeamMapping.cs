using System;
using System.Globalization;
using Tekla.Structures.Model;

namespace PropertySetter
{
    public class BeamMapping : PartMapping
    {
        public BeamMapping()
        {
            Functions.Add("END_X",   new Func<string, ModelObject, bool>(SetEndPointX));
            Functions.Add("END_Y",   new Func<string, ModelObject, bool>(SetEndPointY));
            Functions.Add("END_Z",   new Func<string, ModelObject, bool>(SetEndPointZ));
            Functions.Add("START_X", new Func<string, ModelObject, bool>(SetStartPointX));
            Functions.Add("START_Y", new Func<string, ModelObject, bool>(SetStartPointY));
            Functions.Add("START_Z", new Func<string, ModelObject, bool>(SetStartPointZ));
        }

        private bool SetEndPointX(string value, ModelObject modelObject)
        {
            var parsed = -1.0;
            var result = false;
            if (double.TryParse(value.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out parsed))
            {
                (modelObject as Beam).EndPoint.X = parsed;
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
                (modelObject as Beam).EndPoint.Y = parsed;
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
                (modelObject as Beam).EndPoint.Z = parsed;
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
                (modelObject as Beam).StartPoint.X = parsed;
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
                (modelObject as Beam).StartPoint.Y = parsed;
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
                (modelObject as Beam).StartPoint.Z = parsed;
                result = modelObject.Modify();
            }
            return result;
        }
    }
}

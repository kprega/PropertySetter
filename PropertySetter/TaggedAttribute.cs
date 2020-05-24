using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tekla.Structures.Model;
using System.Collections;

namespace PropertySetter
{
    public static class TaggedAttribute
    {
        public static string Parse(string value, ModelObject modelObject)
        {
            if (!value.Contains("%")) return value;

            var result = value;
            foreach (Match match in Regex.Matches(value, "%(.*?)%"))
            {
                var propertyName = match.Groups[1].Value;

                // there is a problem with PHASE attribute
                // an object on phase 1 is read as both int and double, where results are 1 and 0 respectively
                // in this case invalid double value is replacing correct int reading
                // to be checked if same problem occurs with other parameters

                var hashtable = new Hashtable();
                if (propertyName == "PHASE")
                {
                    modelObject.GetIntegerReportProperties(new ArrayList() { propertyName }, ref hashtable);
                }
                else
                {
                    modelObject.GetAllReportProperties(new ArrayList() { propertyName }, new ArrayList() { propertyName }, new ArrayList() { propertyName }, ref hashtable);
                }
                // Replace tag with value, if property was successfully read; otherwise replace with empty string.
                result = hashtable.Count != 0 ? result.Replace($"%{propertyName}%", hashtable[propertyName].ToString()) : result.Replace($"%{propertyName}%", string.Empty);
            }

            return result;
        }
    }
}

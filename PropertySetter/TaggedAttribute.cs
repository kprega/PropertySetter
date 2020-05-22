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
                var hashtable = new Hashtable();
                modelObject.GetAllReportProperties( // CLASS CANNOT BE RETRIEVED THIS WAY
                    new ArrayList() { propertyName }, 
                    new ArrayList() { propertyName }, 
                    new ArrayList() { propertyName }, 
                    ref hashtable);

                // Replace tag with value, if property was successfully read; otherwise replace with empty string.
                result = hashtable.Count != 0 ? result.Replace($"%{propertyName}%", hashtable[propertyName].ToString()) : result.Replace($"%{propertyName}%", string.Empty);
            }

            return result;
        }
    }
}

namespace PropertySetter
{
    public class Rule
    {
        public string FilterName { get; set; }
        public string PropertyName { get; set; }
        public string PropertyValue { get; set; }

        public Rule()
        {

        }

        public Rule(string filterName, string propertyName, string propertyValue)
        {
            FilterName = filterName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }
    }   
}

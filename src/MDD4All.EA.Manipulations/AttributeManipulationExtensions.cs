using EAAPI = EA;

namespace MDD4All.EnterpriseArchitect.Manipulations
{
    public static class AttributeManipulationExtensions
    {
        public static EAAPI.Element GetClassifier(this EAAPI.Attribute attribute, EAAPI.Repository repository)
        {
            EAAPI.Element result = null;

            if(attribute.ClassifierID != 0)
            {
                result = repository.GetElementByID(attribute.ClassifierID);
            }

            return result;
        }

        public static string GetClassifierName(this EAAPI.Attribute attribute, EAAPI.Repository repository)
        {
            string result = "";

            if (attribute.ClassifierID != 0)
            {
                result = repository.GetElementByID(attribute.ClassifierID).Name;
            }
            else if(attribute.ClassifierID == 0 && !string.IsNullOrEmpty(attribute.Type))
            {
                result = attribute.Type;
            }

            return result;
        }
    }
}

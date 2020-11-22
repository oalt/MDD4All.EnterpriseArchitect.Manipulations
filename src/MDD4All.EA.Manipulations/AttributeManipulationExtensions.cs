#if EA_FACADE
using EAAPI = MDD4All.EAFacade.DataModels.Contracts;
#else
using EAAPI = EA;
#endif

#if EA_FACADE
namespace MDD4All.EAFacade.Manipulations
#else
namespace MDD4All.EnterpriseArchitect.Manipulations
#endif
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

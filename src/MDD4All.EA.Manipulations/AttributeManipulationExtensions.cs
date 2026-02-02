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

        /// <summary>
        /// Create a connector between the attribute and another model element. 
        /// (linked to element feature 'attribute')
        /// </summary>
        /// <param name="attribute">The attribute.</param>
        /// <param name="repository">The EA repository.</param>
        /// <param name="targetElement">The target element for the connector end (supplier).</param>
        /// <param name="connectorType">The connector type.</param>
        /// <returns>The created connector.</returns>
        public static EAAPI.Connector AddConnector(this EAAPI.Attribute attribute, 
                                                   EAAPI.Repository repository, 
                                                   EAAPI.Element targetElement, 
                                                   string connectorType)
        {
            // get the element that contains the attribute
            EAAPI.Element element = repository.GetElementByID(attribute.ParentID);

            // add the connector to the element
            EAAPI.Connector result = element.AddConnector(targetElement, connectorType);

            // set StyleEx of the connector. This will link the connector to the attribute
            result.StyleEx = "LFSP=" + attribute.AttributeGUID + "R;";
            result.Update();

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public static class MethodManipulationExtensions
    {
        public static EAAPI.Element GetClassifier(this EAAPI.Method method, EAAPI.Repository repository)
        {
            EAAPI.Element result = null;

            int classifierID = 0;

            if (int.TryParse(method.ClassifierID, out classifierID))
            {
                if (classifierID != 0)
                {
                    result = repository.GetElementByID(classifierID);
                }
            }

            return result;
        }

        public static string GetClassifierName(this EAAPI.Method method, EAAPI.Repository repository)
        {
            string result = "";

            int classifierID = 0;

            if (int.TryParse(method.ClassifierID, out classifierID))
            {
                if (classifierID != 0)
                {
                    result = repository.GetElementByID(classifierID).Name;
                }
                else
                {
                    result = method.ReturnType;
                }
            }

            return result;
        }
    }
}

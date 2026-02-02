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
    public class ConnectorDescriptor
    {
        public string Type { get; set; } = "";
        public string Subtype { get; set; } = "";
        public string Stereotype { get; set; } = "";
        public string ClientID { get; set; } = "";
        public string SupplierID { get; set; } = "";
        public string DiagramID { get; set; } = "";

        public ConnectorDescriptor(EAAPI.EventProperties preConnectorProps)
        {
            for (int i = 0; i < preConnectorProps.Count; i++)
            {
                EAAPI.EventProperty prop = preConnectorProps.Get(i);
                switch (i)
                {
                    case 0:
                        Type = (string)prop.Value;
                        break;
                    case 1:
                        Subtype = (string)prop.Value;
                        break;
                    case 2:
                        Stereotype = (string)prop.Value;
                        break;
                    case 3:
                        ClientID = (string)prop.Value;
                        break;
                    case 4:
                        SupplierID = (string)prop.Value;
                        break;
                    case 5:
                        DiagramID = (string)prop.Value;
                        break;
                }

            }


        }




    }
}

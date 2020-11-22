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
	public static class ConnectorManipulationExtensions
	{
		public static string GetTaggedValueString(this EAAPI.Connector connector, string tagName)
		{
			string result = "";

			for (int i = 0; i < connector.TaggedValues.Count; i++)
			{
				EAAPI.ConnectorTag tag = (EAAPI.ConnectorTag)connector.TaggedValues.GetAt((short)i);
				if (tag.Name == tagName)
				{
					result = tag.Value;
					break;
				}
			}

			return result;
		}
	}
}

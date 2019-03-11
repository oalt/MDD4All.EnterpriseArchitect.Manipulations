using EAAPI = EA;

namespace MDD4All.EnterpriseArchitect.Manipulations
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

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

        public static void SetTaggedValueString(this EAAPI.Connector connector, string tagName, string value, bool isMemo = false)
        {

            for (short cnt = 0; cnt < connector.TaggedValues.Count; cnt++)
            {
                EAAPI.ConnectorTag tag = connector.TaggedValues.GetAt(cnt) as EAAPI.ConnectorTag;

                if (tag != null && tag.Name == tagName)
                {
                    if (!isMemo)
                    {
                        tag.Value = value;
                    }
                    else
                    {
                        tag.Value = "<memo>*";
                        tag.Notes = value;
                    }
                    tag.Update();
                    connector.TaggedValues.Refresh();
                    return;
                }
            }
            EAAPI.ConnectorTag newTag = connector.TaggedValues.AddNew(tagName, "") as EAAPI.ConnectorTag;
            if (newTag != null)
            {
                if (!isMemo)
                {
                    newTag.Value = value;
                }
                else
                {
                    newTag.Value = "<memo>*";
                    newTag.Notes = value;
                }
                newTag.Update();
            }
            connector.TaggedValues.Refresh();

        }
    }
}

#if FACADE
using EAAPI = MDD4All.EAFacade;
#else
using EAAPI = EA;
#endif

#if EAAPI
namespace MDD4All.EAFacade.Manipulations
#else
namespace MDD4All.EnterpriseArchitect.Manipulations
#endif
{
	/// <summary>
	/// Extension methods for improved EAAPI.Element data manipulation.
	/// </summary>
	public static class ElementManipulationExtensions
	{
		public static string GetClassifierName(this EAAPI.Element element, EAAPI.Repository repository)
		{
			string result = "";

			int classiferID = 0;
			if (element.Type == "Port" || element.Type == "Part" || element.Type == "ActionPin")
			{
				classiferID = element.PropertyType;
			}
			else
			{
				classiferID = element.ClassifierID;
			}

			if (classiferID != 0)
			{
				EAAPI.Element classifierElement = repository.GetElementByID(classiferID);

				if (classifierElement != null)
				{
					result = classifierElement.Name;
				}
			}
			return result;
		}

		public static EAAPI.Element GetClassifier(this EAAPI.Element element, EAAPI.Repository repository)
		{
			EAAPI.Element result = null;

			int classiferID = 0;
			if (element.Type == "Port" || element.Type == "Part" || element.Type == "ActionPin")
			{
				classiferID = element.PropertyType;
			}
			else
			{
				classiferID = element.ClassifierID;
			}
			if (classiferID != 0)
			{
				result = repository.GetElementByID(classiferID);
			}

			return result;
		}

		public static string GetTaggedValueString(this EAAPI.Element modelElement, string tagName)
		{
			string result = "";

			for (short count = 0; count < modelElement.TaggedValues.Count; count++)
			{
				EAAPI.TaggedValue taggedValue = modelElement.TaggedValues.GetAt(count) as EAAPI.TaggedValue;

				if (taggedValue != null && taggedValue.Name == tagName)
				{
					if (!taggedValue.Value.StartsWith("<memo"))
					{
						result = taggedValue.Value;
					}
					else
					{
						result = taggedValue.Notes;
					}
					break;
				}
			}
			return result;
		}

		public static void SetTaggedValueString(this EAAPI.Element modelElement, string tagName, string value, bool isMemo)
		{

			for (short cnt = 0; cnt < modelElement.TaggedValues.Count; cnt++)
			{
				EAAPI.TaggedValue tag = modelElement.TaggedValues.GetAt(cnt) as EAAPI.TaggedValue;

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
					modelElement.TaggedValues.Refresh();
					return;
				}
			}
			EAAPI.TaggedValue newTag = modelElement.TaggedValues.AddNew(tagName, "") as EAAPI.TaggedValue;
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
			modelElement.TaggedValues.Refresh();

		}
	}
}

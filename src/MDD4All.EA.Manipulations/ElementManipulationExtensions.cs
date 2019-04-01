using System.Collections.Generic;
using System.Diagnostics;
#if FACADE
using EAAPI = MDD4All.EAFacade;
#else
using EAAPI = EA;
#endif

#if FACADE
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
		public static EAAPI.Element GetParentElement(this EAAPI.Element childElement, EAAPI.Repository repository)
		{
			return repository.GetElementByID(childElement.ParentID);
		}

		public static EAAPI.Element AddEmbeddedElement(this EAAPI.Element parent, EAAPI.Repository repository,
													   string name, string type)
		{
			EAAPI.Element result = null;

			if (string.IsNullOrEmpty(name))
			{
				name = "empty";
			}

			if (parent.Type == "Package")
			{
				EAAPI.Package package = repository.GetPackageByGuid(parent.ElementGUID) as EAAPI.Package;
				result = package.AddElement(name, type);
			}
			else
			{
				EAAPI.Element newElement = (EAAPI.Element)parent.Elements.AddNew(name, type);
				if (!newElement.Update())
				{
					Debug.WriteLine(newElement.GetLastError());
				}
				parent.Elements.Refresh();

				result = newElement;
			}

			return result;
		}

		public static EAAPI.Connector AddConnector(this EAAPI.Element source, EAAPI.Element target, string connetcorType)
		{
			EAAPI.Connector connector = (EAAPI.Connector)source.Connectors.AddNew("", connetcorType);
			connector.SupplierID = target.ElementID;
			connector.Update();

			source.Connectors.Refresh();

			return connector;
		}

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

		public static EAAPI.Method AddMethod(this EAAPI.Element parent, string name, string returnType = "void")
		{
			EAAPI.Method method = (EAAPI.Method)parent.Methods.AddNew(name, returnType); // 2nd parameter == return type
			method.Update();
			parent.Refresh();

			return method;
		}

		public static EAAPI.Attribute AddAttribute(this EAAPI.Element parent, string name, string type)
		{
			EAAPI.Attribute attribute = (EAAPI.Attribute)parent.Attributes.AddNew(name, type);
			attribute.Update();
			parent.Attributes.Refresh();

			return attribute;
		}

		public static void SetBackgroundColor(this EAAPI.Element element, int color)
		{
			element.SetAppearance(1, 0, color);
			element.Update();
		}

		public static void SetBorderColor(this EAAPI.Element element, int color)
		{
			element.SetAppearance(1, 2, color);
			element.Update();
		}

		public static string GetRunStateValue(this EAAPI.Element element, string variableName)
		{
			string result = "";

			Dictionary<string, ObjectRunState> runStates = ParseRunStateString(element.RunState);

			if (runStates.ContainsKey(variableName))
			{
				result = runStates[variableName].Value;
			}

			return result;
		}

		public static ObjectRunState GetRunStateByName(this EAAPI.Element element, string variableName)
		{
			ObjectRunState result = null;

			Dictionary<string, ObjectRunState> runStates = ParseRunStateString(element.RunState);

			if (runStates.ContainsKey(variableName))
			{
				result = runStates[variableName];
			}

			return result;
		}

		public static void SetRunStateValue(this EAAPI.Element element, string variableName, string value, string operation)
		{
			if (variableName != null && variableName != "")
			{
				Dictionary<string, ObjectRunState> runStates = ParseRunStateString(element.RunState);

				if (value != null && value != "")
				{

					if (runStates.ContainsKey(variableName))
					{
						runStates[variableName].Value = value;
						runStates[variableName].Operator = operation;
					}
					else
					{
						ObjectRunState runState = new ObjectRunState();
						runState.Name = variableName;
						runState.Value = value;
						runState.Operator = operation;
						runStates.Add(variableName, runState);
					}

				}
				else
				{
					if (runStates.ContainsKey(variableName))
					{
						runStates.Remove(variableName);

					}
				}
				element.RunState = CreateRunStateString(runStates);
				element.Update();
			}
		}


		public static List<ObjectRunState> GetObjectRunStates(this EAAPI.Element element)
		{
			List<ObjectRunState> result = new List<ObjectRunState>();

			Dictionary<string, ObjectRunState> runStates = ParseRunStateString(element.RunState);

			foreach (KeyValuePair<string, ObjectRunState> keyValuePair in runStates)
			{
				ObjectRunState runState = keyValuePair.Value;
				result.Add(runState);
			}

			return result;
		}


		private static Dictionary<string, ObjectRunState> ParseRunStateString(string runStateString)
		{
			Dictionary<string, ObjectRunState> result = new Dictionary<string, ObjectRunState>();

			char[] semikolonSeparator = { ';' };

			string[] tokens = runStateString.Split(semikolonSeparator);

			ObjectRunState runState = null;

			foreach (string token in tokens)
			{
				if (token == "@VAR")
				{
					runState = new ObjectRunState();
				}
				else if (token.StartsWith("Variable="))
				{
					runState.Name = token.Substring(9);
				}
				else if (token.StartsWith("Value="))
				{
					runState.Value = token.Substring(6);
				}
				else if (token.StartsWith("Op="))
				{
					runState.Operator = token.Substring(3);
				}
				else if (token.StartsWith("Note="))
				{
					runState.Notes = token.Substring(5);
				}
				else if (token == "@ENDVAR")
				{
					result.Add(runState.Name, runState);
				}
			}

			return result;

		}


		private static string CreateRunStateString(Dictionary<string, ObjectRunState> runStates)
		{
			string result = "";

			foreach (KeyValuePair<string, ObjectRunState> o in runStates)
			{
				ObjectRunState runState = o.Value;

				result += "@VAR;";
				result += "Variable=" + runState.Name + ";";
				result += "Value=" + runState.Value + ";";
				if (runState.Notes != "")
				{
					result += "Notes=" + runState.Notes + ";";
				}
				result += "Op=" + runState.Operator + ";";
				result += "@ENDVAR;";

			}

			return result;
		}

	}
}

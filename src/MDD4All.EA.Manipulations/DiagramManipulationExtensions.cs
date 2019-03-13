using System.Diagnostics;
using EAAPI = EA;

namespace MDD4All.EnterpriseArchitect.Manipulations
{
	public static class DiagramManipulationExtensions
	{
		/// <summary>
		/// Add a model element to a diagram.
		/// </summary>
		/// <param name="diagram">The diagram.</param>
		/// <param name="element">The element to add.</param>
		public static void AddElement(this EAAPI.Diagram diagram, EAAPI.Element element)
		{
			for (short i = 0; i < diagram.DiagramObjects.Count; i++)
			{
				EAAPI.DiagramObject existingDiagramObject = diagram.DiagramObjects.GetAt(i) as EAAPI.DiagramObject;
				if (existingDiagramObject.ElementID == element.ElementID)
				{
					// Element still on diagram, return
					return;
				}
			}

			EAAPI.DiagramObject diagramObject = (EAAPI.DiagramObject)diagram.DiagramObjects.AddNew("", "");

			diagramObject.ElementID = element.ElementID;

			if (!diagramObject.Update())
			{
				Debug.WriteLine(diagramObject.GetLastError());
			}
			diagram.Update();
		}

		/// <summary>
		/// Get the DiagramObject for the given Element
		/// </summary>
		/// <param name="diagram">The diagram to search for the DiagramObject</param>
		/// <param name="element">The element.</param>
		/// <returns>The diagram object or null if not found.</returns>
		public static EAAPI.DiagramObject GetDiagramObjectForElement(this EAAPI.Diagram diagram, EAAPI.Element element)
		{
			EAAPI.DiagramObject result = null;

			for(short counter = 0; counter < diagram.DiagramObjects.Count; counter++)
			{
				EAAPI.DiagramObject diagramObject = diagram.DiagramObjects.GetAt(counter) as EAAPI.DiagramObject;

				if(diagramObject.ElementID == element.ElementID)
				{
					result = diagramObject;
					break;
				}
			}

			return result;
		}
	}
}

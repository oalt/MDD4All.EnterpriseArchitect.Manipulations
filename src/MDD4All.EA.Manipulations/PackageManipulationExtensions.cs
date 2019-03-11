using System.Diagnostics;

using EAAPI = EA;

namespace MDD4All.EnterpriseArchitect.Manipulations
{
	public static class PackageManipulationExtensions
	{

		public static EAAPI.Package AddChildPackage(EAAPI.Package parentPackage, string name)
		{
			// avoid null value for package name
			string packageName = "_UNDEFINED_";
			if (name != null)
			{
				packageName = name;
			}

			EAAPI.Package package = (EAAPI.Package)parentPackage.Packages.AddNew(packageName, "Nothing");

			if (!package.Update())
			{
				Debug.WriteLine(package.GetLastError());
			}
			parentPackage.Packages.Refresh();

			return package;
		}

		public static EAAPI.Element AddElement(this EAAPI.Package parentPackage, string name, string type)
		{
			EAAPI.Element newElement = (EAAPI.Element)parentPackage.Elements.AddNew(name, type);

			newElement.Update();
			parentPackage.Elements.Refresh();
			parentPackage.Element.Refresh();

			return newElement;
		}

		public static EAAPI.Diagram AddDiagram(this EAAPI.Package package, string diagramType)
		{
			EAAPI.Diagram diagram = (EA.Diagram)package.Diagrams.AddNew(package.Name, diagramType);

			diagram.ShowDetails = 0;

			if (!diagram.Update())
			{
				Debug.WriteLine(diagram.GetLastError());
			}
			if (!package.Update())
			{
				Debug.WriteLine(package.GetLastError());
			}

			return diagram;
		}
	}
}

using System.Collections.Generic;
using System.Diagnostics;

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
    public static class PackageManipulationExtensions
    {

        public static EAAPI.Package AddChildPackage(this EAAPI.Package parentPackage, string name)
        {
            // avoid null value for package name
            string packageName = "_UNDEFINED_";
            if (!string.IsNullOrEmpty(name))
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
            EAAPI.Diagram diagram = (EAAPI.Diagram)package.Diagrams.AddNew(package.Name, diagramType);

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

        public static EAAPI.Package GetChildPackageByStereotype(this EAAPI.Package parent, string stereotype)
        {
            for (short i = 0; i < parent.Packages.Count; i++)
            {
                EAAPI.Package package = (EAAPI.Package)parent.Packages.GetAt(i);
                if (package.StereotypeEx == stereotype)
                {
                    return package;
                }
            }
            return null;

        }

        public static EAAPI.Package GetChildPackageByName(this EAAPI.Package parent, string name)
        {
            EAAPI.Package result = null;

            for (short i = 0; i < parent.Packages.Count; i++)
            {
                EAAPI.Package package = (EAAPI.Package)parent.Packages.GetAt(i);
                if (package.Name == name)
                {
                    result = package;
                    break;
                }
            }

            return result;
        }

        public static EAAPI.Package GetChildPackageByNameAndStereotype(this EAAPI.Package parent, string name, string stereotype)
        {
            for (short i = 0; i < parent.Packages.Count; i++)
            {
                EAAPI.Package p = (EAAPI.Package)parent.Packages.GetAt(i);
                if (p.StereotypeEx == stereotype && p.Name == name)
                {
                    return p;
                }
            }
            return null;
        }

        public static List<EAAPI.Package> GetChildPackagesByStereotype(this EAAPI.Package parent, string stereotype)
        {
            List<EAAPI.Package> result = new List<EAAPI.Package>();

            for (short i = 0; i < parent.Packages.Count; i++)
            {
                EAAPI.Package package = (EAAPI.Package)parent.Packages.GetAt(i);
                if (package.StereotypeEx == stereotype)
                {
                    result.Add(package);
                }
            }
            return result;
        }

        public static EAAPI.Package GetModelPackage(this EAAPI.Package package, EAAPI.Repository repository)
        {
            EAAPI.Package result = package;

            while (result.ParentID != 0)
            {
                result = repository.GetPackageByID(result.ParentID);
            }
            return result;

        }

        public static EAAPI.Element AddStartNode(this EAAPI.Package parent)
        {
            EAAPI.Element result = (EAAPI.Element)parent.Elements.AddNew("Start", "StateNode");

            result.Subtype = 100;

            result.Update();

            parent.Elements.Refresh();
            parent.Element.Refresh();

            return result;
        }

        public static EAAPI.Element AddEndNode(this EAAPI.Package parent)
        {
            EAAPI.Element result = (EAAPI.Element)parent.Elements.AddNew("End", "StateNode");

            result.Subtype = 101;

            result.Update();

            parent.Elements.Refresh();
            parent.Element.Refresh();
            return result;
        }

        public static bool SuppressNamespace(this EAAPI.Package package)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(package.Flags) && package.Flags.Contains("SNSP=true"))
            {
                result = true;
            }
            return result;
        }
    }
}

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
	public static class RepositoryManipulationExtensions
	{
		public static void DeletePackage(this EAAPI.Repository repository, EAAPI.Package package)
		{
			EAAPI.Package parent = repository.GetPackageByID(package.ParentID);

			for (int i = 0; i < parent.Packages.Count; i++)
			{
				EAAPI.Package actP = (EAAPI.Package)parent.Packages.GetAt((short)i);
				if (actP.PackageID == package.PackageID)
				{
					parent.Packages.Delete((short)i);
					break;
				}
			}
			parent.Update();
			//Repository.RefreshModelView(parent.PackageID);
		}

		public static EAAPI.Package GetPackageForElement(this EAAPI.Repository repository, EAAPI.Element element)
		{
			return repository.GetPackageByID(element.PackageID);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name=""></param>
		/// <param name="repository"></param>
		/// <param name="element"></param>
		/// <returns>The package ID.</returns>
		public static int DeleteElement(this EAAPI.Repository repository, EAAPI.Element element)
		{
			EAAPI.Package parentPackage = repository.GetPackageForElement(element);

			for (int i = 0; i < parentPackage.Elements.Count; i++)
			{
				EAAPI.Element el = (EAAPI.Element)parentPackage.Elements.GetAt((short)i);
				if (el.ElementID == element.ElementID)
				{
					parentPackage.Elements.Delete((short)i);
					break;
				}
			}
			parentPackage.Elements.Refresh();
			parentPackage.Update();

			return parentPackage.PackageID;
		}

		public static void DeleteConnector(this EAAPI.Repository repository, EAAPI.Connector con)
		{
			EAAPI.Element element = repository.GetElementByID(con.ClientID);

			short index = -1;
			for (short i = 0; i < element.Connectors.Count; i++)
			{
				EAAPI.Connector c = element.Connectors.GetAt(i) as EAAPI.Connector;
				if (c.ConnectorID == con.ConnectorID)
				{
					index = i;
					break;
				}
			}

			if (index != -1)
			{
				element.Connectors.Delete(index);
				element.Connectors.Refresh();
			}

		}
	}
}

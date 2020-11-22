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
	public class ObjectRunState
	{
		public string Name { get; set; }

		public string Value { get; set; }

		public string Operator { get; set; }

		public string Notes { get; set; }
	}
}

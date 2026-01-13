using CargoMISMobileAPI.Models;

namespace CargoMISMobileAPI.Interface
{
	public interface IRoutes
	{

		  Task<string> GetRouteNameById(string RouteId);
		   Task<List<BranchModel>> GetRouteDetailById(String branchId);

	}
}

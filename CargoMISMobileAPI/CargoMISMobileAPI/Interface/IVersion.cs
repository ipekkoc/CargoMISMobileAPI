

namespace CargoMISMobileAPI.Interface
{
    public interface IVersions
    {

		Task<string> GetVersions(string Modul);
		
	}
}

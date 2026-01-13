using CargoMISMobileAPI.Models;


namespace CargoMISMobileAPI.Interface
{
	public interface ITerminal
	{
		 Task<string> TerminalRegister(TerminalRegisterRequest terminalRegisterRequest);
	}


	
}

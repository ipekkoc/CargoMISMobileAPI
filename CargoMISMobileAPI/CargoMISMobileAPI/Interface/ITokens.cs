using CargoMISMobileAPI.Models;


namespace CargoMISMobileAPI.Interface
{
	public interface ITokens
	{

		 Task<TokenUserResponse> GetToken(TokenUserRequest tokenUserRequest);
	}


	
}

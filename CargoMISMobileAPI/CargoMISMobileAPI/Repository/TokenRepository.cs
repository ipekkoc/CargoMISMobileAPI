using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargoMISMobileAPI.Repository
{
	public class TokenRepository : ITokens
	{
		public IConfiguration _configuration;
		private readonly DatabaseContext _context;

		public TokenRepository(IConfiguration config, DatabaseContext context)
		{
			_configuration = config;
			_context = context;
		}
		
		public async Task<TokenUserResponse> GetToken(TokenUserRequest _tokenUserRequest)
		{
			var tokenUserResponse = new TokenUserResponse();
			if (_tokenUserRequest.Kod != null && _tokenUserRequest.Sifre != null)
			{
				var user = await GetUser(_tokenUserRequest.Kod, _tokenUserRequest.Sifre);
				
				if (user != null)
				{
					//create claims details based on the user information
					var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
						new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
						new Claim("Ref", user.Ref.ToString()),
						new Claim("Adi", user.Adi),
						new Claim("Role", user.Gorev),
						new Claim("Lokasyon", user.Lokasyon),
						new Claim("LokasyonKodu", user.LokasyonKodu)
						

					};

					var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
					var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var token = new JwtSecurityToken(
						_configuration["Jwt:Issuer"],
						_configuration["Jwt:Audience"],
						claims,
						expires: DateTime.UtcNow.AddMinutes(600),
						signingCredentials: signIn);

					tokenUserResponse.BranchName = user.Lokasyon.Trim();
					tokenUserResponse.UserName = user.Adi.Trim();
					tokenUserResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
					tokenUserResponse.UserRole = user.Gorev.Trim();


					return tokenUserResponse;
				}
				else
				{
					return tokenUserResponse;
				}
			}
			else
			{
				return tokenUserResponse;
			}
		}
		private async Task<UserInfo> GetUser(string kod, string sifre)
		{
			var useri= await _context.UserInfos.FirstOrDefaultAsync(u => u.Kod == kod && u.Sifre == sifre && u.Modul == "terminal") ;
		
				return useri;
			
			
		}	
	}


}

using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CargoMISMobileAPI.Repository
{
	public class TerminalRepository : ITerminal
	{
		public IConfiguration _configuration;
		private readonly DatabaseContext _context;
		private string? cnnStr;


		public TerminalRepository(IConfiguration config, DatabaseContext context)
		{
			_configuration = config;
			_context = context;
			cnnStr = _configuration.GetConnectionString("dbConnection");
			
		}
		
		public async Task<string> TerminalRegister(TerminalRegisterRequest terminalRegisterRequest)
		{
			
			if (terminalRegisterRequest.seri_no != null)
			{
				var isTerminal = await GetTerminal(terminalRegisterRequest.seri_no);
				
				if (isTerminal != null)
				{
					using (IDbConnection cnn = new SqlConnection(cnnStr))
					{
						cnn.Open();
						await cnn.ExecuteAsync("update terminal set IslemTarihi='" + DateTime.Now + "',Kullanici='" + terminalRegisterRequest.kullanici + "',Sube='" + terminalRegisterRequest.sube + "' where SeriNo='" + terminalRegisterRequest.seri_no + "'");
						cnn.Close();
					}
					return ("TermUpdate");
				}
				else
				{
					using (IDbConnection cnn = new SqlConnection(cnnStr))
					{
						cnn.Open();
						await cnn.ExecuteAsync("INSERT INTO terminal (Model,SeriNo,Sube,IslemTarihi,Kullanici) values ('" + terminalRegisterRequest.model + "','" + terminalRegisterRequest.seri_no + "','" + terminalRegisterRequest.sube + "','" + DateTime.Now + "','" + terminalRegisterRequest.kullanici + "')");
						cnn.Close();
					}
					return ("TermInsert");
				}
			}
			return "ok";
		}
		private async Task<TerminalModel>? GetTerminal(string seri_no)
		{
			var terminals= await _context.Terminals.FirstOrDefaultAsync(u => u.SeriNo == seri_no) ;
		
				return terminals;
			
			
		}	
	}


}

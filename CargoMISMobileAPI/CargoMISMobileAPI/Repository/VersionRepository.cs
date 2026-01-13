using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CargoMISMobileAPI.Repository
{
	
	public class VersionRepository: IVersions
	{
		private string cnnStr;
		private IConfiguration _configuration;
		public VersionRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<string> GetVersions(string Modul)
		{
			cnnStr = _configuration.GetConnectionString("dbConnection");
			using (IDbConnection cnn = new SqlConnection(cnnStr))
			{
				cnn.Open();
				//var routeRef = cnn.Query<RouteResponse>("select ref as RouteId,hat_adi as RouteName from hat where ref=").FirstOrDefault();
				string routeName = cnn.Query<string>("select versiyon from versiyon where modul='" + Modul + "'").FirstOrDefault();
				cnn.Close();
				return routeName;

			}
		}

		
	}
}

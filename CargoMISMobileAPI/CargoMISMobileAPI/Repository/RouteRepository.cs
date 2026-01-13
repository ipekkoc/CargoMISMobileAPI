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
	
	public class RouteRepository : IRoutes
	{
		private string? cnnStr;
		private IConfiguration _configuration;
		public RouteRepository(IConfiguration configuration)
		{
			_configuration = configuration;
		}
	
		
	
		public async Task<List<BranchModel>> GetRouteDetailById(String branchId)
		{
	
			cnnStr = _configuration.GetConnectionString("dbConnection");
			using (IDbConnection cnn = new SqlConnection(cnnStr))
			{
				cnn.Open();
				//var routeRef = cnn.Query<RouteResponse>("select ref as RouteId,hat_adi as RouteName from hat where ref=").FirstOrDefault();
				List<BranchModel> branchModel = cnn.Query<BranchModel>("select ref as BranchId,sube_adi as BranchName from sube where tm_kod in (select tm_kod from sube where ref = " + branchId + ")").ToList();

				return branchModel;
			}
		}


		public async Task<string?> GetRouteNameById(string RouteId)
		{
			cnnStr = _configuration.GetConnectionString("dbConnection");
			using (IDbConnection cnn = new SqlConnection(cnnStr))
			{
				cnn.Open();
				//var routeRef = cnn.Query<RouteResponse>("select ref as RouteId,hat_adi as RouteName from hat where ref=").FirstOrDefault();
				string? routeName = cnn.Query<string>("select sube_adi as RouteName from sube where ref= " + RouteId).FirstOrDefault();
				cnn.Close();
				return routeName;

			}
		}

	
	}
}

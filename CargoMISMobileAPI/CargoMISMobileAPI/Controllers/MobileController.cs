
using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using CargoMISMobileAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using Elasticsearch;

namespace CargoMISMobileAPI.Controllers
{
	
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]

	public class MobileController : ControllerBase
	{
		private readonly IBarcodes _IBarcode;
		private readonly IRoutes _IRoute;
		private readonly IVersions _IVersion;
		private readonly ILogger<MobileController> _Logger;
		private readonly ITerminal _Terminal;
		
	
		
		public MobileController(IBarcodes barcodes, IRoutes routes,IVersions version, ILogger<MobileController> logger,ITerminal terminal)
		{
			_IBarcode = barcodes;
			_IRoute = routes;
			_IVersion = version;
			_Terminal = terminal;
			_Logger = (ILogger<MobileController>?)logger;
			
		}

		[HttpPost]
		[Route("Barcode")]
		public async Task<string> GetStringAsync (List<BarcodeModel> barcode)
		{
			string listOfStrings = "";
			try
			{
				_IBarcode.AddBarcode(barcode);



				foreach (object myObject in barcode)
				{
					string json = JsonConvert.SerializeObject(myObject);
					listOfStrings = listOfStrings + json + ",";
				}

				listOfStrings = listOfStrings.Replace("\"", "");

				_Logger.LogInformation("Crgms-" + listOfStrings, DateTime.UtcNow.ToString());

		

				return "Tamam oldu";
			}
			catch (Exception e)
			{


				


				foreach (object myObject in barcode)
				{
					string json = JsonConvert.SerializeObject(myObject);
					listOfStrings = listOfStrings + json + ",";
				}

				listOfStrings= listOfStrings.Replace("\"","");

				_Logger.LogInformation("Crgms-" + listOfStrings, DateTime.UtcNow.ToString());

				throw;
			}
			
			
		
			


		}


		[HttpPost]
		[Route("GetRouteNamebyId")]
		public async Task<ActionResult<string>> GetActionAsync(string RouteId)
		{

			string routeResponse = await _IRoute.GetRouteNameById(RouteId);
			if (routeResponse == null)
			{
				return BadRequest("Hat bulunamadı");
			}
			else
			{
				return Ok(routeResponse);
			}


		}


		[HttpPost]
		[Route("GetVersion")]
		public async Task<ActionResult<string>> GetVersion(string Modul)
		{

			_Logger.LogInformation("Crgms-" + Modul, DateTime.UtcNow.ToString());
			var versionResponse = await _IVersion.GetVersions(Modul);
			if (versionResponse == null)
			{
				return BadRequest("Uygulama bulunamadı");
			}
			else
			{
				return Ok(versionResponse);
			}


		}

		[HttpPost]
		[Route("GetRouteDetailById")]
		public async Task<ActionResult<List<BranchModel>>> GetAsync(String branchId)
		{

			var routeResponse = await _IRoute.GetRouteDetailById(branchId);
			if (routeResponse == null)
			{
				return BadRequest("Hat bulunamadı");
			}
			else
			{
				return Ok(routeResponse);
			}


		}

		[HttpPost]
		[Route("TerminalRegister")]
		public async Task<string> Terminal(TerminalRegisterRequest terminalRegisterRequest)
		{

			await _Terminal.TerminalRegister(terminalRegisterRequest);

			return "TerminalRegOK";

		}





	}
}

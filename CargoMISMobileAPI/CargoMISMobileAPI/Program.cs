using CargoMISMobileAPI.Interface;
using CargoMISMobileAPI.Models;
using CargoMISMobileAPI.Repository;
using CargoMISMobileAPI.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;






internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		

		builder.Services.AddDbContext<DatabaseContext>
			(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));
		builder.Services.AddTransient<IBarcodes, BarcodeRepository>();
		builder.Services.AddTransient<IRoutes, RouteRepository>();
		builder.Services.AddTransient<IVersions, VersionRepository>();
		builder.Services.AddTransient<ITerminal, TerminalRepository>();


		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();


		//configureLogging();
		builder.Host.UseSerilog();


		//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo
			{
				Title = "CargoMISMobile Api",
				Version = "v1"
			});
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = "",
			});
			c.AddSecurityRequirement(new OpenApiSecurityRequirement {
		{
			new OpenApiSecurityScheme {
				Reference = new OpenApiReference {
					Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
				}
			},
			new string[] {}
		}
			});
		});




		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
		{
			options.RequireHttpsMetadata = false;
			options.SaveToken = true;
			options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidAudience = builder.Configuration["Jwt:Audience"],
				ValidIssuer = builder.Configuration["Jwt:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
			};
		});


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		//if (app.Environment.IsDevelopment())
		//{
			app.UseSwagger();
			app.UseSwaggerUI();
		//}

		//app.UseHttpsRedirection();

		app.UseAuthentication();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();

		void configureLogging()
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile(
					$"appsettings.{environment}.json", optional: true
				).Build();
			Log.Logger = new LoggerConfiguration()
				.Enrich.FromLogContext()
				.Enrich.WithExceptionDetails()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
				.Enrich.WithProperty("Environment", environment)
				.ReadFrom.Configuration(configuration)
				.CreateLogger();
		}

		ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
		{
			return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace('.', '-')}-{environment.ToLower()}-{DateTime.UtcNow:yyyy-MM}"
			};
		}




	}
}


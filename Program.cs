using ElearingEnglis.DataCon;
using ElearingEnglis.services.Appoinment;
using ElearingEnglis.services.Doctor;
using ElearingEnglis.services.Drug;
using ElearingEnglis.services.JWT;
using ElearingEnglis.services.Lgoin;
using ElearingEnglis.services.Patient;
using ElearingEnglis.services.Rgistration;
using ElearingEnglis.services.VitalSignMaster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DBCon>(options =>
    options.UseMySql( connectionString,ServerVersion.AutoDetect(connectionString))
            .LogTo(Console.WriteLine, LogLevel.Information) // Prints SQL to console
           .EnableSensitiveDataLogging()
    );
    
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DBCon>();
builder.Services.AddControllers();

builder.Services.AddScoped<IJWTModule, JWTModule>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new JWTModule(
        config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration."),
        config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found in configuration."),
        config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found in configuration."),
        int.Parse(config["Jwt:ExpireMinutes"] ?? throw new InvalidOperationException("JWT ExpireMinutes not found in configuration."))
    );
}); 

builder.Services.AddScoped<IRgistration, Rgistration>();
builder.Services.AddScoped<ILogin, Login>();
builder.Services.AddScoped<IPatient,SPatient>();
builder.Services.AddScoped<IAppoinment,SAppoinment>();
builder.Services.AddScoped<IDoctor,SDoctor>();
builder.Services.AddScoped<IDrugImportService, DrugImportService>();
builder.Services.AddScoped<IDrugService, DrugService>();
builder.Services.AddScoped<IVitalSignMasterRepo,VitalSignMasterRepo>();
builder.Services.AddScoped<IVitalSignMasterService,VitalSignMasterService>();
builder.Services.AddScoped<IDrugRepo, DrugRepo>();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
     o.RequireHttpsMetadata = false;
     o.SaveToken = true;
     o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration."))
        )
    };
});



var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{

    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });


});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();  
var app = builder.Build();

app.MapOpenApi();
app.UseSwagger();   
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Learning English API v1");
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
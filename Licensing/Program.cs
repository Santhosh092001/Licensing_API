using Licensing.DBContext;
using Licensing.Filters;
using Licensing.IRepositories;
using Licensing.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(configuration =>
{
    configuration.Filters.Add(typeof(CommonExeptionFilter));
});


#region

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Auidence"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateAudience = false,
        };
    });

builder.Services.AddAuthorization();

#endregion


// Add services to the container.
builder.Services.AddCors(options => { options.AddPolicy("cors", a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });

var connectionStrings = builder.Configuration.GetConnectionString("Defaultconnections");
builder.Services.AddDbContext<LDbContext>(options => options.UseSqlServer("server=192.168.0.28,1434;database=IWT-Licensing;user=dev1;password=welcome@123;"));



builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductCustomerMapRepository, ProductCustomerMapRepository>();
builder.Services.AddScoped<IGenerateKeyRepository, GenerateKeyRepository>();

#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

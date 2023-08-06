using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using online_selling.Dto;
using online_selling.Infrastructure;
using online_selling.Infrastructure.Configurations;
using online_selling.Interfaces.Authentication;
using online_selling.Interfaces.DataInitialization;
using online_selling.Interfaces.Items;
using online_selling.Interfaces.Orders;
using online_selling.Interfaces.PendingUsers;
using online_selling.Interfaces.Repository;
using online_selling.Interfaces.UnitOfWork;
using online_selling.Interfaces.Users;
using online_selling.Mapping;
using online_selling.Models;
using online_selling.Services.Authentication;
using online_selling.Services.DataInitialization;
using online_selling.Services.Items;
using online_selling.Services.Orders;
using online_selling.Services.PendingUsers;
using online_selling.Services.Repository;
using online_selling.Services.UnitOfWork;
using online_selling.Services.Users;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string _cors = "cors";


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dtos
builder.Services.AddScoped<LoginDto>();
builder.Services.AddScoped<RegisterDto>();
builder.Services.AddScoped<MessageDto>();
builder.Services.AddScoped<PendingDto>();
builder.Services.AddScoped<RegisterGoogleDto>();


//Interfaces
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<PendingUser>, GenericRepository<PendingUser>>();
builder.Services.AddScoped<IGenericRepository<Item>, GenericRepository<Item>>();
builder.Services.AddScoped<IGenericRepository<Order>, GenericRepository<Order>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPendingUserRepository, PendingUserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPendingUserService, PendingUserService>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IUserInitialization, UserInitialization>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


//Classes
builder.Services.AddScoped<User>();
builder.Services.AddScoped<PendingUser>();


//policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy => policy.RequireRole("admin"));
    options.AddPolicy("seller", policy => policy.RequireRole("seller"));
    options.AddPolicy("buyer", policy => policy.RequireRole("buyer"));
});


//AutoMapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new UserMappingProfile());
    mc.AddProfile(new ItemMappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


//Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
             .AddJwtBearer("Auth-Access-Token", options =>
             {
                 var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretAccessKey"]));
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         if (context.Request.Headers.TryGetValue("Auth-Access-Token", out var token))
                         {
                             context.Token = token;
                         }
                         return Task.CompletedTask;
                     }
                 };
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "http://localhost:7020",
                     IssuerSigningKey = key
                 };
             })
             .AddJwtBearer("Auth-Refresh-Token", options =>
             {
                 var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SecretRefreshKey"]));
                 options.Events = new JwtBearerEvents
                 {
                     OnMessageReceived = context =>
                     {
                         if (context.Request.Headers.TryGetValue("Auth-Refresh-Token", out var token))
                         {
                             context.Token = token;
                         }
                         return Task.CompletedTask;
                     }
                 };
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = "http://localhost:7020",
                     IssuerSigningKey = key
                 };
             });
             //.AddGoogle(options =>
             //{
             //    options.ClientId = "153217510821-8nm0cbg02q4mtcapqtlcfvnk1qeh4eke.apps.googleusercontent.com";
             //    options.ClientSecret = "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";
             //}); 


builder.Services.AddControllers();

/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "online-selling", Version = "v1" });
    // Add JWT authentication to Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{}
                }
            });
});
*/

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _cors, builder => {
        builder.WithOrigins("http://localhost:3000")//Ovde navodimo koje sve aplikacije smeju kontaktirati nasu,u ovom slucaju nas Angular front
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials()
               .WithExposedHeaders("Auth-Access-Token", "Auth-Refresh-Token");
    });
});


//Connection string with Database
builder.Services.AddDbContext<OnlineShopDbContext>(options=>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OnlineShopContext"))
);


var app = builder.Build();

//data initialization
var scope = app.Services.CreateScope();
var service = scope.ServiceProvider.GetService<IUserInitialization>();
service.UserInitialization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "online-selling v1"));
}


app.UseHttpsRedirection();
app.UseCors(_cors);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

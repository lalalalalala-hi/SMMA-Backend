using AMMAAPI.Database;
using AMMAAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.Configure<AMMADatabaseSettings>(settings =>
{
    settings.ConnectionString = Environment.GetEnvironmentVariable("AMMADatabase__ConnectionString");
    settings.DatabaseName = Environment.GetEnvironmentVariable("AMMADatabase__DatabaseName");
    settings.CollectionName = new CollectionName
    {
        Category = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Category"),
        Event = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Event"),
        Store = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Store"),
        User = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__User"),
        Floor = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Floor"),
        Promotion = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Promotion"),
        Routes = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Routes"),
        Location = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__Location"),
        UserRoute = Environment.GetEnvironmentVariable("AMMADatabase__CollectionName__UserRoute")
    };
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = Environment.GetEnvironmentVariable("JwtSettings__Issuer"),
            ValidAudience = Environment.GetEnvironmentVariable("JwtSettings__Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtSettings__Key"))),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<CategoryService>();
builder.Services.AddSingleton<EventsService>();
builder.Services.AddSingleton<FloorService>();
builder.Services.AddSingleton<PromotionService>();
builder.Services.AddSingleton<StoreService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<RoutesService>();
builder.Services.AddSingleton<LocationService>();
builder.Services.AddSingleton<UserRouteService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://smma-eight.vercel.app")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    var settings = serviceProvider.GetRequiredService<IOptions<AMMADatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

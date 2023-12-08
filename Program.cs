using Exercise.DataBase;
using Exercise.Respo.Interface;
using Exercise.Respo.Repo;
using Exercise.Service.Interface;
using Exercise.Service.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApi.TokenConfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//SQL database
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<DBContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DB")));


builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IEmpoylee, EmpoyleeSevice>();
builder.Services.AddScoped<IRepo, Repo>();
builder.Services.AddScoped<Token>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});


//Token Expire Time
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],   // Lấy giá trị Issuer từ cấu hình
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        // Replace "Jwt:Token256" with the correct configuration key for your token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token256"]))
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "authenticationToken"; // Tên của cookie
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login"; // Đường dẫn đến trang đăng nhập
});

//.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins", builder =>
    {
        builder.WithOrigins("http://172.16.11.42:8080/")
            .AllowAnyHeader()
            .WithMethods("POST", "GET", "OPTIONS")
            .AllowCredentials();
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

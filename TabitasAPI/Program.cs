using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TabitasAPI.Data;
using TabitasAPI.DTOs;
using TabitasAPI.Mappers;
using TabitasAPI.Models;
using TabitasAPI.Repository;
using TabitasAPI.Repository.IRepository;
using TabitasAPI.Services;
using TabitasAPI.Services.IServices;
using XAct.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
//Inyeccion del conetexto para que se utlice en cualquier clase las operacion para la base de datos
builder.Services.AddDbContext<TabitasContext>(conexion =>
{
    conexion.UseSqlServer(builder.Configuration.GetConnectionString("TabitasConnection"));
});

//Soporte para cache
builder.Services.AddResponseCaching();

//inyeccion de la capa services de interfaces y sera por key para identificarla y llamarla asi cuando se necesite 
//con la key asi se mandara a llamar para que se pueda reulizar la interface con otras 
builder.Services.AddKeyedScoped<IGeneralServices<GeneralDTO, GeneralInsertDTO, GeneralUpdateDTO>, GeneralServices>("generalServices");
builder.Services.AddKeyedScoped<IProcesoActualServices<ProcesoActualDTO>, ProcesoActualServices>("procesoActualServices");
builder.Services.AddKeyedScoped<IAlmacenServices<AlmacenDTO, AlmacenInsertDTO, AlmacenUpdateDTO>, AlmacenServices>("almacenServices");
builder.Services.AddKeyedScoped<IEtiquetaServices<EtiquetaDTO, EtiquetaInsertDTO, EtiquetaUpdateDTO>, EtiquetaServices>("etiquetaServices");
builder.Services.AddKeyedScoped<ICorteServices<CorteDTO, CorteInsertDTO, CorteUpdateDTO>, CorteServices>("corteServices");
builder.Services.AddKeyedScoped<ICorteLaserServices<CorteLaserDTO, CorteLaserInsertDTO, CorteLaserUpdateDTO>, CorteLaserServices>("corteLaserServices");
builder.Services.AddKeyedScoped<ISerigrafiaServices<SerigrafiaDTO, SerigrafiaInsertDTO, SerigrafiaUpdateDTO>, SerigrafiaServices>("serigrafiaServices");
builder.Services.AddKeyedScoped<IBordadoServices<BordadoDTO, BordadoInsertDTO, BordadoUpdateDTO>, BordadoServices>("bordadoServices");
builder.Services.AddKeyedScoped<IMaquilaServices<MaquilaDTO, MaquilaInsertDTO, MaquilaUpdateDTO>, MaquilaServices>("maquilaServices");
builder.Services.AddKeyedScoped<ILavadoServices<LavadoDTO, LavadoInsertDTO, LavadoUpdateDTO>, LavadoServices>("lavadoServices");
builder.Services.AddKeyedScoped<ICalidadServices<CalidadDTO, CalidadInsertDTO, CalidadUpdateDTO>, CalidadServices>("calidadServices");
builder.Services.AddKeyedScoped<ITerminadoServices<TerminadoDTO, TerminadoInsertDTO, TerminadoUpdateDTO>, TerminadoServices>("terminadoServices");

//Inyeccion Repository
builder.Services.AddScoped<IGeneralRepository<General>, GeneralRepository>();
builder.Services.AddScoped<IProcesoActualRepository<ProcesoActual>, ProcesoActualRepository>();
builder.Services.AddScoped<IAlmacenRepository<Almacen>, AlmacenRepository>();
builder.Services.AddScoped<IEtiquetaRepository<Etiqueta>, EtiquetaRepository>();
builder.Services.AddScoped<ICorteRepository<Corte>, CorteRepository>();
builder.Services.AddScoped<ICorteLaserRepository<CorteLaser>, CorteLaserRepository>();
builder.Services.AddScoped<ISerigrafiaRepository<Serigrafia>, SerigrafiaRepository>();
builder.Services.AddScoped<IBordadoRepository<Bordado>, BordadoRepository>();
builder.Services.AddScoped<IMaquilaRepository<Maquila>, MaquilaRepository>();
builder.Services.AddScoped<ILavadoRepository<Lavado>, LavadoRepository>();
builder.Services.AddScoped<ICalidadRepository<Calidad>, CalidadRepository>();
builder.Services.AddScoped<ITerminadoRepository<Terminado>, TerminadoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var key = builder.Configuration.GetValue<string>("ApiSettings:Secreta");

//inyectado de mappers lo que hace typeof es resolver el nombre del tipo que se va a usar en tiempo de compilacion
// Registra los perfiles de AutoMapper en una sola llamada
//Automapper
builder.Services.AddAutoMapper(typeof(MappingDeGeneral), typeof(MappingUsuarios), typeof(MappingDeAlmacen), typeof(MappingDeEtiqueta), typeof(MappingDeCorte),
    typeof(MappingDeCorteLaser), typeof(MappingDeSerigrafia), typeof(MappingDeBordado),typeof(MappingDeCalidad), typeof(MappingDeTerminado), typeof(MappingDeLavado), typeof(MappingDeProcesoActual));

//configuracion autentificacion
builder.Services.AddAuthentication(
    x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
    ).AddJwtBearer(x=>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,ValidateAudience= false
        };
    });

builder.Services.AddControllers();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autentificacion JWT usando el esquema bearer \r\n\r\n",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type =ReferenceType.SecurityScheme,Id="Bearer"
                },
                Scheme = "oauth2",Name="Bearer", In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Soporte para CORS
//Si pueden habilitar 1-un dominio, 2-multiples dominios
//3cualquier dominio(tener en cuenta seguridad)
//usamos de jemplo el dominio https://localhost:7232 se debe cambiar por el correcto
//se usa(*) para todos los dominios
builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
{
    //build.WithOrigins("https://localhost:7232/").AllowAnyMethod().AllowAnyHeader(); //acceso para todos los metodos, encabezados
    build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Soporte para CORS
app.UseCors("PoliticaCors");

//soporte para autentificacion
app.UseAuthorization();
app.UseAuthorization();

app.MapControllers();
//Soporte para archivos estaticos como img
app.UseStaticFiles();


app.Run();

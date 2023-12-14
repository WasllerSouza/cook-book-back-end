using CookBook.Api.Filters;
using CookBook.Api.Middleware;
using CookBook.Application;
using CookBook.Application.Services.AutoMapper;
using CookBook.Domain.Extension;
using CookBook.Infrastructure;
using CookBook.Infrastructure.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(option => option.LowercaseUrls = true);

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(FilterExceptions)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(configuration =>
{
    configuration.AddProfile(new ConfigureAutoMapper());
}).CreateMapper());

#region [Cors]
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UsuarioLogado", policy => policy.Requirements.Add(new UserLoggedInRequiriment()));
});
#endregion

#region [Adiciona atributo para validar se o usuário esta logado]
builder.Services.AddScoped<AuthenticatedUser>();
#endregion

var app = builder.Build();

AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(100));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#region [Cors]
app.UseCors(x => //x.SetIsOriginAllowed(x => x.StartsWith("http:\\localhost:4200"))
    x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
#endregion


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region [Add middleware para escolha de linguagem]
app.UseMiddleware<CultureMiddleware>();
#endregion

UpdateInternalDatabase();

app.Run();

void UpdateInternalDatabase()
{
    var connection = builder.Configuration.getConnectionDatabase();
    var nameDatabase = builder.Configuration.getNameDatabase();

    Database.CreateDatabase(connection, nameDatabase);

    app.MigrateDataBase();
}
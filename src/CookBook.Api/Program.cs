using CookBook.Api.Filters;
using CookBook.Application;
using CookBook.Application.Services.AutoMapper;
using CookBook.Domain.Extension;
using CookBook.Infrastructure;
using CookBook.Infrastructure.Migrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepository(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddMvc(options => options.Filters.Add(typeof(FilterExceptions)));

builder.Services.AddScoped(provider => new AutoMapper.MapperConfiguration(configuration =>
{
    configuration.AddProfile(new ConfigureAutoMapper());
}).CreateMapper());

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
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

UpdateInternalDatabase();

app.Run();

void UpdateInternalDatabase()
{
    var connection = builder.Configuration.getConnectionDatabase();
    var nameDatabase = builder.Configuration.getNameDatabase();

    Database.CreateDatabase(connection, nameDatabase);

    app.MigrateDataBase();
}
using Florama.Api.AutoMapper;
using Florama.Api.Extensions;
using Florama.Api.Filters;
using Florama.Application;
using Florama.Application.AutoMapper;
using Florama.Infra.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ValidationFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.AddService<ValidationFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddPresentation()
    .AddDatabaseSetup(builder.Configuration)
    .AddApplication()
    .AddInfrastructure()
    .AddRepositories();

builder.Services.AddAutoMapper(
    typeof(PresentationMappingProfile).Assembly,
    typeof(ApplicationMappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

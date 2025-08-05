using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PersonAPI_Second.Domain;
using PersonAPI_Second.Features.Behaviours;
using PersonAPI_Second.Features.Filters;
using PersonAPI_Second.Persistence;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

public partial class Program { }

/// This logic was moved to the PersonController class
//app.MapGet("/people/{id:guid}", async (Guid id, ISender mediatr) =>
//{
//    var person = await mediatr.Send(new GetPersonQuery(id));
//    if (person == null) return Results.NotFound();
//    return Results.Ok(person);
//});

//app.MapPatch("/people", async (UpdatePersonCommand command, ISender mediatr) =>
//{
//    var personId = await mediatr.Send(command);
//    if (Guid.Empty == personId) return Results.BadRequest();
//    return Results.Created($"/people/{personId}", new { id = personId });
//});

//app.MapPost("/people", async (CreatePersonCommand command, IMediator mediatr) =>
//{
//    var personId = await mediatr.Send(command);
//    if (Guid.Empty == personId) return Results.BadRequest();
//    await mediatr.Publish(new PersonCreatedNotification(personId));
//    return Results.Created($"/people/{personId}", new { id = personId });
//});

//app.MapDelete("/people/{id:guid}", async (Guid id, ISender mediatr) =>
//{
//    await mediatr.Send(new DeletePersonCommand(id));
//    return Results.NoContent();
//});

//app.MapGet("/people", async (ISender mediatr) =>
//{
//    var people = await mediatr.Send(new ListPersonQuery());
//    return Results.Ok(people);
//});
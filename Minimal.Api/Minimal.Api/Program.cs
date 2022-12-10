using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Minimal.Api.Configurations;
using Minimal.Api.Context;
using Minimal.Api.DTOs;
using Minimal.Api.Entities;
using Minimal.Api.Entities.Mappings;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s => 
{ 
    s.EnableAnnotations(); 
});

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddSingleton<IConfig, Config>();

builder.Services.AddTransient<CostumerContract>();

var app = builder.Build();

app.MapPost("/costumers", async (
    [FromBody] CostumerPost post,
    AppDbContext _dbContext,
    CostumerContract _contract) =>
{
    var costumer = _contract.MapTo(post);

    if (!_contract.IsValid)
        return Results.BadRequest(_contract.Notifications);

    await _dbContext.Costumers.AddAsync(costumer);
    await _dbContext.SaveChangesAsync();

    return Results.CreatedAtRoute($"PostCostumer", null);
})
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError, typeof(string))
    .WithName("PostCostumer")
    .WithTags("Costumer")
    .WithMetadata(new SwaggerOperationAttribute(summary: "Store", description: "This method store a Costumer Entity"));

app.MapPut("/costumers", async (
    [FromBody] CostumerPut put,
    AppDbContext _dbContext,
    CostumerContract _contract) =>
{
    var costumer = _contract.MapTo(put);

    if (!_contract.IsValid)
        return Results.BadRequest(_contract.Notifications);

    _dbContext.Costumers.Update(costumer);
    await _dbContext.SaveChangesAsync();

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("UpdateCostumers")
.WithTags("Costumer")
.WithMetadata(new SwaggerOperationAttribute(
    summary: "Update", 
    description: "This method update a Costumer Entity"));

app.MapDelete("/costumers/{id}", async (
    [FromRoute] Guid id,
    AppDbContext _dbContext,
    CostumerContract _contract) =>
{
    _dbContext.Costumers.Remove(new Costumer { Id = id });
    await _dbContext.SaveChangesAsync();

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("DeleteCostumers")
.WithTags("Costumer")
.WithMetadata(new SwaggerOperationAttribute(
    summary: "Delete", 
    description: "This method delete a Costumer Entity"));

app.MapGet("/costumers/{id}", async (
    [FromRoute] Guid id,
    AppDbContext _dbContext) =>
{
    return Results.Ok(
        await _dbContext.Costumers.FirstOrDefaultAsync(c => c.Id.Equals(id)));
})
.Produces(StatusCodes.Status200OK, typeof(Costumer))
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("GetCostumer")
.WithTags("Costumer")
.WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve", description: "This method retrieve a list of Costumer Entity"));


app.MapGet("/costumers", async (AppDbContext _dbContext) =>
{
    return Results.Ok(await _dbContext.Costumers.ToListAsync());
})
.Produces(StatusCodes.Status200OK, typeof(IEnumerable<Costumer>))
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("GetAllCostumers")
.WithTags("Costumer")
.WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve All", description: "This method retrieve a list of Costumer Entity"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using Flunt.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Minimal.Api.Configurations;
using Minimal.Api.Context;
using Minimal.Api.DTOs;
using Minimal.Api.Entities;
using Minimal.Api.Entities.Contracts;
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

builder.Services.AddTransient<CustomerContract>();

var app = builder.Build();

app.MapPost("/Customers", async (
    [FromBody] CustomerPost post,
    AppDbContext _dbContext,
    CustomerContract _contract) =>
{
    _contract.MapToEntity(post);

    if (!_contract.IsValid)
        return Results.BadRequest(_contract.Notifications);

    await _dbContext.Customers.AddAsync(_contract.Entity);
    await _dbContext.SaveChangesAsync();

    return Results.CreatedAtRoute($"PostCustomer", null);
})
    .Produces(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status500InternalServerError, typeof(string))
    .WithName("PostCustomer")
    .WithTags("Customer")
    .WithMetadata(new SwaggerOperationAttribute(summary: "Store", description: "This method store a Customer Entity"));

app.MapPut("/Customers", async (
    [FromBody] CustomerPut put,
    AppDbContext _dbContext,
    CustomerContract _contract) =>
{
    _contract.MapToEntity(put);

    if (!_contract.IsValid)
        return Results.BadRequest(_contract.Notifications);

    _dbContext.Customers.Update(_contract.Entity);
    await _dbContext.SaveChangesAsync();

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("UpdateCustomers")
.WithTags("Customer")
.WithMetadata(new SwaggerOperationAttribute(
    summary: "Update", 
    description: "This method update a Customer Entity"));

app.MapDelete("/Customers/{id}", async (
    [FromRoute] Guid id,
    AppDbContext _dbContext,
    CustomerContract _contract) =>
{
    _dbContext.Customers.Remove(new Customer { Id = id });
    await _dbContext.SaveChangesAsync();

    return Results.Ok();
})
.Produces(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("DeleteCustomers")
.WithTags("Customer")
.WithMetadata(new SwaggerOperationAttribute(
    summary: "Delete", 
    description: "This method delete a Customer Entity"));

app.MapGet("/Customers/{id}", async (
    [FromRoute] Guid id,
    AppDbContext _dbContext) =>
{
    return Results.Ok(
        await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id.Equals(id)));
})
.Produces(StatusCodes.Status200OK, typeof(Customer))
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("GetCustomer")
.WithTags("Customer")
.WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve", description: "This method retrieve a list of Customer Entity"));


app.MapGet("/Customers", async (AppDbContext _dbContext) =>
{
    return Results.Ok(await _dbContext.Customers.ToListAsync());
})
.Produces(StatusCodes.Status200OK, typeof(IEnumerable<Customer>))
.Produces(StatusCodes.Status400BadRequest, typeof(IReadOnlyCollection<Notification>))
.Produces(StatusCodes.Status500InternalServerError, typeof(string))
.WithName("GetAllCustomers")
.WithTags("Customer")
.WithMetadata(new SwaggerOperationAttribute(summary: "Retrieve All", description: "This method retrieve a list of Customer Entity"));

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

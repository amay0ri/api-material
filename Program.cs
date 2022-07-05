using ApiMaterial.Data;
using ApiMaterial.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniValidation;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MaterialDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.MapGet("/material", async (MaterialDbContext context) =>
await context.Materiais.ToListAsync())
    .WithName("GetMaterial")
    .WithTags("Material");

app.MapGet("/material/{id}", async (int id, MaterialDbContext context) =>
await context.Materiais.FindAsync(id)
is Material material
? Results.Ok(material)
: Results.NotFound())
    .Produces<Material>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetMaterialPorId")
    .WithTags("Material");

app.MapPost("/material", async (
    MaterialDbContext context,
    Material material) =>
{
    if (!MiniValidator.TryValidate(material, out var errors))
        return Results.ValidationProblem(errors);
    
    context.Materiais.Add(material);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.Created($"/material/{material.Id}", material)
        : Results.BadRequest("Houve um problema ao registrar o material.");
})
    .ProducesValidationProblem()
    .Produces<Material>(StatusCodes.Status201Created)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("PostMaterial")
    .WithTags("Material");

app.MapPut("/material/{id}", async (
    int id,
    MaterialDbContext context,
    Material material) =>
{
    var materialDb = await context.Materiais.AsNoTracking<Material>().FirstOrDefaultAsync(m => m.Id == id);
    if (materialDb == null) return Results.NotFound();

    if (!MiniValidator.TryValidate(material, out var errors))
        return Results.ValidationProblem(errors);

    context.Materiais.Update(material);
    var result = await context.SaveChangesAsync();

    return result > 0
        ? Results.NoContent()
        : Results.BadRequest("Houve um problema ao atualizar o material.");
}
    )
    .ProducesValidationProblem()
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("PutMaterial")
    .WithTags("Material");

app.MapDelete("/material/{id}", async (
    int id,
    MaterialDbContext context) =>
{
    var material = await context.Materiais.FindAsync(id);
    if (material == null) return Results.NotFound();

    context.Materiais.Remove(material);
    var result = await context.SaveChangesAsync();

    return result > 0
    ? Results.NoContent()
    : Results.BadRequest("Houve um problema ao excluir o registro");
}
)
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("DeleteMaterial")
    .WithTags("Material");

app.Run();


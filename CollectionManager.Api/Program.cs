using CollectionManager.Api.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Collection Manager API",
        Version = "v1",
        Description = "API that allows students to create MongoDB collections and perform CRUD operations using unique API keys.",
        Contact = new OpenApiContact
        {
            Name = "MultiApi.Net",
            Url = new Uri("https://github.com/MultiApi.Net")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);

    options.TagActionsBy(api =>
    {
        var controller = api.GroupName ?? api.ActionDescriptor.RouteValues["controller"];
        return new[] { controller switch
        {
            "Collections" => "Collections Management",
            "Documents" => "Document Operations",
            "Query" => "Custom Queries",
            _ => controller
        }};
    });
    options.DocInclusionPredicate((_, _) => true);
});

builder.Services.AddSingleton<MongoService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setup =>
    {
        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "Collection Manager API v1");
        setup.DocumentTitle = "Collection Manager API";
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();

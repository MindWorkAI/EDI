using System.Text.Json.Serialization;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Title = "EDI - (E)xternal (D)ata AP(I) for AI Studio";
    options.HiddenClients = true;
});

app.MapGet("/auth/methods", () => new List<AuthMethods> { AuthMethods.NONE })
    .WithDescription("Get the available authentication methods.")
    .WithName("GetAuthMethods")
    .WithTags("Authentication");

app.MapGet("/security/requirements", () => new SecurityRequirements(ProviderType.SELF_HOSTED))
    .WithDescription("Get the security requirements for this data source.")
    .WithName("GetSecurityRequirements")
    .WithTags("Security");

app.Run();
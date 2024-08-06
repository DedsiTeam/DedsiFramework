
using ProjectName.HttpApi.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAutofac();

await builder.AddApplicationAsync<ProjectNameHttpApiHostModule>();

var app = builder.Build();

await app.InitializeApplicationAsync();
await app.RunAsync();
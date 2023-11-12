using DomeGym.Application;
using DomeGym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// to allow other projects grow independently from one another
// we are not going to register all the services inside the Program.cs (which is in the presentation layer)
// instead, each of the project has got the DependencyInjectionConfiguration class, responsible
// for registering additional serivces and their responsibility is to register all necessary
// Infrastructure, Application, etc. services, for scalability sake.
builder.Services
    .AddAplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

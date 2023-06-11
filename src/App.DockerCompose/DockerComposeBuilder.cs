using App.DockerCompose.Services;
using App.DockerCompose.Services.Interfaces;
using App.DotnetApps;
using App.Dsl;

namespace App.DockerCompose;

public class DockerComposeBuilder
{
    public readonly DockerCompose File;

    public DockerComposeBuilder(string systemName)
    {
        File = new DockerCompose
        {
            Version = "3",
            Name = systemName,
            Services = new List<IService>()
        };
    }
    
    public void AddDotnetApp(Service service)
    {
        var dotnetApp = new Dotnet
        {
            Name = service.Name,
            Image = new Image
            {
                Name = service.Name.ToLower(),
                Version = "latest"
            },
            Build = new Build
            {
                Context = $"./{File.Name}/{service.Name}",
                Dockerfile = $"{service.MainProject.Name}/Dockerfile"
            }
        };
        
        File.Services.Add(dotnetApp);
    }

    public void AddRedis(Container container)
    {
        var redis = new Redis
        {
            Name = container.Name,
            Image = new Image
            {
                Name = Redis.ImageName,
                Version = container.Properties?.ImageVersion ?? "latest"
            }
        };
        
        File.Services.Add(redis);
    }
    
    public void AddPostgres(Container container)
    {
        var postgres = new Postgres
        {
            Name = container.Name,
            Image = new Image
            {
                Name = Postgres.ImageName,
                Version = container.Properties?.ImageVersion ?? "latest"
            },
            Environment = new Dictionary<string, string>
            {
                {"POSTGRES_PASSWORD", "postgres"}
            }
        };
        
        File.Services.Add(postgres);
    }
}
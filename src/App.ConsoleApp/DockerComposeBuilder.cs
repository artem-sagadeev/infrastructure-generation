using App.Entities.DockerCompose;
using App.Entities.DockerCompose.Services;
using App.Entities.DockerCompose.Services.Interfaces;
using App.Entities.Dsl;

namespace App.ConsoleApp;

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

    public void AddDotnetApp(Container container)
    {
        var dotnetApp = new Dotnet
        {
            Name = container.Name,
            Image = new Image
            {
                Name = container.Name.ToLower(),
                Version = container.Properties.ImageVersion ?? "latest"
            },
            Build = new Build
            {
                Context = $"./{File.Name}",
                Dockerfile = $"{container.Name}/Dockerfile"
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
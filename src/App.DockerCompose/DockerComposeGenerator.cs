using App.Common;
using App.DotnetApps;
using App.Dsl;

namespace App.DockerCompose;

public static class DockerComposeGenerator
{
    public static void CreateDockerCompose(SoftwareSystem softwareSystem, Solution solution, Configuration configuration)
    {
        Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating docker-compose");
    
        var containers = softwareSystem.Containers;

        var dockerComposeBuilder = new DockerComposeBuilder(softwareSystem.Name);

        foreach (var container in containers)
        {
            switch (container.Technology)
            {
                case "redis":
                    dockerComposeBuilder.AddRedis(container);
                    break;
                case "rabbit":
                    dockerComposeBuilder.AddRabbit(container);
                    break;
                case "postgres":
                    dockerComposeBuilder.AddPostgres(container);
                    break;
                case "mongo":
                    dockerComposeBuilder.AddMongo(container);
                    break;
            }
        }

        foreach (var service in solution.Services)
        {
            dockerComposeBuilder.AddDotnetApp(service);
        }

        var dockerComposeString = dockerComposeBuilder.File.ToString();
    
        using var writer = new StreamWriter($"{configuration.PathToPlaceFiles}/docker-compose.yml");
        writer.Write(dockerComposeString);
    
        Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating docker-compose");
    }
}
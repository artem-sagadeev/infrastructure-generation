using App.Entities.DockerCompose.Services;
using App.Entities.DockerCompose.Services.Interfaces;
using App.Utilities;

namespace App.Entities.DockerCompose;

public class DockerCompose
{
    public string Version { get; set; }
    
    public string Name { get; set; }
    
    public List<IService> Services { get; set; }

    public override string ToString()
    {
        var stringBuilder = new CustomStringBuilder("  ");
        var level = 0;
    
        stringBuilder.AppendLine($"version: \"{Version}\"", level);
        stringBuilder.AppendLine($"name: \"{Name}\"", level);
        stringBuilder.AppendLine();

        if (Services.Any())
        {
            stringBuilder.AppendLine("services:", level);

            level++;
            foreach (var service in Services)
            {
                switch (service)
                {
                    case Dotnet dotnet:
                        stringBuilder
                            .AppendLine($"{dotnet.Name}:", level)
                            .AppendLine($"container_name: {dotnet.Name}", level + 1)
                            .AppendLine($"image: {dotnet.Image.Name}:{dotnet.Image.Version}", level + 1)
                            .AppendLine("build:", level + 1)
                            .AppendLine($"context: {dotnet.Build.Context}", level + 2)
                            .AppendLine($"dockerfile: {dotnet.Build.Dockerfile}", level + 2)
                            .AppendLine();
                        break;
                    case Redis redis:
                        stringBuilder
                            .AppendLine($"{redis.Name}:", level)
                            .AppendLine($"container_name: {redis.Name}", level + 1)
                            .AppendLine($"image: {redis.Image.Name}:{redis.Image.Version}", level + 1)
                            .AppendLine();
                        break;
                    case Postgres postgres:
                        stringBuilder
                            .AppendLine($"{postgres.Name}:", level)
                            .AppendLine($"container_name: {postgres.Name}", level + 1)
                            .AppendLine($"image: {postgres.Image.Name}:{postgres.Image.Version}", level + 1)
                            .AppendLine("environment:", level + 1);
                        foreach (var keyValue in postgres.Environment)
                        {
                            stringBuilder.AppendLine($"{keyValue.Key}: {keyValue.Value}", level + 2);
                        }
                        stringBuilder.AppendLine();
                        break;
                }
            }
        }

        var dockerComposeString = stringBuilder.ToString();

        return dockerComposeString;
    }
}
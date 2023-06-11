using App.Common;

namespace App.Dockerfiles;

public class Dockerfile
{
    public List<(string pathToProject, string projectName)> Projects { get; set; }

    public string PathToMainProject { get; set; }
    
    public string MainProjectName { get; set; }
    
    public override string ToString()
    {
        var stringBuilder = new CustomStringBuilder("  ");
        const int level = 0;

        stringBuilder
            .AppendLine("FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base", level)
            .AppendLine("WORKDIR /app", level)
            .AppendLine("EXPOSE 80", level)
            .AppendLine("EXPOSE 443", level)
            .AppendLine()
            .AppendLine("FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build", level)
            .AppendLine("WORKDIR /src", level);

        foreach (var (pathToProject, projectName) in Projects)
        {
            stringBuilder.AppendLine($"COPY [\"{pathToProject}/{projectName}\", \"{pathToProject}/\"]", level);
        }

        stringBuilder
            .AppendLine($"RUN dotnet restore \"{PathToMainProject}/{MainProjectName}\"", level)
            .AppendLine("COPY . .", level)
            .AppendLine($"WORKDIR \"/src/{PathToMainProject}\"", level)
            .AppendLine($"RUN dotnet build \"{MainProjectName}\" -c Release -o /app/build", level)
            .AppendLine()
            .AppendLine("FROM build AS publish", level)
            .AppendLine($"RUN dotnet publish \"{MainProjectName}\" -c Release -o /app/publish", level)
            .AppendLine()
            .AppendLine("FROM base AS final", level)
            .AppendLine("WORKDIR /app", level)
            .AppendLine("COPY --from=publish /app/publish .", level)
            .AppendLine($"ENTRYPOINT [\"dotnet\", \"{PathToMainProject}.dll\"]", level);

        return stringBuilder.ToString();
    }
}
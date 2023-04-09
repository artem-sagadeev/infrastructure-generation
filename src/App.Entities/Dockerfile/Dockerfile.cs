using App.Utilities;

namespace App.Entities.Dockerfile;

public class Dockerfile
{
    public string PathToProject { get; set; }
    
    public string ProjectName { get; set; }
    
    public override string ToString()
    {
        var stringBuilder = new CustomStringBuilder("  ");
        const int level = 0;

        stringBuilder.AppendLine("FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base", level)
            .AppendLine("WORKDIR /app", level)
            .AppendLine("EXPOSE 80", level)
            .AppendLine("EXPOSE 443", level)
            .AppendLine()
            .AppendLine("FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build", level)
            .AppendLine("WORKDIR /src", level)
            .AppendLine($"COPY [\"{PathToProject}/{ProjectName}\", \"{PathToProject}/\"]", level)
            .AppendLine($"RUN dotnet restore \"{PathToProject}/{ProjectName}\"", level)
            .AppendLine("COPY . .", level)
            .AppendLine($"WORKDIR \"/src/{PathToProject}\"", level)
            .AppendLine($"RUN dotnet build \"{ProjectName}\" -c Release -o /app/build", level)
            .AppendLine()
            .AppendLine("FROM build AS publish", level)
            .AppendLine($"RUN dotnet publish \"{ProjectName}\" -c Release -o /app/publish", level)
            .AppendLine()
            .AppendLine("FROM base AS final", level)
            .AppendLine("WORKDIR /app", level)
            .AppendLine("COPY --from=publish /app/publish .", level)
            .AppendLine($"ENTRYPOINT [\"dotnet\", \"{PathToProject}.dll\"]", level);

        return stringBuilder.ToString();
    }
}
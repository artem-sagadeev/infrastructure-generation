using System.Diagnostics;
using App.ConsoleApp;
using App.Entities.Dockerfile;
using App.Entities.Dsl;
using Newtonsoft.Json;

SoftwareSystem ReadModel(string modelName)
{
    Console.WriteLine($"{DateTime.Now.TimeOfDay} Start reading model");
    
    using var reader = new StreamReader(modelName);
    var jsonString = reader.ReadToEnd();
    reader.Dispose();

    var workspace = JsonConvert.DeserializeObject<Workspace>(jsonString);
    
    Console.WriteLine($"{DateTime.Now.TimeOfDay} End reading model");

    return workspace.Model.SoftwareSystems[0];
}

async Task CreateFiles(SoftwareSystem softwareSystem)
{
    Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating files");
    
    var containers = softwareSystem.Containers;
    
    var createCommands = new List<string>
    {
        $"new sln --name {softwareSystem.Name} --output ./{softwareSystem.Name} --force"
    };
    foreach (var container in containers)
    {
        if (container.Technology != "dotnet")
        {
            continue;
        }

        var appTemplate = container.Properties?.AppTemplate;
        createCommands.Add($"new {appTemplate} --name {container.Name} --output ./{softwareSystem.Name}/{container.Name} --force");
        createCommands.Add($"dotnet sln ./{softwareSystem.Name}/{softwareSystem.Name}.sln add {softwareSystem.Name}/{container.Name}");
    }

    foreach (var createCommand in createCommands)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = createCommand,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
    
        var proc = Process.Start(startInfo);
        ArgumentNullException.ThrowIfNull(proc);
    
        await proc.WaitForExitAsync();
    }
    
    Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating files");
}

void CreateDockerCompose(SoftwareSystem softwareSystem)
{
    Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating docker-compose");
    
    var containers = softwareSystem.Containers;

    var dockerComposeBuilder = new DockerComposeBuilder(softwareSystem.Name);

    foreach (var container in containers)
    {
        switch (container.Technology)
        {
            case "dotnet":
                dockerComposeBuilder.AddDotnetApp(container);
                break;
            case "redis":
                dockerComposeBuilder.AddRedis(container);
                break;
            case "postgres":
                dockerComposeBuilder.AddPostgres(container);
                break;
        }
    }

    var dockerComposeString = dockerComposeBuilder.File.ToString();
    
    using var writer = new StreamWriter("docker-compose.yml");
    writer.Write(dockerComposeString);
    
    Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating docker-compose");
}

void AddDockerfiles(SoftwareSystem softwareSystem)
{
    Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating Dockerfiles");
    
    var dotnetContainers = softwareSystem.Containers.Where(container => container.Technology == "dotnet");
    foreach (var container in dotnetContainers)
    {
        var dockerfile = new Dockerfile
        {
            PathToProject = container.Name,
            ProjectName = $"{container.Name}.csproj"
        };
        
        using var writer = new StreamWriter($"./{softwareSystem.Name}/{container.Name}/Dockerfile");
        writer.Write(dockerfile.ToString());
    }
    
    Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating Dockerfiles");
}

var softwareSystem = ReadModel("model.json");
await CreateFiles(softwareSystem);
AddDockerfiles(softwareSystem);
CreateDockerCompose(softwareSystem);









using App.ConsoleApp;
using App.Entities.Dsl;
using Newtonsoft.Json;

using var reader = new StreamReader("model.json");
var jsonString = reader.ReadToEnd();
reader.Dispose();

var workspace = JsonConvert.DeserializeObject<Workspace>(jsonString);
var stringBuilder = new CustomStringBuilder("  ");
var level = 0;

stringBuilder.AppendLine("version: \"3\"", level);
stringBuilder.AppendLine();
var containers = workspace.Model.SoftwareSystems[0].Containers;

if (containers != null && containers.Any())
{
    stringBuilder.AppendLine("services:", level);

    level++;
    foreach (var container in containers)
    {
        stringBuilder
            .AppendLine($"{container.Name}:", level)
            .AppendLine($"image: {container.Technology}:latest", level + 1)
            .AppendLine();
    } 
}

var dockerComposeString = stringBuilder.ToString();
using var writer = new StreamWriter("docker-compose.yml");
writer.Write(dockerComposeString);
using App.Common;
using App.DotnetApps;

namespace App.Dockerfiles;

public static class DockerfilesGenerator
{
    public static void CreateDockerfiles(Solution solution, Configuration configuration)
    {
        Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating Dockerfiles");
        
        foreach (var service in solution.Services)
        {
            var dockerfile = new Dockerfile
            {
                Projects = service.Projects.Select(project => (project.Name, $"{project.Name}.csproj")).ToList(),
                PathToMainProject = service.MainProject.Name,
                MainProjectName = $"{service.MainProject.Name}.csproj"
            };

            var path = $"{configuration.PathToPlaceFiles}/{solution.Name}/{service.Name}/{service.MainProject.Name}/Dockerfile";
            using var writer = new StreamWriter(path);
            writer.Write(dockerfile.ToString());
        }
    
        Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating Dockerfiles");
    }
}
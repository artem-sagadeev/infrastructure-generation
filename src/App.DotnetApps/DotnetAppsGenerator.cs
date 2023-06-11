using System.Diagnostics;
using App.Common;
using App.Dsl;

namespace App.DotnetApps;

public static class DotnetAppsGenerator
{
    public static Solution CreateDotnetApps(SoftwareSystem softwareSystem, Configuration configuration)
    {
        Console.WriteLine($"{DateTime.Now.TimeOfDay} Start creating dotnet apps");

        var solution = BuildSolution(softwareSystem);
    
        var commands = new List<string>();
        commands.AddRange(GetCreateSolutionCommands(solution, configuration));
        commands.AddRange(GetCreateProjectsCommands(solution, configuration));

        foreach (var command in commands)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = command,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
    
            var proc = Process.Start(startInfo);
            ArgumentNullException.ThrowIfNull(proc);
    
            proc.WaitForExit();
        }
    
        Console.WriteLine($"{DateTime.Now.TimeOfDay} End creating dotnet apps");

        return solution;
    }

    private static Solution BuildSolution(SoftwareSystem softwareSystem)
    {
        var solution = new Solution
        {
            Name = softwareSystem.Name,
            Services = new List<Service>()
        };
        foreach (var container in softwareSystem.Containers)
        {
            if (container.Technology != "dotnet")
            {
                continue;
            }

            var service = new Service
            {
                Name = container.Name,
                Solution = solution,
                Projects = new List<Project>()
            };
            
            solution.Services.Add(service);
            
            foreach (var component in container.Components)
            {
                var project = new Project
                {
                    Name = $"{container.Name}.{component.Name}",
                    AppTemplate = component.Properties.AppTemplate,
                    Service = service
                };
                
                service.Projects.Add(project);
            }
        }

        return solution;
    }
    
    private static IEnumerable<string> GetCreateSolutionCommands(Solution solution, Configuration configuration)
    {
        var name = $"{solution.Name}";
        var output = $"{configuration.PathToPlaceFiles}/{solution.Name}";

        yield return $"new sln --name {name} --output {output} --force";
    }

    private static IEnumerable<string> GetCreateProjectsCommands(Solution solution, Configuration configuration)
    {
        var projects = solution.Services.SelectMany(service => service.Projects);
        var solutionFilePath = $"{configuration.PathToPlaceFiles}/{solution.Name}/{solution.Name}.sln";
        
        foreach (var project in projects)
        {
            var projectFolderPath = $"{configuration.PathToPlaceFiles}/{solution.Name}/{project.Service.Name}/{project.Name}";
            
            yield return $"new {project.AppTemplate} --name {project.Name} --output {projectFolderPath} --force";
            yield return $"dotnet sln {solutionFilePath} add {projectFolderPath}";
        }
    }
}
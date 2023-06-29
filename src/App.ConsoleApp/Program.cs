using App.Common;
using App.DockerCompose;
using App.Dockerfiles;
using App.DotnetApps;

namespace App.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("---------- Start ----------");
            
            const string defaultModelFilePath = "model.json";
            const string defaultPathToPlaceFiles = "./";
            
            var modelFilePath = args.Length > 0 ? args[0] : null;
            modelFilePath = string.IsNullOrEmpty(modelFilePath) ? defaultModelFilePath : modelFilePath;
            
            var pathToPlaceFiles = args.Length > 1 ? args[1] : null;
            pathToPlaceFiles = string.IsNullOrEmpty(pathToPlaceFiles) ? defaultPathToPlaceFiles : pathToPlaceFiles;

            var configuration = new Configuration(pathToPlaceFiles);

            var softwareSystems = ModelReader.ReadModel(modelFilePath);
            foreach (var softwareSystem in softwareSystems)
            {
                var solution = DotnetAppsGenerator.CreateDotnetApps(softwareSystem, configuration);
                DockerfilesGenerator.CreateDockerfiles(solution, configuration);
                DockerComposeGenerator.CreateDockerCompose(softwareSystem, solution, configuration);
            }
            
            Console.WriteLine("---------- End ----------");
            Console.WriteLine();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
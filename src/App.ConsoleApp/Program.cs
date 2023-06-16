using App.Common;
using App.ConsoleApp;
using App.DockerCompose;
using App.Dockerfiles;
using App.DotnetApps;

const string defaultModelFilePath = "model1.json";
const string defaultPathToPlaceFiles = "./";

Console.WriteLine("Укажите путь до json-файла, содержащего модель информационной системы");
var modelFilePath = Console.ReadLine();
modelFilePath = string.IsNullOrEmpty(modelFilePath) ? defaultModelFilePath : modelFilePath;

Console.WriteLine("Укажите путь для расположения сгенерированных файлов");
var pathToPlaceFiles = Console.ReadLine();
pathToPlaceFiles = string.IsNullOrEmpty(pathToPlaceFiles) ? defaultPathToPlaceFiles : pathToPlaceFiles;

var configuration = new Configuration(pathToPlaceFiles);

var softwareSystems = ModelReader.ReadModel(modelFilePath);
foreach (var softwareSystem in softwareSystems)
{
    var solution = DotnetAppsGenerator.CreateDotnetApps(softwareSystem, configuration);
    DockerfilesGenerator.CreateDockerfiles(solution, configuration);
    DockerComposeGenerator.CreateDockerCompose(softwareSystem, solution, configuration);
}
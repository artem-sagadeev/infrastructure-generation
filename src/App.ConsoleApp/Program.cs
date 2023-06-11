using App.Common;
using App.ConsoleApp;
using App.DockerCompose;
using App.Dockerfiles;
using App.DotnetApps;

const string modelFilePath = "model1.json";
const string pathToPlaceFiles = "./";

var configuration = new Configuration(pathToPlaceFiles);

var softwareSystem = ModelReader.ReadModel(modelFilePath);
var solution = DotnetAppsGenerator.CreateDotnetApps(softwareSystem, configuration);
DockerfilesGenerator.CreateDockerfiles(solution, configuration);
DockerComposeGenerator.CreateDockerCompose(softwareSystem, solution, configuration);









using App.DockerCompose.Services.Interfaces;

namespace App.DockerCompose.Services;

public class Mongo : IService
{
    public const string ImageName = "mongo"; 
    
    public string Name { get; set; }

    public Image Image { get; set; }
}
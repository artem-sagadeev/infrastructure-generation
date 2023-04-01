using App.Entities.DockerCompose.Services.Interfaces;

namespace App.Entities.DockerCompose.Services;

public class Redis : IService
{
    public const string ImageName = "redis"; 
    
    public string Name { get; set; }

    public Image Image { get; set; }
}
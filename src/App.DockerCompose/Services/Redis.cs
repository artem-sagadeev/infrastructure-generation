using App.DockerCompose.Services.Interfaces;

namespace App.DockerCompose.Services;

public class Redis : IService
{
    public const string ImageName = "redis"; 
    
    public string Name { get; set; }

    public Image Image { get; set; }
}
using App.DockerCompose.Services.Interfaces;

namespace App.DockerCompose.Services;

public class Rabbit : IService
{
    public const string ImageName = "rabbitmq"; 
    
    public string Name { get; set; }

    public Image Image { get; set; }
}
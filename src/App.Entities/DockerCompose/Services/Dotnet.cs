using App.Entities.DockerCompose.Services.Interfaces;

namespace App.Entities.DockerCompose.Services;

public class Dotnet : IService
{
    public string Name { get; set; }
    
    public Image Image { get; set; }
    
    public Build Build { get; set; }
}
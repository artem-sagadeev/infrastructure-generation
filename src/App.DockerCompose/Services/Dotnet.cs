using App.DockerCompose.Services.Interfaces;

namespace App.DockerCompose.Services;

public class Dotnet : IService
{
    public string Name { get; set; }
    
    public Image Image { get; set; }
    
    public Build Build { get; set; }
}
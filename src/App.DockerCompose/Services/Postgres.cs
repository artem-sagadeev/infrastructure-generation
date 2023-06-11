using App.DockerCompose.Services.Interfaces;

namespace App.DockerCompose.Services;

public class Postgres : IService
{
    public const string ImageName = "postgres"; 
    
    public string Name { get; set; }
    
    public Image Image { get; set; }
    
    public Dictionary<string, string> Environment { get; set; }
}
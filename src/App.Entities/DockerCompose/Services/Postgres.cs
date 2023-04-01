using App.Entities.DockerCompose.Services.Interfaces;

namespace App.Entities.DockerCompose.Services;

public class Postgres : IService
{
    public const string ImageName = "postgres"; 
    
    public string Name { get; set; }
    
    public Image Image { get; set; }
    
    public Dictionary<string, string> Environment { get; set; }
}
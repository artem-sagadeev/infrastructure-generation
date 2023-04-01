namespace App.Entities.DockerCompose.Services.Interfaces;

public interface IService
{
    public string Name { get; set; }
    
    public Image Image { get; set; }
}
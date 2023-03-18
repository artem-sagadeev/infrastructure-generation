namespace App.Entities;

public class SoftwareSystem
{
    public SoftwareSystem(
        string id, 
        string tags, 
        string name, 
        Container[] containers)
    {
        Id = id;
        Tags = tags;
        Name = name;
        Containers = containers;
    }

    public string Id { get; set; }
    
    public string? Tags { get; set; }
    
    public string Name { get; set; }
    
    public Container[]? Containers { get; set; }
}
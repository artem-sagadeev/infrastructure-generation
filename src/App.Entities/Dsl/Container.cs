namespace App.Entities.Dsl;

public class Container
{
    public Container(
        string id, 
        string tags, 
        string name, 
        string technology,
        Dictionary<string, string> properties,
        Relationship[] relationships)
    {
        Id = id;
        Tags = tags;
        Name = name;
        Technology = technology;
        Properties = properties;
        Relationships = relationships;
    }

    public string Id { get; set; }
    
    public string? Tags { get; set; }
    
    public string Name { get; set; }
    
    public string Technology { get; set; }
    
    public Dictionary<string, string> Properties { get; set; }
    
    public Relationship[]? Relationships { get; set; }
}
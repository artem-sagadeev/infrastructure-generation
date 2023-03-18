namespace App.Entities;

public class Container
{
    public Container(
        string id, 
        string tags, 
        string name, 
        Relationship[] relationships)
    {
        Id = id;
        Tags = tags;
        Name = name;
        Relationships = relationships;
    }

    public string Id { get; set; }
    
    public string? Tags { get; set; }
    
    public string Name { get; set; }
    
    public Relationship[]? Relationships { get; set; }
}
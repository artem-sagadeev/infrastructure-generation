namespace App.Entities.Dsl;

public class Component
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string Technology { get; set; }
    
    public ComponentProperties Properties { get; set; }
    
    public Relationship[] Relationships { get; set; }
}

public class ComponentProperties
{
    
}
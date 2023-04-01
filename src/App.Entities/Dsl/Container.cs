namespace App.Entities.Dsl;

public class Container
{
    public string Id { get; set; }
    
    public string Tags { get; set; }
    
    public string Name { get; set; }
    
    public string Technology { get; set; }
    
    public ContainerProperties Properties { get; set; }
    
    public Relationship[] Relationships { get; set; }
}

public class ContainerProperties
{
    public string ImageVersion { get; set; }
    
    public string AppTemplate { get; set; }
}
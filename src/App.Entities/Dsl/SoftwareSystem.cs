namespace App.Entities.Dsl;

public class SoftwareSystem
{
    public string Id { get; set; }
    
    public string Tags { get; set; }
    
    public string Name { get; set; }
    
    public Container[] Containers { get; set; }
}
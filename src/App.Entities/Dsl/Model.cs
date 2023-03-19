namespace App.Entities.Dsl;

public class Model
{
    public Model(SoftwareSystem[] softwareSystems)
    {
        SoftwareSystems = softwareSystems;
    }

    public SoftwareSystem[] SoftwareSystems { get; set; }
}
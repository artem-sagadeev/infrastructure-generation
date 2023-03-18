namespace App.Entities;

public class Model
{
    public Model(SoftwareSystem[] softwareSystems)
    {
        SoftwareSystems = softwareSystems;
    }

    public SoftwareSystem[] SoftwareSystems { get; set; }
}
namespace App.Entities;

public class Workspace
{
    public Workspace(Model model)
    {
        Model = model;
    }

    public Model Model { get; set; }
}
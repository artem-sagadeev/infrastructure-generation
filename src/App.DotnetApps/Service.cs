namespace App.DotnetApps;

public class Service
{
    public string Name { get; set; }
    
    public List<Project> Projects { get; set; }
    
    public Solution Solution { get; set; }

    public Project MainProject => Projects.Single(project => project.AppTemplate != "classlib");
}
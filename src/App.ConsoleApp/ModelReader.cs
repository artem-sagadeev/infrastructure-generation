using App.Dsl;
using Newtonsoft.Json;

namespace App.ConsoleApp;

public static class ModelReader
{
    public static SoftwareSystem[] ReadModel(string modelName)
    {
        Console.WriteLine($"{DateTime.Now.TimeOfDay} Start reading model");
    
        using var reader = new StreamReader(modelName);
        var jsonString = reader.ReadToEnd();
        reader.Dispose();

        var workspace = JsonConvert.DeserializeObject<Workspace>(jsonString);
    
        Console.WriteLine($"{DateTime.Now.TimeOfDay} End reading model");

        return workspace.Model.SoftwareSystems;
    }
}
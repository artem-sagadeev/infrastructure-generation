using App.Entities;
using Newtonsoft.Json;

using var reader = new StreamReader("model.json");
var jsonString = reader.ReadToEnd();
var workspace = JsonConvert.DeserializeObject<Workspace>(jsonString);
Console.WriteLine(jsonString);
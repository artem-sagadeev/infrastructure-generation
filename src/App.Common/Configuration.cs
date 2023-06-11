namespace App.Common;

public class Configuration
{
    public string PathToPlaceFiles { get; }

    public Configuration(string pathToPlaceFiles)
    {
        PathToPlaceFiles = pathToPlaceFiles;
    }
}
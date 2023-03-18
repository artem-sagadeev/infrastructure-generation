namespace App.Entities;

public class Relationship
{
    public Relationship(
        string id, 
        string tags, 
        string sourceId, 
        string destinationId, 
        string description)
    {
        Id = id;
        Tags = tags;
        SourceId = sourceId;
        DestinationId = destinationId;
        Description = description;
    }

    public string Id { get; set; }
    
    public string? Tags { get; set; }
    
    public string SourceId { get; set; }
    
    public string DestinationId { get; set; }
    
    public string? Description { get; set; }
}
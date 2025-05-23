using System.Text.Json;
using System.Text.Json.Serialization;
namespace ujin.Models;

public class DetalasedObject
{
    [JsonPropertyName("data")]
    public List<ObjectData> Data { get; set; }
}

public class ObjectData
{
    [JsonPropertyName("object")]
    public ObjectJson Object { get; set; }

    [JsonPropertyName("parts")]
    public List<PartInfo> 
    Parts { get; set; }
}

public class PartInfo
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}
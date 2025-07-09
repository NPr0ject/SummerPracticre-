using System.Text.Json;
using System.Text.Json.Serialization;

namespace task13;

public class Subject
{
  public string? Name {get; set; }
  public int Grade {get; set; }
}

public class Student
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject>? Grades { get; set; }
}

public class SerializerDeserializer()
{
    private readonly JsonSerializerOptions options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonDateConverter("dd.MM.yyyy")}
    };
    public void SerializerSafeFile(Student student, string path)
    {
        string json = JsonSerializer.Serialize(student);
        File.WriteAllText(path, json);
    }
    public Student DeserializerLoadFile(string path)
    {
        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<Student>(json)!;
    }
    
public class JsonDateConverter : JsonConverter<DateTime>
{
    private readonly string _format;

    public JsonDateConverter(string format)
    {
        _format = format;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var s = reader.GetString()!;
        return DateTime.ParseExact(s, _format, null);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}
}

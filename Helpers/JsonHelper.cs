using System.Text.Json;

namespace Red.Json.Helpers;

public static class JsonHelper
{
    public static string CastToJson(object? json)
    {
        string str;
        using MemoryStream stream = new();

        JsonSerializer.Serialize(stream, json);
        stream.Seek(0, SeekOrigin.Begin);

        using (StreamReader sr = new(stream))
        {
            str = sr.ReadToEnd();
        }
        return str;
    }
}

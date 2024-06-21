using Red.Json.Helpers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Red.Json.Extencions.String;
public static class JsonExtencions
{
    public static T? DeserializeJson<T>(this string str)
    {
        try
        {
            T? obj;
            using (MemoryStream stream = new(Encoding.UTF8.GetBytes(str)))
            {
                obj = JsonSerializer.Deserialize<T>(stream);
            }
            return obj;
        }
        catch (JsonException)
        {
            throw new JsonException("It was not possible to convert");
        }
    }
    public static bool IsValidJson(this string str)
    {
        try
        {
            JsonDocument.Parse(str);
            return true;
        }
        catch (JsonException)
        {
            return false;
        }
    }
    public static string AddJsonElement(this string str, string key, string value)
    {
        JsonObject? json = JsonSerializer.Deserialize<JsonObject>(str);
        json?.Add(key, value);

        return JsonHelper.CastToJson(json);
    }
    public static string RemoveJsonElement(this string str, string key)
    {
        JsonObject? json = JsonSerializer.Deserialize<JsonObject>(str);
        json?.Remove(key);
        return JsonHelper.CastToJson(json);
    }
    public static bool TryGetJsonValue(this string str, string key, out string? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        var dic = JsonSerializer.Deserialize<Dictionary<string, string>>(str);
        _ = dic ?? throw new ArgumentException("It is necessary to have json format");
        if (dic.TryGetValue(key, out value))
            return true;

        return false;
    }
}

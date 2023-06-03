using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Unicode;

namespace InternetStore.Controls
{
    public static class JsonMaster
    {
        public static Dictionary<V, K>? Parse<V, K>(this string jsonString)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            return IsValidJson(jsonString) ? JsonSerializer.Deserialize<Dictionary<V, K>>(jsonString, options) : null;
        }

        public static object? GetValue(this Dictionary<string, object> dict, string keyName)
        {
            object? value = null;

            if (dict != null) dict.TryGetValue(keyName, out value);
            return value;
        }

        public static bool IsValidJson(string jsonString)
        {
            if (jsonString == null) return false;

            try
            {
                return JsonObject.Parse(jsonString) != null;
            }
            catch
            {
                return false;
            }
        }
    }
}

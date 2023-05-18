using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Text.Json;

namespace InternetStore.Controls
{
    public static class JsonMaster
    {
        public static Dictionary<V, K>? Parse<V, K>(this string jsonString)
        {
            return JsonSerializer.Deserialize<Dictionary<V, K>>(jsonString);
        }

        public static object? GetValue(this Dictionary<string, object> dict, string keyName)
        {
            object? value;
            dict.TryGetValue(keyName, out value);
            return (value != null) ? value : null;
        }
    }
}

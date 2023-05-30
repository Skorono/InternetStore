using DocumentFormat.OpenXml.Office2013.Drawing.Chart;
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
            return jsonString != null ? JsonSerializer.Deserialize<Dictionary<V, K>>(jsonString) : null;
        }

        public static object? GetValue(this Dictionary<string, object> dict, string keyName)
        {
            object? value = null;

            if (dict != null) dict.TryGetValue(keyName, out value);
            return value;
        }
    }
}

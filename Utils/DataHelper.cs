using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace Connect4Game.Utils
{
    public static class DataHelper
    {
        private static Dictionary<string, object> settings;
        public static string FilePath = "Data.json";

        /// <summary>
        /// we check if file already exists we delete the file
        /// </summary>
        public static void Clear()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        /// <summary>
        /// we make a new instance of the disctionary and then use the key and values to add them to the settings variable
        /// to write json to a string or to a file we call the JsonSerializer.Serialize method.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(string key, object value)
        {
            settings = new();
            using (StreamReader stream = new(File.Open(FilePath, FileMode.OpenOrCreate)))
            {
                var data = stream.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(data))
                    settings = JsonSerializer.Deserialize<Dictionary<string, object>>(data);
                if (settings.ContainsKey(key))
                    settings[key] = value;
                else
                    settings.Add(key, value);
                using (StreamWriter writer = new(stream.BaseStream))
                {
                    writer.BaseStream.Position = 0;
                    writer.Flush();
                    writer.Write(JsonSerializer.Serialize(settings));
                }
            }
        }

        /// <summary>
        /// {T} can be the type or base type of the underlying value. 
        /// If the underlying value is a JsonElement then {T} can also be the type of any primitive value supported by current JsonElement.
        /// Specifying the Object type for {T} will always succeed and return the underlying value as Object.
        /// The underlying value of a JsonValue after deserialization is an instance of JsonElement, otherwise it's the value specified when the JsonValue was created.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns>A value converted from the JsonValue instance and associated with a specific key.</returns>
        public static T GetValue<T>(string key)
        {
            settings = new();
            using (StreamReader stream = new(File.Open(FilePath, FileMode.OpenOrCreate)))
            {
                try
                {
                    var data = stream.ReadToEnd();
                    if (!string.IsNullOrWhiteSpace(data))
                        settings = JsonSerializer.Deserialize<Dictionary<string, object>>(data);
                    if (settings.ContainsKey(key))
                    {
                        var json = JsonSerializer.Serialize(settings[key]);
                        return JsonSerializer.Deserialize<T>(json);
                    }
                    else
                        return default(T);
                }
                catch (Exception ex) { return default(T); }
            }
        }
    }
}

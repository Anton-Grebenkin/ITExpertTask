using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json;

namespace ITExpertTask.Data
{
    public class DataFilter
    {
        public string PropertyName { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DataFilterType DataFilterType { get; set; }
        public JsonElement Value { get; set; }
    }
}

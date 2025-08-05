using System.Text.Json;

namespace temperature_api.Dto;

using System;
using System.Globalization;
using System.Text.Json.Serialization;

public class TemperatureResponse
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string[] _dateFormats =
        {
            "yyyy-MM-dd'T'HH:mm:sszzz"
        };

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string dateString = reader.GetString();
            return DateTime.ParseExact(
                dateString,
                _dateFormats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal
            );
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture));
        }
    }

    public TemperatureResponse(string? location, string? sensorId, int number)
    {
        Unit = "Some unit";
        Location = location ?? SensorID switch
        {
            "1" => "Living Room",
            "2" => "Bedroom",
            "3" => "Kitchen",
            _ => "Unknown"
        };
        Status = "OK";
        SensorID = sensorId ?? location switch
        {
            "Living Room" => "1",
            "Bedroom" => "2",
            "Kitchen" => "3",
            _ => "0"
        };
        SensorType = "Type";
        Description = "Some description";
        Value = number;
        Timestamp = DateTime.UtcNow;
    }

    [JsonPropertyName("value")] public float Value { get; set; }
    [JsonPropertyName("unit")] public string Unit { get; set; }

    [JsonPropertyName("timestamp")]
    [JsonConverter(typeof(CustomDateTimeConverter))]
    public DateTime Timestamp { get; set; }

    [JsonPropertyName("location")] public string Location { get; set; }
    [JsonPropertyName("status")] public string Status { get; set; }
    [JsonPropertyName("sensor_id")] public string SensorID { get; set; }
    [JsonPropertyName("sensor_type")] public string SensorType { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
}
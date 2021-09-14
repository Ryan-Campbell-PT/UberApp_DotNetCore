using Newtonsoft.Json;

namespace UberApp.Shared
{
    public class TestUser
    {
        public string Email { get; set; }
        // public string NormalizedEmail { get; set; }
        public string Password { get; set; }
        
        [JsonProperty(PropertyName = "partitionKey")]
        public string PartitionKey { get; set; }
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        
        // public string UserId { get; set; }

    }
}
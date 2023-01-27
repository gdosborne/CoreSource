using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongregationManager.Data {
    [JsonObject("member")]
    public class Member {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
    }
}

using CsvHelper.Configuration;
using System.Runtime.Serialization;

namespace ScottsJewels.Models
{
    [DataContract]
    public class Car
    {
        [CsvField(Ignore=true)]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [CsvField(Index = 0)]
        [DataMember(Name = "year")]
        public string Year { get; set; }

        [CsvField(Index = 1)]
        [DataMember(Name = "make")]
        public string Make { get; set; }

        [CsvField(Index = 4)]
        [DataMember(Name = "model")]
        public string Model { get; set; }
    }
}
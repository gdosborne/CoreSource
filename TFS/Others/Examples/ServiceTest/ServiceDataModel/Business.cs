namespace ServiceDataModel {
    using System.Runtime.Serialization;

    [DataContract]
    public class Business {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}

namespace ServiceDataModel {
    using System.Runtime.Serialization;

    [DataContract]
    public class Customer {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public Business Business { get; set; }
    }
}

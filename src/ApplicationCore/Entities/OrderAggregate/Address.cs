namespace ApplicationCore.Entities.OrderAggregate
{
    public class Address
    {
        public Address(string country, string state, string city, string street,string zipCode)
        {
            Country = country;
            State = state;
            City = city;
            Street = street;
            ZipCode = zipCode;
        }
        public Address()
        {
        }
        public string Street { get;private set; }  // 街道
        public string City { get; private set; }  // 城市
        public string State { get; private set; }  // 州/省
        public string Country { get; private set; } // 国家
        public string ZipCode { get; private set; }  // 邮政编码
    }
}

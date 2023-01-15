using Newtonsoft.Json;

namespace WebDriverNUnit.Entities
{
	public class UserInfo
	{
		public string Id {get; set;}
		public string Name { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public Address Address { get; set; }
		public string Phone { get; set; }
		public string Website{get;set; }
		public Company Company { get; set; }
	}

	public class Company 
	{
		public string Name { get; set; }
		public string CatchPhrase { get; set; }
		[JsonProperty("bs")]
		public string BusinessServices { get; set; }
	}

	public class Address
	{
		public string Street { get; set; }
		public string Suite { get; set; }
		public string City { get; set; }
		public string Zipcode { get; set; }
		public Geo Geo {get;set;}
}

	public class Geo
	{
		[JsonProperty("lat")]
		public string Latitude { get; set; }
		[JsonProperty("lng")]
		public string Longitude { get; set; }
	}
}

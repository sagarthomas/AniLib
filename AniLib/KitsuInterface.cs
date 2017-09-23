using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace AniLib
{
	/// <summary>
	/// This Class will serialize the Kitsu JSON API result into a C# object
	/// </summary>
	class KitsuInterface
	{
		public async static Task<RootObject> GetAnime(string id) {
			var http = new HttpClient();
			var url = "https://kitsu.io/api/edge/anime/" + id;
			var response = await http.GetAsync(url);
			var result = await response.Content.ReadAsStringAsync();

			var serializer = new DataContractJsonSerializer(typeof(RootObject));
			var ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(result));
			var data = (RootObject)serializer.ReadObject(ms);

			return data;
		}
	}


	//Kitsu JSON interface for all anime. All objects returned from the API call will follow the same hierarchy

	[DataContract]
	public class RootObject
	{
		[DataMember]
		public Data data { get; set; }
	}

	[DataContract]
	public class Data
	{
		[DataMember]
		public string id { get; set; }
		[DataMember]
		public string type { get; set; }
		[DataMember]
		public Links links { get; set; }
		[DataMember]
		public Attributes attributes { get; set; }
		[DataMember]
		public Relationships relationships { get; set; }
	}

	[DataContract]
	public class Links
	{
		[DataMember]
		public string self { get; set; }
	}

	[DataContract]
	public class Attributes
	{
		[DataMember]
		public string createdAt { get; set; }    //Was DateTime
		[DataMember]
		public string updatedAt { get; set; }    //Was DateTime
		[DataMember]
		public string slug { get; set; }
		[DataMember(EmitDefaultValue = false)]
		public string synopsis { get; set; }
		[DataMember]
		public int coverImageTopOffset { get; set; }
		[DataMember]
		public Titles titles { get; set; }
		[DataMember]
		public string canonicalTitle { get; set; }
		[DataMember]
		public string[] abbreviatedTitles { get; set; }
		[DataMember]
		public string averageRating { get; set; }
		[DataMember]
		public Ratingfrequencies ratingFrequencies { get; set; }
		[DataMember]
		public int userCount { get; set; }
		[DataMember]
		public int favoritesCount { get; set; }
		[DataMember]
		public string startDate { get; set; }
		[DataMember]
		public string endDate { get; set; }
		[DataMember]
		public int popularityRank { get; set; }
		[DataMember]
		public int ratingRank { get; set; }
		[DataMember]
		public string ageRating { get; set; }
		[DataMember]
		public string ageRatingGuide { get; set; }
		[DataMember]
		public string subtype { get; set; }
		[DataMember]
		public string status { get; set; }
		[DataMember(EmitDefaultValue = false)]
		public object tba { get; set; }
		[DataMember]
		public Posterimage posterImage { get; set; }
		[DataMember]
		public Coverimage coverImage { get; set; }
		[DataMember]
		public int episodeCount { get; set; }
		//[DataMember(IsRequired =false)]			//Commented out due to issue with Hunter x Hunter
		//public int episodeLength { get; set; }
		[DataMember]
		public string youtubeVideoId { get; set; }
		[DataMember]
		public string showType { get; set; }
		[DataMember]
		public bool nsfw { get; set; }
	}

	[DataContract]
	public class Titles
	{
		[DataMember]
		public string en { get; set; }
		[DataMember]
		public string en_jp { get; set; }
		[DataMember]
		public string ja_jp { get; set; }
	}

	[DataContract]
	public class Ratingfrequencies
	{
		[DataMember]
		public string _2 { get; set; }
		[DataMember]
		public string _3 { get; set; }
		[DataMember]
		public string _4 { get; set; }
		[DataMember]
		public string _5 { get; set; }
		[DataMember]
		public string _6 { get; set; }
		[DataMember]
		public string _7 { get; set; }
		[DataMember]
		public string _8 { get; set; }
		[DataMember]
		public string _9 { get; set; }
		[DataMember]
		public string _10 { get; set; }
		[DataMember]
		public string _11 { get; set; }
		[DataMember]
		public string _12 { get; set; }
		[DataMember]
		public string _13 { get; set; }
		[DataMember]
		public string _14 { get; set; }
		[DataMember]
		public string _15 { get; set; }
		[DataMember]
		public string _16 { get; set; }
		[DataMember]
		public string _17 { get; set; }
		[DataMember]
		public string _18 { get; set; }
		[DataMember]
		public string _19 { get; set; }
		[DataMember]
		public string _20 { get; set; }
	}

	[DataContract]
	public class Posterimage
	{
		[DataMember(EmitDefaultValue =false)]
		public string tiny { get; set; }
		[DataMember]
		public string small { get; set; }
		[DataMember]
		public string medium { get; set; }
		[DataMember]
		public string large { get; set; }
		[DataMember]
		public string original { get; set; }
	}

	[DataContract]
	public class Coverimage
	{
		[DataMember(EmitDefaultValue =false)]
		public string tiny { get; set; }
		[DataMember]
		public string small { get; set; }
		[DataMember]
		public string large { get; set; }
		[DataMember]
		public string original { get; set; }
	}

	[DataContract]
	public class Relationships
	{
		[DataMember]
		public Genres genres { get; set; }
		[DataMember]
		public Categories categories { get; set; }
		[DataMember]
		public Castings castings { get; set; }
		[DataMember]
		public Installments installments { get; set; }
		[DataMember]
		public Mappings mappings { get; set; }
		[DataMember]
		public Reviews reviews { get; set; }
		[DataMember]
		public Mediarelationships mediaRelationships { get; set; }
		[DataMember]
		public Episodes episodes { get; set; }
		[DataMember]
		public Streaminglinks streamingLinks { get; set; }
		[DataMember]
		public Animeproductions animeProductions { get; set; }
		[DataMember]
		public Animecharacters animeCharacters { get; set; }
		[DataMember]
		public Animestaff animeStaff { get; set; }
	}

	[DataContract]
	public class Genres
	{
		[DataMember]
		public Links1 links { get; set; }
	}

	[DataContract]
	public class Links1
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Categories
	{
		[DataMember]
		public Links2 links { get; set; }
	}

	[DataContract]
	public class Links2
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Castings
	{
		[DataMember]
		public Links3 links { get; set; }
	}

	[DataContract]
	public class Links3
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Installments
	{
		[DataMember]
		public Links4 links { get; set; }
	}

	[DataContract]
	public class Links4
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Mappings
	{
		[DataMember]
		public Links5 links { get; set; }
	}

	[DataContract]
	public class Links5
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Reviews
	{
		[DataMember]
		public Links6 links { get; set; }
	}

	[DataContract]
	public class Links6
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Mediarelationships
	{
		[DataMember]
		public Links7 links { get; set; }
	}

	[DataContract]
	public class Links7
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Episodes
	{
		[DataMember]
		public Links8 links { get; set; }
	}

	[DataContract]
	public class Links8
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Streaminglinks
	{
		[DataMember]
		public Links9 links { get; set; }
	}

	[DataContract]
	public class Links9
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Animeproductions
	{
		[DataMember]
		public Links10 links { get; set; }
	}

	[DataContract]
	public class Links10
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Animecharacters
	{
		[DataMember]
		public Links11 links { get; set; }
	}

	[DataContract]
	public class Links11
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}

	[DataContract]
	public class Animestaff
	{
		[DataMember]
		public Links12 links { get; set; }
	}

	[DataContract]
	public class Links12
	{
		[DataMember]
		public string self { get; set; }
		[DataMember]
		public string related { get; set; }
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core.Models.Dtos
{
	public class WeekNewsDto
	{
		[JsonProperty("_id")]
		public string weekliesId { get; set; }

		public string title { get; set; }

		[JsonProperty("author")]
		public string authorId { get; set; }

		public long date { get; set; }

		public List<string> news { get; set; }

		public bool published { get; set; }

		public int __v { get; set; }

		public List<FullNewsDto> fullNews { get; set; }
	}
}


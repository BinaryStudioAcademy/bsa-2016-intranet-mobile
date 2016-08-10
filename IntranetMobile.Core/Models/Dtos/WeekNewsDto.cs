using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core
{
	public class WeekNewsDto
	{
		[JsonProperty("_id")]
		public string WeekliesId { get; set; }

		public string Title { get; set; }

		[JsonProperty("author")]
		public string AuthorId { get; set; }

		public long Date { get; set; }

		public List<string> News { get; set; }

		public bool Published { get; set; }

		public int __V { get; set; }

		public List<FullNewsDto> FullNews { get; set; }
	}
}


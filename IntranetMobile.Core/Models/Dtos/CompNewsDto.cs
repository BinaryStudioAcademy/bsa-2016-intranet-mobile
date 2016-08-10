using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core
{
	public class CompNewsDto
	{
		[JsonProperty("_id")]
		public string NewsId { get; set; }

		[JsonProperty("author")]
		public string AuthorId { get; set; }
		public string Title { get; set; }

		public string Body { get; set; }

		public long Date { get; set; }

		public string Type { get; set;}

		public int _V { get; set;}

		public List<string> Restrict_ids { get; set;}

		public List<string> Access_roles { get; set; }

		public List<string> Likes { get; set; }

		public List<CommentDto> Comments { get; set; }
	}
}


using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IntranetMobile.Core
{
	public class CommentDto
	{
		[JsonProperty("author")]
		public string AuthorId { get; set;}

		public string Body { get; set; }

		public long Date { get; set;}

		public string CommentId { get; set;}

		public List<string> Likes { get; set;}
	}
}


﻿using System.Text.Json.Serialization;

namespace Lab6.Models
{
    public class PostData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}

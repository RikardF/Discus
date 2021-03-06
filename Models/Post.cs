using Google.Cloud.Firestore;
using ExArbete.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExArbete.Models
{
    [FirestoreData]
    public class Post : IPost
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty("category_id")]
        public string? CategoryId { get; set; }
        [FirestoreProperty]
        [Required]
        [MinLength(2, ErrorMessage = "Message too short. Be more creative please.")]
        public string? Content { get; set; }
        [FirestoreProperty("created_at")]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty("created_by")]
        public string? CreatedBy { get; set; }
        [FirestoreProperty]
        public List<string>? Likes { get; set; }
        [FirestoreProperty("sub-posts")]
        public List<SubPost>? SubPosts { get; set; }
        [FirestoreProperty]
        [Required]
        [MinLength(2, ErrorMessage = "Title too short. Be more creative please.")]
        public string? Title { get; set; }
        public Post(){}
    }
}
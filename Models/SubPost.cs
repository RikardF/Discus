using Google.Cloud.Firestore;
using ExArbete.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExArbete.Models
{
    [FirestoreData]
    public class SubPost : IPost
    {
        [FirestoreProperty]
        public string? Id { get; set; }
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
        [FirestoreProperty("parent-post-id")]
        public string? ParentPostId { get; set; }
        [FirestoreProperty]
        public string? Title { get; set; }
        public SubPost(){}
    }
}
using Google.Cloud.Firestore;

namespace ExArbete.Data
{
    [FirestoreData]
    public class Post
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty("category_id")]
        public string? CategoryId { get; set; }
        [FirestoreProperty]
        public string? Content { get; set; }
        [FirestoreProperty("created_at")]
        [FirestoreDocumentCreateTimestamp]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty("created_by")]
        public string? CreatedBy { get; set; }
        [FirestoreProperty]
        public List<string>? Likes { get; set; }
        [FirestoreProperty]
        public List<Post>? SubPosts { get; set; }
        [FirestoreProperty]
        public string? Title { get; set; }
    }
}
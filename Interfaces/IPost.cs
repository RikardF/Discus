using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IPost
    {
        public string? Id { get; set; }
        public string? Content { get; set; }
        public Timestamp CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public List<string>? Likes { get; set; }
    }
}
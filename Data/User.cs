using Google.Cloud.Firestore;

namespace ExArbete.Data
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? Username { get; set; }
        [FirestoreProperty("created_at")]
        [FirestoreDocumentCreateTimestamp]
        public DateTime CreatedAt { get; set; }
    }
}
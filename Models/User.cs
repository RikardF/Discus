using Google.Cloud.Firestore;

namespace ExArbete.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? Username { get; set; }
        [FirestoreProperty]
        public string? Email { get; set; }
        [FirestoreProperty("created_at")]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty("last_visit")]
        public Timestamp LastVisit { get; set; }
        [FirestoreProperty("google_image")]
        public string? GoogleImage { get; set; }
        [FirestoreProperty("google_name")]
        public string? GoogleName { get; set; }
        
    }
}
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
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty("last_visit")]
        public Timestamp LastVisit { get; set; }
    }
}
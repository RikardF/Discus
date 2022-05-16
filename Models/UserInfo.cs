using Google.Cloud.Firestore;
using ExArbete.Interfaces;

namespace ExArbete.Models
{
    [FirestoreData]
    public class UserInfo : IUser
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? Username { get; set; }
        [FirestoreProperty("created_at")]
        public Timestamp CreatedAt { get; set; }
        [FirestoreProperty("last_visit")]
        public Timestamp LastVisit { get; set; }
        [FirestoreProperty("google_image")]
        public string? GoogleImage { get; set; }
        
    }
}
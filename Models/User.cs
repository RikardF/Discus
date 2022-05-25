using Google.Cloud.Firestore;
using ExArbete.Interfaces;
using ExArbete.Data;

namespace ExArbete.Models
{
    [FirestoreData]
    public class User : IUser
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
        [FirestoreProperty("profile_image")]
        public string? ProfileImage { get; set; }
        [FirestoreProperty("google_name")]
        public string? GoogleName { get; set; }
        [FirestoreProperty("enable_notifications")]
        public bool EnableNotifications { get; set; }
        [FirestoreProperty("user_role")]
        public UserRole UserRole { get; set; }
    }
}
using Google.Cloud.Firestore;
using ExArbete.Interfaces;
using System.ComponentModel.DataAnnotations;

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
        
    }
}
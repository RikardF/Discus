using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IUser
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public Timestamp CreatedAt { get; set; }
        public Timestamp LastVisit { get; set; }
        public string? ProfileImage { get; set; }
    }
}
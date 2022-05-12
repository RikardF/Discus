using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IUserService
    {
        User? User { get; set; }
        bool IsNewUser { get; set; }
        Task GetDataIfExistingUser(string email, FirestoreDb firestorDb);
        Task<bool> CreateUser(FirestoreDb firestoreDb);
    }
}
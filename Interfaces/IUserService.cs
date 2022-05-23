using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IUserService
    {
        User? User { get; set; }
        bool IsNewUser { get; set; }
        Task GetDataIfExistingUser(string email, FirestoreDb firestoreDb);
        Task<bool> CreateUser(UserSettings userSettings, FirestoreDb firestoreDb);
        Task UpdateProfile(UserSettings newInfo, FirestoreDb firestoreDb, ICloudStorageService cloudStorageService);
        Task UpdateLastVisit(FirestoreDb firestoreDb);
    }
}
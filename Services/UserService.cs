using ExArbete.Interfaces;
using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Services
{
    public class UserService : IUserService
    {
        public User? User { get; set; }
        public bool IsNewUser { get; set; }

        public async Task GetDataIfExistingUser(string email, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            Query byEmail = collection.WhereEqualTo("Email", email);
            QuerySnapshot snapshot = await byEmail.GetSnapshotAsync();

            if (snapshot.Documents.Count() > 0)
            {
                User = snapshot.Documents.First().ConvertTo<User>();
                this.IsNewUser = false;
            }
            else
            {
                this.IsNewUser = true;
            }
        }

        public async Task<bool> CreateUser(UserSettings userSettings, FirestoreDb firestoreDb)
        {
            bool error;
            User!.CreatedAt = Timestamp.GetCurrentTimestamp();
            User.LastVisit = Timestamp.GetCurrentTimestamp();
            User.Username = userSettings.NewUsername;
            User.EnableNotifications = true;

            CollectionReference collection = firestoreDb.Collection("users");
            Query byUsername = collection.WhereEqualTo("Username", User.Username);
            QuerySnapshot snapshot = await byUsername.GetSnapshotAsync();
            if (snapshot.Documents.Count() > 0)
            {
                error = true;
            }
            else
            {
                DocumentReference doc = await collection.AddAsync(User);
                User.Id = doc.Id;
                error = false;
                IsNewUser = false;
            }
            return error;
        }
        public async Task UpdateProfile(UserSettings newInfo, FirestoreDb firestoreDb, ICloudStorageService cloudStorageService)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            if (newInfo.NewProfileImage?.Count() > 0)
            {
                bool imageExist = await cloudStorageService.CheckIfImageExist(User?.Id!);
                if (imageExist)
                {
                    await cloudStorageService.DeleteFileAsync(User?.Id!);
                }
                string newImageUrl = await cloudStorageService.UploadFileAsync(newInfo.NewProfileImage.First(), User?.Id!);
                User!.ProfileImage = newImageUrl;
            }
            if (newInfo.NewUsername != null)
            {
                User!.Username = newInfo.NewUsername;
            }
            User!.EnableNotifications = newInfo.EnableNotifications;
            await collection.Document(User?.Id).SetAsync(User);
        }
        public async Task UpdateLastVisit(FirestoreDb firestoreDb)
        {
            DocumentReference doc = firestoreDb.Collection("users").Document(User?.Id);
            await doc.UpdateAsync("last_visit", Timestamp.GetCurrentTimestamp());
        }
    }
}
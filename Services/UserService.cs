using ExArbete.Interfaces;
using ExArbete.Models;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Components;

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

            if(snapshot.Documents.Count() > 0)
            {
                User = snapshot.Documents.First().ConvertTo<User>();
                this.IsNewUser = false;
            } else {
                this.IsNewUser = true;
            }
        }

        public async Task<bool> CreateUser(FirestoreDb firestoreDb)
        {
            bool error;
            User.CreatedAt = Timestamp.GetCurrentTimestamp();
            User.LastVisit = Timestamp.GetCurrentTimestamp();

            CollectionReference collection = firestoreDb.Collection("users");
            Query byUsername = collection.WhereEqualTo("Username", User.Username);
            QuerySnapshot snapshot = await byUsername.GetSnapshotAsync();
            if(snapshot.Documents.Count() > 0)
            {
                error = true;
            } else {
                DocumentReference doc = await collection.AddAsync(User);
                User.Id = doc.Id;
                error = false;
            }
            return error;
        }
    }
}
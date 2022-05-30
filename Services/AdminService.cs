using ExArbete.Interfaces;
using ExArbete.Models;
using ExArbete.Data;
using Google.Cloud.Firestore;

namespace ExArbete.Services
{
    public class AdminService : IAdminService
    {
        public List<User> OwnerUsers { get; set; }
        public List<User> AdminUsers { get; set; }
        public List<User> UserUsers { get; set; }
        public event EventHandler? OnCategoriesChanged;
        public AdminService()
        {
            OwnerUsers = new();
            AdminUsers = new();
            UserUsers = new();
        }
        public async Task GetUsersByRole(UserRole userRole, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            Query byRole = collection.WhereEqualTo("user_role", userRole);
            QuerySnapshot snapshot = await byRole.GetSnapshotAsync();

            if (snapshot.Documents.Count() > 0)
            {
                switch (userRole)
                {
                    case UserRole.Owner:
                        {
                            OwnerUsers = snapshot.Documents.Select(u => u.ConvertTo<User>()).ToList<User>();
                            break;
                        }
                    case UserRole.Admin:
                        {
                            AdminUsers = snapshot.Documents.Select(u => u.ConvertTo<User>()).ToList<User>();
                            break;
                        }
                    case UserRole.User:
                        {
                            UserUsers = snapshot.Documents.Select(u => u.ConvertTo<User>()).ToList<User>();
                            break;
                        }
                }
            }
        }
        public async Task UpdateUserRole(User userToUpdate, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            DocumentReference doc = collection.Document(userToUpdate.Id);
            await doc.UpdateAsync("user_role", userToUpdate.UserRole);
        }
        public async Task<string> AddCategory(Category newCategory, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("categories");
            DocumentReference result = await collection.AddAsync(newCategory);
            return result.Id;
        }
        public async Task DeleteUser(User user, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            DocumentReference doc = collection.Document(user.Id);
            await doc.DeleteAsync();
            await GetUsersByRole(user.UserRole, firestoreDb);
        }
        public async Task DeleteCategory(Category cat, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("categories");
            DocumentReference doc = collection.Document(cat.Id);
            await doc.DeleteAsync();
            
        }
        public async Task UpdateCategory(Category cat, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("categories");
            DocumentReference doc = collection.Document(cat.Id);
            await doc.SetAsync(cat);
        }
        public void CategoriesChanged()
        {
            OnCategoriesChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
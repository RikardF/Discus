using ExArbete.Models;
using Google.Cloud.Firestore;
using ExArbete.Data;

namespace ExArbete.Interfaces
{
    public interface IAdminService
    {
        List<User> OwnerUsers { get; set; }
        List<User> AdminUsers { get; set; }
        List<User> UserUsers { get; set; }
        event EventHandler? OnCategoriesChanged;
        Task GetUsersByRole(UserRole userRole, FirestoreDb firestoreDb);
        Task UpdateUserRole(User userToUpdate, FirestoreDb firestoreDb);
        Task<string> AddCategory(Category newCategory, FirestoreDb firestoreDb);
        Task DeleteUser(User user, FirestoreDb firestoreDb);
        Task DeleteCategory(Category cat, FirestoreDb firestoreDb);
        Task UpdateCategory(Category cat, FirestoreDb firestoreDb);
        void CategoriesChanged();
    }
}
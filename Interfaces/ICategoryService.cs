using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface ICategoryService
    {
        List<Category> CategoryList { get; set; }
        Dictionary<string, int> BadgeValues { get; set; }
        event EventHandler? BadgeValueChanged;
        void CalculateBadgeValues(List<Post> postList, Timestamp lastVisit);
        void ClearBadgeValue(string categoryId);
        Task GetCategories(FirestoreDb firestoreDb);
    }
}
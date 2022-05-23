using ExArbete.Interfaces;
using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Services
{
    public class CategoryService : ICategoryService
    {
        public List<Category> CategoryList { get; set; }
        public Dictionary<string, int> BadgeValues { get; set; }
        public event EventHandler? BadgeValueChanged;
        public CategoryService()
        {
            this.CategoryList = new();
            this.BadgeValues = new();
        }
        public async Task GetCategories(FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("categories");
            Query orderByName = collection.OrderBy("Name");
            QuerySnapshot snapshot = await orderByName.GetSnapshotAsync();
            this.CategoryList = snapshot.Documents.Select(d => d.ConvertTo<Category>()).ToList<Category>();
        }
        public void CalculateBadgeValues(List<Post> postList, Timestamp lastVisit)
        {
            foreach (Category cat in CategoryList)
            {
                int badgeCount = postList.Where(p => p.CategoryId == cat.Id).Where(p => p.CreatedAt > lastVisit).Count();
                if (!BadgeValues.ContainsKey(cat.Id!))
                {
                    BadgeValues.Add(cat.Id!, badgeCount);
                }
            }
            BadgeValueChanged?.Invoke(this, EventArgs.Empty);
        }
        public void ClearBadgeValue(string categoryId)
        {
            if (BadgeValues.ContainsKey(categoryId))
            {
                BadgeValues[categoryId] = 0;
                BadgeValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
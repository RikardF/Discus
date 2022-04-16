using Google.Cloud.Firestore;

namespace ExArbete.Data
{
    [FirestoreData]
    public class Category
    {
        [FirestoreProperty]
        public string? Name { get; set; }
        [FirestoreProperty]
        public string? Icon { get; set; }
    }
}
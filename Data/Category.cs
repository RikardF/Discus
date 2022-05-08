using Google.Cloud.Firestore;

namespace ExArbete.Data
{
    [FirestoreData]
    public class Category
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty]
        public string? Name { get; set; }
        [FirestoreProperty]
        public string? Icon { get; set; }
    }
}
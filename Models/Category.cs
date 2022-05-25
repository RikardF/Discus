using Google.Cloud.Firestore;
using System.ComponentModel.DataAnnotations;

namespace ExArbete.Models
{
    [FirestoreData]
    public class Category
    {
        [FirestoreDocumentId]
        public string? Id { get; set; }
        [FirestoreProperty]
        [Required]
        public string? Name { get; set; }
        [FirestoreProperty]
        [Required]
        public string? Icon { get; set; }
    }
}
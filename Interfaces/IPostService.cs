using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IPostService
    {
        List<Post> PostList { get; set; }
        Task PublishPost(string? parentId, IPost post, FirestoreDb firestoreDb);
        Task<IUser> GetUserInfo(string userId, FirestoreDb firestoreDb);
    }
}
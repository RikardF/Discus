using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Interfaces
{
    public interface IPostService
    {
        List<Post> PostList { get; set; }
        Task PublishPost(string? parentId, IPost post, FirestoreDb firestoreDb);
        Task<IUser> GetUserInfo(string userId, FirestoreDb firestoreDb);
        Task DeletePost(string postId, FirestoreDb firestoreDb);
        Task LikePost(string? parentId, string postId, string userId, FirestoreDb firestoreDb);
        Task UnLikePost(string? parentId, string postId, string userId, FirestoreDb firestoreDb);
        event EventHandler? OnPostDataChanged;
        void PostDataChanged();
    }
}
using ExArbete.Interfaces;
using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Services
{
    public class PostService : IPostService
    {
        public List<Post> PostList { get; set; }
        public PostService()
        {
            this.PostList = new();
        }
        public async Task PublishPost(string? parentId, IPost post, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("posts");
            if(parentId == null)
            {
                await collection.AddAsync(post);
            } else {
                DocumentReference doc = collection.Document(parentId);
                await doc.UpdateAsync("sub-posts", FieldValue.ArrayUnion(post));
            }
        }
        public async Task<IUser> GetUserInfo(string userId, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("users");
            DocumentReference doc = collection.Document(userId);
            DocumentSnapshot snapshot = await doc.GetSnapshotAsync();
            return snapshot.ConvertTo<UserInfo>();
        }
    }
}
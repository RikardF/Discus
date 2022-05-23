using ExArbete.Interfaces;
using ExArbete.Models;
using Google.Cloud.Firestore;

namespace ExArbete.Services
{
    public class PostService : IPostService
    {
        public List<Post> PostList { get; set; }
        public event EventHandler? OnPostDataChanged;
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
        public async Task DeletePost(string postId, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("posts");
            DocumentReference doc = collection.Document(postId);
            await doc.DeleteAsync();
            PostDataChanged();
        }
        public async Task LikePost(string? parentId, string postId, string userId, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("posts");
            if(parentId != null)
            {
                DocumentReference doc = collection.Document(parentId);
                DocumentSnapshot snapshot = await doc.GetSnapshotAsync();
                List<SubPost> subPosts = snapshot.ConvertTo<Post>().SubPosts!;
                subPosts?.Where(s => s.Id == postId)?.First()?.Likes?.Add(userId);
                await doc.UpdateAsync("sub-posts", subPosts);
            } else {
                DocumentReference doc = collection.Document(postId);
                await doc.UpdateAsync("Likes", FieldValue.ArrayUnion(userId));
            }
            PostDataChanged();
        }
        public async Task UnLikePost(string? parentId, string postId, string userId, FirestoreDb firestoreDb)
        {
            CollectionReference collection = firestoreDb.Collection("posts");
            if(parentId != null)
            {
                DocumentReference doc = collection.Document(parentId);
                DocumentSnapshot snapshot = await doc.GetSnapshotAsync();
                List<SubPost> subPosts = snapshot.ConvertTo<Post>().SubPosts!;
                subPosts?.Where(s => s.Id == postId)?.First()?.Likes?.Remove(userId);
                await doc.UpdateAsync("sub-posts", subPosts);
            } else {
                DocumentReference doc = collection.Document(postId);
                await doc.UpdateAsync("Likes", FieldValue.ArrayRemove(userId));
            }
            PostDataChanged();
        }
        public void PostDataChanged()
        {
            OnPostDataChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
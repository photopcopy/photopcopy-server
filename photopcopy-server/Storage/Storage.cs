using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace photopcopy_server
{

    public abstract class Storage
	{
        // after spending 10 minutes reading on whether to use struct or classes
        // i still don't know the difference

        public readonly static Storage instance = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? new LocalStorage() : null;

        public struct RegisterUserDetails
        {
            public string Username;
            public string Password;
        }

        public struct CreateCommentDetails
		{
            public string Post;
            public string User; //userid
            public string Content;
		}

        public struct LikePostDetails
        {
            public string Post; //postId
            public string User; //userId
        }

        public struct CreatePostDetails
		{
            public string User; //userid
            public string Content;
            public string[] Attachments;
		}

        public struct GetPostsDetails
		{
            public string? Group;
            public string Last; //postid
		}

        public class Comment
		{
            public DateTime CreatedAt;
            public string Id { get; set; } //commentid
            public string Author { get; set; } //userid
            public string Content { get; set; }
		}

        [Flags]
        public enum Badges
		{
            None = 0,
            Verified = 1,
            Moderator = 2,
            Owner = 4,
        }

        public class Post
		{
            public string Id { get; set; } //postid
            public string Author { get; set; } // userid
            public string[] Attachments { get; set; } // url[]
            public string Content { get; set; }
            public bool IsLiked { get; set; } // this will be false for users that aren't signed in
            public int Likes { get; set; }
            public Badges Badges { get; set; }
            public List<Comment> Comments { get; set; }

        }

        public class User
		{
            public string Id { get; set; } //userid
            public string Username { get; set; }
            // maybe: public string displayName;
            public string Avatar { get; set; }  //url
            public List<string> Followers { get; set; }

        }

        public abstract Task<User> GetUser(string userid);
        public abstract Task<string> GetUserIdFromToken(string token);

        public abstract void LikePost(LikePostDetails details);

        public abstract void CreateComment(CreateCommentDetails details);

        public abstract void CreatePost(CreatePostDetails details);

        public abstract Task<List<Post>> GetPosts(GetPostsDetails details);

        public abstract Task<string> RegisterUser(RegisterUserDetails details);

        public static string CreateToken()
        {
            return Guid.NewGuid().ToString();
        }

    }

}

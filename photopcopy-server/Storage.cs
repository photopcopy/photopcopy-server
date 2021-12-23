using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace photopcopy_server
{

    public abstract class Storage
	{
        // after spending 10 minutes reading on whether to use struct or classes
		// i still don't know the difference

        public readonly static Storage instance = new LocalStorage();

        public struct CreateCommentDetails
		{
            public string authorization;
            public string content;
		}

        public struct CreatePostDetails
		{
            public string authorization;
            public string content;
            public string[] attachments;
		}

        public struct GetPostsDetails
		{
            public string last; //postid
		}

        public class Comment
		{
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
            // public Comment[] comments { get; set; }

        }

        public class User
		{
            public string id; //userid
            public string username;
            // maybe: public string displayName;
            public string avatar; //url
		}

        public abstract User GetUser(string userid);

        public abstract void CreateComment(CreateCommentDetails details);

        public abstract void CreatePost(CreatePostDetails details);

        public abstract Task<List<Post>> GetPosts(GetPostsDetails details);

        public static string CreateToken()
        {
            return Guid.NewGuid().ToString();
        }

    }

}

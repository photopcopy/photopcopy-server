using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace photopcopy_server
{
    public class LocalStorage : Storage
    {

        public struct UserRegisterDetails
        {
            public string username;
            public string password;
        }

        struct UserDetailsEntry
        {
            
            public string id;
            public string username;
            public string avatar;
            //public string hashedPassword;
            //public string salt;
        }

        //Lookup user by id
        Dictionary<string, UserDetailsEntry> users = new Dictionary<string, UserDetailsEntry>();

        //Lookup userid by token
        Dictionary<string, string> userTokens = new Dictionary<string, string>();

        List<Post> posts = new List<Post>();



        public override User GetUser(string userid)
        {
            var user = users[userid];

            Console.WriteLine(user);
		
			return null;
        }

        public override void CreateComment(CreateCommentDetails details)
        {
            Console.WriteLine(details.content);
        }

        public override void CreatePost(CreatePostDetails details)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<Post>> GetPosts(GetPostsDetails details) {
            var list = new List<Post>();

            list.Add(posts[0]);
            list.Add(posts[1]);

            return list;
        }

        public static async Task<string> RegisterUser(UserRegisterDetails details)
        {

            //Dumbass idea to hash passwords while in development mode
			//// generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
			//byte[] salt = new byte[128 / 8];
			//using (var rngCsp = new RNGCryptoServiceProvider())
			//{
			//	rngCsp.GetNonZeroBytes(salt);
			//}

			//// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
			//string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
			//	password: details.password,
			//	salt: salt,
			//	prf: KeyDerivationPrf.HMACSHA256,
			//	iterationCount: 100000,
			//	numBytesRequested: 256 / 8));

			return await Task.FromResult("okay");
        }

        public LocalStorage()
		{
            for (int i = 0; i < 100; i++)
            {
                posts.Add(new Post
                {
                    Author = "abcdef",
                    IsLiked = false,
                    Likes = 0,
                    Attachments = { },
                    Content = "It's deeeep!! Yes. That's feels amazing. Harder, haaaaarder, oohhhhhhhh, I'M COMMMMMING",
                    Id = i.ToString(),
                });
            }
        }
    }

}

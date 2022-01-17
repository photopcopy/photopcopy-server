// LocalStorage.cs
// During development mode, we use a local server but we aren't handling big amounts of data so we just make a sort of pseudo-database

using System;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace photopcopy_server
{
	public class LocalStorage : Storage
	{

		// user details entry is only for local so password can just be in plaintext
		struct UserDetailsEntry
		{

			public string Id;
			public string Username;
			public string Avatar;
			public string Password;

			//public string Timestamp;
			//public string HashedPassword;
			//public string Salt;
		}

		//Lookup user by id
		readonly Dictionary<string, UserDetailsEntry> users = new() { { "test_user", new () {
			Id="test_user",
			Username="Test_User",
			Avatar = "./assets/DefaultProfilePic.svg",
			Password="Abcdef"
		} } };

		//Lookup userid by token
		readonly Dictionary<string, string> userTokens = new() { {"test_user_token", "test_user"} };
		readonly List<Post> postsList = new();

		// Dictionary<PostId, ...>
		readonly Dictionary<string, Post> posts = new();

		// Dictionary<CommentId, ...>
		readonly Dictionary<string, Comment> comments = new();

		int counter = 0;
		string GetUniqueId()
		{
			return (++counter).ToString();
		}


		public override Task<User> GetUser(string userid)
		{
			UserDetailsEntry userdetails = users[userid];

			return Task.FromResult(new User { Avatar = userdetails.Avatar, Id = userdetails.Id, Username = userdetails.Username });
		}

		public override Task<string> GetUserIdFromToken(string token)
		{
			if (userTokens.ContainsKey(token))
			{
				return Task.FromResult(userTokens[token]);
			} else
			{
				return Task.FromException<string>(new Exception("Userid not found"));
			}
		}

		public override void LikePost(LikePostDetails details)
		{
			throw new NotImplementedException();
		}

		public override void CreateComment(CreateCommentDetails details)
		{
			List<Comment> list = posts[details.Post].Comments;
			if (list != null)
			{
				var comment = new Comment { Author = details.User, Content = details.Content, CreatedAt = DateTime.Now, Id = GetUniqueId() };
				list.Add(comment);
				comments.Add(comment.Id, comment);
			}
		}

		public override void CreatePost(CreatePostDetails details)
		{
			throw new NotImplementedException();
		}

		public override Task<List<Post>> GetPosts(GetPostsDetails details)
		{
			List<Post> list = new();

			int start = details.Last == "" ? 0 : postsList.FindIndex((Post obj) => obj.Id == details.Last) + 1;
			for (int i = start; i < start + 10; i++)
			{
				list.Add(postsList[i]);
			}

			return Task.FromResult(list);
		}

		public override async Task<string> RegisterUser(RegisterUserDetails details)
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
			string[] names = { "Alice", "Bob", "Charlie", "David", "Eve", "Frank" };
			List<string> male = new () { "Bob" , "Charlie", "David", "Frank" };
			List<string> female = new () { "Alice", "Eve" };

			var random = new Random();

			foreach (string name in names)
			{
				// https://www.delftstack.com/howto/csharp/integer-to-hexadecimal-in-csharp/
				string id = random.Next().ToString("X");
				UserDetailsEntry entry = new UserDetailsEntry
				{
					Username = name,
					Avatar = "./assets/DefaultProfilePic.svg",
					Id = id,
				};
				users.Add(id, entry);
			}

			List<string> keyList = new(users.Keys);

			for (int i = 0; i < 100; i++)
			{
				//Author is supposed to be a user id but currently front end doesn't work that way so i will fix it later
				string target = keyList[random.Next(keyList.Count)];
				string author = keyList[random.Next(keyList.Count)];

				List<Comment> comments = new();
				if (male.IndexOf(users[author].Username)!=-1 && male.IndexOf(users[target].Username) !=-1 && author!=target)
				{
					comments.Add(new Comment
					{
						Id=GetUniqueId(),
						Author = target,
						Content ="That's gay af bro",
					});
				}



				string[] attachments = Array.Empty<string>();
				var post = new Post
				{
					Author = author,
					IsLiked = false,
					Likes = 0,
					Attachments = attachments,
					Content = string.Format(
@"Post #{0}
I want to kiss {1} so bad...
But everyone knows that will never happen.
"
					, i, target == author ? "myself" : users[target].Username),
					Id = i.ToString(),
					Badges = Badges.None,
					Comments = comments,
				};
				postsList.Add(post);
				posts.Add(post.Id, post);
			}
		}
	}

}

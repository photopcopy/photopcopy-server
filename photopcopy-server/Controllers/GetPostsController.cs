using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace photopcopy_server.Controllers
{
    [ApiController]
    [Route("/api/getposts")]
    public class GetPostsController : ControllerBase
    {
        public class GetPostResult
		{
            public Dictionary<string, Storage.Comment> Comments { get; set; }
            public List<Storage.Post> Posts { get; set;  }
            public Dictionary<string, Storage.User> Users { get; set; }
        }

        [HttpGet]
        public async Task<GetPostResult> GetPosts()
        {

            var posts = await Storage.instance.GetPosts(new Storage.GetPostsDetails
            {
                Last = Request.Query["last"].ToString()
            });

            var userids = new Dictionary<string, Storage.User>();
            var comments = new Dictionary<string, Storage.Comment>();
            foreach (Storage.Post post in posts)
			{
                if (!userids.ContainsKey(post.Author))
                {
                    userids[post.Author] = await Storage.instance.GetUser(post.Author);
                }
                foreach (Storage.Comment comment in await Storage.instance.GetComments(post.Comments.ToArray()))
                {
                    if (!userids.ContainsKey(comment.Author))
                    {
                        userids[comment.Author] = await Storage.instance.GetUser(comment.Author);
                    }
                    comments[comment.Id] = comment;
                };
            }


            return new GetPostResult
            {
                Comments = comments,
                Users = userids,
                Posts = posts,
            };
            
        }
    }
}

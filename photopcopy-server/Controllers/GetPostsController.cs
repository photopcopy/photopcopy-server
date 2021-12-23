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
            public List<Storage.Post> posts { get; set;  }
        }

        [HttpGet]
        public async Task<GetPostResult> GetPosts()
        {
            return new GetPostResult
            {
                posts = await Storage.instance.GetPosts(new Storage.GetPostsDetails
                {
                    last = Request.Query["last"].ToString()
                })
            };
        }
    }
}

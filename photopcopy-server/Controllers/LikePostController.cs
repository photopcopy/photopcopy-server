using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace photopcopy_server.Controllers
{
	[ApiController]
	[Route("/api/likepost")]
	public class LikePostController : ControllerBase
	{
		public class GetPostResult
		{
			public List<Storage.Post> posts { get; set; }
		}



		[HttpPost]
		public async void LikePost(Storage.LikePostDetails details)
		{
			string auth = Request.Headers["authorization"];
			Storage.instance.LikePost(details);

		}
	}
}
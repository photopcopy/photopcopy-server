using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace photopcopy_server.Controllers
{

	[ApiController]
	[Route("/api/comment")]
	public class CreateMessageController : ControllerBase
	{

		public class CreateCommentBody
		{
			public string Content { get; set; }
			public string Post { get; set; }
		}

		[HttpPost]
		public async void Post(CreateCommentBody details)
		{
			string auth = Request.Headers["authorization"];

			if (details.Content == null || details.Post == null)
			{
				Response.StatusCode = StatusCodes.Status400BadRequest;
				return;
			}

			if (auth == null)
			{
				Response.StatusCode = StatusCodes.Status401Unauthorized;
				return;
			}
			try
			{
				string userid = await Storage.instance.GetUserIdFromToken(auth);
				Storage.instance.CreateComment(new Storage.CreateCommentDetails { Content = details.Content, Post = details.Post, User = userid });
			} catch (Exception e) { }

		}
	}
}

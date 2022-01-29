using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using photopcopy_server.Hubby;

namespace photopcopy_server.Controllers
{

	[ApiController]
	[Route("/api/comment")]
	public class CreateMessageController : ControllerBase
	{

		private readonly IHubContext<MainHub> _hubContext;

		public class CreateCommentBody
		{
			public string Content { get; set; }
			public string Post { get; set; }
		}

		public CreateMessageController(IHubContext<MainHub> hubContext)
		{
			_hubContext = hubContext;
		}

		[HttpPost]
		public async void Post(CreateCommentBody details)
		{
			string auth = Request.Headers["authorization"];

			if (details.Content == null || details.Post == null || details.Content=="")
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
				var user = await Storage.instance.GetUser(userid);
				var comment = await Storage.instance.CreateComment(new Storage.CreateCommentDetails { Content = details.Content, Post = details.Post, User = userid });
				await _hubContext.Clients.All.SendAsync("ChatAdded", new Storage.CommentWithRefs{ Post=details.Post, Comment=comment, User=user });
			}
			catch (Exception)
			{

			}

		}
	}
}

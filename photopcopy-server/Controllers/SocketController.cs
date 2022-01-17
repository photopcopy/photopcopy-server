using System;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Text;
using Microsoft.AspNetCore.SignalR;

namespace photopcopy_server.Controllers
{
    // i really can't i've read 20+ websocket articles at this point and they all show the most rudimentary "Echo" thing and don't bother to explain what to do if there are multiple things to received.
    [ApiController]
	[Route("/api/socket")]
	public class SocketController : ControllerBase
	{
        WebSocket socket;

        [HttpGet]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
				await HandleSocket(HttpContext, webSocket);
            }
            else
            {
               Response.StatusCode = 400;
            }
        }

        class Message
		{

		}

        static void HandleMessage(Message message)
		{

		}

        // i cant i cant i cant i cant i cant i cant i cant 
        static async Task HandleSocket(HttpContext context, WebSocket webSocket)
		{
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                Encoding.UTF8.ToString();
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        //private async Task Echo(WebSocket webSocket)
        //{


        //    var buffer = new byte[1024 * 4];
        //    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //    while (!result.CloseStatus.HasValue)
        //    {
        //        var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello. You said: {Encoding.UTF8.GetString(buffer)}");
        //        await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);

        //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //    }
        //    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        //}
    }
}

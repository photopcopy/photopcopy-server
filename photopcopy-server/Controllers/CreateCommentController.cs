using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace photopcopy_server.Controllers
{

    [ApiController]
    [Route("api/comment")]
    public class CreateMessageController : ControllerBase
    {
        [HttpPost]
        public string Post(Storage.CreateCommentDetails details)
        {
            string auth = Request.Headers["Authorization"];

            Console.WriteLine(details);

            Storage.instance.CreateComment(details);

            return "yes";
        }
    }
}

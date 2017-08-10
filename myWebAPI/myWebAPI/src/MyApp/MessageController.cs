using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyApp
{
    public class MessageController : ApiController
    {
        readonly MessageGenerate messageGenerate;

        public MessageController(MessageGenerate messageGenerate)
        {
            this.messageGenerate = messageGenerate;
        }

        [HttpGet]
        public HttpResponseMessage GetMessage()
        {
            var message = messageGenerate.getMessage();
            return Request.CreateResponse(HttpStatusCode.OK, new MessageDto {Message = message});
        }
    }
}
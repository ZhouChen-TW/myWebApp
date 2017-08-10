using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using MyApp;
using Xunit;

namespace MyTests
{
    public class MessageFacts : ApiTestBase
    {
        [Fact]
        public async Task should_return_ok_with_message_when_get_message()
        {
            var client = CreateHttpClient();
            var response = await client.GetAsync("/message");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var messageDto = await response.Content.ReadAsAsync<MessageDto>();
            Assert.Equal("Hello", messageDto.Message);
        }
    }
}

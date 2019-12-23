using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace ChallengeChatBot_UnitTestProject
{
    public class StooqUnitTest
    {
        [Fact]
        public async Task GetStooq_Test()
        {
            var client = new TestClientProvider().Client;
             
            string code = "";
            var response = await client.GetAsync($"api/StooqInfo/GetStooq?stock_code={code}");

            response.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}

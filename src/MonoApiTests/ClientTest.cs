using MonoApi;
using Newtonsoft.Json;

namespace MonoApiTests
{
    public class Tests
    {
        private MonoClient _client { get; set; }

        [SetUp]
        public void Setup()
        {
            this._client = new MonoClient(Environment.GetEnvironmentVariable("token"));
        }

        [Test]
        public void GetClientInfo()
        {
            this._client.GetClientInfo();
            Assert.Pass();
        }

        [Test]
        public void GetCurrency()
        {
            this._client.GetCurrency();
            Assert.Pass();
        }

        [Test]
        public void GetExtract()
        {
            var info = this._client.GetClientInfo();
            var result = this._client.GetExtract(info.Accounts.First().Id, DateTime.Now.AddDays(-10));
            Assert.Pass();
        }
    }
}
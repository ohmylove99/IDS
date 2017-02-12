using System;
using System.Threading.Tasks;

namespace IDS.QueryService.Service
{
    public interface IHelloService
    {
        string SayHello(string username);
        Task<string> SayHelloAsync(string username);
    }
    public class HelloService : IHelloService
    {
        const string Hello = "Hello";
        public string SayHello(string username)
        {
            return Format(username);
        }

        public async Task<string> SayHelloAsync(string username)
        {
            return Format(username);
        }

        private string Format(string username)
        {
            return string.Format("{0}:{1}", Hello, username);
        }
    }
}

namespace IDS.QueryService.Controller
{
    using Service;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Hello")]
    public class HelloController : ApiController
    {
        private IHelloService helloService;

        public HelloController(IHelloService helloService)
        {
            this.helloService = helloService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(string))]
        [Route("SayHello")]
        public async Task<HttpResponseMessage> SayHello(string username)
        {
            var result = helloService.SayHello(username);
            HttpResponseMessage response = Request.CreateResponse<string>(HttpStatusCode.OK, result);
            return response;
        }

        [HttpGet]
        [ResponseType(typeof(string))]
        [Route("SayHelloAsync")]
        public async Task<HttpResponseMessage> SayHelloAsync()
        {
            var result = await helloService.SayHelloAsync("Jason");
            HttpResponseMessage response = Request.CreateResponse<string>(HttpStatusCode.OK, result);
            return response;
        }
    }
}
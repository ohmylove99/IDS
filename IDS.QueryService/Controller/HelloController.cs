namespace IDS.QueryService.Controller
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    /// <summary>
    /// 
    /// </summary>
    public class HelloController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(string))]
        public async Task<HttpResponseMessage> Get()
        {
            HttpResponseMessage response = Request.CreateResponse<string>(HttpStatusCode.OK, "Hello World!");
            return response;
        }
    }
}
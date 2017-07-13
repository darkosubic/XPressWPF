using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XPressWPF.Model;

namespace XPressWPF.WebApi.Controllers
{
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        private string _connectionString;
        public UserController()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            const string getSql = "SELECT * FROM USR.AppUser";

            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<AppUser> users = connection.Query<AppUser>(getSql);
                return Request.CreateResponse(HttpStatusCode.OK, users);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<HttpResponseMessage> Get(int id)
        {
            const string getCurrentUserSql = "SELECT * FROM USR.AppUser WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                object user = await connection.QueryAsync(getCurrentUserSql, new { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
        }
    }
}
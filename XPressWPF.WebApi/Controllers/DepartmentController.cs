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
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private string _connectionString;
        public DepartmentController()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            const string getSql = "SELECT * FROM dbo.Department";

            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<DepartmentModel> departments = connection.Query<DepartmentModel>(getSql);
                return Request.CreateResponse(HttpStatusCode.OK, departments);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<HttpResponseMessage> Insert(DepartmentModel model)
        {
            const string insertSql = "INSERT INTO dbo.Department (Name) VALUES (@Name)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(insertSql, new
                {
                    Id = model.Id,
                    Name = model.Name
                });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(DepartmentModel model)
        {
            const string updateSql = "UPDATE dbo.Department SET Name = @Name WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(updateSql, new
                {
                    Id = model.Id,
                    Name = model.Name
                });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            const string deleteSql = "DELETE FROM dbo.Department WHERE ID = @ID";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(deleteSql, new { ID = id });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
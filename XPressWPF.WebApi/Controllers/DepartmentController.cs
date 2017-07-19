using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XPressWPF.Model;
using XPressWPF.WebApi.Services;

namespace XPressWPF.WebApi.Controllers
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : ApiController
    {
        private readonly IQueryReader _queryReader;
        private string _connectionString;
        public DepartmentController(IQueryReader queryReader)
        {
            _queryReader = queryReader;
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            string getSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("GetAllDepartments");

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
            string insertSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("InsertNewDepartment");

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
            string updateSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("UpdateDepartment");

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
            string deleteSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("DeleteDepartment");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(deleteSql, new { ID = id });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}
using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using XPressWPF.Model;
using XPressWPF.WebApi.Services;

namespace XPressWPF.WebApi.Controllers
{


    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {
        private readonly IQueryReader _queryReader;

        private string _connectionString;
        public EmployeeController(IQueryReader queryReader)
        {
            _queryReader = queryReader;
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            string getSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("GetAllEmployees");

            using (var connection = new SqlConnection(_connectionString))
            {
                IEnumerable<EmployeeModel> employees = connection.Query<EmployeeModel>(getSql);
                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<HttpResponseMessage> Insert(EmployeeModel model)
        {
            string insertSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("InsertNewEmployee");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(insertSql, new
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Salary = model.Salary
                });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<HttpResponseMessage> Update(EmployeeModel model)
        {
            string updateSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("UpdateEmployee");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(updateSql, new
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Salary = model.Salary
                });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            string deleteSql = _queryReader.GetQueryFromSqlFolderWithSqlExtension("DeleteEmployee");

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(deleteSql, new { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}

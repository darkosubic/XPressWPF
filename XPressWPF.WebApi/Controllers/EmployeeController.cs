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
    [RoutePrefix("api/Employee")]
    public class EmployeeController : ApiController
    {

        private string _connectionString;
        public EmployeeController()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetAll()
        {
            const string getSql = "SELECT em.[Id] ,em.[FirstName]	,em.[LastName] ,em.[Age] ,em.[Salary] ,em.[DepartmentId] ,dp.[Name] as DepartmentName FROM [DapperTutorialDB].[dbo].[Employee] AS em LEFT JOIN [dbo].[Department] dp on em.DepartmentId = dp.Id";

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
            const string insertSql = "INSERT INTO dbo.Employee (FirstName, LastName, Age, Salary) VALUES (@FirstName, @LastName, @Age, @Salary)";

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
            const string updateSql = "UPDATE dbo.Employee SET FirstName = @FirstName, LastName = @LastName, Age = @Age, Salary = @Salary WHERE Id = @Id";

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
            const string deleteSql = "DELETE FROM dbo.Employee WHERE Id = @Id";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(deleteSql, new { Id = id });
                return Request.CreateResponse(HttpStatusCode.OK);
            }
        }
    }
}

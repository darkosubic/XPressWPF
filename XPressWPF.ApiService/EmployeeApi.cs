using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XPressWPF.Model;

namespace XPressWPF.ApiService
{
    public interface IEmployeeApi
    {
        Task<IEnumerable<EmployeeModel>> GetAllEmployees();
        Task InsertEmployee(EmployeeModel model);
        Task UpdateEmployee(EmployeeModel model);
        Task DeleteEmployee(int id);
    }
    public class EmployeeApi : IEmployeeApi
    {
        private static HttpClient _apiClient;
        // From the Tools menu, hover over Nuget Package Manager, then select Package Manager Console.  
        // Then type: Install-Package Microsoft.AspNet.WebApi.Client
        public EmployeeApi()
        {

            _apiClient = new ApiClient().Client;
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployees()
        {
            IEnumerable<EmployeeModel> employees = null;

            HttpResponseMessage response = await _apiClient.GetAsync("Employee/");
            if (response.IsSuccessStatusCode)
            {
                employees = await response.Content.ReadAsAsync<IEnumerable<EmployeeModel>>();
            }

            return employees;
        }

        public async Task InsertEmployee(EmployeeModel model)
        {
            await _apiClient.PostAsJsonAsync("Employee/Insert", model);
        }

        public async Task UpdateEmployee(EmployeeModel model)
        {
            await _apiClient.PutAsJsonAsync("Employee/Update", model);
        }

        public async Task DeleteEmployee(int id)
        {
            await _apiClient.DeleteAsync(string.Format("Employee/Delete/{0}", id));
        }
    }
}

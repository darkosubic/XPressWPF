using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XPressWPF.Model;

namespace XPressWPF.ApiService
{
    public interface IDepartmentApi
    {
        Task<IEnumerable<DepartmentModel>> GetAllDepartments();
        Task InsertDepartment(DepartmentModel model);
        Task UpdateDepartment(DepartmentModel model);
        Task DeleteDepartment(int id);
    }

    public class DepartmentApi : IDepartmentApi
    {
        private static HttpClient _apiClient;
        // From the Tools menu, hover over Nuget Package Manager, then select Package Manager Console.  
        // Then type: Install-Package Microsoft.AspNet.WebApi.Client
        public DepartmentApi()
        {

            _apiClient = new ApiClient().Client;
        }

        public async Task<IEnumerable<DepartmentModel>> GetAllDepartments()
        {
            IEnumerable<DepartmentModel> departments = null;

            HttpResponseMessage response = await _apiClient.GetAsync("Department/");
            if (response.IsSuccessStatusCode)
            {
                departments = await response.Content.ReadAsAsync<IEnumerable<DepartmentModel>>();

            }
            return departments;
        }

        public async Task InsertDepartment(DepartmentModel model)
        {
            await _apiClient.PostAsJsonAsync("Department/Insert", model);
        }

        public async Task UpdateDepartment(DepartmentModel model)
        {
            await _apiClient.PutAsJsonAsync("Department/Update", model);
        }

        public async Task DeleteDepartment(int id)
        {
            await _apiClient.DeleteAsync(string.Format("Department/Delete/{0}", id));
        }
    }

}

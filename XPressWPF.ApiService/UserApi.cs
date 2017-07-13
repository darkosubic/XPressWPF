using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using XPressWPF.Model;

namespace XPressWPF.ApiService
{
    public interface IUserApi
    {
        Task<AppUser> GetCurrentUser(int userId);
        Task<IEnumerable<AppStyles>> GetAllStyles(int userId);
        Task<AppStyles> GetCurrentStyle(int userId, int styleId);
        Task UpdateCurrentStyle(int userId, AppStyles style);
        Task InsertNewStyleAsync(int userId, AppStyles style);
        Task DeleteStyle(int styleId);
    }

    public class UserApi : IUserApi
    {
        private static HttpClient _apiClient;

        public UserApi()
        {

            _apiClient = new ApiClient().Client;
        }

        public async Task<AppUser> GetCurrentUser(int userId)
        {
            AppUser user = null;
            HttpResponseMessage response = await _apiClient.GetAsync($"User/{userId}");

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<AppUser>();
            }
            return user;
        }

        public async Task<IEnumerable<AppStyles>> GetAllStyles(int userId)
        {
            IEnumerable<AppStyles> styles = null;
            HttpResponseMessage response = await _apiClient.GetAsync($"User/{userId}/Styles");

            if (response.IsSuccessStatusCode)
            {
                styles = await response.Content.ReadAsAsync<IEnumerable<AppStyles>>();
            }
            return styles;
        }

        public async Task<AppStyles> GetCurrentStyle(int userId, int styleId)
        {
            AppStyles user = null;
            HttpResponseMessage response = await _apiClient.GetAsync($"User/{userId}/Styles/{styleId}");

            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<AppStyles>();
            }
            return user;
        }

        public async Task UpdateCurrentStyle(int userId, AppStyles style)
        {
            await _apiClient.PostAsJsonAsync($"User/Post/{userId}/Styles", style);
        }

        public async Task InsertNewStyleAsync(int userId, AppStyles style)
        {
            await _apiClient.PutAsJsonAsync($"User/Put/{userId}/Style", style );
        }

        public async Task DeleteStyle(int styleId)
        {
            await _apiClient.DeleteAsync($"User/Delete/{styleId}");
        }
    }
}

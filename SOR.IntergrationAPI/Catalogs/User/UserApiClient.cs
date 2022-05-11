using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Mobile;
using SOR.ViewModel.Catalogs.Users;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.User
{
    public class UserApiClient : BaseApiClient, IUserApiClient 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration): base(httpClientFactory, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> LoginInWed(GetLoginRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.userName.ToString()), "userName");
            requestContent.Add(new StringContent(request.passWord.ToString()), "passWord");
            var response = await client.PostAsync("/V1/admin-panel/users/login-in-web", requestContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ApiStatus> Create(GetCreateToUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.userName.ToString()), "userName");
            requestContent.Add(new StringContent(!string.IsNullOrEmpty(request.email)? request.email.ToString() : ""), "email");
            requestContent.Add(new StringContent(request.gender.ToString()), "gender");
            requestContent.Add(new StringContent(request.fullName.ToString()), "fullName");
            requestContent.Add(new StringContent(!string.IsNullOrEmpty(request.numberPhone)? request.numberPhone.ToString() : "" ), "numberPhone");
            requestContent.Add(new StringContent(request.password.ToString()), "password");
            requestContent.Add(new StringContent(request.iPCreate.ToString() ?? ""), "iPCreate");
            var response = await client.PostAsync($"/V1/admin-panel/users", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
        }
        public async Task<ApiStatus> ResetPassword(string userName, GetUpdateToUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.PassWord.ToString()), "PassWord");
            requestContent.Add(new StringContent(request.NewPassWord.ToString()), "NewPassWord");
            var response = await client.PatchAsync($"/V1/admin-panel/users/{userName}", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiStatus>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ShowNotificationViewModel> NotificationInMobile(GetNotificationRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseMobileAddress]);
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _configuration[SystemContants.AppSettings.Authorization]);

            var dNotification = new GetNotificationRequest()
            {
                notification = new Notification()
                {
                    tille = $"{request.notification.tille}",
                    body = $"{request.notification.body}"
                },
                registration_ids = request.registration_ids
            };

            string payload = JsonConvert.SerializeObject(dNotification);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"/fcm/send", content);
            
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ShowNotificationViewModel>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ShowNotificationViewModel>(await response.Content.ReadAsStringAsync());
        }
    }
}

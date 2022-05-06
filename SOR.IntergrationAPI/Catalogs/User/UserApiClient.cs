using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SOR.Data.SystemBase;
using SOR.ViewModel;
using SOR.ViewModel.Catalogs.Users;
using System;
using System.Net.Http;
using System.Threading.Tasks;


namespace SOR.IntergrationAPI.Catalogs.User
{
    public class UserApiClient : BaseApiClient, IUserApiClient 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UserApiClient(IHttpClientFactory httpClientFactory,
                    IConfiguration configuration)
            : base(httpClientFactory, configuration)
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

        public async Task<ApiResponse> Create(GetCreateToUserRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemContants.AppSettings.BaseAddress]);

            var requestContent = new MultipartFormDataContent();
            requestContent.Add(new StringContent(request.userName.ToString()), "userName");
            requestContent.Add(new StringContent(request.email.ToString()), "email");
            requestContent.Add(new StringContent(request.gender.ToString()), "gender");
            requestContent.Add(new StringContent(request.fullName.ToString()), "fullName");
            requestContent.Add(new StringContent(request.identification.ToString()), "identification");
            requestContent.Add(new StringContent(request.numberPhone.ToString()), "numberPhone");
            requestContent.Add(new StringContent(request.password.ToString()), "password");
            requestContent.Add(new StringContent(request.confirmPassword.ToString()), "confirmPassword");
            requestContent.Add(new StringContent(request.iPCreate.ToString()), "iPCreate");
            requestContent.Add(new StringContent(request.agenciesId.ToString()), "agenciesId");
            var response = await client.PostAsync($"api/User", requestContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ApiResponse>(await response.Content.ReadAsStringAsync());
        }

    }
}

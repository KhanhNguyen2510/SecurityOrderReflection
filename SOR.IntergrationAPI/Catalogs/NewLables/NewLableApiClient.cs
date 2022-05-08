using Microsoft.Extensions.Configuration;
using SOR.ViewModel.Catalogs.NewLables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SOR.IntergrationAPI.Catalogs.NewLables
{
    public class NewLableApiClient : BaseApiClient, INewLableApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public NewLableApiClient(IHttpClientFactory httpClientFactory,
                   IConfiguration configuration)
           : base(httpClientFactory, configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<GetNewsLableViewModel>> GetListToNewsLable(GetMangagerToNewsLableRequest request)
        {
            var gNewLable = await GetAsync<IEnumerable<GetNewsLableViewModel>>($"/V1/admin-panel/news-lables/list-news-lables?keyWord={request.keyWord}");
            return gNewLable;
        }

    }
}

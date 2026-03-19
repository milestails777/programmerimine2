using System.Net;
using System.Net.Http.Json;

namespace KooliProjekt.WindowsForms.Api
{
    public class ApiClient : IApiClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;

        public ApiClient()
        {
            _baseUrl = "http://localhost:5086/api/Projects/";
            _client = new HttpClient();
        }

        public async Task<OperationResult<PagedResult<Project>>> List(int page, int pageSize)
        {
            var url = _baseUrl + "List?page=" + page + "&pageSize=" + pageSize;

            return await _client.GetFromJsonAsync<OperationResult<PagedResult<Project>>>(url);
        }

        public async Task Save(Project project
            )
        {
            var url = _baseUrl + "Save";

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(project)
            };

            await _client.SendAsync(request);
        }
    }
}
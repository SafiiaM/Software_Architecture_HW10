using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClinicService.Models;

namespace ClinicServiceNamespace
{
    public class ClinicClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ClinicClient(string baseUrl, HttpClient httpClient)
        {
            _baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
            _httpClient = httpClient;
        }

        public async Task<ICollection<Client>> ClientGetAllAsync(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/api/clients", cancellationToken);
        response.EnsureSuccessStatusCode();
        string responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<List<Client>>(responseContent);
    }
        

        

        public async Task<Client> ClientGetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/api/clients/{id}";

            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            {
                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Client>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }

        
        public async Task<Client> CreateClientAsync(Client newClient, CancellationToken cancellationToken = default)
        {
            var url = $"{_baseUrl}/api/clients";

            var clientJson = JsonSerializer.Serialize(newClient);

            using (var request = new HttpRequestMessage(HttpMethod.Post, url))
            {
                request.Content = new StringContent(clientJson);
                request.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Client>(responseContent, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
        }
    }

}

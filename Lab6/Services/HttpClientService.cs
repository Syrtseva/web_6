using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Lab6.Models;

namespace Lab6.Services
{
    public class HttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://jsonplaceholder.typicode.com";
        private readonly JsonSerializerOptions _jsonOptions;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ApiResponse<PostData>> GetAsync(string endpoint) //Метод GetAsync
        {
            try
            {
                var url = $"{_baseUrl}/{endpoint}";
                var response = await _httpClient.GetAsync(url);//асинхроний гет запит
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)//200
                {
                    var data = JsonSerializer.Deserialize<List<PostData>>(content, _jsonOptions);
                    if (data == null)
                    {
                        return new ApiResponse<PostData>(
                            "Deserialization error",
                            (int)response.StatusCode
                        );
                    }
                    return new ApiResponse<PostData>("Success", (int)response.StatusCode, data);
                }
                return new ApiResponse<PostData>("Error", (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PostData>($"Exception: {ex.Message}", 500);
            }
        }

        public async Task<ApiResponse<PostResponse>> PostAsync(string endpoint, PostData postData) //Метод PostAsync
        {
            try
            {
                var url = $"{_baseUrl}/{endpoint}";
                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(postData, _jsonOptions),
                    System.Text.Encoding.UTF8,
                    "application/json"
                );
                var response = await _httpClient.PostAsync(url, jsonContent); //Виконання пост запиту
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)//201
                {
                    var data = JsonSerializer.Deserialize<PostResponse>(content, _jsonOptions);
                    return new ApiResponse<PostResponse>(
                        "Success",
                        (int)response.StatusCode,
                        new List<PostResponse> { data }
                    );
                }
                return new ApiResponse<PostResponse>("Error", (int)response.StatusCode);
            }
            catch (Exception ex)
            {
                return new ApiResponse<PostResponse>($"Exception: {ex.Message}", 500);
            }
        }
    }
}

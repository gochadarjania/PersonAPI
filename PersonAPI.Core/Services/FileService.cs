using Microsoft.Extensions.Configuration;
using PersonAPI.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PersonAPI.Core.Services
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        public FileService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["FileService:ApiKey"];
            _apiUrl = configuration["FileService:ApiUrl"];

        }

        public async Task<string> UploadImageAsync(MemoryStream image)
        {
            try
            {
                using var form = new MultipartFormDataContent
                                {
                                    { new StringContent(_apiKey), "key" },
                                    { new StringContent("upload"), "action" },
                                    { new ByteArrayContent(image.ToArray()) 
                                        { Headers = { ContentType = MediaTypeHeaderValue.Parse("image/jpeg") } }, "source", Guid.NewGuid().ToString() 
                                    }
                                };

                HttpResponseMessage response = await _httpClient.PostAsync(_apiUrl, form);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var uploadResponse = JsonSerializer.Deserialize<UploadResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (uploadResponse?.Image != null)
                {
                    return uploadResponse.Image.Url;
                }
                
               throw new Exception("Image upload failed. No URL returned.");                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    public class UploadResponse
    {
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }

        [JsonPropertyName("image")]
        public ImageDetails Image { get; set; }

        [JsonPropertyName("status_txt")]
        public string StatusText { get; set; }
    }

    public class ImageDetails
    {
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

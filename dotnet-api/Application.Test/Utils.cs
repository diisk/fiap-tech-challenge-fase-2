using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTOs.Auth;
using Application.DTOs;

namespace Application.Test
{
    public class Utils
    {
        public static async Task ConfigureAuthorization(HttpClient client)
        {
            var loginRequest = new
            {
                login = "teste",
                senha = "teste"
            };

            var content = new StringContent(JsonSerializer.Serialize(loginRequest), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/auth/logar", content);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var responseObject = DeserializerJson<BaseResponse<LogarResponse>>(responseData);

            var token = responseObject?.Data?.Token ?? throw new InvalidOperationException("Token não encontrado na resposta.");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static T? DeserializerJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}

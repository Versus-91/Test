using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using WebApplication3.Constants.cs;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClient;


        public OrderService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Order>> GetAll()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var request = new HttpRequestMessage(HttpMethod.Get,
           $"{AppConstants.BaseOrderUrl}?{GenerateApiQueryString()}");

            var client = _httpClient.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var orders = await JsonSerializer.DeserializeAsync
                    <List<Order>>(responseStream, options);
                return orders;
            }
            else
            {
                throw new Exception("error in receiving orders.");
            }
        }
        public async Task Update(Order order)
        {
            var requestContent = new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json");
            var client = _httpClient.CreateClient();
            var response = await client.PutAsync($"{AppConstants.BaseOrderUrl}/{order.Id}?{GenerateApiQueryString()}", requestContent);
            response.EnsureSuccessStatusCode();
        }
        private string GenerateApiQueryString()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[AppConstants.ConsumerSecret] = AppConstants.ConsumerSecretValue;
            query[AppConstants.Consumerkey] = AppConstants.ConsumerkeyValue;
            return query.ToString();
        }
    }
}

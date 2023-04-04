using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using VacationModule.POCO;
using VacationModule.Services.Interfaces;

using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace VacationModule.Services.Implementations
{
    public class QueryService: IQueryService
    {
        public async Task<List<Holiday>> nationalHolidays(string country, int year)
        {
            string QUERY_URL = $"https://api.api-ninjas.com/v1/holidays?country={country}&year={year}&type=national_holiday";

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };


            var httpClient = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, QUERY_URL);
            requestMessage.Headers.Add("X-Api-Key", "WVPB/TbqsDwQkzHUdN69bA==NY4VYIoQrIWjBAlR");
            var responseExample1 = await httpClient.SendAsync(requestMessage);
            var json_data = JsonSerializer.Deserialize<List<Holiday>>(await responseExample1.Content.ReadAsStringAsync(), jsonSerializerOptions);
            Console.WriteLine(responseExample1);
            return json_data;

           

        }
    }
}

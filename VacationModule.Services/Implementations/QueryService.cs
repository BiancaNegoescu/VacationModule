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
using VacationModule.DTO;
using AutoMapper;
using System.Globalization;
using System.Runtime.InteropServices;
using VacationModule.Services.Constants;

namespace VacationModule.Services.Implementations
{
    public class QueryService: IQueryService
    {
        private readonly IMapper _mapper;

        public QueryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<NationalHolidayDTO>> nationalHolidays(int year)
        {

            string QUERY_URL_NATIONAL = $"https://api.api-ninjas.com/v1/holidays?country={Countries.Romania}&year={year}&type=national_holiday";
            string QUERY_URL_ORTHODOX = $"https://api.api-ninjas.com/v1/holidays?country={Countries.Romania}&year={year}&type=national_holiday_orthodox";

            var jsonSerializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

            var httpClient = new HttpClient();
            var requestMessageNational = new HttpRequestMessage(HttpMethod.Get, QUERY_URL_NATIONAL);
            var requestMessageOrthodox = new HttpRequestMessage(HttpMethod.Get, QUERY_URL_ORTHODOX);


            requestMessageNational.Headers.Add("X-Api-Key", "WVPB/TbqsDwQkzHUdN69bA==NY4VYIoQrIWjBAlR");
            requestMessageOrthodox.Headers.Add("X-Api-Key", "WVPB/TbqsDwQkzHUdN69bA==NY4VYIoQrIWjBAlR");


            var responseNational = await httpClient.SendAsync(requestMessageNational);
            var responseOrthodox = await httpClient.SendAsync(requestMessageOrthodox);


            var nationalHolidays = JsonSerializer.Deserialize<List<NationalHoliday>>(await responseNational.Content.ReadAsStringAsync(), jsonSerializerOptions);
            var nationalHolidaysOrthodox = JsonSerializer.Deserialize<List<NationalHoliday>>(await responseOrthodox.Content.ReadAsStringAsync(), jsonSerializerOptions);

            if (nationalHolidays != null && nationalHolidaysOrthodox != null)
            {
                nationalHolidays.AddRange(nationalHolidaysOrthodox);

            } else
            {
                throw new ArgumentException("Holiday API failed fetching!");
            }

            List<NationalHolidayDTO> nationalHolidayDTOs = new List<NationalHolidayDTO>();

            for(int i = 0; i < nationalHolidays.Count; i++)
            {
                NationalHolidayDTO nationalHoliday = _mapper.Map<NationalHoliday, NationalHolidayDTO>(nationalHolidays[i]);
                DateTime date = stringToDate(nationalHolidays[i].date);
                nationalHoliday.date = date;
                nationalHolidayDTOs.Add(nationalHoliday);
            }
            return nationalHolidayDTOs;
        }

        public async Task<List<DateTime>> holidayList(int year)
        {
            List<NationalHolidayDTO> nationalHolidaysDTO = await nationalHolidays(year);


            List<DateTime> holidaysList = new List<DateTime>();
            nationalHolidaysDTO.ForEach(holiday =>
            {
                holidaysList.Add(holiday.date);
            });
            return holidaysList;
        }

        public DateTime stringToDate(string dateAsString)
        {
            string format = "yyyy-MM-dd";
            CultureInfo provider = CultureInfo.InvariantCulture;

            DateTime date = DateTime.ParseExact(dateAsString, format, provider);
            return date;
        }
    }
}

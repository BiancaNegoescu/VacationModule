using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationModule.DTO;
using VacationModule.POCO;

namespace VacationModule.Services.Mappers
{
    public class HolidayProfile: Profile
    {
        public HolidayProfile()
        {
            CreateMap<NationalHoliday, NationalHolidayDTO>()
                .ForMember(dest => dest.country, src => src.MapFrom(x => x.country))
                .ForMember(dest => dest.year, src => src.MapFrom(x => x.year))
                .ForMember(dest => dest.date, src => src.Ignore())
                .ForMember(dest => dest.weekDay, src => src.MapFrom(x => x.day))
                .ForMember(dest => dest.holidayName, src => src.MapFrom(x => x.name))
                .ForMember(dest => dest.holidayType, src => src.MapFrom(x => x.type));

            CreateMap<VacationRequest, VacationRequestDTO>()
                .ForMember(dest => dest.requestedDays, src => src.MapFrom(x => x.requestedDays))
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id));

            CreateMap<VacationRequest, AdminRequestsDTO>()
                .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.requestedDays, src => src.MapFrom(x => x.requestedDays))
                .ForMember(dest => dest.UserId, src => src.MapFrom(x => x.UserId));


        }
    }
}

using AutoMapper;
using PPSRRegistrations.Application.ViewModels;
using PPSRRegistrations.Domain.Models;
using System;
using System.Globalization;

namespace PPSRRegistrations.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {           
            CreateMap<CsvRecordViewModel, Registration>()
                .ForMember(dest => dest.RegistrationStartDate, opt => opt.MapFrom(src => DateOnly.ParseExact(src.RegistrationStartDateRaw, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None)))
                .ForMember(dest => dest.SPGACN, opt => opt.MapFrom(src => src.SPGACN.Replace(" ", "")));
        }
    }
}

using AutoMapper;
using PersonAPI.Core.Entity;
using PersonAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonAPI.Core.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonEntity, PersonModel>()
                .ForMember(dest => dest.CityName, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<PhoneNumberEntity, PhoneNumberModel>().ReverseMap();
            CreateMap<RelatedPersonEntity, RelatedPersonModel>()
                .ForMember(dest => dest.RelatedPersonFullName, opt => opt.MapFrom(src => src.RelatedPerson.FirstName + " " + src.RelatedPerson.LastName))
                .ReverseMap();

            CreateMap<CityEntity, CityModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom((src, dest, _, context) =>
                {
                    var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                    return culture == "ka" ? src.NameKa : src.NameEn;
                }));
        }
    }
}

using AutoMapper;
using LinkShortening.Business.Models;
using LinkShortening.Data.Models;
using LinkShortening.Presentation.Models;

namespace LinkShortening.Business.Profiles
{
    public class ModelProfile : Profile
    {
        public ModelProfile()
        {
            CreateMap<UrlPl, UrlBl>().ReverseMap();
            CreateMap<UrlBl, UrlDl>().ReverseMap();
        }
    }
}

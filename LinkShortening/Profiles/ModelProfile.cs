using AutoMapper;
using Business.Models;
using Data.Models;
using Presentation.Models;

namespace Business.Profiles
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

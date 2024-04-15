using AutoMapper;
using BusinessLayer.Models;
using DataLayer.Models;
using PresentationLayer.Models;

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

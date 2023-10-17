using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_EL.Entities;
using TivitProject_EL.ViewModels;

namespace TivitProject_EL.Mappings
{
    public class Maps : Profile
    {
        //BL katmanında _mapper dönüşüm yapabilsin diye buraya maps içine
        //kimin kime dönüşeceğini yazdık
        public Maps()
        {
            CreateMap<UserTivit, UserTivitDTO>().ReverseMap();
            CreateMap<TivitPhoto, TivitPhotoDTO>().ReverseMap();
            CreateMap<TivitTags, TivitTagsDTO>().ReverseMap();
        }

    }
}

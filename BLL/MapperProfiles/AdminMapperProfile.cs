using AutoMapper;
using BLL.Models.AdminModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class AdminMapperProfile : Profile
    {
        public AdminMapperProfile()
        {
            CreateMap<AdminModel, Admin>()
               .ForMember(a => a.Id, config => config.MapFrom(am => am.Id))
               .ForMember(a => a.Email, config => config.MapFrom(am => am.Email))
               .ReverseMap()
               .ForMember(am => am.Id, config => config.MapFrom(a => a.Id))
               .ForMember(am => am.Email, config => config.MapFrom(a => a.Email));

            CreateMap<CreateAdminModel, Admin>()
               .ForMember(a => a.Email, config => config.MapFrom(cam => cam.Email))
               .ForMember(a => a.Password, config => config.MapFrom(cam => cam.Password))
               .ReverseMap()
               .ForMember(cam => cam.Email, config => config.MapFrom(a => a.Email))
               .ForMember(cam => cam.Password, config => config.MapFrom(a => a.Password));
        }
    }
}

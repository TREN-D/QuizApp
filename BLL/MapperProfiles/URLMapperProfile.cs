using AutoMapper;
using BLL.Models.URLModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class URLMapperProfile: Profile
    {
        public URLMapperProfile()
        {
            CreateMap<URL, URLModel>()
                .ForMember(um => um.Id, config => config.MapFrom(u => u.Id))
                .ForMember(um => um.ActiveFrom, config => config.MapFrom(u => u.ActiveFrom))
                .ForMember(um => um.ActiveTill, config => config.MapFrom(u => u.ActiveTill))
                .ForMember(um => um.NumberOfRuns, config => config.MapFrom(u => u.NumberOfRuns))
                .ForMember(um => um.CreatedById, config => config.MapFrom(u => u.CreatedById))
                .ForMember(um => um.TestId, config => config.MapFrom(u => u.TestId))
                .ForMember(um => um.Identifier, config => config.MapFrom(u => u.Identifier))
                .ForMember(um => um.UserName, config => config.MapFrom(u => u.UserName));

            CreateMap<CreateURLModel, URL>()
                .ForMember(u => u.ActiveFrom, config => config.MapFrom(um => um.ActiveFrom))
                .ForMember(u => u.ActiveTill, config => config.MapFrom(um => um.ActiveTill))
                .ForMember(u => u.NumberOfRuns, config => config.MapFrom(um => um.NumberOfRuns))
                .ForMember(u => u.UserName, config => config.MapFrom(um => um.UserName))
                .ForMember(u => u.CreatedById, config => config.MapFrom(um => um.CreatedById))
                .ForMember(u => u.TestId, config => config.MapFrom(um => um.TestId));
        }
    }
}

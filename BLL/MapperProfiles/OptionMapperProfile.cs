using AutoMapper;
using BLL.Models.OptionModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class OptionMapperProfile: Profile
    {
        public OptionMapperProfile()
        {
            CreateMap<Option, OptionModel>()
                .ForMember(om => om.Id, config => config.MapFrom(o => o.Id))
                .ForMember(om => om.IsCorrect, config => config.MapFrom(o => o.IsCorrect))
                .ForMember(om => om.OptionText, config => config.MapFrom(o => o.OptionText))
                .ForMember(om => om.QuestionId, config => config.MapFrom(o => o.QuestionId));

            CreateMap<PatchOptionModel, Option>()
                .ForMember(o => o.OptionText, config => config.MapFrom(pom => pom.OptionText))
                .ForMember(o => o.IsCorrect, config => config.MapFrom(pom => pom.IsCorrect))
                .ReverseMap()
                .ForMember(pom => pom.OptionText, config => config.MapFrom(o => o.OptionText))
                .ForMember(pom => pom.IsCorrect, config => config.MapFrom(o => o.IsCorrect));

            CreateMap<CreateOptionModel, Option>()
                .ForMember(o => o.OptionText, config => config.MapFrom(cop => cop.OptionText))
                .ForMember(o => o.QuestionId, config => config.MapFrom(cop => cop.QuestionId))
                .ForMember(o => o.IsCorrect, config => config.MapFrom(cop => cop.IsCorrect));
        }
    }
}

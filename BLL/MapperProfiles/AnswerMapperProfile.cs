using AutoMapper;
using BLL.Models.AnswerModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class AnswerMapperProfile : Profile
    {
        public AnswerMapperProfile()
        {
            CreateMap<UpsertAnswerModel, Answer>()
                .ForMember(a => a.UserAnswer, conf => conf.MapFrom(uam => uam.UserAnswer))
                .ForMember(a => a.QuestionId, conf => conf.MapFrom(uam => uam.QuestionId))
                .ForMember(a => a.OptionId, conf => conf.MapFrom(uam => uam.OptionId))
                .ForMember(a => a.TestResultId, conf => conf.MapFrom(uam => uam.TestResultId));

            CreateMap<Answer, AnswerModel>()
                .ForMember(am => am.Id, conf => conf.MapFrom(a => a.Id))
                .ForMember(am => am.IsCorrect, conf => conf.MapFrom(a => a.IsCorrect))
                .ForMember(am => am.UserAnswer, conf => conf.MapFrom(a => a.UserAnswer))
                .ForMember(am => am.TestResultId, conf => conf.MapFrom(a => a.TestResultId))
                .ForMember(am => am.QuestionId, conf => conf.MapFrom(a => a.QuestionId))
                .ForMember(am => am.OptionId, conf => conf.MapFrom(a => a.OptionId));
        }
    }
}

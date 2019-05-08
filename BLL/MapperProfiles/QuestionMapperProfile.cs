using AutoMapper;
using BLL.Models.QuestionModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class QuestionMapperProfile : Profile
    {
        public QuestionMapperProfile()
        {
            CreateMap<Question, QuestionModel>()
                .ForMember(qm => qm.Id, config => config.MapFrom(q => q.Id))
                .ForMember(qm => qm.ScoreForAnswer, config => config.MapFrom(q => q.ScoreForAnswer))
                .ForMember(qm => qm.QuestionText, config => config.MapFrom(q => q.QuestionText))
                .ForMember(qm => qm.QuestionType, config => config.MapFrom(q => q.QuestionType))
                .ForMember(qm => qm.TestId, config => config.MapFrom(q => q.TestId))
                .ForMember(qm => qm.Options, config => config.MapFrom(q => q.Options));

            CreateMap<PatchQuestionModel, Question>()
                .ForMember(q => q.QuestionText, config => config.MapFrom(pqm => pqm.QuestionText))
                .ForMember(q => q.ScoreForAnswer, config => config.MapFrom(pqm => pqm.ScoreForAnswer))
                .ForMember(q => q.QuestionType, config => config.MapFrom(pqm => pqm.QuestionType))
                .ReverseMap()
                .ForMember(pqm => pqm.QuestionText, config => config.MapFrom(q => q.QuestionText))
                .ForMember(pqm => pqm.ScoreForAnswer, config => config.MapFrom(q => q.ScoreForAnswer))
                .ForMember(pqm => pqm.QuestionType, config => config.MapFrom(q => q.QuestionType));

            CreateMap<CreateQuestionModel, Question>()
                .ForMember(q => q.QuestionText, config => config.MapFrom(cqm => cqm.QuestionText))
                .ForMember(q => q.TestId, config => config.MapFrom(cqm => cqm.TestId));
        }
    }
}

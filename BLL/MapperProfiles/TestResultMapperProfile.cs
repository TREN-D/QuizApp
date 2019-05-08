using AutoMapper;
using BLL.Models.AnswerModels;
using BLL.Models.TestResultModels;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.MapperProfiles
{
    public class TestResultMapperProfile: Profile
    {
        public TestResultMapperProfile()
        {
            CreateMap<TestResult, TestResultModel>()
                .ForMember(trm => trm.TimeTestPassed, conf => conf.MapFrom(tr => tr.TimeTestPassed))
                .ForMember(trm => trm.DiffScore, conf => conf.MapFrom(tr => tr.DiffScore))
                .ForMember(trm => trm.Id, conf => conf.MapFrom(tr => tr.Id))
                .ForMember(trm => trm.Answers, conf => conf.MapFrom(tr => tr.Answers))
                .ForMember(trm => trm.UrlId, conf => conf.MapFrom(tr => tr.URL.Id))
                .ForMember(trm => trm.UserName, conf => conf.MapFrom(tr => tr.UserName));
        }
    }
}

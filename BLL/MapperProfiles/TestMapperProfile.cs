using AutoMapper;
using BLL.Models.TestModels;
using DAL.Entities;

namespace BLL.MapperProfiles
{
    public class TestMapperProfile: Profile
    {
        public TestMapperProfile()
        {
            CreateMap<CreateTestModel, Test>()
                .ForMember(t => t.CreatedById, config => config.MapFrom(ctm => ctm.CreatedById))
                .ForMember(t => t.TestDescription, config => config.MapFrom(ctm => ctm.TestDescription));


            CreateMap<Test, TestModel>()
                .ForMember(tm => tm.Id, config => config.MapFrom(t => t.Id))
                .ForMember(tm => tm.TestTime, config => config.MapFrom(t => t.TestTime))
                .ForMember(tm => tm.TestDescription, config => config.MapFrom(t => t.TestDescription))
                .ForMember(tm => tm.Questions, config => config.MapFrom(t => t.Questions))
                .ForMember(tm => tm.CreatedById, config => config.MapFrom(t => t.CreatedById));

            CreateMap<PatchTestModel, Test>()
                .ForMember(t => t.TestDescription, config => config.MapFrom(ptm => ptm.TestDescription))
                .ForMember(t => t.TestTime, config => config.MapFrom(ptm => ptm.TestTime))
                .ReverseMap()
                .ForMember(ptm => ptm.TestDescription, config => config.MapFrom(t => t.TestDescription))
                .ForMember(ptm => ptm.TestTime, config => config.MapFrom(t => t.TestTime));
        }
    }
}

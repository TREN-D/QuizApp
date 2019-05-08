using AutoMapper;
using BLL.MapperProfiles;

namespace WebAPI.App_Start
{
    public class AutomapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {
                config.AddProfile<AdminMapperProfile>();
                config.AddProfile<TestMapperProfile>();
                config.AddProfile<OptionMapperProfile>();
                config.AddProfile<QuestionMapperProfile>();
                config.AddProfile<URLMapperProfile>();
                config.AddProfile<TestResultMapperProfile>();
                config.AddProfile<AnswerMapperProfile>();
            });
        }
    }
}
using System.Reflection;
using Abp.Modules;
using AutoSignin.People;
using AutoSignin.People.Dto;
using AutoMapper;

namespace AutoSignin
{
    [DependsOn(typeof(AutoSigninCoreModule))]
    public class AutoSigninApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Mapper.CreateMap<Person, PersonDto>();
        }
    }
}

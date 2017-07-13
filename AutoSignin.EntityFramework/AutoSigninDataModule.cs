using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using AutoSignin.EntityFramework;

namespace AutoSignin
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(AutoSigninCoreModule))]
    public class AutoSigninDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<AutoSigninDbContext>(null);
        }
    }
}

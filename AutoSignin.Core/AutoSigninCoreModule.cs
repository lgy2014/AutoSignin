using System.Reflection;
using Abp.Modules;

namespace AutoSignin
{
    public class AutoSigninCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

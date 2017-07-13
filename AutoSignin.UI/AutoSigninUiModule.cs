using System.Reflection;
using Abp.Modules;

namespace AutoSignin.UI
{
    [DependsOn(typeof(AutoSigninDataModule), typeof(AutoSigninApplicationModule))]
    public class AutoSigninUiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

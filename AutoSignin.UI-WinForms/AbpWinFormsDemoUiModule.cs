using System.Reflection;
using Abp.Modules;

namespace AutoSignin.UI_WinForms
{
    [DependsOn(typeof(AutoSigninDataModule), typeof(AutoSigninApplicationModule))]
    public class AbpWinFormsDemoUiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

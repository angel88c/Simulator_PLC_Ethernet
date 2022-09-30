using Caliburn.Micro;
using System.Windows;
using TCP_CommTest_Server.ViewModels;

namespace TCP_CommTest_Server
{
    class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewForAsync<ShellViewModel>();
        }
    }
}

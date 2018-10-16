using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NetworkEngine.Manager.Core;
using NetworkEngine.Manager.Core.Connector;
using NetworkEngine.Manager.Core.Services;
using NetworkEngine.Manager.Dialogs;

namespace NetworkEngine.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            IoC.Setup();
            IoC.Kernel.Bind<IDialogService>().To<DialogService>().InSingletonScope();

            await IoC.ConnectorService.Init();
            Console.WriteLine("Connected");

            base.OnStartup(e);
        }
    }
}

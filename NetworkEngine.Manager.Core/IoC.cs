using NetworkEngine.Manager.Core.Connector;
using NetworkEngine.Manager.Core.Services;
using NetworkEngine.Manager.Core.ViewModels;
using Ninject;

namespace NetworkEngine.Manager.Core
{
    public static class IoC
    {
        public static IKernel Kernel { get; } = new StandardKernel();

        #region Public Service Shortcuts

        /// <summary>
        /// Shortcut to access the <see cref="ApplicationViewModel"/>
        /// </summary>
        public static ApplicationViewModel Application => Kernel.Get<ApplicationViewModel>();

        /// <summary>
        /// Shortcut to access the <see cref="IDialogService"/>
        /// </summary>
        public static IDialogService DialogService => Kernel.Get<IDialogService>();

        /// <summary>
        /// Shortcut to access the <see cref="IConnectorService"/>
        /// </summary>
        public static IConnectorService ConnectorService => Kernel.Get<IConnectorService>();

        #endregion

        public static void Setup()
        {
            Kernel.Bind<IConnectorService>()
                .To<ConnectorService>()
                .InSingletonScope();

            Kernel.Bind<ApplicationViewModel>()
                  .To<ApplicationViewModel>()
                  .InSingletonScope();
        }

        /// <summary>
        /// Get's a service from the IoC, of the specified type
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}

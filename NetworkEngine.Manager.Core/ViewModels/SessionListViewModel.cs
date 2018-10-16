using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkEngine.Manager.Core.DataModels;
using NetworkEngine.Manager.Core.DataModels.Connector;
using NetworkEngine.Manager.Core.Exceptions;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Core.ViewModels
{
    public class SessionListViewModel : BaseViewModel
    {
        #region Public Properties

        public bool IsRefreshing { get; private set; }
        public bool CanConnect => SelectedSession != null && !IsRefreshing;

        public ObservableCollection<Session> Sessions { get; internal set; }
        public Session SelectedSession { get; set; }

        #endregion

        #region Commands

        public ICommand RefreshCommand { get; }
        public ICommand ConnectCommand { get; }

        #endregion

        #region Constructor

        public SessionListViewModel()
        {
            Sessions = new ObservableCollection<Session>();

            RefreshCommand = new RelayCommand(async () => await Refresh());
            ConnectCommand = new RelayCommand(async  () => await Connect());

            Task.Run(async () => await Refresh());
        }

        #endregion

        #region Private Methods

        private async Task Refresh()
        {
            IsRefreshing = true;

            var sessions = await IoC.ConnectorService.GetSessions();

            Sessions = new ObservableCollection<Session>(sessions
                .Where(session => session.Features.Contains("tunnel"))
                .ToList());

            IsRefreshing = false;
        }

        private async Task Connect()
        {
            try
            {
                var tunnel = await IoC.ConnectorService.OpenTunnel(SelectedSession);

                Console.WriteLine($"Connected to tunnel {tunnel}");

                IoC.Application.GoToPage(ApplicationPage.Manage);
            }
            catch (TunnelException e)
            {
                // TODO: Find a way to pass through errors to the non core application
                Console.WriteLine(e);
                IoC.DialogService.ShowDialog(null);
            }
        }

        #endregion
    }
}

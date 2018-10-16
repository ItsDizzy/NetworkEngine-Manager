using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.DataModels;
using NetworkEngine.Manager.Core.DataModels.Connector;

namespace NetworkEngine.Manager.Core.ViewModels.Design
{
    public class SessionListDesignModel : SessionListViewModel
    {
        #region Singleton

        public static SessionListViewModel Instance { get; } = new SessionListDesignModel();

        #endregion

        #region Constructor

        public SessionListDesignModel()
        {
            Sessions = new ObservableCollection<Session>()
            {
                new Session()
                {
                    ID = Guid.NewGuid(),
                    ClientInfo = new ClientInfo() {
                        Host = "PaulBeast",
                        User = "paulh",
                        FileName = @"C:\Users\paulh\Desktop\NetworkEngine\NetworkEngine.exe"
                    }
                },
                new Session()
                {
                    ID = Guid.NewGuid(),
                    ClientInfo = new ClientInfo() {
                        Host = "PaulBook",
                        User = "paulh",
                        FileName = @"C:\Users\paulh\Downloads\NetworkEngine18.0.1\NetworkEngine\NetworkEngine.exe"
                    }
                }
            };
        }

        #endregion
    }
}

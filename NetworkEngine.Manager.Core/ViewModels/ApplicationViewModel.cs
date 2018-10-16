using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.Connector;
using NetworkEngine.Manager.Core.DataModels;
using NetworkEngine.Manager.Core.Services;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Core.ViewModels
{
    /// <summary>
    /// Application state
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.SessionSelect;

        #endregion

        public ApplicationViewModel()
        {
            IoC.ConnectorService.Connector.Subscribe<dynamic>("tunnel/close", msg =>
            {
                Console.WriteLine(msg);
                // Our tunnel closed, back to overview
                GoToPage(ApplicationPage.SessionSelect);
            });
        }

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }
    }
}

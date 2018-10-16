using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NetworkEngine.Manager.Core.DataModels;
using NetworkEngine.Manager.Core.DataModels.Connector;
using NetworkEngine.Manager.Core.ViewModels.Base;
using NetworkEngine.Manager.Core.ViewModels.Design;
using NetworkEngine.Manager.Core.ViewModels.Node;

namespace NetworkEngine.Manager.Core.ViewModels
{
    public class ManageViewModel : BaseViewModel
    {
        #region Private Members


        #endregion

        #region Public Properties

        public NodeStructureViewModel NodeStructure { get; }

        #endregion

        #region Commands

        public ICommand ConnectCommand { get; }
        public ICommand GetSessionsCommand { get; }

        #endregion

        #region Constructor

        public ManageViewModel()
        {
            NodeStructure = NodeStructureDesignModel.Instance;
        }

        #endregion


        #region Private Methods

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Core.ViewModels.Node
{
    public class NodeStructureViewModel : BaseViewModel
    {
        #region Public Properties

        public ObservableCollection<NodeViewModel> Items { get; internal set; }

        #endregion


        #region Constructor

        public NodeStructureViewModel()
        {

        }

        #endregion

    }
}

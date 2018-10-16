using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Core.ViewModels.Node
{
    public class NodeViewModel : BaseViewModel
    {
        #region Public Properties

        public string Name { get; internal set; }
        public Guid UUID { get; internal set; }

        public bool IsExpanded { get; set; }
        public ObservableCollection<NodeViewModel> Children { get; internal set; }
        public ObservableCollection<dynamic> Components { get; internal set; }

        #endregion

        #region Construcotr

        public NodeViewModel()
        {
            Children = new ObservableCollection<NodeViewModel>();
            Components = new ObservableCollection<dynamic>();

            IsExpanded = Children.Count > 0;
        }

        #endregion
    }
}

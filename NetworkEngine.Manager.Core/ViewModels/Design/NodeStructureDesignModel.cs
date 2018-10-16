using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.ViewModels.Node;

namespace NetworkEngine.Manager.Core.ViewModels.Design
{
    public class NodeStructureDesignModel : NodeStructureViewModel
    {
        #region Singleton

        public static NodeStructureDesignModel Instance { get; } = new NodeStructureDesignModel();

        #endregion

        #region Constructor

        public NodeStructureDesignModel()
        {
            Items = new ObservableCollection<NodeViewModel>()
            {
                new NodeViewModel()
                {
                    Name = "Camera",
                    UUID = Guid.NewGuid()
                },
                new NodeViewModel()
                {
                    Name = "Head",
                    UUID = Guid.NewGuid()
                },
                new NodeViewModel()
                {
                    Name = "GroundPlane",
                    UUID = Guid.NewGuid()
                },
                new NodeViewModel()
                {
                    Name = "Grouped",
                    UUID = Guid.NewGuid(),
                    Children = new ObservableCollection<NodeViewModel>()
                    {
                        new NodeViewModel()
                        {
                            Name = "Child",
                            UUID = Guid.NewGuid()
                        },
                        new NodeViewModel()
                        {
                            Name = "Child",
                            UUID = Guid.NewGuid()
                        },
                        new NodeViewModel()
                        {
                            Name = "Child",
                            UUID = Guid.NewGuid()
                        },
                        new NodeViewModel()
                        {
                            Name = "Child",
                            UUID = Guid.NewGuid()
                        }
                    }
                }
            };
        }

        #endregion
    }
}

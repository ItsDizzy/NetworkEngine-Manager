using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Pages
{
    /// <summary>
    /// A page with added ViewModel support
    /// </summary>
    /// <typeparam name="VM">The ViewModel to associate with this page</typeparam>
    public class BasePage<VM> : Page
        where VM : BaseViewModel, new()
    {
        #region Private Members

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        private VM _viewModel;

        #endregion

        #region Public Properties

        /// <summary>
        /// The View Model associated with this page
        /// </summary>
        public VM ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel == value)
                    return;

                _viewModel = value;

                DataContext = _viewModel;
            }
        }

        #endregion

        #region Constructor

        public BasePage()
        {
            ViewModel = new VM();
        }

        #endregion
    }
}

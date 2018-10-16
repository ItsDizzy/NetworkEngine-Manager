using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NetworkEngine.Manager.Core.Services;
using NetworkEngine.Manager.Core.ViewModels.Base;

namespace NetworkEngine.Manager.Dialogs
{
    public class DialogService : IDialogService
    {
        #region Implementation of IDialogService

        public void ShowDialog(BaseViewModel viewModel)
        {
            new DialogWindow().ShowDialog();
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException e)
            {
                MessageBox.Show(e.ToString(), "DialogService Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        #endregion
    }
}

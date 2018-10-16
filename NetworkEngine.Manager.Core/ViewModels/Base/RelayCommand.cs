using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkEngine.Manager.Core.ViewModels.Base
{
    /// <summary>
    /// A basic command that runs an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private readonly Action _execute;

        #endregion

        #region Public Events

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="execute">The action to execute</param>
        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        #endregion

        #region Command Methods

        /// <inheritdoc />
        /// <summary>
        /// A relay command can always execute
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Executes the commands Action
        /// </summary>
        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion
    }
}

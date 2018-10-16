using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetworkEngine.Manager.Core.ViewModels.Base
{
    /// <summary>
    /// A parameterized command that runs an Action
    /// </summary>
    /// <typeparam name="T">The expected parameter type</typeparam>
    public class RelayParameterizedCommand<T> : ICommand
    {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private readonly Action<T> _execute;

        #endregion

        #region Public Events

        /// <inheritdoc />
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="execute">The action to execute</param>
        public RelayParameterizedCommand(Action<T> execute)
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
            _execute((T)parameter);
        }

        #endregion
    }
}

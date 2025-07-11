using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdventureBoard
{
    public class PlaySound_Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private ViewModel viewModel;

        public PlaySound_Command(ViewModel vm)
        {
            viewModel = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Signal to Eventhandler
        /// </summary>
        public void FireCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            // TODO: implement try/catch here?
            Console.WriteLine("\n - Command Execute hit\n");
            viewModel.playSound();
        }
    }
}

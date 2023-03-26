using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        private INavigator _navigator;
        private readonly IViewModelAbstractFactory _viewModelFactory;
        private readonly Store _store;

        public event EventHandler? CanExecuteChanged;

        public UpdateCurrentViewModelCommand(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store)
        {
            _navigator = navigator; 
            _viewModelFactory = viewModelFactory;
            _store = store;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType, _navigator, _store, null);
            }
        }
    }
}

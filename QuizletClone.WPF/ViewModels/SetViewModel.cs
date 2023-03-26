using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizletClone.WPF.ViewModels.Factories;
using QuizletClone.WPF.Commands.Base;

namespace QuizletClone.WPF.ViewModels
{
    public class SetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TermCount { get; set; }

        public string AuthorName { get; set; }

        private INavigator _navigator { get; set; }
        private readonly Store _store;
        private readonly IViewModelAbstractFactory _viewModelFactory;
        public ICommand GoToSetDetailCommand { get; set; }

        public SetViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store)
        {
            // Init
            _store = store;
            _viewModelFactory = viewModelFactory;
            _navigator = navigator;

            // Commands
            GoToSetDetailCommand = new RelayCommand(() => GoToSetDetail());
        }

        private void GoToSetDetail()
        {
            _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.SetDetail, _navigator, _store, Id);
        }
    }
}

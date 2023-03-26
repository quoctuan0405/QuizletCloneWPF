using QuizletClone.WPF.Commands.Base;
using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class HeaderViewModel
    {
        private readonly INavigator _navigator;
        private readonly IViewModelAbstractFactory _viewModelFactory;
        private readonly Store _store;

        public string Keyword { get; set; }

        public ICommand SearchSetCommand { get; set; }

        public HeaderViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store)
        {
            _navigator = navigator;
            _store = store;
            _viewModelFactory = viewModelFactory;

            Keyword = _store.Keyword;

            SearchSetCommand = new RelayCommand(async () => SearchSet());
        }

        public async Task SearchSet()
        {
            _store.Keyword = Keyword;
            _store.FetchListSet(Keyword);

            if (_navigator.CurrentViewModel is not HomeViewModel)
            {
                _navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Home, _navigator, _store, null);
            }
        }
    }
}

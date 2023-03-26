using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using QuizletClone.API.Services;
using QuizletClone.WPF.State;
using System.Windows.Input;
using QuizletClone.WPF.Commands.Base;

namespace QuizletClone.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public HeaderViewModel HeaderViewModel { get; set; }
        public SetListingViewModel SetListingViewModel { get; set; }

        public ICommand GoToCreateSetPageCommand { get; set; }

        public HomeViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store) 
        {
            SetListingViewModel = new SetListingViewModel(navigator, viewModelFactory, store);
            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            GoToCreateSetPageCommand = new RelayCommand(() => GoToCreateSet());
        }

        private void GoToCreateSet()
        {
            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.CreateSet, Navigator, _store, null);
        }
    }
}

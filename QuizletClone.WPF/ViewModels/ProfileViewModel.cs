using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public SetProfileListingViewModel SetProfileListingViewModel { get; set; }

        public HeaderViewModel HeaderViewModel { get; set; }

        public ProfileViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store) 
        {
            SetProfileListingViewModel = new SetProfileListingViewModel(store);
            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            Username = _store.Me.Username;
        }
    }
}

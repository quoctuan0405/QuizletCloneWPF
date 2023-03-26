using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public class ProfileViewModelFactory : IViewModelFactory<ProfileViewModel>
    {
        public ProfileViewModel CreateViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data)
        {
            return new ProfileViewModel(navigator, viewModelFactory, store);
        }
    }
}

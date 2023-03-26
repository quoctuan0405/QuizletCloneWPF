using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public class SignupViewModelFactory : IViewModelFactory<SignUpViewModel>
    {
        public SignUpViewModel CreateViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data)
        {
            return new SignUpViewModel(navigator, viewModelFactory, store);
        }
    }
}

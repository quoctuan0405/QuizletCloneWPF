using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public class UpdateSetViewModelFactory : IViewModelFactory<UpdateSetViewModel>
    {
        public UpdateSetViewModel CreateViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data)
        {
            return new UpdateSetViewModel(navigator, viewModelFactory, store, data);
        }
    }
}

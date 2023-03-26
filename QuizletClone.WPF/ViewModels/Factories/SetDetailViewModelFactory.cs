using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public class SetDetailViewModelFactory : IViewModelFactory<SetDetailViewModel>
    {
        public SetDetailViewModel CreateViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data)
        {
            return new SetDetailViewModel(navigator, viewModelFactory, store, data);
        }
    }
}

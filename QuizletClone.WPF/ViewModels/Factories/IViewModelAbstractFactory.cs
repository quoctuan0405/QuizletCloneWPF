using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels.Factories
{
    public interface IViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType, INavigator navigator, Store store, object? data);
    }
}

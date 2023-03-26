using QuizletClone.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.State.Navigators
{
    public enum ViewType
    {
        CreateSet,
        UpdateSet,
        SetDetail,
        Learning,
        Login,
        Signup,
        Profile,
        Home
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }

        ICommand UpdateCurrentViewModelCommand { get; }
    }
}

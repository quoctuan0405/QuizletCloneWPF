using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        protected readonly IViewModelAbstractFactory _viewModelFactory;
        public INavigator Navigator { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected readonly Store _store;

        public ViewModelBase(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store)
        {
            Navigator = navigator;
            _viewModelFactory = viewModelFactory;
            _store = store;
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        protected async Task RunCommand(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            if (updatingFlag.GetPropertyValue())
            {
                return;
            }

            updatingFlag.SetPropertyValue(true);

            try
            {
                await action();
            }
            finally
            {
                updatingFlag.SetPropertyValue(false);
            }
        }
    }
}

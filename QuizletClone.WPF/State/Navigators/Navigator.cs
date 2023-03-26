using QuizletClone.API.Presenter;
using QuizletClone.WPF.Commands;
using QuizletClone.WPF.Models;
using QuizletClone.WPF.ViewModels;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.State.Navigators
{
    public class Navigator : ObservableObject, INavigator
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel 
        { 
            get
            {
                return _currentViewModel;
            }
            
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            } 
        }

        private UserPresenter _me;
        public UserPresenter Me
        {
            get
            {
                return _me;
            }
            set
            {
                _me = value;
                OnPropertyChanged(nameof(Me));
            }
        }

        public ICommand UpdateCurrentViewModelCommand { get; set; }

        public Navigator(IViewModelAbstractFactory viewModelFactory, Store store)
        {
            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(this, viewModelFactory, store);

            // Default value
            CurrentViewModel = viewModelFactory.CreateViewModel(ViewType.Login, this, store, null);

            // Me
            store.MeChanged += () => { 
                Me = store.Me; 
            };
        }
    }
}

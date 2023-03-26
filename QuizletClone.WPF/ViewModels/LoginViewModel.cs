using QuizletClone.WPF.Commands.Base;
using QuizletClone.WPF.Security;
using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string Username { get; set; }

        private string _message;
        public string Message {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            } 
        }

        public bool LoginIsRunning { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand GoToSignupPageCommand { get; set; }

        public LoginViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store) 
        {
            LoginCommand = new RelayParameterizedCommand(async (parameter) => await Login(parameter));
            GoToSignupPageCommand = new RelayCommand(async () => await GoToSignupPage());
        }

        public async Task GoToSignupPage()
        {
            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Signup, Navigator, _store, null);
        }

        public async Task Login(object parameter)
        {
            await RunCommand(() => this.LoginIsRunning, async () =>
            {
                var password = (parameter as System.Windows.Controls.PasswordBox).SecurePassword.Unsecure();

                var response = await _store.Login(Username, password);
                if (!string.IsNullOrEmpty(response.Token))
                {
                    await _store.GetMe();
                    Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Home, Navigator, _store, null);
                } 
                else
                {
                    Message = "Invalid credentials";
                }
            });
        }
    }
}

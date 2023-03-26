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
    public class SignUpViewModel : ViewModelBase
    {
        public string Username { get; set; }

        private string _message;
        public string Message
        {
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

        public bool SignupIsRunning { get; set; }

        public ICommand SignupCommand { get; set; }

        public ICommand GoToLoginPageCommand { get; set; }

        public SignUpViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store)
        {
            SignupCommand = new RelayParameterizedCommand(async (parameter) => await Signup(parameter));
            GoToLoginPageCommand = new RelayCommand(async () => await GoToLoginPage());
        }

        public async Task GoToLoginPage()
        {
            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Login, Navigator, _store, null);
        }

        public async Task Signup(object parameter)
        {
            await RunCommand(() => this.SignupIsRunning, async () =>
            {
                var password = (parameter as System.Windows.Controls.PasswordBox).SecurePassword.Unsecure();

                var response = await _store.Signup(Username, password);
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    await _store.GetMe();
                    Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Home, Navigator, _store, null);
                }
                else
                {
                    Message = "Please try again";
                }
            });
        }
    }
}

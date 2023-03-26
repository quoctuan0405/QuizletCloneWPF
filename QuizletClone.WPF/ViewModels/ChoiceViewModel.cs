using QuizletClone.WPF.Commands.Base;
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
    public class ChoiceViewModel : ViewModelBase
    {
        public string Choice { get; set; }

        public bool IsCorrectAnswer { get; set; }

        private bool _isNotAnswered;
        public bool IsNotAnswered
        {
            get
            {
                return _isNotAnswered;
            }
            set
            {
                _isNotAnswered = value;
                OnPropertyChanged(nameof(IsNotAnswered));
            }
        }

        private string _borderBrush;
        public string BorderBrush
        {
            get
            {
                return _borderBrush;
            }
            set
            {
                _borderBrush = value;
                OnPropertyChanged(nameof(BorderBrush));
            }
        }

        public ICommand AnswerCommand { get; set; }

        public ChoiceViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store)
        {
            IsNotAnswered = true;

            AnswerCommand = new RelayCommand(async () => await Answer());

            _store.IsAnsweredChanged += IsAnsweredListener;
        }

        private async Task Answer()
        {
            await _store.ReportLearningProgress(_store.SetDetail.Id, _store.RandomLearningTerm.Id, IsCorrectAnswer);

            _store.IsAnswered = true;

            if (IsCorrectAnswer)
            {
                BorderBrush = "#a5d6ad";
            }
            else
            {
                BorderBrush = "#d66363";
            }
        }

        private void IsAnsweredListener()
        {
            IsNotAnswered = !_store.IsAnswered;

            if (IsCorrectAnswer)
            {
                BorderBrush = "#a5d6ad";
            }
        }
    }
}

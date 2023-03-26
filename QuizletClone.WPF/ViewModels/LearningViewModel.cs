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
    public class LearningViewModel : ViewModelBase
    {
        public HeaderViewModel HeaderViewModel { get; set; }

        public ChoiceListingViewModel ChoiceListingViewModel { get; set; }

        private string _setName;
        public string SetName { 
            get
            {
                return _setName;
            }
            set
            {
                _setName = value;
                OnPropertyChanged(nameof(SetName));
            } 
        }

        private int _termCount;
        public int TermCount { 
            get
            {
                return _termCount;
            } 
            set
            {
                _termCount = value;
                OnPropertyChanged(nameof(TermCount));
            } 
        }

        private int _termLearningCount;
        public int TermLearningCount { 
            get
            {
                return _termLearningCount;
            }
            set
            {
                _termLearningCount = value;
                OnPropertyChanged(nameof(TermLearningCount));
            }
        }

        private string _question;
        public string Question { 
            get
            {
                return _question;
            }
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            } 
        }

        private bool _isAnswered;
        public bool IsAnswered
        {
            get
            {
                return _isAnswered;
            }
            set
            {
                _isAnswered = value;
                OnPropertyChanged(nameof(IsAnswered));
            }
        }

        public bool _isLearningInProgress;
        public bool IsLearningInProgress
        {
            get
            {
                return _isLearningInProgress;
            }
            set
            {
                _isLearningInProgress = value;
                OnPropertyChanged(nameof(IsLearningInProgress));
            }
        }

        public ICommand NextQuestionCommand { get; set; }

        public LearningViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data) : base(navigator, viewModelFactory, store)
        {
            // Header
            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            ChoiceListingViewModel = new ChoiceListingViewModel(navigator, viewModelFactory, store);

            // Fetch data
            SetName = _store.SetDetail.Name;
            TermCount = _store.SetDetail.Terms.Count;

            IsLearningInProgress = true;

            FetchRandomLearningTerm();

            // Binding listener
            _store.RandomLearningTermChanged += UpdateRandomLearningTerm;
            _store.CountLearningTermChanged += UpdateCountLearningTermProgress;
            _store.IsAnsweredChanged += IsAnsweredListener;

            // Command
            NextQuestionCommand = new RelayCommand(() => FetchRandomLearningTerm());
        }

        private void FetchRandomLearningTerm()
        {
            _store.GetRandomLearningTerm(_store.SetDetail.Id);
            _store.CountLearningTermProgress(_store.SetDetail.Id);
            IsAnswered = false;
            ChoiceListingViewModel.Reset();
        }

        private void UpdateRandomLearningTerm()
        {
            if (_store.RandomLearningTerm != null)
            {
                Question = _store.RandomLearningTerm.Question;
            }
            else
            {
                IsLearningInProgress = false;
            }
        }

        private void UpdateCountLearningTermProgress()
        {
            TermLearningCount = _store.CountLearningTerm.Count;
        }

        private void IsAnsweredListener()
        {
            IsAnswered = _store.IsAnswered;
        }
    }
}

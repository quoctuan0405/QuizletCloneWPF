using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class ChoiceListingViewModel : ViewModelBase
    {
        private readonly Store _store;

        public ObservableCollection<ChoiceViewModel> Items { get; set; } = new ObservableCollection<ChoiceViewModel>();

        public ChoiceListingViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store)
        {
            _store = store;

            _store.RandomLearningTermChanged += UpdateRandomLearningTerm;
        }

        private void UpdateRandomLearningTerm()
        {
            if (_store.RandomLearningTerm != null)
            {
                List<ChoiceViewModel> choices = new List<ChoiceViewModel>();

                choices.Add(new ChoiceViewModel(Navigator, _viewModelFactory, _store)
                {
                    Choice = _store.RandomLearningTerm.Answer,
                    IsCorrectAnswer = true
                });


                foreach (var choice in _store.RandomLearningTerm.Choices)
                {
                    choices.Add(new ChoiceViewModel(Navigator, _viewModelFactory, _store)
                    {
                        Choice = choice,
                        IsCorrectAnswer = false
                    });
                }

                Random random = new Random();
                choices = choices.OrderBy(choice => random.Next()).ToList();

                foreach (var choice in choices)
                {
                    App.Current.Dispatcher.Invoke((Action)delegate
                    {
                        Items.Add(choice);
                    });
                }
            }
        }

        public void Reset()
        {
            Items.Clear();
        }
    }
}

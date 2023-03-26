using QuizletClone.API.Payload;
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
    public class UpdateSetViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UpdateSetInputListingViewModel UpdateSetInputListingViewModel { get; set; }

        public HeaderViewModel HeaderViewModel { get; set; }

        public ICommand AddNewEmptyTermCommand { get; set; }

        public ICommand UpdateSetCommand { get; set; }

        public UpdateSetViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data) : base(navigator, viewModelFactory, store)
        {
            Id = store.SetDetail.Id;
            Name = store.SetDetail.Name;

            UpdateSetInputListingViewModel = new UpdateSetInputListingViewModel(store);

            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            AddNewEmptyTermCommand = new RelayCommand(() => AddNewEmptyTerm());

            UpdateSetCommand = new RelayCommand(async () => await UpdateSet());
        }

        private void AddNewEmptyTerm()
        {
            UpdateSetInputListingViewModel.AddEmptyItem();
        }

        private async Task UpdateSet()
        {
            List<TermPayload> terms = new List<TermPayload>();

            foreach (var term in UpdateSetInputListingViewModel.Items)
            {
                terms.Add(new TermPayload()
                {
                    Question = term.Question,
                    Answer = term.Answer
                });
            }

            SetPayload setPayload = new SetPayload()
            {
                Id = Id,
                Name = Name,
                Terms = terms
            };

            await _store.UpdateSet(Id, setPayload);

            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.SetDetail, Navigator, _store, Id);
        }
    }
}

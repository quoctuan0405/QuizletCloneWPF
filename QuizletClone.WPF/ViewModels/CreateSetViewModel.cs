using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Threading.Tasks;
using System.Windows.Input;
using QuizletClone.API.Payload;
using QuizletClone.WPF.Commands.Base;
using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;

namespace QuizletClone.WPF.ViewModels
{
    public class CreateSetViewModel : ViewModelBase
    {
        public string Name { get; set; }

        public CreateSetInputListingViewModel CreateSetInputListingViewModel { get; set; }

        public HeaderViewModel HeaderViewModel { get; set; }

        public ICommand AddNewEmptyTermCommand { get; set; }

        public ICommand ImportExcelFileCommand { get; set; }

        public ICommand CreateNewSetCommand { get; set; }

        public CreateSetViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store) : base(navigator, viewModelFactory, store)
        {
            CreateSetInputListingViewModel = new CreateSetInputListingViewModel(store);

            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            AddNewEmptyTermCommand = new RelayCommand(() => AddNewEmptyTerm());

            CreateNewSetCommand = new RelayCommand(async () => await CreateNewSet());

            ImportExcelFileCommand = new RelayCommand(async () => await ImportExcelFile());
        }

        private async Task ImportExcelFile()
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".xlsx";
            ofd.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            var sel = ofd.ShowDialog();
            if (sel == true)
            {
                var fileName = ofd.FileName;
                await _store.ReadTermPayloadFromFile(fileName);
            }
        }

        private void AddNewEmptyTerm()
        {
            CreateSetInputListingViewModel.AddEmptyItem();
        }

        private async Task CreateNewSet()
        {
            List<TermPayload> terms = new List<TermPayload>();

            foreach(var term in CreateSetInputListingViewModel.Items)
            {
                terms.Add(new TermPayload()
                {
                    Question = term.Question,
                    Answer = term.Answer
                });
            }

            SetPayload setPayload = new SetPayload()
            {
                Name = Name,
                Terms = terms
            };

            await _store.CreateNewSet(setPayload);

            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.SetDetail, Navigator, _store, _store.SetDetail.Id);
        }
    }
}

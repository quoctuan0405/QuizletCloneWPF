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
using System.Windows.Markup;

namespace QuizletClone.WPF.ViewModels
{
    public class SetDetailViewModel : ViewModelBase
    {
        private int _setId;
        private string _setName;

        public string SetName
        { 
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

        private bool _editable;
        public bool Editable { 
            get
            {
                return _editable;
            }
            set
            {
                _editable = value; 
                OnPropertyChanged(nameof(Editable));
            }
        }

        public TermListingViewModel TermListingViewModel { get; set; }

        public HeaderViewModel HeaderViewModel { get; set; } 

        public ICommand GoToLearnPageCommand { get; set; }

        public ICommand GoToUpdateSetPageCommand { get; set; }

        public SetDetailViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store, object? data) : base(navigator, viewModelFactory, store)
        {
            // Get set id
            _setId = 0;
            if (data != null)
            {
                _setId = (int)data;
            }

            TermListingViewModel = new TermListingViewModel(store);
            HeaderViewModel = new HeaderViewModel(navigator, viewModelFactory, store);

            // Store
            store.GetDetailSet(_setId);

            _store.SetDetailChanged += UpdateSetInfo;

            // Command
            GoToLearnPageCommand = new RelayCommand(() => GoToLearnPage());
            GoToUpdateSetPageCommand = new RelayCommand(() => GoToUpdateSetPage());
        }

        private void GoToUpdateSetPage()
        {
            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.UpdateSet, Navigator, _store, _store.SetDetail.Id);
        }

        private void GoToLearnPage()
        {
            Navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(ViewType.Learning, Navigator, _store, _store.SetDetail.Id);
        }

        private void UpdateSetInfo()
        {
            if (_store.SetDetail != null)
            {
                SetName = _store.SetDetail.Name;

                Editable = _store.SetDetail.Author.Id.Equals(_store.Me.Id);
            }
        }
    }
}

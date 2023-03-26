using QuizletClone.API.Services;
using QuizletClone.WPF.Commands.Base;
using QuizletClone.WPF.Models;
using QuizletClone.WPF.State;
using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class SetListingViewModel : ObservableObject
    {
        private INavigator _navigator { get; set; }
        private readonly IViewModelAbstractFactory _viewModelFactory;
        private readonly Store _store;

        private ObservableCollection<SetViewModel> _items = new ObservableCollection<SetViewModel>();

        public ObservableCollection<SetViewModel> Items 
        { 
            get
            {
                return _items;
            }

            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public SetListingViewModel(INavigator navigator, IViewModelAbstractFactory viewModelFactory, Store store)
        {
            // Init
            _navigator = navigator;
            _store = store;
            _viewModelFactory = viewModelFactory;

            // Store
            _store.ListSetChanged += UpdateItems;

            _store.FetchListSet(null);
        }

        private void UpdateItems()
        {
            Items = new ObservableCollection<SetViewModel>();

            foreach (var set in _store.Sets)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Items.Add(
                        new SetViewModel(_navigator, _viewModelFactory, _store)
                        {
                            Id = set.Id,
                            Name = set.Name,
                            TermCount = set.TermCount,
                            AuthorName = set.Author.Username
                        }
                    );
                });
            }
        }
    }
}

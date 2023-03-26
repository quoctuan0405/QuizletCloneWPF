using QuizletClone.WPF.State.Navigators;
using QuizletClone.WPF.State;
using QuizletClone.WPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizletClone.WPF.Models;
using QuizletClone.API.Presenter;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace QuizletClone.WPF.ViewModels
{
    public class TermListingViewModel : ObservableObject
    {
        private readonly Store _store;

        private ObservableCollection<TermViewModel> _items = new ObservableCollection<TermViewModel>();

        public ObservableCollection<TermViewModel> Items
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

        public TermListingViewModel(Store store)
        {
            // Store
            _store = store;

            _store.SetDetailChanged += UpdateItems;
        }

        private void UpdateItems()
        {
            Items = new ObservableCollection<TermViewModel>();

            foreach (var term in _store.SetDetail.Terms)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // Using delegate to make sure it runs on the UI Thread
                {
                    Items.Add(
                        new TermViewModel()
                        {
                            Answer = term.Answer,
                            Question = term.Question
                        }
                    );
                });
            }
        }
    }
}

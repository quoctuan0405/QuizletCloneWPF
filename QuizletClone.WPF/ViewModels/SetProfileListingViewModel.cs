using QuizletClone.WPF.State;
using QuizletClone.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class SetProfileListingViewModel : ObservableObject
    {
        private readonly Store _store;

        private ObservableCollection<SetProfileViewModel> _items;
        public ObservableCollection<SetProfileViewModel> Items { 
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

        public SetProfileListingViewModel(Store store)
        {
            _store = store;

            _store.FetchListMySet(null);

            _store.ListMySetChanged += UpdateListMySet;
        }

        private void UpdateListMySet()
        {
            Items = new ObservableCollection<SetProfileViewModel>();

            foreach(var set in _store.MySets)
            {
                App.Current.Dispatcher.Invoke((Action)delegate // Using delegate to make sure it runs on the UI Thread
                {
                    Items.Add(new SetProfileViewModel(DeleteMySet)
                    {
                        SetId = set.Id,
                        SetName = set.Name
                    });
                });
            }

            OnPropertyChanged(nameof(Items));
        }

        private async void DeleteMySet(int setId)
        {
            await _store.DeleteSet(setId);
            await _store.FetchListMySet(null);
        }
    }
}

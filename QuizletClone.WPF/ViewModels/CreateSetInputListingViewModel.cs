using QuizletClone.WPF.Models;
using QuizletClone.WPF.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class CreateSetInputListingViewModel : ObservableObject
    {
        private readonly Store _store;
        private ObservableCollection<CreateSetInputViewModel> _items = new ObservableCollection<CreateSetInputViewModel>();

        public ObservableCollection<CreateSetInputViewModel> Items
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

        public CreateSetInputListingViewModel(Store store)
        {
            _store = store;
            AddEmptyItem();

            _store.ImportedTermPayloadsChanged += ImportListTermListener;
        }

        public void AddEmptyItem()
        {
            _items.Add(
                new CreateSetInputViewModel(RemoveItem)
                {
                    Index = _items.Count,
                }
            );
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < _items.Count)
            {
                _items.RemoveAt(index);
            }

            int i = 0;
            foreach (var item in _items)
            {
                item.Index = i;
                i++;
            }
        }

        public void ImportListTermListener()
        {
            Items = new ObservableCollection<CreateSetInputViewModel>();

            int i = 0;
            foreach(var item in _store.ImportedTermPayloads)
            {
                App.Current.Dispatcher.Invoke((Action)delegate
                {
                    Items.Add(new CreateSetInputViewModel(RemoveItem)
                    {
                        Index = i,
                        Question = item.Question,
                        Answer = item.Answer,
                    });

                    i++;
                });
            }
        }
    }
}

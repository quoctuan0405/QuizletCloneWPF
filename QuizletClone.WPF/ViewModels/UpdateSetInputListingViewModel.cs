using QuizletClone.WPF.State;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.ViewModels
{
    public class UpdateSetInputListingViewModel
    {
        private readonly Store _store;
        private ObservableCollection<UpdateSetInputViewModel> _items = new ObservableCollection<UpdateSetInputViewModel>();

        public ObservableCollection<UpdateSetInputViewModel> Items
        {
            get
            {
                return _items;
            }
        }

        public UpdateSetInputListingViewModel(Store store)
        {
            _store = store;

            foreach(var item in _store.SetDetail.Terms.Select((value, index) => new {index, value}))
            {
                Items.Add(
                    new UpdateSetInputViewModel(RemoveItem)
                    {
                        Index = item.index,
                        Id = item.value.Id,
                        Question = item.value.Question,
                        Answer = item.value.Answer,
                    }
                );
            }
        }

        public void AddEmptyItem()
        {
            _items.Add(
                new UpdateSetInputViewModel(RemoveItem)
                {
                    Index = _items.Count,
                }
            );
        }

        public void RemoveItem(int index)
        {
            _items.RemoveAt(index);
        }
    }
}

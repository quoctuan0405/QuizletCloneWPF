using QuizletClone.WPF.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class SetProfileViewModel
    {
        public int SetId { get; set; }

        public string SetName { get; set; }

        public ICommand DeleteSetCommand { get; set; }

        public SetProfileViewModel(Action<int> DeleteSet)
        {
            DeleteSetCommand = new RelayCommand(() => DeleteSet(SetId));
        }
    }
}

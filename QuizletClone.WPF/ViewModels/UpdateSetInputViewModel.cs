using QuizletClone.WPF.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class UpdateSetInputViewModel
    {
        public int Index { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public ICommand RemoveSetInputCommand { get; set; }

        public UpdateSetInputViewModel(Action<int> RemoveUpdateSetInput)
        {
            RemoveSetInputCommand = new RelayCommand(() => RemoveUpdateSetInput(Index));
        }
    }
}

using QuizletClone.WPF.Commands.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuizletClone.WPF.ViewModels
{
    public class CreateSetInputViewModel
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }

        public ICommand RemoveSetInputCommand { get; set; }

        public CreateSetInputViewModel(Action<int> RemoveCreateSetInput)
        {
            RemoveSetInputCommand = new RelayCommand(() => RemoveCreateSetInput(Index));
        }
    }
}

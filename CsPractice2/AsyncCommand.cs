using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaUKMA.CS.Practice02
{
    public class AsyncCommand<TResult> : AsyncCommandBase//, INotifyPropertyChanged
    {
        private readonly Func<Task<TResult>> _command;
        private NotifyTaskCompletion<TResult> _execution;
        public AsyncCommand(Func<Task<TResult>> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Execution = new NotifyTaskCompletion<TResult>(_command());
            RaiseCanExecuteChanged();
            await Execution.TaskCompletion;
            RaiseCanExecuteChanged();
        }
        // Raises PropertyChanged
        public NotifyTaskCompletion<TResult> Execution { get; private set; }
    }
}

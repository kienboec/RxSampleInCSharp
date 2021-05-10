using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp.AccessMethods
{
    public abstract class AccessMethodBase : ICommand
    {
        protected readonly IDataStore _store;

        public AccessMethodBase(IDataStore store)
        {
            _store = store;
        }

        public bool CanExecute(object parameter) => true;
        
        public async void Execute(object parameter)
        {
            await this.Execute();
        }

        public abstract Task Execute();

        public event EventHandler? CanExecuteChanged;

    }
}

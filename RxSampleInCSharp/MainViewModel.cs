using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RxSampleInCSharp.AccessMethods;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp
{
    public class MainViewModel : INotifyPropertyChanged, IDataStore
    {
        // consider: ComboBox and Strategy pattern
        // see: https://en.wikipedia.org/wiki/Strategy_pattern

        private ICommand _clearCommand = null;
        private ICommand _getDataSyncCommand = null;
        private ICommand _getDataASyncCommand = null;
        private ICommand _getDataASyncOtherTaskCommand = null;
        private ICommand _getDataThreadedCommand = null;
        private ICommand _getDataReactiveCommand = null;

        public ObservableCollection<SimpleStringData> Data { get; protected set; }
            = new ObservableCollection<SimpleStringData>();

        public ICommand ClearCommand => _clearCommand ??= new RelayCommand(p => Clear());
        public ICommand GetDataSyncCommand => _getDataSyncCommand ??= new SynchronousCall(this);
        public ICommand GetDataASyncCommand => _getDataASyncCommand ??= new ASyncCall(this);
        public ICommand GetDataASyncOtherTaskCommand => _getDataASyncOtherTaskCommand ??= new ASyncWithDifferentTaskCall(this);
        public ICommand GetDataThreadedCommand => _getDataThreadedCommand ??= new ThreadedCall(this);
        public ICommand GetDataReactiveCommand => _getDataReactiveCommand ??= new ReactiveCall(this);

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetSimpleStringData(IEnumerable<SimpleStringData> data)
        {
            Clear();
            data.ToList().ForEach(AddSimpleStringData);
        }

        public void Clear()
        {
            Data.Clear();
        }

        public void AddSimpleStringData(SimpleStringData item)
        {
            Data.Add(item);
        }
    }
}

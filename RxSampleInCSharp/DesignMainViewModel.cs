using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp
{
    public class DesignMainViewModel : MainViewModel
    {
        public DesignMainViewModel()
        {
            Data = new ObservableCollection<SimpleStringData>()
            {
                new SimpleStringData(){ Data = "first demo data" },
                new SimpleStringData(){ Data = "second demo data" },
                new SimpleStringData(){ Data = "third demo data" },
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp
{
    public interface IDataStore
    {
        void SetSimpleStringData(IEnumerable<SimpleStringData> data);
        void Clear();
        void AddSimpleStringData(SimpleStringData data);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSampleInCSharp.Model
{
    public class SimpleStringData
    {
        public string Data { get; set; }

        public static SimpleStringData Create(string data)
        {
            return new SimpleStringData() { Data = data };
        }
    }
}

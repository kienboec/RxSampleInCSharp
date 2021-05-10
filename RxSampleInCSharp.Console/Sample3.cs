using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSampleInCSharp.ConsoleApp
{
    public class Sample3
    {
        public class CharEventArgs : EventArgs
        {
            public char C { get; }

            public CharEventArgs(char c)
            {
                C = c;
            }
        }

        public static event EventHandler<CharEventArgs> NewCharReceived;

        public static void SampleMain(string[] args)
        {
            // Observables can be subscribed (see interface from .net5)
            var observable = Observable.FromEventPattern(typeof(Sample3), nameof(NewCharReceived))
                .Select(x => (x.EventArgs as CharEventArgs).C)
                .TakeUntil(x => x == 'q')
                .Where(x => x >= '0' && x <= '9')
                .DistinctUntilChanged(); 
            
            observable
                .Subscribe(data =>
                {
                    Console.WriteLine($"data: {data}");
                });

            observable.Count().Subscribe(data =>
            {
                Console.WriteLine($"count: {data}");
            });

            int data;
            while ((data = Console.Read()) != 'q')
            {
                NewCharReceived.Invoke(null, new CharEventArgs((char)data));
            }

            NewCharReceived.Invoke(null, new CharEventArgs('q'));

        }
    }
}

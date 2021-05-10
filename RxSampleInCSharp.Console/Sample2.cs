using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSampleInCSharp.ConsoleApp
{
    public class Sample2
    {
        public static event EventHandler<EventArgs> NewDataReceived; 

        public static void SampleMain(string[] args)
        {
            // checks observables and reacts on data...
            IObserver<int> observer = new AnonymousObserver<int>(
                onNext: data => Print(data, "onNext"),
                onError: exc => Print(null, "error: " + exc.Message),
                onCompleted: () => Print(null, "onCompleted"));

            // Observables can be subscribed (see interface from .net5)
            int count = 1;
            IObservable<int> observable = 
                    Observable.FromEventPattern(typeof(Sample2), nameof(NewDataReceived))
                        .Select(x => count++)
                        .Where(x => x % 2 == 1)
                ;
            
            // some IDisposable representing the subscription (on Dispose everything is deattached)
            var subscription = observable.Subscribe(observer);

            NewDataReceived.Invoke(null, EventArgs.Empty); // 1
            NewDataReceived.Invoke(null, EventArgs.Empty);
            NewDataReceived.Invoke(null, EventArgs.Empty); // 3
            NewDataReceived.Invoke(null, EventArgs.Empty);
            NewDataReceived.Invoke(null, EventArgs.Empty); // 5
            NewDataReceived.Invoke(null, EventArgs.Empty);
            NewDataReceived.Invoke(null, EventArgs.Empty); // 7

        }

        private static void Print(int? data, string additionalText)
        {
            Console.WriteLine($"data: {data?.ToString() ?? "-"} ({additionalText})");
        }
    }
}

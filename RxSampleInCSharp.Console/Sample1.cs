using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxSampleInCSharp.ConsoleApp
{
    public class Sample1
    {
        public static void SampleMain(string[] args)
        {
            // checks observables and reacts on data...
            IObserver<int> observer = new AnonymousObserver<int>(
                onNext: data => Print(data, "onNext"),
                onError: exc => Print(null, "error: " + exc.Message),
                onCompleted: () => Print(null, "onCompleted"));

            // Observables can be subscribed (see interface from .net5)
            IObservable<int> observable =
                //Observable.Range(1, 3);
                (new int[] {1, 2, 3}).ToObservable();
            // some IDisposable representing the subscription (on Dispose everything is deattached)
            var subscription = observable.Subscribe(observer);
        }

        private static void Print(int? data, string additionalText)
        {
            Console.WriteLine($"data: {data?.ToString() ?? "-"} ({additionalText})");
        }
    }
}

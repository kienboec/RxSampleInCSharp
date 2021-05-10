using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp.AccessMethods
{
    public class ReactiveCall : AccessMethodBase
    {
        public ReactiveCall(IDataStore store) : base(store)
        {
        }

        public override Task Execute()
        {
            _store.Clear();

            WebClient client = new WebClient();
            var regexFindHeadlines = new Regex("ticker-story-headline.*?a\\ href.*?>(.*?)<\\/a",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            Observable
                .FromAsync(() => client.DownloadStringTaskAsync("https://sport.orf.at/"))
                .Delay(TimeSpan.FromSeconds(10))
                //.ObserveOn(SynchronizationContext.Current)
                .SubscribeOn(NewThreadScheduler.Default)
                .Select(content => regexFindHeadlines.Matches(content))
                .SelectMany(matches => matches.ToImmutableArray())
                .Select(match => match.Groups[1].ToString().Trim())
                .Subscribe(headline =>
                {
                    Debug.Print($"Task: {Task.CurrentId} / Thread: {Thread.CurrentThread.ManagedThreadId}");
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _store.AddSimpleStringData(SimpleStringData.Create(headline));
                    });
                })
                ;

            return Task.CompletedTask;
        }
    }
}

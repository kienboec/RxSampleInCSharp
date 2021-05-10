using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp.AccessMethods
{
    public class ThreadedCall : AccessMethodBase
    {
        public ThreadedCall(IDataStore store) : base(store)
        {
        }

        public override Task Execute()
        {
            Thread thread = new Thread(() =>
            {
                Application.Current.Dispatcher.Invoke(() => { _store.Clear(); });
                Thread.Sleep(10_000);

                WebClient client = new WebClient();
                var content = client.DownloadString("https://sport.orf.at/");

                var regexFindHeadlines = new Regex("ticker-story-headline.*?a\\ href.*?>(.*?)<\\/a",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                var matches = regexFindHeadlines.Matches(content);
                foreach (Match match in matches)
                {
                    var myHeadline = match.Groups[1].ToString().Trim();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _store.AddSimpleStringData(SimpleStringData.Create(myHeadline));
                    });
                    Thread.Sleep(1_000);
                }
            });
            thread.Start();
            return Task.CompletedTask;
        }
    }
}

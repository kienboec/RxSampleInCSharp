using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp.AccessMethods
{
    class ASyncWithDifferentTaskCall : AccessMethodBase
    {
        public ASyncWithDifferentTaskCall(IDataStore store) : base(store)
        {
        }

        public override async Task Execute()
        {
            await Task.Run(async () =>
            {
                // check if you need to invoke (here: yes, because new Task is created
                // directly a line above; good check for reuse)
                if (!Application.Current.Dispatcher.CheckAccess())
                {
                    Application.Current.Dispatcher.Invoke(() => { _store.Clear(); });
                }
                else
                {
                    _store.Clear();
                }

                await Task.Delay(10_000);

                WebClient client = new WebClient();
                var content = await client.DownloadStringTaskAsync("https://sport.orf.at/");

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
                    await Task.Delay(1_000);
                }
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using RxSampleInCSharp.Model;

namespace RxSampleInCSharp.AccessMethods
{
    public class ASyncCall : AccessMethodBase
    {
        public ASyncCall(IDataStore store) : base(store)
        {
        }

        public override async Task Execute()
        {
            _store.Clear();
            await Task.Delay(10_000);

            WebClient client = new WebClient();
            var content = await client.DownloadStringTaskAsync("https://sport.orf.at/");

            var regexFindHeadlines = new Regex("ticker-story-headline.*?a\\ href.*?>(.*?)<\\/a",
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            var matches = regexFindHeadlines.Matches(content);
            foreach (Match match in matches)
            {
                var myHeadline = match.Groups[1].ToString().Trim();
                _store.AddSimpleStringData(SimpleStringData.Create(myHeadline));
                await Task.Delay(1_000);
            }
        }
    }
}

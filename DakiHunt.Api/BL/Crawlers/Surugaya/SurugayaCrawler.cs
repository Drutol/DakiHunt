using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DakiHunt.ApiUtils;
using DakiHunt.DataAccess.Entities;
using DakiHunt.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace DakiHunt.Api.BL.Crawlers
{
    public class SurugayaCrawler : IDomainCrawler
    {
        private readonly ILogger<SurugayaCrawler> _logger;

        public Uri HandledDomain { get; } = new Uri("https://www.suruga-ya.jp");
       
        public SurugayaCrawler(ILogger<SurugayaCrawler> logger)
        {
            _logger = logger;
        }

        public async Task<DakiItemHistoryEntry> ObtainItemState(Hunt hunt)
        {
            using (var client = HttpClientFactory.Create(new SocketsHttpHandler()))
            {
                try
                {
                    var rawHtml = await client.GetStreamAsync(hunt.HuntedItem.Url);
                    var doc = new HtmlDocument();
                    doc.Load(rawHtml);


                    var infoSection = doc.DocumentNode.Descendants("div")
                        .First(node => node.HasAttribute("id", "sellInfo_left"));

                    var output = new DakiItemHistoryEntry
                    {
                        DateTime = DateTime.UtcNow,
                    };

                    if (infoSection.InnerText.Contains("品切れ中です"))
                    {
                        output.IsAvailable = false;
                        output.Price = -1;                      
                    }
                    else
                    {
                        output.IsAvailable = true;
                        var priceSectionText = infoSection.Descendants("p").First(node => node.HasAttribute("id", "price")).InnerText;
                        var parts = priceSectionText.Split('円');
                        output.Price = int.Parse(parts[0].Replace(",",""));
                    }

                    return output;
                }
                catch (Exception e)
                {
                    _logger.LogError(e,
                        $"Error parsing single surugaya item. Hunt {hunt.Id}, item {hunt.HuntedItem.Id}");
                    return null;
                }
            }           
        }

        public bool ValidateUrl(Uri uri)
        {
            return Regex.IsMatch(uri.ToString(), @"^https:\/\/www\.suruga-ya\.jp\/product\/detail\/\d+$");
        }
    }
}

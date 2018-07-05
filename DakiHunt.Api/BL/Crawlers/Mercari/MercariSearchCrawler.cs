using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using DakiHunt.ApiUtils;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Models;
using DakiHunt.Interfaces;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;

namespace DakiHunt.Api.BL.Crawlers.Mercari
{
    public class MercariSearchCrawler : IDomainSearchCrawler
    {
        private readonly ILogger<MercariSearchCrawler> _logger;

        public Uri HandledDomain { get; } = new Uri("https://www.mercari.com/jp/");

        public MercariSearchCrawler(ILogger<MercariSearchCrawler> logger)
        {
            _logger = logger;
        }

        public async Task<DakiItemSearchHistoryEntry> ObtainItemState(Hunt hunt)
        {
            var client = HttpClientFactory.Create();

            try
            {
                var rawHtml = await client.GetStreamAsync(
                    $"https://www.mercari.com/jp/search/?sort_order=created_desc&keyword={string.Join('+',hunt.SearchRequiredPhrases)}");

                var doc = new HtmlDocument();
                doc.Load(rawHtml);

                var output = new DakiItemSearchHistoryEntry();
                var matchingItems = new List<DakiItemSearchResultEntry>();

                foreach (var itemBox in doc.DocumentNode.Descendants("section").Where(node => node.HasAttribute("class","items-box")))
                {
                    var title = WebUtility.HtmlDecode(itemBox.Descendants("h3").First().InnerText);

                    // if item has any required phrases and no forbidden ones
                    if (hunt.SearchRequiredPhrases.Any(s => title.Contains(s,StringComparison.InvariantCultureIgnoreCase)) &&
                        hunt.SearchForbiddenPhrases.All(s => !title.Contains(s,StringComparison.CurrentCultureIgnoreCase)))
                    {
                        matchingItems.Add(new DakiItemSearchResultEntry
                        {
                            Identifier = itemBox.Descendants("a").First().Attributes["href"].Value.Split('/').Last(),
                            IsAvailable = !itemBox.Descendants("div").Any(node => node.HasAttribute("class", "item-sold-out-badge")),
                            Price = int.Parse(itemBox.Descendants("div").First(node => node.HasAttributeContainingValue("class", "items-box-price")).InnerText.Replace("¥","").Replace(",","").Trim()),
                            ImageUrl = itemBox.Descendants("img").First().Attributes["src"].Value,                                                     
                        });
                    }
                }

                output.SetScrapingResults(matchingItems);

                return output;
            }   
            catch (Exception e)
            {
                _logger.LogError(e,
                    $"Error parsing single surugaya item. Hunt {hunt.Id}, item {hunt.HuntedItem.Id}");
                return null;
            }
        }

        public bool ValidateUrl(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}

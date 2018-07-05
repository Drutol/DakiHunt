using System;
using System.Collections.Generic;
using System.Text;

namespace DakiHunt.Models.Dtos
{
    public class HuntConfigurationsOptionsDto
    {
        public List<string> AvailableCrawlers { get; set; }
        public List<string> AvailableSearchCrawlers { get; set; }

        public int MaxRequiredSearchPhrases { get; set; }
        public int MaxForbiddenSearchPhrases { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DakiHunt.DataAccess.Entities.Auth;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.Interfaces;
using DakiHunt.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DakiHunt.Api.Controllers
{
    [ApiController]
    [Authorize]
    [EnableCors("GlobalPolicy")]
    [Route("[controller]/[action]")]
    public class HuntController : ApiControllerBase
    {
        private readonly IEnumerable<IDomainCrawler> _domainCrawlers;
        private readonly IEnumerable<IDomainSearchCrawler> _domainSearchCrawlers;
        private readonly IConfiguration _configuration;

        public HuntController(
            IUserService userService,
            IEnumerable<IDomainCrawler> domainCrawlers,
            IEnumerable<IDomainSearchCrawler> domainSearchCrawlers,
            IConfiguration configuration,
            UserManager<AuthUser> userManager) 
            : base(userService, userManager)
        {
            _domainCrawlers = domainCrawlers;
            _domainSearchCrawlers = domainSearchCrawlers;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> GetHuntConfigurationsOptions()
        {
            _userService.ConfigureIncludes().WithChain(query =>
                query.AsNoTracking().Include(u => u.AccountProperties).Include(u => u.Hunts)).Commit();
            var user = await CurrentUser;

            if (user.Hunts.Count >= user.AccountProperties.MaxHunts)
                return Unauthorized();

            return new JsonResult(new HuntConfigurationsOptionsDto
            {
                AvailableCrawlers = _domainCrawlers.Select(crawler => crawler.HandledDomain.ToString()).ToList(),
                AvailableSearchCrawlers = _domainSearchCrawlers.Select(crawler => crawler.HandledDomain.ToString()).ToList(),

                MaxRequiredSearchPhrases = int.Parse(_configuration["Hunts:SearchPhrases:MaxRequired"]),
                MaxForbiddenSearchPhrases = int.Parse(_configuration["Hunts:SearchPhrases:MaxForbidden"]),
            });
        }
    }
}
